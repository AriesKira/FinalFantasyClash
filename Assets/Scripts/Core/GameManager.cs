using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core {
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public List<EntityStats> blueTeamUnits = new List<EntityStats>();
        public List<EntityStats> redTeamUnits = new List<EntityStats>();
    
        [Header("Mana Settings")]
        public ManaBarManager blueManaBar;
        public ManaBarManager redManaBar;
        public float manaRegenRate = 1.0f;
    
        [Header("UI Managers")]
        public HandManager blueHandManager;
        public HandManager redHandManager;
    
        private bool isGameOver = false;
    
        private float blueMana = 0;
        private float redMana = 0;
        private readonly int maxMana = 10;
    
        private readonly List<GameObject> availableBlueUnits = new List<GameObject>();
        private readonly List<GameObject> availableRedUnits = new List<GameObject>();
        private const string WinnerKey = "WINNER";

        private void Awake() {
            Instance = this;
        }

        private void Start() 
        {
            SoundManager.Instance.PlayBattleTheme();
            
            List<string> blueIDs = new List<string>();
            List<string> redIDs = new List<string>();

            if (AppManager.Instance != null) {
                blueIDs = AppManager.Instance.blueTeamIDs;
                redIDs = AppManager.Instance.redTeamIDs;
            } else {
                Debug.LogWarning("Test Mode : Pas d'AppManager trouvé. Listes vides.");
            }

            SetUpAvailableUnits(blueIDs, availableBlueUnits);
            SetUpAvailableUnits(redIDs, availableRedUnits);

            if (blueHandManager != null) blueHandManager.GenerateHand(blueIDs);
            if (redHandManager != null) redHandManager.GenerateHand(redIDs);
        }

        public bool IsGameOver() {
            return isGameOver;
        }


        private void SetUpAvailableUnits(List<string> unitIDs, List<GameObject> availableUnits) 
        {
            foreach (string id in unitIDs) {
                availableUnits.Add(UnitDatabase.Instance.GetUnitPrefab(id));
            }
        }
    
        private void Update()
        {
            if (isGameOver) return;
        
            RegenerateMana();
        }

        private void RegenerateMana()
        {
            if (blueMana < maxMana)
            {
                blueMana += manaRegenRate * Time.deltaTime;
                blueMana = Mathf.Min(blueMana, maxMana);
            
                if (blueManaBar != null)
                    blueManaBar.UpdateMana(blueMana, maxMana);
            }

            if (redMana < maxMana)
            {
                redMana += manaRegenRate * Time.deltaTime;
                redMana = Mathf.Min(redMana, maxMana);
                if (redManaBar != null)
                    redManaBar.UpdateMana(redMana, maxMana);
            }
        }

        public bool CanSpendMana(string teamTag, int cost)
        {
            if (teamTag == nameof(TeamColor.BlueTeam)) return blueMana >= cost;
            if (teamTag == nameof(TeamColor.RedTeam)) return redMana >= cost;
            return false;
        }

        public void SpendMana(string teamTag, int cost)
        {
            if (teamTag == nameof(TeamColor.BlueTeam))blueMana -= cost;
            else if (teamTag == nameof(TeamColor.RedTeam)) redMana -= cost;
        }

        public void RegisterUnit(EntityStats unit, string teamTag)
        {
            if (teamTag == nameof(TeamColor.BlueTeam))blueTeamUnits.Add(unit);
            else if (teamTag == nameof(TeamColor.RedTeam)) redTeamUnits.Add(unit);
        }

        public void UnregisterUnit(EntityStats unit, string teamTag)
        {
            if (teamTag == nameof(TeamColor.BlueTeam))blueTeamUnits.Remove(unit);
            else if (teamTag == nameof(TeamColor.RedTeam)) redTeamUnits.Remove(unit);
        }
        public void EndGame(string winnerTag)
        {
            if (isGameOver) return;
            SoundManager.Instance.PlayVictoryTheme();
            isGameOver = true;
            
            if (winnerTag == nameof(TeamColor.BlueTeam))
            {
                PlayEndGameSequence(blueTeamUnits, redTeamUnits);
            }
            else if (winnerTag == nameof(TeamColor.RedTeam))
            {
                PlayEndGameSequence(redTeamUnits, blueTeamUnits);
            }
            PlayerPrefs.SetString(WinnerKey, winnerTag);

            StartCoroutine(TransitionToNextScene());
        }

        private void PlayEndGameSequence(List<EntityStats> winners, List<EntityStats> losers)
        {
            foreach (EntityStats loser in new List<EntityStats>(losers))
            {
                if (loser != null)
                {
                    loser.TakeDamage(9999f);
                }
            }

            foreach (EntityStats winner in winners)
            {
                if (winner != null)
                {
                    CharactersAI ai = winner.GetComponent<CharactersAI>();
                    if (ai != null) ai.enabled = false;

                    Rigidbody2D rb = winner.GetComponent<Rigidbody2D>();
                    if (rb != null) rb.linearVelocity = Vector2.zero;

                    AnimationManager anim = winner.GetComponent<AnimationManager>();
                    if (anim != null) anim.TriggerWin();
                }
            }
        }

        private IEnumerator TransitionToNextScene()
        {
            yield return new WaitForSeconds(5f);

            SceneManager.LoadScene("EndGameScene");
        }
    }
}