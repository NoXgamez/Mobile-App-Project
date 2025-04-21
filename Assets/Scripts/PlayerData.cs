using System;
using UnityEngine;
using UnityEngine.Rendering;

public static class PlayerData
{
    public static int Level = 1;
    public static int XP = 0;
    public static int Premium = 500; // Cash

    public static bool SaveData()
    {
        try
        {
            PlayerSaveData data = new PlayerSaveData(Level, XP, Premium);
            SaveSystem.Save(data);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Save failed: " + e.Message);
            return false;
        }
    }

    public static bool LoadData()
    {
        try
        {
            PlayerSaveData data = SaveSystem.Load();
            if (data != null)
            {
                Level = data.Level;
                XP = data.XP;
                Premium = data.Premium;
                return true;
            }
            return false;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Load failed: " + e.Message);
            return false;
        }
    }

    public static void UpdateLevel()
    {
        int xpRequired = ReturnXPRequired();

        while (XP >= xpRequired)
        {
            if (xpRequired <= 0)
            {
                Debug.LogWarning("XP requirement returned 0. Breaking loop to avoid infinite leveling.");
                break;
            }

            XP -= xpRequired;
            Level += 1;
            xpRequired = ReturnXPRequired();

            Debug.Log($"You have levelled up to: {Level}! You have {XP} XP left.");
        }
    }

    private static int ReturnXPRequired()
    {
        int value = 100;
        double multiplier = 0;
        int result = 0;

        if (Level == 1) { return value; }
        else
        if (Level <= 9)
        {
            multiplier = Math.Pow(1.05, Level);
            result = (int)Math.Floor(value * multiplier);

            return result;
        }
        else
        if (Level <= 19)
        {
            multiplier = Math.Pow(1.10, Level);
            result = (int)Math.Floor(value * multiplier);

            return result;
        }
        else { return 0; }
        // Currently the Max Level is 20
        // pls fix the multiplier, it will crazy too fast: level 100 requires 400 quadrillion xp!!!!
    }
}