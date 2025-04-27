using System;
using Unity.VisualScripting;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject[] Slots = new GameObject[4];
    public Player player;

    private void OnEnable()
    {
        for (int i = 0; i < Slots.Length; i++)
            UpdateTexts(i);
    }

    private void UpdateTexts(int i)
    {
        if (player.team.SelectedCharacters[i] != null)
        {
            int exp = UnityEngine.Random.Range(-1, -2);
            int cost = UnityEngine.Random.Range(1, 5);

            Slots[i].SetActive(true);
            StoreSlot slot = Slots[i].GetComponent<StoreSlot>();
            slot.CharacterImage.sprite = player.team.SelectedCharacters[i].GetComponent<BaseCharacter>().Evolutions[player.team.SelectedCharacters[i].GetComponent<BaseCharacter>().SpriteIndex];
            string health = player.team.SelectedCharacters[i].GetComponent<BaseCharacter>().MaxHealth.ToString() + "/" + player.team.SelectedCharacters[i].GetComponent<BaseCharacter>().HealthCap.ToString();
            slot.HealthTxt.text = health;
            string damage = player.team.SelectedCharacters[i].GetComponent<BaseCharacter>().Damage.ToString() + "/" + player.team.SelectedCharacters[i].GetComponent<BaseCharacter>().DamageCap.ToString();
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
            Slots[i].SetActive(false);
        }
    }

    public void Upgrade(int index)
    {
        BaseCharacter character = player.team.SelectedCharacters[index].GetComponent<BaseCharacter>();

        if (character != null && !Slots[index].GetComponent<StoreSlot>().IsBought && player.money >= Slots[index].GetComponent<StoreSlot>().cost)
        {
            if (character.MaxHealth < character.HealthCap || character.Damage < character.DamageCap || !character.IsEvolved)
            {
                character.IncreaseStat(1, BaseCharacter.Stat.Health);
                character.IncreaseStat(1, BaseCharacter.Stat.Damage);
                character.LevelUp(Slots[index].GetComponent<StoreSlot>().experience);

                Slots[index].gameObject.SetActive(false);
                Slots[index].GetComponent<StoreSlot>().IsBought = true;
                player.money -= Slots[index].GetComponent<StoreSlot>().cost;
                UpdateTexts(index);
            }
        }
    }
}
