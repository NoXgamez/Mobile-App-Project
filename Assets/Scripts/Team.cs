using UnityEngine;

public class Team : MonoBehaviour
{
    public GameObject[] CharacterPrefabs = new GameObject[4];
    public GameObject[] SelectedCharacters = new GameObject[4];
    public Vector2[] SpawnPositions = new Vector2[4];
    //public bool isPlayer = false;
    public bool IsInBattle;
    public Team enemyTeam;
    public GameObject[] enemies = new GameObject[4];

    private void Start()
    {
        foreach (GameObject obj in SelectedCharacters)
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

        FindEnemyTeam(); // For testing purposes only, want to actually find the enemy team when a battle begins to be called in Player.StartBattle()
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

        foreach (GameObject sc in SelectedCharacters)
            sc.GetComponent<BaseCharacter>().team = this;

        for (int i = 0; i < enemyTeam.SelectedCharacters.Length; i++)
        {
            GameObject sc = enemyTeam.SelectedCharacters[i];
            if (sc != null)
                enemies[i] = sc;
            else
                Debug.LogWarning($"Enemy character at index {i} is null");
        }
    }

    public void SpawnCharacter(GameObject obj, int index)
    {
        if (obj != null)
        {
            GameObject instance = Instantiate(obj, SpawnPositions[index], Quaternion.identity);
            SelectedCharacters[index] = instance;
        }
    }
}
