using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StorageBox : MonoBehaviour
{
    // Components
    public GameObject Player;
    public GameObject[] PlayerCharactersListItems = new GameObject[4];
    public GameObject Box;
    public GameObject BoxSlot;

    // Variables
    public GameObject[] SelectedCharacters = new GameObject[2];
    public Color defaultColor;

    private GameObject[] playerTeam = new GameObject[4]; // Instance of player's team
    private GameObject[] playerStorage; // Instance of player's storage

    private void OnEnable()
    {
        Player player = Player.GetComponent<Player>();
        playerTeam = PlayerCharactersListItems;
        playerStorage = new GameObject[player.storage.CharactersStored.Count];

        for (int i = 0; i < PlayerCharactersListItems.Length; i++)
        {
            PlayerCharactersListItems[i].GetComponentInChildren<PartySlot>().CharacterImage.sprite = player.team.SelectedCharacters[i].GetComponent<SpriteRenderer>().sprite;
            PlayerCharactersListItems[i].GetComponentInChildren<PartySlot>().CharacterName.text = player.team.SelectedCharacters[i].GetComponent<BaseCharacter>().name;
            string stats = "H: " + player.team.SelectedCharacters[i].GetComponent<BaseCharacter>().MaxHealth + " D: " + player.team.SelectedCharacters[i].GetComponent<BaseCharacter>().Damage.ToString();
            PlayerCharactersListItems[i].GetComponentInChildren<PartySlot>().CharacterStats.text = stats;
            playerTeam[i] = PlayerCharactersListItems[i];
        }

        /*
        for (int i = 0; i < player.storage.CharactersStored.Count; i++)
        {
            GameObject obj = Instantiate(BoxSlot, Box.transform);
            obj.GetComponent<Button>().onClick.AddListener(() => SelectCharacter(obj));
            obj.GetComponent<BoxSlot>().CharacterImage.sprite = player.storage.CharactersStored[i].GetComponent<SpriteRenderer>().sprite;
            playerStorage[i] = obj;
        }
        */

        /*
        for (int i = 0; i < player.storage.CharactersStored.Count; i++)
        {
            GameObject obj = Instantiate(BoxSlot, Box.transform);

            GameObject capturedObj = obj; // Capture the current obj for this iteration
            obj.GetComponent<Button>().onClick.AddListener(() => SelectCharacter(capturedObj));

            obj.GetComponent<BoxSlot>().CharacterImage.sprite =
                player.storage.CharactersStored[i].GetComponent<SpriteRenderer>().sprite;

            playerStorage[i] = obj;
        }
        */

        for (int i = 0; i < player.storage.CharactersStored.Count; i++)
        {
            GameObject obj = Instantiate(BoxSlot, Box.transform);
            obj.GetComponent<Button>().onClick.AddListener(() => SelectCharacter(obj));
            obj.GetComponent<BoxSlot>().CharacterImage.sprite = player.storage.CharactersStored[i].GetComponent<SpriteRenderer>().sprite;
            playerStorage[i] = obj;
        }
    }

    private void OnDisable()
    {
        // Clear the selected characters
        SelectedCharacters[0] = null;
        SelectedCharacters[1] = null;

        // Destroy all instantiated box slots
        foreach (Transform child in Box.transform)
        {
            Destroy(child.gameObject);
        }

        // Clear the player team array
        playerTeam = new GameObject[4];

        for (int i = 0; i < playerStorage.Length; i++)
        {
            playerStorage[i] = null; // Clear the player storage array
        }
    }

    public void SelectCharacter(GameObject obj)
    {
        if (obj == null)
        {
            Debug.Log("Object is null");
            return;
        }

        if (SelectedCharacters[0] == null)
        {
            SelectedCharacters[0] = obj; // Add the character to the first slot
            SelectedCharacters[0].GetComponent<Image>().color = new Color(255, 200, 0, 255);
        }
        else if (SelectedCharacters[1] == null)
        {
            SelectedCharacters[1] = obj; // Add the character to the second slot
            SelectedCharacters[1].GetComponent<Image>().color = new Color(255, 200, 0, 255);
        }
        else if (SelectedCharacters[0] == obj)
        {
            SelectedCharacters[0] = null; // Deselect the character
            SelectedCharacters[0].GetComponent<Image>().color = defaultColor;
        }
        else if (SelectedCharacters[1] == obj)
        {
            SelectedCharacters[1] = null; // Deselect the character
            SelectedCharacters[1].GetComponent<Image>().color = defaultColor;
        }

        CheckAreBothCharactersSelected();
    }

    private void UpdatePartyUI()
    {
        Player player = Player.GetComponent<Player>();

        for (int i = 0; i < PlayerCharactersListItems.Length; i++)
        {
            var slot = PlayerCharactersListItems[i].GetComponentInChildren<PartySlot>();
            GameObject character = player.team.SelectedCharacters[i];

            slot.CharacterImage.sprite = character.GetComponent<SpriteRenderer>().sprite;
            slot.CharacterName.text = character.GetComponent<BaseCharacter>().name;
            slot.CharacterStats.text = $"H: {character.GetComponent<BaseCharacter>().MaxHealth} D: {character.GetComponent<BaseCharacter>().Damage}";
        }
    }

    private void UpdateStorageUI()
    {
        Player player = Player.GetComponent<Player>();

        for (int i = 0; i < playerStorage.Length; i++)
        {
            GameObject character = player.storage.CharactersStored[i];
            BoxSlot boxSlot = playerStorage[i].GetComponent<BoxSlot>();
            boxSlot.CharacterImage.sprite = character.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void CheckAreBothCharactersSelected() // Logic for swapping characters needs updating
    {
        Player player = Player.GetComponent<Player>();

        if (SelectedCharacters[0] != null && SelectedCharacters[1] != null)
        {
            if (playerTeam.Contains(SelectedCharacters[0]) && playerStorage.Contains(SelectedCharacters[1]))
            {
                // Get the indexes of the characters in the box and the party
                int i = System.Array.IndexOf(playerStorage, SelectedCharacters[1]);
                int j = System.Array.IndexOf(playerTeam, SelectedCharacters[0]);

                // Create an instance of the characters
                GameObject character0 = player.storage.CharactersStored[i];
                GameObject character1 = player.team.instances[j];

                // Swap the characters in the box and between inventory and box
                player.storage.CharactersStored[i] = character1;
                player.team.instances[j] = character0;

                // Reset the colours
                SelectedCharacters[0].GetComponent<Image>().color = defaultColor;
                SelectedCharacters[1].GetComponent<Image>().color = defaultColor;
            }
            else if (playerTeam.Contains(SelectedCharacters[1]) && playerStorage.Contains(SelectedCharacters[0]))
            {
                // Get the indexes of the characters in the box and the party
                int i = System.Array.IndexOf(playerStorage, SelectedCharacters[0]);
                int j = System.Array.IndexOf(playerTeam, SelectedCharacters[1]);

                // Create an instance of the characters
                GameObject character0 = player.storage.CharactersStored[i];
                GameObject character1 = player.team.instances[j];

                // Swap the characters in the box and between inventory and box
                player.storage.CharactersStored[i] = character1;
                player.team.instances[j] = character0;

                // Reset the colours
                SelectedCharacters[0].GetComponent<Image>().color = defaultColor;
                SelectedCharacters[1].GetComponent<Image>().color = defaultColor;
            }
            else if (playerStorage.Contains(SelectedCharacters[0]) && playerStorage.Contains(SelectedCharacters[1]))
            {
                // Get the indexes of the characters in the box
                int i = System.Array.IndexOf(playerStorage, SelectedCharacters[0]);
                int j = System.Array.IndexOf(playerStorage, SelectedCharacters[1]);

                // Create an instance of the characters
                GameObject character0 = player.storage.CharactersStored[i];
                GameObject character1 = player.storage.CharactersStored[j];

                // Swap the position of the characters in the box
                player.storage.CharactersStored[i] = character1;
                player.storage.CharactersStored[j] = character0;

                // Reset the colours
                SelectedCharacters[0].GetComponent<Image>().color = defaultColor;
                SelectedCharacters[1].GetComponent<Image>().color = defaultColor    ;
            }
            else if (playerTeam.Contains(SelectedCharacters[0]) && playerTeam.Contains(SelectedCharacters[1]))
            {
                // Get the indexes of the characters in the party
                int i = System.Array.IndexOf(playerTeam, SelectedCharacters[0]);
                int j = System.Array.IndexOf(playerTeam, SelectedCharacters[1]);

                // Create an instance of the characters
                GameObject character0 = player.team.instances[i];
                GameObject character1 = player.team.instances[j];

                // Swap the position of the characters in the party
                player.team.instances[i] = character1;
                player.team.instances[j] = character0;

                // Reset the colours
                SelectedCharacters[0].GetComponent<Image>().color = defaultColor;
                SelectedCharacters[1].GetComponent<Image>().color = defaultColor;
            }

            // Update the UI for both the party and the storage
            UpdatePartyUI();
            UpdateStorageUI();

            // Deselect both characters
            SelectedCharacters[0] = null;
            SelectedCharacters[1] = null;
        }
    }
}
