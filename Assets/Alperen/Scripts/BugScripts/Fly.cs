using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BugGameNameSpace
{
    public class Fly : EnemyBug
    {
        [SerializeField] private Vector2 amplitudeMinMax = new Vector2(.2f, 0.27f);
        [SerializeField] private Vector2 frequencyMinMax = new Vector2 (.5f, 1.25f);
        [SerializeField] private Vector2 sinWaveAdditionMinMax = new Vector2(-2, 1.75f);
        [SerializeField] private Vector2 speedMinMax = new Vector2(.5f,3);

        Vector3 smoothVelocity;

        [Header("Values")]
        [SerializeField] float amplitude;
        [SerializeField] float frequency;
        [SerializeField] float sinWaveAddition;
        float randomDirPercent;
        public float randomDir;

        protected override void Start()
        {
            base.Start();
            currentState = State.Chasing;
            randomDir = Random.Range(-1, 2);

            float randomPercent = Random.Range(0f, 1f);
            amplitude = randomPercent * (amplitudeMinMax.y - amplitudeMinMax.x) + amplitudeMinMax.x;
            frequency= randomPercent * (frequencyMinMax.y - frequencyMinMax.x) + frequencyMinMax.x;
            sinWaveAddition = randomPercent * (sinWaveAdditionMinMax.y - sinWaveAdditionMinMax.x) + sinWaveAdditionMinMax.x;
            bugSpeed = randomPercent * (speedMinMax.y - speedMinMax.x) + speedMinMax.x;
            randomDirPercent = randomPercent * (.25f - .05f) + .05f;
        }

        protected override void Update()
        {
            if (!clinged)
            {
                if (targetAlive)
                {
                    base.Update();

                    Vector3 dirToTarget = (targetBugTransform.position - transform.position).normalized;

                    Vector3 targetPosition = targetBugTransform.position + dirToTarget * .75f + Vector3.up * 1f;
                    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothVelocity, bugSpeed);

                    //if (currentState == State.Chasing)
                    {
                        float offsetXZ = Mathf.Sin(Time.time * frequency + sinWaveAddition) * amplitude;
                        float offsetY = (Mathf.Sin(Time.time * frequency) + .3f) * amplitude;

                        //transform.position += (transform.right * Mathf.Sin(Time.time * frequency) * amplitude) * Time.deltaTime;
                        Vector3 total = transform.right * offsetXZ;
                        total += transform.up * offsetY;
                        total += transform.forward * offsetXZ;
                        transform.position += total * Time.deltaTime;
                    }
                }
                else
                {
                    Vector3 dirToTarget = (targetBugTransform.position - transform.position).normalized;

                    Vector3 targetPosition = targetBugTransform.position - dirToTarget * .1f + Vector3.up * .05f;
                    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothVelocity, bugSpeed);

                    float offsetX = Mathf.Sin(Time.time * frequency*2 + sinWaveAddition) * amplitude;
                    float offsetZ = Mathf.Cos(Time.time * frequency*2) * amplitude;

                    transform.position += transform.right * offsetX * Time.deltaTime;
                    transform.position += transform.forward * offsetZ * Time.deltaTime;
                }
            }
        }
    }
}