using Map;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MapManager mm;
    public InterstitialAd ad;
    public Player player;
    public Enemy enemy;
    public GameObject[] scenes;
    // 0 is map
    // 1 is battle
    // 2 is inventory
    // 3 is treasure
    // 4 is store
    // 5 is get new character

    private void Start()
    {
        //player.Load();
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
        player.team.DespawnCharacters();
        enemy.team.DespawnCharacters();
        scenes[1].gameObject.SetActive(false);
        player.team.IsInBattle = false;
        enemy.team.IsInBattle = false;

        if (IsPlayer)
        {
            Debug.Log("Player won the battle!");
            // Handle player win logic here
            player.money += 10;
            OpenGetNewCharacter();
        }
        else
        {
            Debug.Log("Enemy won the battle!");
            // Handle enemy win logic here
            ad.LoadAd();
            ad.ShowAd();
        }

        OpenMap();
    }

    private void Restart()
    {

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

    public TMPro.TMP_Text AmountText;
    public void OpenTreasure()
    {
        scenes[3].gameObject.SetActive(true);
        int amount = Random.Range(20, 51);
        AmountText.text = "+" + amount.ToString();
        player.money += amount;
        StartCoroutine(CloseTreasure());
    }

    public IEnumerator CloseTreasure()
    {
        yield return new WaitForSeconds(2f);
        scenes[3].gameObject.SetActive(false);
    }

    public void OpenStore()
    {
        CloseMap();
        scenes[4].gameObject.SetActive(true);
    }

    public void CloseStore()
    {
        scenes[4].gameObject.SetActive(false);
        OpenMap();
    }

    public void OpenGetNewCharacter()
    {
        CloseMap();
        scenes[5].gameObject.SetActive(true);
        GenNewCharacter gen = scenes[5].gameObject.GetComponent<GenNewCharacter>();
        gen.GetNewCharacter();
        StartCoroutine(CloseGetNewCharacter());
    }

    public IEnumerator CloseGetNewCharacter()
    {
        yield return new WaitForSeconds(2f);
        scenes[5].gameObject.SetActive(false);
        OpenMap();
    }
}
