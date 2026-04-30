using UnityEngine;
using UnityEngine.UI;
public class MainMenuBackground : MonoBehaviour {
    
    public Sprite[] availableBackgrounds;

    private Image backgroundImage;

    void Start() {

        backgroundImage = GetComponent<Image>();

        if (availableBackgrounds.Length > 0) {

            int randomIndex = Random.Range(0, availableBackgrounds.Length);

            backgroundImage.sprite = availableBackgrounds[randomIndex];
        } else {
            Debug.LogWarning("Attention : Aucune image n'a été assignée au script !");
        }
    }

}
