using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BugGameNameSpace
{
    public class Reptile : EnemyBug
    {
        [SerializeField] private float climbSpeed = .45f;

        float sqrDistanceToTarget;
        Vector3 velocity;
        bool isClimbing;

        protected override void Start()
        {
            base.Start();
            isClimbing = true;
        }

        protected override void Update()
        {
            if (clinged)
            {
                return;
            }
            if (targetBugTransform == null)
            {
                return;
            }

            base.Update();
            if (isClimbing)
            {
                transform.Translate(transform.up * climbSpeed * Time.deltaTime);
                if (targetBugTransform.position.y - transform.position.y < 0)
                {
                    isClimbing = false;
                }
            }
            else
            {
                Vector3 displacementToTarget = (targetBugTransform.position - transform.position);
                Vector3 directionToTarget = displacementToTarget.normalized;
                velocity = directionToTarget * bugSpeed;

                sqrDistanceToTarget = displacementToTarget.sqrMagnitude;

                if (sqrDistanceToTarget > Mathf.Pow(myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2, 2))
                {
                    transform.Translate(velocity * Time.deltaTime);
                }
            }

        }
    }

}