using UnityEngine;
using UnityEngine.UI;

public class StoreSlot : MonoBehaviour
{
    public Image CharacterImage;
    public TMPro.TMP_Text HealthTxt;
    public TMPro.TMP_Text DamageTxt;
    public int experience = 0;
    public TMPro.TMP_Text ExperienceTxt;
    public int cost = 0;
    public TMPro.TMP_Text CostTxt;
    public bool IsBought = false;
}
