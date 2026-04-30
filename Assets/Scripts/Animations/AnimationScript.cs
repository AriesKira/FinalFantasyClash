using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private SpriteRenderer _sprite;
    public Sprite[] walkSprites;
    private int spriteIndex = 0;
    public float spriteChangeDelay = 0.1f;
    private float timer;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        timer = spriteChangeDelay;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = spriteChangeDelay;
            spriteIndex = (spriteIndex + 1) % walkSprites.Length;
            _sprite.sprite = walkSprites[spriteIndex];
        }
    }
}