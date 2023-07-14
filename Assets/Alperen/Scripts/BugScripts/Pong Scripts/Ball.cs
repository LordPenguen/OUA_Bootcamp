using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BugGameNameSpace
{
    public class Ball : MonoBehaviour
    {
        public event System.Action <bool> OnScored;

        //[SerializeField] private Vector2 speedMinMax= new Vector2(.45f, 1.3f);
        //        float secondsToMaxSpeed = 10;
        [SerializeField] private float speed = .6f;
        [SerializeField] private Vector2 minMaxSpeed = new Vector2(.6f, 1.6f);
        float radius;
        Vector3 direction;
        bool scored;

        void Start()
        {
            BallInitialize();
            radius = transform.localScale.x / 2;
        }

        void Update()
        {
            //float speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Utility.GetDifficultyPercent());

            // movement
            transform.Translate(direction * speed * Time.deltaTime);

            // check top and left boundries
            if (transform.position.y < PongGameManager.bottomLeftPos.y + radius && direction.y < 0)
            {
                direction.y *= -1;
            }

            else if (transform.position.y > PongGameManager.topRightPos.y - radius && direction.y > 0)
            {
                direction.y *= -1;
            }

            // scoring AI
            else if (transform.position.x < PongGameManager.topLeftPos.x + radius && direction.x < 0)
            {
                Score(false);
            }
            // scoring player
            else if (transform.position.x > PongGameManager.topRightPos.x - radius && direction.x > 0)
            {
                Score(true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!scored)
            {
                Paddle paddle = other. GetComponent<Paddle>();
                if (paddle != null)
                {
                    direction.x *= -1;
                    direction.y = Random.Range(-1f, 1f);

                    if (speed <= minMaxSpeed.y)
                    {
                        speed += speed * .12f;
                        print("new speed is = " + speed);
                    }                
                }
            }
        }

        void Score(bool isPlayerScored)
        {
            scored = true;
            direction = Vector3.zero;

            if (OnScored != null)
            {
                OnScored(isPlayerScored);
            }
        }

        public void BallInitialize()
        {
            direction = Vector3.right;
            speed = minMaxSpeed.x;
            scored = false;
        }
    }
}
