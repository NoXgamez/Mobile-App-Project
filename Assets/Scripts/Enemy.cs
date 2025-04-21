using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Team team;

    void Start()
    {
        team = GetComponent<Team>();

        StartCoroutine(DelayedStart());
    }
    private IEnumerator DelayedStart()
    {
        yield return null;

        GetRandomCharacters();
        StartBattle();
        team.FindEnemyTeam();
    }

    public void StartBattle()
    {
        team.IsInBattle = true; // Set the character as in battle

        for (int i = 0; i < team.CharacterPrefabs.Length; i++)
        {
            if (team.CharacterPrefabs[i] != null)
            {
                team.SpawnCharacter(team.CharacterPrefabs[i], i);

                BaseCharacter c = team.SelectedCharacters[i].GetComponent<BaseCharacter>();
                if (c != null)
                {
                    c.Health = c.MaxHealth;
                    c.Stamina = 0; // Start the battle with no stamina
                    //StaminaCount = 0; // Start the battle with no stamina points
                }
            }
        }
    }

    private void GetRandomCharacters()
    {
        GameObject[] allPrefabs = Resources.LoadAll<GameObject>("Characters");

        Debug.Log($"Found {allPrefabs.Length} prefabs in Resources/Characters");

        foreach (var prefab in allPrefabs)
        {
            Debug.Log("Loaded prefab: " + prefab.name);
        }

        if (team.enemyTeam.SelectedCharacters.Length == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                int randomIndex = Random.Range(0, allPrefabs.Length);
                GameObject randomPrefab = allPrefabs[randomIndex];

                team.CharacterPrefabs[i] = randomPrefab;
            }
        }
        else if (team.enemyTeam.SelectedCharacters.Length == 1)
        {
            int randomAmount = Random.Range(1, 3);

            for (int i = 0; i < randomAmount; i++)
            {
                int randomIndex = Random.Range(0, allPrefabs.Length);
                GameObject randomPrefab = allPrefabs[randomIndex];

                team.CharacterPrefabs[i] = randomPrefab;
            }
        }
        else if (team.enemyTeam.SelectedCharacters.Length > 1 && team.enemyTeam.SelectedCharacters.Length < 4)
        {
            int randomAmount = Random.Range(2, 4);

            for (int i = 0; i < randomAmount; i++)
            {
                int randomIndex = Random.Range(0, allPrefabs.Length);
                GameObject randomPrefab = allPrefabs[randomIndex];

                team.CharacterPrefabs[i] = randomPrefab;
            }
        }
        else
        {

        }
    }
}
