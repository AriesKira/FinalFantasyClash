using System;
using TMPro;
using UnityEngine;

public class UnitManaCost : MonoBehaviour
{
    private TextMeshProUGUI costText;

    private void Start() {
        costText = GetComponent<TextMeshProUGUI>();
    }
}
