using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public GameObject[] SelectedCharacters = new GameObject[4];
    public bool isPlayer = false;
    public bool IsInBattle;
    public Team enemyTeam;

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
    }

    public void SpawnCharacter()
    {

    }
}
