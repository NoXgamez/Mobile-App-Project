using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Components
    [SerializeField]
    private Storage storage;
    [SerializeField]
    private Team team;

    void Start()
    {
        //DontDestroyOnLoad(this.gameObject); // This will make the object persist between scenes

        // Get the storage and team components
        storage = GetComponentInChildren<Storage>();
        team = GetComponentInChildren<Team>();

        Load(); // Load the data when the game starts
    }

    public void StartBattle()
    {
        team.IsInBattle = true; // Set the character as in battle

        foreach (GameObject obj in team.SelectedCharacters)
        {
            if (obj != null)
            {
                team.SpawnCharacter(obj);

                BaseCharacter c = obj.GetComponent<BaseCharacter>();
                if (c != null)
                {
                    c.Health = c.MaxHealth;
                    c.Stamina = 0; // Start the battle with no stamina
                    //StaminaCount = 0; // Start the battle with no stamina points
                }
            }
        }

        team.FindEnemyTeam();
    }

    public void EndBattle()
    {
        team.IsInBattle = false; // Set the character as not in battle
    }

    public void Save()
    {
        TeamSaveData teamData = new TeamSaveData();

        foreach (GameObject obj in team.SelectedCharacters)
        {
            if (obj != null)
            {
                BaseCharacter character = obj.GetComponent<BaseCharacter>();
                if (character != null)
                {
                    CharacterSaveData saveData = new CharacterSaveData
                    {
                        Id = character.Id,
                        Health = character.MaxHealth,
                        HealthCap = character.HealthCap,
                        Damage = character.Damage,
                        DamageCap = character.DamageCap,
                        StaminaRate = character.StaminaRecoveryRate,
                        Experience = character.Experience,
                        IsEvolved = character.IsEvolved,
                        SpriteIndex = character.SpriteIndex
                    };

                    teamData.selectedCharacters.Add(saveData);
                }
            }
        }

        string teamJson = JsonUtility.ToJson(teamData, true);
        File.WriteAllText(Application.persistentDataPath + "/SaveData/teamData.json", teamJson);

        TeamSaveData storageData = new TeamSaveData();

        foreach (GameObject obj in storage.CharactersStored)
        {
            if (obj != null)
            {
                BaseCharacter character = obj.GetComponent<BaseCharacter>();
                if (character != null)
                {
                    CharacterSaveData saveData = new CharacterSaveData
                    {
                        Id = character.Id,
                        Health = character.MaxHealth,
                        HealthCap = character.HealthCap,
                        Damage = character.Damage,
                        DamageCap = character.DamageCap,
                        StaminaRate = character.StaminaRecoveryRate,
                        Experience = character.Experience,
                        IsEvolved = character.IsEvolved,
                        SpriteIndex = character.SpriteIndex
                    };

                    storageData.selectedCharacters.Add(saveData);
                }
            }
        }

        string storageJson = JsonUtility.ToJson(storageData, true);
        File.WriteAllText(Application.persistentDataPath + "/SaveData/storageData.json", storageJson);
    }

    private Dictionary<string, GameObject> prefabLookup;

    private void BuildPrefabLookup()
    {
        prefabLookup = new Dictionary<string, GameObject>();
        GameObject[] allPrefabs = Resources.LoadAll<GameObject>("Characters");

        foreach (GameObject prefab in allPrefabs)
        {
            BaseCharacter character = prefab.GetComponent<BaseCharacter>();
            if (character != null && !prefabLookup.ContainsKey(character.Id))
            {
                prefabLookup.Add(character.Id, prefab);
            }
        }
    }

    public void Load()
    {
        BuildPrefabLookup();

        string teamPath = Application.persistentDataPath + "/SaveData/teamData.json";
        if (File.Exists(teamPath))
        {
            string teamJson = File.ReadAllText(teamPath);
            TeamSaveData teamData = JsonUtility.FromJson<TeamSaveData>(teamJson);

            for (int i = 0; i < team.SelectedCharacters.Length; i++)
            {
                if (i < teamData.selectedCharacters.Count)
                {
                    var savedCharacter = teamData.selectedCharacters[i];

                    if (prefabLookup.TryGetValue(savedCharacter.Id, out GameObject prefab))
                    {
                        GameObject obj = Instantiate(prefab);
                        BaseCharacter c = obj.GetComponent<BaseCharacter>();
                        ApplyCharacterStats(c, savedCharacter);
                        team.SelectedCharacters[i] = obj;
                    }
                    else
                    {
                        Debug.LogWarning("Prefab not found for ID: " + savedCharacter.Id);
                    }
                }
            }
        }

        string storagePath = Application.persistentDataPath + "/SaveData/storageData.json";
        if (File.Exists(storagePath))
        {
            string storageJson = File.ReadAllText(storagePath);
            TeamSaveData storageData = JsonUtility.FromJson<TeamSaveData>(storageJson);

            storage.CharactersStored.Clear(); // Clear existing characters in storage to avoid duplicating

            foreach (var savedCharacter in storageData.selectedCharacters)
            {
                if (prefabLookup.TryGetValue(savedCharacter.Id, out GameObject prefab))
                {
                    GameObject obj = Instantiate(prefab);
                    BaseCharacter c = obj.GetComponent<BaseCharacter>();
                    ApplyCharacterStats(c, savedCharacter);
                    storage.CharactersStored.Add(obj);
                }
                else
                {
                    Debug.LogWarning("Prefab not found for ID: " + savedCharacter.Id);
                }
            }
        }
    }

    private void ApplyCharacterStats(BaseCharacter character, CharacterSaveData data)
    {
        character.MaxHealth = data.Health;
        character.HealthCap = data.HealthCap;
        character.Damage = data.Damage;
        character.DamageCap = data.DamageCap;
        character.StaminaRecoveryRate = data.StaminaRate;
        character.Experience = data.Experience;
        character.IsEvolved = data.IsEvolved;
        character.SpriteIndex = data.SpriteIndex;
    }
}
