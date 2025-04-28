using System;
using Unity.VisualScripting;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject[] Slots = new GameObject[4];
    public Player player;
    public TMPro.TMP_Text MoneyTxt;

    private void OnEnable()
    {
        PlayerMoney();
        for (int i = 0; i < Slots.Length; i++)
        {
            UpdateTexts(i);
        }
    }

    private void PlayerMoney()
    {
        MoneyTxt.text = player.money.ToString();
    }

    private void UpdateTexts(int i)
    {
        PlayerMoney();
        if (player.team.instances[i] != null)
        {
            int exp = UnityEngine.Random.Range(-1, -2);
            int cost = UnityEngine.Random.Range(1, 5);

            Slots[i].gameObject.SetActive(true);
            StoreSlot slot = Slots[i].GetComponent<StoreSlot>();
            slot.CharacterImage.sprite = player.team.instances[i].GetComponent<BaseCharacter>().Evolutions[player.team.instances[i].GetComponent<BaseCharacter>().SpriteIndex];
            string health = player.team.instances[i].GetComponent<BaseCharacter>().MaxHealth.ToString() + "/" + player.team.instances[i].GetComponent<BaseCharacter>().HealthCap.ToString();
            slot.HealthTxt.text = health;
            string damage = player.team.instances[i].GetComponent<BaseCharacter>().Damage.ToString() + "/" + player.team.instances[i].GetComponent<BaseCharacter>().DamageCap.ToString();
            slot.DamageTxt.text = damage;
            slot.CostTxt.text = cost.ToString();
            if (exp > 0)
            {
                slot.ExperienceTxt.text = "+" + exp.ToString();
                slot.experience = exp;
            }
            else if (exp < 0)
            {
                slot.ExperienceTxt.text = exp.ToString();
                slot.experience = exp;
            }
            else
            {
                Slots[i].gameObject.SetActive(false);
            }
        }
        else
        {
            Slots[i].gameObject.SetActive(false);
        }
    }

    public void Upgrade(int index)
    {
        BaseCharacter character = player.team.instances[index].GetComponent<BaseCharacter>();

        if (character.MaxHealth < character.HealthCap || character.Damage < character.DamageCap || !character.IsEvolved)
        {
            if (character != null && player.money >= Slots[index].GetComponent<StoreSlot>().cost)
            {
                character.IncreaseStat(1, BaseCharacter.Stat.Health);
                character.IncreaseStat(1, BaseCharacter.Stat.Damage);
                character.LevelUp(Slots[index].GetComponent<StoreSlot>().experience);

                Slots[index].gameObject.SetActive(false);
                player.money -= Slots[index].GetComponent<StoreSlot>().cost;
                UpdateTexts(index);
            }
        }
    }
}
