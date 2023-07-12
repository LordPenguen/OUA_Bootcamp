using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BugGameNameSpace
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField] protected float speed = 1;
        float playerHalfLength;

        void Start()
        {
            playerHalfLength = transform.localScale.y / 2;
        }

        void Update()
        {
            // movement
            transform.Translate(Vector3.up * Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);

            // check top and bottom boundries
            if (transform.position.y < PongGameManager.bottomLeftPos.y + playerHalfLength)
            {
                transform.position = new Vector3(transform.position.x, PongGameManager.bottomLeftPos.y + playerHalfLength, transform.position.z);
            }
            if (transform.position.y > PongGameManager.topLeftPos.y - playerHalfLength)
            {
                transform.position = new Vector3(transform.position.x, PongGameManager.topLeftPos.y - playerHalfLength, transform.position.z);
            }
        }
    }
}
