using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public BaseCharacter[] SelectedCharacters = new BaseCharacter[4];
    public BaseCharacter[] AllCharacters;
    public Team EnemyTeam;
}
