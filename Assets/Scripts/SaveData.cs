using System.Collections.Generic;

[System.Serializable]
public class CharacterSaveData
{
    public string Id;
    public int Health;
    public int Damage;
    public float StaminaRate;
    public int Experience;
    public bool IsEvolved;
    public int SpriteIndex;
}

public class TeamSaveData
{
    public List<CharacterSaveData> selectedCharacters = new List<CharacterSaveData>();
}
