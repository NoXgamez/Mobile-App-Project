using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Vars
    [Header("Frames")]
    [SerializeField] GameObject ButtonsFrame;
    [SerializeField] GameObject MenuFrame;

    [Header("Buttons")]
    [SerializeField] Button PlayBtn;
    [SerializeField] Button ProfileBtn;
    [SerializeField] Button InventoryBtn;
    [SerializeField] Button SkillTreeBtn;
    [SerializeField] Button ShopBtn;
    [SerializeField] Button SettingsBtn;
    [SerializeField] Button CloseBtn;
    [SerializeField] Button LeftBtn;
    [SerializeField] Button RightBtn;
    [SerializeField] Button LoadMapBtn;

    [Header("Panels")]
    [SerializeField] GameObject PlayMenu;
    [SerializeField] GameObject ProfileMenu;
    [SerializeField] GameObject InventoryMenu;
    [SerializeField] GameObject SkillTreeMenu;
    [SerializeField] GameObject ShopMenu;
    [SerializeField] GameObject SettingsMenu;

    [Header("MapMenus")]
    [SerializeField] List<GameObject> Maps;

    [Header("Map Prefabs")]
    [SerializeField] List<GameObject> MapPrefabs;

    private int MapCounter = 0;

    private void Menus(int m)
    {
        MenuFrame.SetActive(true);
        PlayMenu.SetActive(false);
        ProfileMenu.SetActive(false);
        InventoryMenu.SetActive(false);
        SkillTreeMenu.SetActive(false);
        ShopMenu.SetActive(false);
        SettingsMenu.SetActive(false);

        switch (m)
        {
            case 0:
                break;
            case 1:
                PlayMenu.SetActive(true);
                GameSwitcher(0);
                break;
            case 2:
                ProfileMenu.SetActive(true);
                break;
            case 3:
                InventoryMenu.SetActive(true);
                break;
            case 4:
                SkillTreeMenu.SetActive(true);
                break;
            case 5:
                ShopMenu.SetActive(true);
                break;
            case 6:
                SettingsMenu.SetActive(true);
                break;
            default:
                Debug.LogWarning($"Invalid menu num: {m}");
                break;
        }

        if (m == 0)
        {
            MenuFrame.SetActive(false);
        }
    }

    private void GameSwitcher(int o)
    {
        foreach (var Map in Maps)
        {
            Map.SetActive(false);
        }
        MapCounter += o;
        MapCounter = Mathf.Clamp(MapCounter, 0, Maps.Count - 1);

        if (MapCounter == 0)
        {
            LeftBtn.GetComponent<Image>().color = Color.red;
        }
        else
        {
            LeftBtn.GetComponent<Image>().color = Color.white;
        }
        if (MapCounter == (Maps.Count - 1))
        {
            RightBtn.GetComponent<Image>().color = Color.red;
        }
        else
        {
            RightBtn.GetComponent<Image>().color = Color.white;
        }

        Maps[MapCounter].SetActive(true);
    }

    //private void LoadMap()
    //{
    //    string name = Maps[MapCounter].name;
    //    string mapName = name.Replace("Map_[", "").Replace("]_Panel", "");
    //    GameObject map = MapPrefabs.FirstOrDefault(m => m.name.Contains(mapName));

    //    if (map != null)
    //    {
    //        //Debug.Log($"Map was found!: {map.name}, and is being spawned!");
    //        //Vector3 SP = new Vector3(0, 0, 100);
    //        //Instantiate(map, SP, Quaternion.identity);

    //        Debug.Log($"Map was found!: {map.name}, loading into game scene!");
    //        GameData.MapName = map.name;
    //        SceneManager.LoadScene("GameScene");
    //    }
    //    else
    //    {
    //        Debug.LogWarning($"Map was not found!: {mapName}");
    //    }
    //}


    private void Start()
    {
        Menus(0);
        PlayBtn.onClick.AddListener(() => Menus(1));
        ProfileBtn.onClick.AddListener(() => Menus(2));
        InventoryBtn.onClick.AddListener(() => Menus(3));
        SkillTreeBtn.onClick.AddListener(() => Menus(4));
        ShopBtn.onClick.AddListener(() => Menus(5));
        SettingsBtn.onClick.AddListener(() => Menus(6));
        CloseBtn.onClick.AddListener(() => Menus(0));

        LeftBtn.onClick.AddListener(() => GameSwitcher(-1));
        RightBtn.onClick.AddListener(() => GameSwitcher(1));
        //LoadMapBtn.onClick.AddListener(() => LoadMap());
    }
}
