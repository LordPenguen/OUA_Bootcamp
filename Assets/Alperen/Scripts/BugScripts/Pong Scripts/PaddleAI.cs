using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BugGameNameSpace
{
    public class PaddleAI : Paddle
    {
        Transform ballT;

        void Start()
        {
            ballT = FindObjectOfType<Ball>().transform;
        }

        void Update()
        {
            Vector3 targetPosition = new Vector3(transform.position.x, ballT.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
}
