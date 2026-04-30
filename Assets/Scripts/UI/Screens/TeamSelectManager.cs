using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UI.HUD;

public class TeamSelectManager : MonoBehaviour {

    [Header("References")] 
    [SerializeField] private GameObject unitCardPrefab;
    
    [Header("Scroll Contents")]
    [SerializeField] private Transform redScrollContent;
    [SerializeField] private Transform blueScrollContent;
    
    [Header("Team Slots Containers")]
    [SerializeField] private Transform redTeamSlotsParent;
    [SerializeField] private Transform blueTeamSlotsParent;
    
    private string BLUE_TEAM_TAG = "BlueTeam";
    private string RED_TEAM_TAG = "RedTeam";
    
    private void Start() {
        GenerateRosters();
    }

    private void GenerateRosters() {
        ClearContent(redScrollContent);
        ClearContent(blueScrollContent);

        List<string> allUnits = UnitDatabase.Instance.GetAllUnitIds();

        foreach (string id in allUnits) {
            
            GameObject redCard = Instantiate(unitCardPrefab, redScrollContent);
            redCard.transform.localScale = Vector3.one;
            UnitSelectorManager redCardScript = redCard.GetComponent<UnitSelectorManager>();
            redCard.tag = RED_TEAM_TAG;
            if (redCardScript != null) {
                redCardScript.Setup(id, redScrollContent); 
            }

            GameObject blueCard = Instantiate(unitCardPrefab, blueScrollContent);
            blueCard.transform.localScale = Vector3.one;
            UnitSelectorManager blueCardScript = blueCard.GetComponent<UnitSelectorManager>();
            blueCard.tag = BLUE_TEAM_TAG;
            if (blueCardScript != null) {
                blueCardScript.Setup(id, blueScrollContent); 
            }
        }
    }

    private void ClearContent(Transform contentParent) {
        foreach (Transform child in contentParent) {
            Destroy(child.gameObject);
        }
    }

    public void TryStartBattle() {
        List<string> redTeamIds = new List<string>();
        List<string> blueTeamIds = new List<string>();

        foreach (Transform slot in redTeamSlotsParent) {
            if (slot.childCount > 0) {
                UnitSelectorManager card = slot.GetChild(0).GetComponent<UnitSelectorManager>();
                if (card != null) redTeamIds.Add(card.unitId);
            }
        }

        foreach (Transform slot in blueTeamSlotsParent) {
            if (slot.childCount > 0) {
                UnitSelectorManager card = slot.GetChild(0).GetComponent<UnitSelectorManager>();
                if (card != null) blueTeamIds.Add(card.unitId);
            }
        }

        if (redTeamIds.Count == 4 && blueTeamIds.Count == 4) {
            
            if (AppManager.Instance != null) {
                AppManager.Instance.SaveTeamsForBattle(blueTeamIds, redTeamIds);
            }

            SceneManager.LoadScene("GameScene");
        } 
        else {
            Debug.LogWarning($"Équipes incomplètes ! Rouge: {redTeamIds.Count}/4 | Bleu: {blueTeamIds.Count}/4");
        }
    }
}