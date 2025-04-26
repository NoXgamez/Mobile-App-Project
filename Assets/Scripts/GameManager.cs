using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;
    public GameObject[] scenes;
    // 0 is battle
    // 1 is inventory
    // 2 is treasure

    public void Start()
    {
        player.Load();
    }

    public void StartBattle()
    {
        scenes[0].gameObject.SetActive(true);
        player.StartBattle();
        enemy.StartBattle();
    }

    public void EndBattle(bool IsPlayer)
    {
        scenes[0].gameObject.SetActive(false);
        player.team.IsInBattle = false;
        enemy.team.IsInBattle = false;

        if (IsPlayer)
        {
            Debug.Log("Player won the battle!");
            // Handle player win logic here
        }
        else
        {
            Debug.Log("Enemy won the battle!");
            // Handle enemy win logic here
        }
    }

    public void OpenInventory()
    {
        scenes[1].gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        scenes[1].gameObject.SetActive(false);
    }

    public void GetTreasure()
    {
        scenes[2].gameObject.SetActive(true);

        int amount = Random.Range(20, 51);

        StartCoroutine(CloseTreasure());
    }

    public IEnumerator CloseTreasure()
    {
        yield return new WaitForSeconds(2f);
        scenes[2].gameObject.SetActive(false);
    }
}
