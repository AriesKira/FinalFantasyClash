using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.HUD {
    public class SlotDropHandler : MonoBehaviour, IDropHandler {
        
        public void OnDrop(PointerEventData eventData) {
            if (eventData.pointerDrag != null) {
                UnitSelectorManager draggedCard = eventData.pointerDrag.GetComponent<UnitSelectorManager>();
                
                if (draggedCard != null) {
                    if (transform.CompareTag(draggedCard.tag)) {
                        
                        if (transform.childCount > 0) {
                            Transform existingCardTransform = transform.GetChild(0);
                            UnitSelectorManager existingCard = existingCardTransform.GetComponent<UnitSelectorManager>();
                            
                            if (existingCard != null) {
                                existingCard.transform.SetParent(existingCard.GetScrollParent());
                            }
                        }
                        
                        draggedCard.transform.SetParent(transform);
                    
                        RectTransform rt = draggedCard.GetComponent<RectTransform>();
                    
                        rt.anchorMin = new Vector2(0.5f, 0.5f);
                        rt.anchorMax = new Vector2(0.5f, 0.5f);
                        rt.pivot = new Vector2(0.5f, 0.5f);
                    
                        rt.anchoredPosition = Vector2.zero;
                    }
                }
            }
        }
        
    }
}