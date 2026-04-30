using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    private AudioSource audioSource;
    private AudioSource sfxSource;
    
    [SerializeField] private AudioClip mainTheme;
    [SerializeField] private AudioClip battleTheme;
    [SerializeField] private AudioClip victoryTheme;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject); 
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start() {
        PlayMainTheme();
    }
    
    public void PlayMainTheme() {
        ChangeTheme(mainTheme);
    }

    public void PlayBattleTheme() {
        ChangeTheme(battleTheme);
    }

    public void PlayVictoryTheme() {
        ChangeTheme(victoryTheme);
    }

    private void ChangeTheme(AudioClip newTheme) {
        if (newTheme == null) return;

        if (audioSource.clip == newTheme) return;
        audioSource.Stop();
        audioSource.clip = newTheme;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    public void PlaySFX(AudioClip clip) {
        if (clip == null) return;
        
        sfxSource.PlayOneShot(clip);
    }
}