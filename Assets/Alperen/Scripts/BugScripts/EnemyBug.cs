using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBug : MonoBehaviour
{
    public enum State { Idle, Chasing, Attack };
    public State currentState;

    [SerializeField] protected float bugSpeed = 1;
    [SerializeField] protected float attackDistanceThreshold = .3f;
    [SerializeField] protected float timeBetweenAttacks = 2;
    [SerializeField] protected float attackSpeed = 3;
    [SerializeField]protected bool targetAlive;
    public bool clinged = false;

    [SerializeField] private Color attackColor = Color.red;
    Color defaultColor;

    protected float nextAttackTime;
    protected float myCollisionRadius;
    protected float targetCollisionRadius;
    protected Transform targetBugTransform;


    Material material;


    protected virtual void Start()
    {
        targetBugTransform = FindObjectOfType<PlayerBug>().transform;
        myCollisionRadius = GetComponent<Transform>().localScale.x / 2;
        targetCollisionRadius = targetBugTransform.localScale.x / 2;
        material = GetComponent<Renderer>().material;
        defaultColor = material.color;
        targetAlive = true;
    }

    protected virtual void Update()
    {
        if (targetAlive)
        {
            if (Time.time > nextAttackTime)
            {
                float sqrDistanceToTarget = (targetBugTransform.position - transform.position).sqrMagnitude;
                if (sqrDistanceToTarget < Mathf.Pow(attackDistanceThreshold, 2))
                {
                    nextAttackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            targetAlive = false;
            currentState = State.Idle;
        }
    }

    IEnumerator Attack()
    {
        currentState = State.Attack;
        Vector3 startPosition = transform.position;
        Vector3 directionToTarget = (targetBugTransform.position - startPosition).normalized;
        Vector3 attackPosition = targetBugTransform.position - directionToTarget * (myCollisionRadius);
        material.color = attackColor;

        float percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = 4 * (-percent * percent + percent);
            transform.position = Vector3.Lerp(startPosition, attackPosition, interpolation);
            yield return null;
        }
        currentState = State.Chasing;
        material.color = defaultColor;
    }
}
