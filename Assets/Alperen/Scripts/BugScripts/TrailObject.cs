using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailObject : MonoBehaviour
{
    TrailRenderer trail;
    IEnumerator currentCoroutine;
    float trailTime;
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        trailTime = trail.time;
    }

    public void MoveTowardsPosition(Vector3 from, Vector3 to, float time)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine =  MoveTowardsCoroutine(from, to, time);
        StartCoroutine(currentCoroutine);
    }

    IEnumerator MoveTowardsCoroutine(Vector3 from, Vector3 to, float time)
    {
        trail.time = trailTime;
        trail.Clear();
        transform.position = from;
        float speed = 1 / time;
        float percent = 0;
        while (percent <= 1)
        {
            percent += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(from, to, percent);
            yield return null;
        }
        trail.time = -1;
        trail.Clear();
    }
}
