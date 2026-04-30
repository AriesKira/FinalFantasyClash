using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ManaBarManager : MonoBehaviour {
    public Color fullColor = Color.cyan;
    public Color emptyColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    public TextMeshProUGUI manaText;

    private List<Image> segments = new List<Image>();

    void Awake() {
        foreach (Transform segment in transform) {
            if (segment.childCount > 0) {
                Image img = segment.GetChild(0).GetComponent<Image>();
                if (img != null) {
                    segments.Add(img);
                    Image parentImg = segment.GetComponent<Image>();
                    if(parentImg == null) {
                        parentImg = segment.gameObject.AddComponent<Image>();
                    }
                    parentImg.color = emptyColor;
                }
            }
        }
    }

    public void UpdateMana(float currentMana, int maxMana) {
        int currentManaInt = Mathf.FloorToInt(currentMana);
        if (manaText != null) {
            manaText.text = $"{currentManaInt} / {maxMana}";
        }

        for (int i = 0; i < segments.Count; i++) {
            if (i < currentManaInt) {
                segments[i].color = fullColor;
                segments[i].fillAmount = 1f;
            }
            else if (i == currentManaInt) {
                segments[i].color = fullColor;
                segments[i].fillAmount = currentMana % 1f;
            }
            else {
                segments[i].fillAmount = 0f; 
            }
        }
    }
}