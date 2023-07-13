using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostScript : MonoBehaviour
{
    private NavMeshAgent navMeshAI;
    private Transform playerTransform;
    private float collisionRadius;
    private float targetCollisionRadius;
    void Start()
    {
        navMeshAI = GetComponent<NavMeshAgent>();
        playerTransform = FindObjectOfType<PacmanPlayer>().transform;
        collisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = playerTransform.GetComponent<SphereCollider>().radius;
        StartCoroutine(FollowPlayer());
    }
    

    private IEnumerator FollowPlayer()
    {
        float refreshRate = 0.20f;          // saniyede 4 defa calisacak
        while (playerTransform != null)
        {
            Vector3 directionToTarget = (playerTransform.position - transform.position).normalized;
            Vector3 targetPosition =playerTransform.position - directionToTarget * (collisionRadius + targetCollisionRadius);
            navMeshAI.SetDestination(targetPosition);
            yield return new WaitForSeconds(refreshRate);       
        }
    }
}
