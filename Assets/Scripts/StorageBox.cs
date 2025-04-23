using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StorageBox : MonoBehaviour
{
    // Components
    public GameObject[] PlayerCharactersListItems = new GameObject[4];
    public GameObject Player;
    public GameObject Box;

    // Variables
    public GameObject[] SelectedCharacters = new GameObject[2];

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
            CheckAreBothCharactersSelected();
            return;
        }
        else if (SelectedCharacters[1] == null)
        {
            SelectedCharacters[1] = obj; // Add the character to the second slot
            CheckAreBothCharactersSelected();
            return;
        }
        else if (SelectedCharacters[0] == obj)
        {
            SelectedCharacters[0] = null; // Deselect the character
            return;
        }
        else if (SelectedCharacters[1] == obj)
        {
            SelectedCharacters[1] = null; // Deselect the character
            return;
        }
    }

    private void CheckAreBothCharactersSelected()
    {
        Player player = Player.GetComponent<Player>();

        if (SelectedCharacters[0] != null && SelectedCharacters[1] != null)
        {
            if ((player.team.SelectedCharacters.Contains(SelectedCharacters[0]) && player.storage.CharactersStored.Contains(SelectedCharacters[1])) ||
                (player.team.SelectedCharacters.Contains(SelectedCharacters[1]) && player.storage.CharactersStored.Contains(SelectedCharacters[0])))
            {
                // Swap the characters in the box and between inventory and box

                // Deselect both characters
                SelectedCharacters[0] = null;
                SelectedCharacters[1] = null;
                UpdateBox();
            }
            else if (player.storage.CharactersStored.Contains(SelectedCharacters[0]) && player.storage.CharactersStored.Contains(SelectedCharacters[1]))
            {
                // Swap the position of the characters in the box

                // Deselect both characters
                SelectedCharacters[0] = null;
                SelectedCharacters[1] = null;
                UpdateBox();
            }
            else if (player.team.SelectedCharacters.Contains(SelectedCharacters[0]) && player.team.SelectedCharacters.Contains(SelectedCharacters[1]))
            {
                // Swap the position of the characters in the party

                // Deselect both characters
                SelectedCharacters[0] = null;
                SelectedCharacters[1] = null;
                UpdateBox();
            }
        }
    }

    public void UpdateBox()
    {

    }
}
