using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;
    public Camera cam;
    public GameObject[] scenes = new GameObject[4];
    // 0 is map
    // 1 is battle
    // 2 is inventory
    // 3 is treasure

    private void Start()
    {
        //player.Load();
    }

    private void GetCameraSize()
    {

    }

    public void OpenMap()
    {
        scenes[0].gameObject.SetActive(true);
    }

    public void CloseMap()
    {
        scenes[0].gameObject.SetActive(false);
    }

    public void StartBattle()
    {
        CloseMap();
        scenes[1].gameObject.SetActive(true);
        player.StartBattle();
        enemy.StartBattle();
    }

    public void EndBattle(bool IsPlayer)
    {
        scenes[1].gameObject.SetActive(false);
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
        CloseMap();
        scenes[2].gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        scenes[2].gameObject.SetActive(false);
        OpenMap();
    }

    public void OpenTreasure()
    {
        CloseMap();
        scenes[3].gameObject.SetActive(true);

        int amount = Random.Range(20, 51);
        StartCoroutine(CloseTreasure());
    }

    public IEnumerator CloseTreasure()
    {
        yield return new WaitForSeconds(2f);
        scenes[3].gameObject.SetActive(false);
        OpenMap();
    }
}
