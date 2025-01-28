using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    // Components
    public Texture2D texture;

    // Variables
    public int MaxHealth = 0;
    public int Health = 0;
    public int HealthCap = 0;
    public int MaxDamage = 0;
    public int Damage = 0;
    public int DamageCap = 0;
    public const float MaxStamina = 1f;
    public float Stamina = 0f;
    [Tooltip("How much should the stamina recover every second")][Range(0f, 0.3f)] 
    public float StaminaRecoveryRate = 0f; // Max Stamina Regen Rate is still to be decided
    public int StaminaCount = 0;
    // Experience may be a float or an int, depends which exp system we decide on
    public float Experience = 0f;
    public float ExperienceNeeded = 0f;

    private void Start()
    {
        // Setting the components & variables
        texture = GetComponent<Texture2D>();
        Health = MaxHealth;
        Damage = MaxDamage;
        Stamina = MaxStamina * 0.75f; // Start the battle with a bit of stamina
    }

    private void FixedUpdate()
    {
        // Passively increase stamina
        Stamina += StaminaRecoveryRate;

        if (Stamina >= MaxStamina)
        {
            // Reset the stamina and raise the stamina count by 1
            Stamina = 0f;
            StaminaCount++;
            // Use the basic attack when the bar fills up
            BasicAttack();
        }
    }

    private void BasicAttack()
    {

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
        StaminaRecoveryRate = 0f;
    }

    public void LevelUp(float amount)
    {
        // Increase the experience
        Experience += amount;

        // Evolve when ExperienceNeeded is surpassed
        if (Experience >= ExperienceNeeded)
        {
            Experience -= ExperienceNeeded;
            Evolve();
        }
    }

    private void Evolve()
    {

    }

    public void IncreaseStat( int amount, string stat)
    {
        switch (stat)
        {
            case "Health":
                // Increase the health
                MaxHealth += amount;
                // Ensure the health doesn't surpass the cap
                if (MaxHealth > HealthCap)
                    MaxHealth = HealthCap;
                break;
            case "Damage":
                // Increase the damage
                MaxDamage += amount;
                // Ensure the damage doesn't surpass the cap
                if (MaxDamage > DamageCap)
                    MaxDamage = DamageCap;
                break;
        }
    }
}
