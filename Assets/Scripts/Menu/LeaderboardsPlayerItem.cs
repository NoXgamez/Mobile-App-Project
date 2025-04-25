using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Leaderboards.Models;
using UnityEngine.UI;

public class LeaderboardsPlayerItem : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI rankText = null;
    [SerializeField] public TextMeshProUGUI nameText = null;
    [SerializeField] public TextMeshProUGUI scoreText = null;
    [SerializeField] private Button selectButton = null;
    
    private LeaderboardEntry player = null;
    
    private void Start()
    {
        selectButton.onClick.AddListener(Clicked);
    }

    
    public void Initialize(LeaderboardEntry player)
    {
        this.player = player;
        rankText.text = (player.Rank + 1).ToString();
        nameText.text = player.PlayerName;
        scoreText.text = player.Score.ToString();
    }
    
    private void Clicked()
    {
        Debug.Log("TODO -> Open profile: " + player.PlayerName);
    }
    
}