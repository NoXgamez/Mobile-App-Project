using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using RangeAttribute = UnityEngine.RangeAttribute;

public class BaseCharacter : MonoBehaviour
{
    // Components
    [Header("Components")]
    private Sprite ActiveSprite;
    [Range(0, 2)]
    public int SpriteIndex;
    [Tooltip("Base should be 0, devil should be 1, angel should be 2")]
    public Sprite[] Evolutions = new Sprite[3];
    private SpriteRenderer spriteRenderer;

    public Team team;

    [Header("Character UI Components")]
    [SerializeField]
    TextMeshPro healthText;
    [SerializeField]
    TextMeshPro damageText;
    //[SerializeField]
    // Stamina Bar

    // Variables
    [Header("Character Info")]
    public string Id; // Unique identifier for the character
    public string Name;
    public bool IsEvolved;

    // Health
    [Header("Health")]
    public int MaxHealth;
    public int Health;
    public int HealthCap;

    // Damage
    [Header("Damage")]
    public int Damage;
    public int DamageCap;

    // Stamina
    [Header("Stamina")]
    public float Stamina;
    [Tooltip("How much should the stamina recover every second")][Range(0f, 0.3f)] // Max Stamina Regen Rate is still to be decided
    public float StaminaRecoveryRate;
    private const float MaxStamina = 1f;
    //public int StaminaCount = 0; // How many points of stamina the character currently has // Might not use this variable, depends on time

    // Experience
    [Header("Experience")]
    [Range(-3, 3)]
    public int Experience = 0;

    private void Start()
    {
        // Setting the components & variables
        spriteRenderer = GetComponent<SpriteRenderer>();

        //UpdateSprite();
    }

    private void FixedUpdate()
    {
        if (team.IsInBattle)
        {
            // Passively increase stamina
            Stamina += StaminaRecoveryRate * Time.deltaTime;

            if (Stamina >= MaxStamina)
            {
                // Reset the stamina and raise the stamina count by 1
                Stamina = 0f;
                //StaminaCount++;
                // Use the basic attack when the bar fills up
                BasicAttack();
            }
            // Update the stamina bar and health count
        }
    }

    private void BasicAttack()
    {
        /*
        // Moved to Team so that it doesn't run every attack, place back here if it doesn't work there
        // Find the enemy team
        Team[] teams = FindObjectsByType<Team>(FindObjectsSortMode.None);
        Team enemyTeam = null;

        foreach (Team t in teams)
        {
            if (t != team)
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
        */

        if (team == null)
        {
            Debug.LogError($"{name} tried to attack but has no team assigned!");
            return;
        }
        if (team.enemies == null || team.enemies.Length == 0)
        {
            Debug.LogError($"{name} has an empty enemies array!");
            return;
        }

        // Find a random enemy to attack
        //var aliveEnemies = team.enemies
        //    .Where(character => character.Health > 0)
        //    .ToList();

        List<GameObject> aliveEnemies = new List<GameObject>();
        foreach (GameObject enemy in team.enemies)
        {
            if (enemy != null && enemy.GetComponent<BaseCharacter>().Health > 0)
            {
                aliveEnemies.Add(enemy);
            }
            else if (enemy == null)
            {
                Debug.LogWarning($"Enemy character is null in {name}'s enemies list.");
            }
            else
            {
                Debug.LogWarning($"Enemy character {enemy.name} is dead.");
            }
        }

        if (aliveEnemies.Count == 0)
        {
            if (team.IsPlayer)
            {
                team.gm.EndBattle(true);
            }
            else
            {
                team.gm.EndBattle(false);
            }
        }

        // Attack the enemy
        int selectedIndex = Random.Range(0, aliveEnemies.Count);
        GameObject selectedEnemy = aliveEnemies[selectedIndex];

        Debug.Log($"{name} is attacking {selectedEnemy.name} for {Damage} damage.");
        selectedEnemy.GetComponent<BaseCharacter>().UpdateHealth(-Damage);
    }

    public void UpdateHealth(int amount)
    {
        // Update the health variable
        Health += amount;

        // Decide what to do if the health crosses the thresholds
        if (Health <= 0)
            KillCharacter();
        else if (Health > MaxHealth)
            Health = MaxHealth;
    }

    private void KillCharacter()
    {
        // Make the character useless
        StaminaRecoveryRate = 0f;
        Stamina = 0;
        //StaminaCount = 0;
    }

    public void LevelUp(int amount)
    {
        if (!IsEvolved)
        {
            // Increase the experience
            Experience += amount;

            // Evolve when ExperienceNeeded is surpassed
            if (Experience <= 3)
            {
                SpriteIndex = 1;
                Evolve();
            }
            else if (Experience >= 3)
            {
                SpriteIndex = 2;
                Evolve();
            }
        }
    }

    private void Evolve()
    {
        // Change the sprite and set it as evolved
        UpdateSprite();
        IsEvolved = true;

        // Increase the stats
        IncreaseStat(2, Stat.Health);
        HealthCap += 3; // Increase the health cap
        IncreaseStat(2, Stat.Damage);
        DamageCap += 3; // Increase the damage cap
    }

    private void UpdateSprite()
    {
        ActiveSprite = Evolutions[SpriteIndex];
        spriteRenderer.sprite = ActiveSprite;
    }

    private void IncreaseStat( int amount, Stat stat)
    {
        switch (stat)
        {
            case Stat.Health:
                // Increase the health
                MaxHealth += amount;
                // Ensure the health doesn't surpass the cap
                if (MaxHealth > HealthCap)
                    MaxHealth = HealthCap;
                break;
            case Stat.Damage:
                // Increase the damage
                Damage += amount;
                // Ensure the damage doesn't surpass the cap
                if (Damage > DamageCap)
                    Damage = DamageCap;
                break;
        }
    }

    private enum Stat
    {
        Health = 0,
        Damage = 1
    }
}
