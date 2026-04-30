using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.HUD {
    public class UnitSelectorManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
        public String unitId { get; private set; }
        
        [SerializeField] private Canvas canvas;
        [SerializeField] private Transform scrollParent;
        [SerializeField] private TextMeshProUGUI textBox;
        [SerializeField] private Image image;
        
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        private void Awake() {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>(); 
            if (canvas == null) {
                canvas = GetComponentInParent<Canvas>();
            }
        }

        public void Setup(string id, Transform defaultScrollParent) {
            unitId = id;
            scrollParent = defaultScrollParent;

            int unitCost = UnitDatabase.Instance.GetUnitCostFromId(unitId);
            Sprite unitSprite = UnitDatabase.Instance.GetUnitPortrait(unitId);
            
            textBox.text = unitCost.ToString();
            image.sprite = unitSprite;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            transform.SetParent(canvas.transform);
            transform.SetAsLastSibling();

            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData) {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData) {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            
            if (transform.parent == canvas.transform) {
                transform.SetParent(scrollParent);
            }
        }
        
        public Transform GetScrollParent() {
            return scrollParent;
        }

        public void UnitSelected() {
            
        }
    }
}