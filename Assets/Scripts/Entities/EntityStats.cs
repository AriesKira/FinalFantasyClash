using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Core;
using Enums;

public class EntityStats : MonoBehaviour {
    public float maxHealth = 100f;
    public float damage = 20f;
    public float attackCooldown = 1f;
    public int cost = 2;
    public Image healthBarFill;
    public int numberOfHits;
    public CharacterType characterType;
    public AttackType attackType;
    public float aoeRadius = 0f;
    [Header("Sons")]
    public AudioClip attackSound;
    
    private float currentHealth;
    private float nextAttackTime = 0f;
    private AnimationManager animationManager;
    private bool isDead = false;
    private EntityStats currentTarget;

    void Start() {
        currentHealth = maxHealth;
        animationManager = GetComponent<AnimationManager>();
        GameManager.Instance.RegisterUnit(this, gameObject.tag);
    }

    public int GetCost() {
        return cost;
    }

    public void Attack(Transform targetTransform) {
        if (Time.time >= nextAttackTime && !isDead) {
            currentTarget = targetTransform.GetComponent<EntityStats>();

            if (currentTarget != null && !currentTarget.isDead) {
                if (animationManager != null) animationManager.TriggerAttack();
            }

            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void ExecuteAttackHit() {
        if (this.isDead) return;

        float damagePerHit = damage / numberOfHits;

        if (attackType == AttackType.SingleTarget) {
            if (currentTarget != null && !currentTarget.isDead) {
                if (attackSound != null) SoundManager.Instance.PlaySFX(attackSound);
                currentTarget.TakeDamage(damagePerHit);
            }
        }
        else if (attackType == AttackType.MultiTarget) {
            if (currentTarget == null) return;
            if (attackSound != null) SoundManager.Instance.PlaySFX(attackSound);
            Vector2 impactPoint = currentTarget.transform.position;

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(impactPoint, aoeRadius);

            foreach (Collider2D hit in hitColliders) {
                EntityStats hitStats = hit.GetComponent<EntityStats>();
                if (hitStats != null && !hitStats.isDead && !hit.gameObject.CompareTag(this.gameObject.tag)) {
                    hitStats.TakeDamage(damagePerHit);
                }
            }
        }
    }
    
    public void TakeDamage(float amount) {
        if (isDead) return;
        currentHealth -= amount;

        if (healthBarFill != null) {
            healthBarFill.fillAmount = getCurrentHealthPercent();
        }

        if (currentHealth <= 0) {
            isDead = true;
            if (healthBarFill != null) healthBarFill.transform.parent.gameObject.SetActive(false);
            Die();
        }
    }

    public float getCurrentHealthPercent() {
        return currentHealth / maxHealth;
    }

    void Die() {
        string originalTag = gameObject.tag;

        GameManager.Instance.UnregisterUnit(this, originalTag);

        if (animationManager != null) animationManager.TriggerDeath();

        CharactersAI ai = GetComponent<CharactersAI>();
        if (ai != null) {
            if (animationManager != null) {
                GameObject corpse = animationManager.spriteTransform.gameObject;

                corpse.transform.SetParent(null);

                Destroy(corpse, 5f);
            }

            Destroy(gameObject);
            return;
        }


        Castle castle = GetComponent<Castle>();
        if (castle != null) {
            string winnerTag = (originalTag == nameof(BuildingColor.BlueBuilding))
                ? nameof(TeamColor.RedTeam)
                : nameof(TeamColor.BlueTeam);
            GameManager.Instance.EndGame(winnerTag);
        }

        if (animationManager != null) animationManager.spriteTransform.SetParent(null);
        Destroy(gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (attackType == AttackType.MultiTarget)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, aoeRadius);
        }
    }
}