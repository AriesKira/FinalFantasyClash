using UnityEngine;
using System.Collections.Generic;
using Enums;
using UI.Cards;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform handContainer;
    [SerializeField] private Image teamSummonZone;
    [SerializeField] private TeamColor teamColor; 

    public void GenerateHand(List<string> unitIDs)
    {
        foreach (Transform child in handContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (string id in unitIDs)
        {
            GameObject newCard = Instantiate(cardPrefab, handContainer);

            CardDisplay display = newCard.GetComponent<CardDisplay>();
            if (display != null)
            {
                display.SetupCard(id,teamSummonZone, teamColor.ToString());
            }
        }
    }
}