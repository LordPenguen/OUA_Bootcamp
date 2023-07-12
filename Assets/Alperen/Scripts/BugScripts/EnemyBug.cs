using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BugGameNameSpace
{
    public class EnemyBug : Bug
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
        protected float damage = 1;

        PlayerBug playerBug;
        Material material;


        protected override void Start()
        {
            base.Start();
            if (GameObject.FindObjectOfType<PlayerBug>() != null)
            {
                playerBug = GameObject.FindObjectOfType<PlayerBug>();
                playerBug.OnDeath += OnTargetDeath;
                targetBugTransform = playerBug.transform;
                targetAlive = !playerBug.GetDeadSituation();
                targetCollisionRadius = targetBugTransform.localScale.x / 2;
            }
            myCollisionRadius = GetComponent<Transform>().localScale.x / 2;
            material = GetComponent<Renderer>().material;
            defaultColor = material.color;
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

        public override void TakeBite(float damage)
        {
            // particle effect and stuff...
            base.TakeBite(damage);
        }

        IEnumerator Attack()
        {
            currentState = State.Attack;
            Vector3 startPosition = transform.position;
            Vector3 directionToTarget = (targetBugTransform.position - startPosition).normalized;
            Vector3 attackPosition = targetBugTransform.position - directionToTarget * (myCollisionRadius);
            material.color = attackColor;
            bool hasAppliedDamage = false;
            float percent = 0;

            while (percent <= 1)
            {
                if (percent >= .5f && !hasAppliedDamage)
                {
                    hasAppliedDamage = true;
                    playerBug.TakeBite(damage);
                }
                percent += Time.deltaTime * attackSpeed;
                float interpolation = 4 * (-percent * percent + percent);
                transform.position = Vector3.Lerp(startPosition, attackPosition, interpolation);


                yield return null;
            }
            currentState = State.Chasing;
            material.color = defaultColor;
        }

        void OnTargetDeath()
        {
            print("player is dead");
            material.color = Color.green;
            targetAlive = false;
        }

        protected override void Die()
        {
            base.Die();
            GameObject.Destroy(gameObject);
        }
    }
}