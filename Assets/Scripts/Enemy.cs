using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Team team;

    void Start()
    {
        team = GetComponent<Team>();
        GetRandomCharacters();
    }

    private IEnumerator DelayedFindTeam()
    {
        yield return null;
        team.FindEnemyTeam();
        StartCoroutine(DelayedGetRandom());
    }

    private IEnumerator DelayedGetRandom()
    {
        yield return null;
        GetRandomCharacters();
    }

    /*
    private IEnumerator DelayedStart()
    {
        yield return null;

        GetRandomCharacters();
        StartBattle();
        team.FindEnemyTeam();
    }
    */

    public void StartBattle()
    {
        team.IsInBattle = true; // Set the character as in battle
        StartCoroutine(DelayedFindTeam()); // Find the enemy team after a frame

        for (int i = 0; i < team.CharacterPrefabs.Length; i++)
        {
            if (team.CharacterPrefabs[i] != null)
            {
                team.instances[i] = Instantiate(team.CharacterPrefabs[i]); // Assign the character prefab to the selected characters array

                if (team.instances[i] != null)
                {
                    team.SpawnCharacter(team.instances[i], i);

                    team.instances[i].GetComponent<BaseCharacter>().Health = team.instances[i].GetComponent<BaseCharacter>().MaxHealth;
                    team.instances[i].GetComponent<BaseCharacter>().GetComponent<BaseCharacter>().Stamina = 0; // Start the battle with no stamina
                    //StaminaCount = 0; // Start the battle with no stamina points
                    team.instances[i].GetComponent<BaseCharacter>().team = team; // Assign the enemy team to the character
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

        if (team.enemies.Length == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                int randomIndex = Random.Range(0, allPrefabs.Length);
                GameObject randomPrefab = allPrefabs[randomIndex];

                team.CharacterPrefabs[i] = randomPrefab;
            }
        }
        else if (team.enemies.Length == 1)
        {
            int randomAmount = Random.Range(1, 3);

            for (int i = 0; i < randomAmount; i++)
            {
                int randomIndex = Random.Range(0, allPrefabs.Length);
                GameObject randomPrefab = allPrefabs[randomIndex];

                team.CharacterPrefabs[i] = randomPrefab;
            }
        }
        else if (team.enemies.Length > 1 && team.enemies.Length < 4)
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
