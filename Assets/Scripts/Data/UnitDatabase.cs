using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitDatabase : MonoBehaviour {
    public static UnitDatabase Instance { get; private set; }
    
    [Serializable]
    public struct UnitEntry {
        public string unitID;  
        public GameObject characterPrefab;
        public Sprite cardPortrait;
    }
    
    private Dictionary<string, UnitEntry> unitsDictionary = new Dictionary<string, UnitEntry>();
    
    [SerializeField]
    private List<UnitEntry> units;
    
    private int ERROR_COST = 11;
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
            if (units != null) {
                foreach (UnitEntry entry in units) {
                    unitsDictionary[entry.unitID] = entry;
                }
            }
        }
    }

    public GameObject GetUnitPrefab(string unitId) {
        if (unitsDictionary.ContainsKey(unitId)) {
            return unitsDictionary[unitId].characterPrefab;
        }
        return null;
    }

    public Sprite GetUnitPortrait(string unitId) {
        if (unitsDictionary.ContainsKey(unitId)) {
            return unitsDictionary[unitId].cardPortrait;
        }
        return null;
    }

    public int GetUnitCostFromId(string unitId) {
        if (unitsDictionary.ContainsKey(unitId)) {
            return unitsDictionary[unitId].characterPrefab.GetComponent<EntityStats>().GetCost();
        }
        return ERROR_COST;
    }

    public List<String> GetAllUnitIds() {
        return new  List<string>(unitsDictionary.Keys);
    }
}