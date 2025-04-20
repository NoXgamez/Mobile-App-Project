using System.Linq;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    // Components
    [Header("Components")]
    private Sprite ActiveSprite;
    [Range(0, 2)]
    public int SpriteIndex;
    [Tooltip("Base should be 0, devil should be 1, angel should be 2")]
    public Sprite[] Evolutions;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Team team;

    // Variables
    private bool IsEvolved;
    public bool IsInBattle; // Is the character in battle? (used for the UI)

    // Health
    [Header("Health")]
    [SerializeField]
    private int MaxHealth;
    public int Health;
    [SerializeField]
    private int HealthCap;

    // Damage
    [Header("Damage")]
    [SerializeField]
    private int MaxDamage;
    public int Damage;
    [SerializeField]
    private int DamageCap;

    // Stamina
    [Header("Stamina")]
    private const float MaxStamina = 1f;
    public float Stamina;
    [SerializeField][Tooltip("How much should the stamina recover every second")][Range(0f, 0.3f)] // Max Stamina Regen Rate is still to be decided
    private float StaminaRecoveryRate;
    //public int StaminaCount = 0; // How many points of stamina the character currently has // Might not use this variable, depends on time

    // Experience
    [Header("Experience")]
    [Range(-3, 3)]
    public int Experience = 0;

    private void Start()
    {
        // Setting the components & variables
        spriteRenderer = GetComponent<SpriteRenderer>();
        team = GetComponent<Team>();
        UpdateSprite();
    }

    public void StartBattle()
    {
        IsInBattle = true; // Set the character as in battle

        Health = MaxHealth;
        Damage = MaxDamage;
        Stamina = 0; // Start the battle with no stamina
        //StaminaCount = 0; // Start the battle with no stamina points

        // Show the battle screen
    }

    public void EndBattle()
    {
        // Hide the battle screen
        IsInBattle = false;
    }

    private void FixedUpdate()
    {
        // Passively increase stamina
        Stamina += StaminaRecoveryRate;

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

    private void BasicAttack()
    {
        // Find the enemy team
        Team[] teams = FindObjectsByType<Team>(FindObjectsSortMode.None);
        Team enemyTeam;

        if (team.isPlayer)
            enemyTeam = teams[1];
        else
            enemyTeam = teams[0];

        // Find a random enemy to attack
        var aliveEnemies = enemyTeam.SelectedCharacters
        .Select(go => go.GetComponent<BaseCharacter>()) // get the script from each GameObject
        .Where(character => character != null && character.Health > 0)
        .ToList();

        if (aliveEnemies.Count == 0)
        {
            Debug.Log("No valid targets to attack.");
            return;
        }

        // Attack the enemy
        int selectedIndex = Random.Range(0, aliveEnemies.Count);
        BaseCharacter selectedEnemy = aliveEnemies[selectedIndex];

        selectedEnemy.UpdateHealth(-Damage);
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
                MaxDamage += amount;
                // Ensure the damage doesn't surpass the cap
                if (MaxDamage > DamageCap)
                    MaxDamage = DamageCap;
                break;
        }
    }

    private enum Stat
    {
        Health = 0,
        Damage = 1
    }
}
