using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PacmanGame
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Ghost : MonoBehaviour
    {
        public float refreshRate = 1f;
        public float delayTime = 3f;

        NavMeshAgent navMeshAI;
        Transform playerTransform;
        bool playerCaptured;

        void Start()
        {
            navMeshAI = GetComponent<NavMeshAgent>();
            playerTransform = FindObjectOfType<PacmanPlayer>().transform;
            //print(name + "refresh rate = " + refreshRate + " - delay time" + delayTime);
            //delayTime = 10 - refreshRate;
            ResetGhost();
        }

        private IEnumerator FollowPlayer()
        {
            while (!playerCaptured)
            {
                navMeshAI.SetDestination(playerTransform.position);
                yield return new WaitForSeconds(refreshRate);       
            }
        }

        public void OnPlayerCaptured()
        {
            StopAllCoroutines();
            CancelInvoke();
            playerCaptured = true;
            navMeshAI.isStopped = true;
        }

        public void ResetGhost()
        {
            Invoke("DelayedResetGhost", delayTime);
        }

        private void DelayedResetGhost()
        {
            playerCaptured = false;
            navMeshAI.isStopped = false;
            StartCoroutine(FollowPlayer());
        }
    }
}
