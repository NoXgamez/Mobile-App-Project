using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenNewCharacter : MonoBehaviour
{
    public Image image;
    public Player player;

    public void GetNewCharacter()
    {
        GameObject[] allPrefabs = Resources.LoadAll<GameObject>("Characters");

        int randomIndex = Random.Range(0, allPrefabs.Length);
        GameObject randomPrefab = allPrefabs[randomIndex];

        for (int i = 0; i < player.team.instances.Length; i++)
        {
            if (player.team.instances[i] != null)
            {
                player.team.instances[i] = randomPrefab;
                return;
            }
        }

        player.storage.CharactersStored.Add(randomPrefab);
    }
}
