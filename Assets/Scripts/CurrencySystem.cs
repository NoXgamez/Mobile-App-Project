public static class CurrencySystem
{
    public static float MinCash = 0;
    public static float StartCash = 3000000;

    private static GameManager manager;

    public static void Init(GameManager gm)
    {
        manager = gm;
        GameData.Cash = StartCash;
        //manager.UpdateUI(1, GameData.Cash);
    }

    public static bool Purchase(float cost)
    {
        if (GameData.Cash - cost >= MinCash)
        {
            RemoveCash(cost);
            return true;
        }
        return false;
    }

    public static bool HasEnough(float amount)
    {
        return GameData.Cash >= amount;
    }

    public static void AddCash(float amount)
    {
        GameData.Cash += amount;
       // manager?.UpdateUI(1, GameData.Cash);
    }

    public static void RemoveCash(float amount)
    {
        GameData.Cash -= amount;
       // manager?.UpdateUI(1, GameData.Cash);
    }
}
