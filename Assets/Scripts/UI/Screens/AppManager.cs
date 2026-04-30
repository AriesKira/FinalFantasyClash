using UnityEngine;
using System.Collections.Generic;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance { get; private set; }

    public List<string> blueTeamIDs = new List<string>();
    public List<string> redTeamIDs = new List<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveTeamsForBattle(List<string> blue, List<string> red)
    {
        blueTeamIDs = blue;
        redTeamIDs = red;
    }
}