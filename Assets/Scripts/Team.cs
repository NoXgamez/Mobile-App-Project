using UnityEngine;
using UnityEngine.UI;

public class Team : MonoBehaviour
{
    public GameObject[] CharacterPrefabs = new GameObject[4];
    public GameObject[] SelectedCharacters = new GameObject[4];
    public CharacterPositionObject[] CharacterPositions = new CharacterPositionObject[4];
    //public bool isPlayer = false;
    public bool IsInBattle;
    public bool IsPlayer;
    public GameManager gm;
    public Team enemyTeam;
    public GameObject[] enemies = new GameObject[4];

    public void AssignTeam()
    {
        foreach (GameObject obj in instances)
        {
            if (obj != null)
            {
                BaseCharacter c = obj.GetComponent<BaseCharacter>();
                if (c != null)
                {
                    c.team = this;
                }
            }
        }

        //FindEnemyTeam(); // For testing purposes only, want to actually find the enemy team when a battle begins to be called in Player.StartBattle()
    }

    public void FindEnemyTeam()
    {
        // Find the enemy team
        Team[] teams = FindObjectsByType<Team>(FindObjectsSortMode.None);

        foreach (Team t in teams)
        {
            if (t != this)
            {
                enemyTeam = t;
                break;
            }
        }
        if (enemyTeam == null)
        {
            Debug.Log("No enemy team found.");
            return;
        }

        /*
        for (int i = 0; i < enemyTeam.SelectedCharacters.Length; i++)
        {
            GameObject sc = enemyTeam.SelectedCharacters[i];
            if (sc == null)
            {
                Debug.LogWarning($"Enemy character at index {i} is null");
                continue;
            }

            BaseCharacter c = sc.GetComponent<BaseCharacter>();
            if (c == null)
            {
                Debug.LogWarning($"No BaseCharacter on enemy character at index {i}");
                continue;
            }

            enemies[i] = c;
            Debug.Log($"Added enemy character {c.name} to enemies[{i}]");
        }
        */

        foreach (GameObject sc in instances)
            sc.GetComponent<BaseCharacter>().team = this;

        for (int i = 0; i < enemyTeam.instances.Length; i++)
        {
            GameObject sc = enemyTeam.instances[i];
            if (sc != null)
                enemies[i] = sc;
            else
                Debug.LogWarning($"Enemy character at index {i} is null");
        }
    }
    public GameObject[] instances = new GameObject[4];

    public void SpawnCharacter(GameObject obj, int index)
    {
        if (obj != null)
        {
            if (instances[index] == null)
            {
                instances[index] = Instantiate(obj, Vector3.zero, Quaternion.identity);
            }

            BaseCharacter bc = obj.GetComponent<BaseCharacter>();
            instances[index].GetComponent<BaseCharacter>().IsEvolved = bc.IsEvolved;
            instances[index].GetComponent<BaseCharacter>().MaxHealth = bc.MaxHealth;
            instances[index].GetComponent<BaseCharacter>().HealthCap = bc.HealthCap;
            instances[index].GetComponent<BaseCharacter>().Damage = bc.Damage;
            instances[index].GetComponent<BaseCharacter>().DamageCap = bc.DamageCap;
            instances[index].GetComponent<BaseCharacter>().Experience = bc.Experience;

            //SelectedCharacters[index] = instances[index];

            CharacterPositions[index].image.sprite = instances[index].GetComponent<SpriteRenderer>().sprite;
            CharacterPositions[index].healthText.text = instances[index].GetComponent<BaseCharacter>().Health.ToString();
            CharacterPositions[index].damageText.text = instances[index].GetComponent<BaseCharacter>().Damage.ToString();
            instances[index].GetComponent<BaseCharacter>().HealthText = CharacterPositions[index].healthText;
            instances[index].gameObject.SetActive(true);
        }
    }
}
