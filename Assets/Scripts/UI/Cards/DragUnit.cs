using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUnit : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
    [SerializeField] private Canvas canvas;
    
    public string unitToSummonID;
    public Image summonZone;
    
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private CanvasGroup canvasGroup;
    private Image cardImage;
    private Color originalColor;
    private Transform originalParent;
    [SerializeField] private Transform canvasTransform;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        cardImage = GetComponent<Image>();

        originalPosition = rectTransform.anchoredPosition;
        if (cardImage != null) {
            originalColor = cardImage.color;
        }
        
        if (canvasTransform == null) {
            canvasTransform = GetComponentInParent<Canvas>().transform;
        }
    }

    public void SetUnitID(string unitID) {
        unitToSummonID = unitID;
    }

    public string GetUnitID() {
        return unitToSummonID;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        rectTransform.anchoredPosition = originalPosition;
        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = originalPosition;
        if (cardImage != null) {
            cardImage.color = originalColor;
        }
    }
    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        originalParent = transform.parent;
        transform.SetParent(canvasTransform);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData) {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)rectTransform.parent,
            eventData.position, 
            eventData.pressEventCamera, 
            out Vector2 localPointerPosition)) {
            rectTransform.anchoredPosition = localPointerPosition;
        }

        if (summonZone != null && cardImage != null) {
            bool isOverZone = RectTransformUtility.RectangleContainsScreenPoint(
                summonZone.rectTransform,
                eventData.position, 
                eventData.pressEventCamera
            );

            if (isOverZone) {
                cardImage.color = new Color(0.5f, 1f, 0.5f); 
            } else {
                cardImage.color = new Color(1f, 0.5f, 0.5f); 
            }
        }
    }
}