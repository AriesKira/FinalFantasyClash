using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Cards {
    public class CardDisplay : MonoBehaviour
    {
        [Header("UI Elements")]
        public TextMeshProUGUI costText;

        public Image characterIcon;
    
        [Header("Logic")]
        public DragUnit dragScript;

        public void SetupCard(string unitId, Image summonZone, string teamColor) {
            int cost = UnitDatabase.Instance.GetUnitCostFromId(unitId);
            costText.text = cost.ToString();

            Sprite unitSprite = UnitDatabase.Instance.GetUnitPortrait(unitId);
            if (unitSprite != null)
            {
                characterIcon.sprite = unitSprite;
            }

            if (dragScript != null)
            {
                dragScript.SetUnitID(unitId);
                dragScript.summonZone = summonZone;
                dragScript.tag = teamColor;
            }
        }
    }
}