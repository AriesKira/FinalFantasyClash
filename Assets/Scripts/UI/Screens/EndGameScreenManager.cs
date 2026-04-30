using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EndGameScreenManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Sprite blueTeam; 
    [SerializeField] private Sprite redTeam;
    [SerializeField] private Image background;
    
    private const string WinnerKey = "WINNER";
    void Start()
    {
        string winnerTeam = PlayerPrefs.GetString(WinnerKey);
        winnerText.text = winnerTeam + " WINS";
        if (background != null) {
            if (winnerTeam == "BlueTeam") {
                background.sprite = blueTeam;
                winnerText.color = Color.black;
                gameOverText.color = Color.black;
            }
            else {
                background.sprite = redTeam;
                winnerText.color = Color.white;
                gameOverText.color = Color.white;
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("TeamSelectScene");
    }
}
