using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variables
    //public GameObject[] PlayerPositions = new GameObject[4];
    //public GameObject[] EnemyPositions = new GameObject[4];
    public GameObject PlayerTeam;
    public GameObject EnemyTeam;

    void Start()
    {
        // If using multiple scenes
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(PlayerTeam);
    }
}
