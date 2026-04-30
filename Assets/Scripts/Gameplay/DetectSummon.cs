using System;
using Core;
using Enums;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectSummon : MonoBehaviour, IDropHandler {
    
    public TeamColor summonColorTag;

    public void OnDrop(PointerEventData eventData) {
        if (GameManager.Instance.IsGameOver()) return;
        if (eventData.pointerDrag != null) {
            DragUnit draggedCard = eventData.pointerDrag.GetComponent<DragUnit>();
            
            if (draggedCard != null) {
                
                string teamTag = draggedCard.tag;

                if (teamTag != summonColorTag.ToString()) {
                    return;
                }
                
                string unitId = draggedCard.GetUnitID();

                if (!GameManager.Instance.CanSpendMana(teamTag, UnitDatabase.Instance.GetUnitCostFromId(unitId))) 
                    return;
                
                GameManager.Instance.SpendMana(teamTag, UnitDatabase.Instance.GetUnitCostFromId(unitId));
                
                string enemyTag = GetOpposingColorTagByTag(teamTag);
                string enemyBuildingTag = GetOpposingBuildingColorTagByTag(teamTag);
                Color teamColor = GetTeamColorByTag(teamTag);
                
                Vector3 screenPos = eventData.position;
                screenPos.z = Mathf.Abs(Camera.main.transform.position.z); 
                Vector3 worldSpawnPosition = Camera.main.ScreenToWorldPoint(screenPos);
                worldSpawnPosition.z = 0f;

                GameObject prefabToSpawn = UnitDatabase.Instance.GetUnitPrefab(unitId);
                
                if (prefabToSpawn != null) {
                    GameObject newUnit = Instantiate(prefabToSpawn, worldSpawnPosition, Quaternion.identity);
                    newUnit.tag = teamTag;

                    CharactersAI ai = newUnit.GetComponent<CharactersAI>();
                    if (ai != null) {
                        ai.InitializeTags(enemyBuildingTag, enemyTag);
                    }
                    
                    EntityStats stats = newUnit.GetComponent<EntityStats>();
                    if (stats != null && stats.healthBarFill != null) {
                        stats.healthBarFill.color = teamColor;
                    }

                }
            }
        }
    }

    private string GetOpposingColorTagByTag(string teamTag) {
        return teamTag == nameof(TeamColor.BlueTeam)
            ? nameof(TeamColor.RedTeam)
            : nameof(TeamColor.BlueTeam);
    }
    private string GetOpposingBuildingColorTagByTag(string teamTag) {
        return teamTag == nameof(TeamColor.BlueTeam)
            ? nameof(BuildingColor.RedBuilding)
            : nameof(BuildingColor.BlueBuilding);
    }

    private Color GetTeamColorByTag(string teamTag) {
        return teamTag == nameof(TeamColor.BlueTeam)
            ? Color.cyan
            : Color.red;
    }
}