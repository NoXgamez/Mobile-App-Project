using Map;
using System.Collections;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MapManager mm;
    public InterstitialAd ad;
    public Player player;
    public Enemy enemy;
    public GameObject[] scenes;
    public SceneManage sm;
    // 0 is map
    // 1 is battle
    // 2 is inventory
    // 3 is treasure
    // 4 is store
    // 5 is get new character
    // 6 is lose

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
        scenes[1].gameObject.SetActive(false);
        player.team.IsInBattle = false;
        enemy.team.IsInBattle = false;

        if (IsPlayer)
        {
            Debug.Log("Player won the battle!");
            player.money += 10;
            OpenMap();
            OpenGetNewCharacter();
        }
        else
        {
            Debug.Log("Enemy won the battle!");

            OpenLose();
            player.money = 0;
            for (int i = 0; i < player.team.instances.Length; i++)
            {
                player.team.instances[i] = null;
                player.team.SelectedCharacters[i] = null;
                player.team.CharacterPrefabs[i] = null;
            }
            foreach (GameObject obj in player.storage.CharactersStored)
            {
                player.storage.CharactersStored.Remove(obj);
            }
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

    public void OpenLose()
    {
        scenes[6].gameObject.SetActive(true);

        ad.LoadAd();
        ad.ShowAd();
    }
    public void CloseLose()
    {
        scenes[6].gameObject.SetActive(false);
        sm.StartScene("MenuScene");
    }
}
