using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public int Level;
    public int XP;
    public int Premium;

    public PlayerSaveData()
    {
        Level = 1;
        XP = 0;
        Premium = 500;
    }

    public PlayerSaveData(int level, int xp, int premium)
    {
        Level = level;
        XP = xp;
        Premium = premium;
    }
}