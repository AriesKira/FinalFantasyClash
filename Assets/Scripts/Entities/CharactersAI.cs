using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Pathfinding;

public class CharactersAI : MonoBehaviour
{
    
    public float speed = 5f;
    public float attackRange = 1.5f;
    
    private string objectivesTag;
    private string ennemyTag;
    private List<Transform> targets;
    private float nextWaypointDistance = 0.1f;
    private Transform target;
    private Animator anim;
    private AnimationManager animationManager;
    private Path path;
    private int currentWaypoint = 0;
    private bool isAttacking = false;
    private Seeker seeker;
    private Rigidbody2D characterBody;
    private EntityStats characterStats;
    private CircleCollider2D aggroZone;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        characterBody = GetComponent<Rigidbody2D>();
        characterStats = GetComponent<EntityStats>();
        anim = GetComponentInChildren<Animator>();
        animationManager = GetComponent<AnimationManager>();
        aggroZone = GetComponent<CircleCollider2D>();

        GameObject[] enemyBuildings = GameObject.FindGameObjectsWithTag(objectivesTag);

        targets = new List<Transform>();

        for (int i = 0; i < enemyBuildings.Length; i++)
        {
            targets.Add(enemyBuildings[i].transform);
        }

        if (characterStats.characterType == CharacterType.RANGED) {
            attackRange = aggroZone.radius;
        }

        target = getClosestTarget();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    public void InitializeTags(string objectivesTag, string ennemyTag) {
        this.objectivesTag = objectivesTag;
        this.ennemyTag = ennemyTag;
    } 
    private Transform getClosestTarget()
    {
        if (targets == null || targets.Count == 0) return null;

        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (Transform t in targets)
        {
            if (t == null) continue;

            float dist = Vector2.Distance(characterBody.position, t.position);

            if (dist < minDist)
            {
                minDist = dist;
                closest = t;
            }
        }
        return closest;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ennemyTag))
        {
            if (!targets.Contains(other.transform))
            {
                targets.Add(other.transform);
            }
        }
    }
    void UpdatePath()
    {
        target = getClosestTarget();
        if (target == null) return;

        if (seeker.IsDone() && !isAttacking)
        {
            seeker.StartPath(characterBody.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;
        float distanceToTarget = Vector2.Distance(characterBody.position, target.position);

        if (distanceToTarget <= attackRange)
        {
            isAttacking = true;
            characterBody.linearVelocity = Vector2.zero;
            characterStats.Attack(target);
            return;
        }
        else
        {
            isAttacking = false;
        }

        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            characterBody.linearVelocity = Vector2.zero;
            if (animationManager != null) animationManager.SetMoving(false);
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - characterBody.position).normalized;
        characterBody.linearVelocity = direction * speed;

        if (animationManager != null)
        {
            animationManager.SetMoving(true);
        }

        float distanceToWaypoint = Vector2.Distance(characterBody.position, path.vectorPath[currentWaypoint]);
        if (distanceToWaypoint < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void Update()
    {
        if (!isAttacking)
        {
            if (animationManager != null)
            {
                animationManager.UpdateOrientation(characterBody.linearVelocity.x);
            }
        }
        else if (target != null)
        {
            float distanceX = target.position.x - characterBody.position.x;

            if (animationManager != null)
            {
                animationManager.UpdateOrientation(distanceX);
            }
        }
    }
}