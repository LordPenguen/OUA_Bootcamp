using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BugGameNameSpace
{
    public class Web : MonoBehaviour
    {
        [SerializeField] private LayerMask collisionMask;
        [SerializeField] private Transform webMuzzle;
        [SerializeField] private ParticleSystem bloodEffect;
        public event System.Action OnTargetDeath;
        float clingTime = .60f;
        Camera viewCamera;
        bool isClinged = false;
        int damage = 1;
        TrailObject trailObject;

        void Start()
        {
            viewCamera = Camera.main;
            trailObject = transform.GetChild(0).GetComponent<TrailObject>();
            //trailObject.gameObject.SetActive(false);
        }

        public bool ShootWeb(float essentialClingTime)
        {
            if (isClinged)
                return true;
            clingTime = essentialClingTime;
            Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            trailObject.gameObject.SetActive(true);

            if (Physics.Raycast(ray, out hit, 100, collisionMask))
            {
                trailObject.MoveTowardsPosition(webMuzzle.position, hit.point, .4f);
                //print(hit.collider.name);
                //Debug.DrawLine(transform.position, hit.point, Color.red);
                Vector3 dir = ray.GetPoint(100) - transform.position;
                Debug.DrawRay(transform.position, dir * 100f, Color.red);
                EnemyBug enemyBug = hit.collider.GetComponent<EnemyBug>();

                if (enemyBug != null && !isClinged)
                {
                    enemyBug.clinged = true;
                    //playerBug.isClinging = true;
                    StartCoroutine(PullBug(enemyBug));
                    if (OnTargetDeath != null)
                    {
                        OnTargetDeath();
                    }
                    return true;
                }
            }
            //trailObject.gameObject.SetActive(false);
            return false;
        }

        IEnumerator PullBug(EnemyBug enemyBug)
        {
            yield return new WaitForSeconds(.25f);
            trailObject.MoveTowardsPosition(enemyBug.transform.position, webMuzzle.transform.position, .35f);
            isClinged = true;
            float speed = 1 / clingTime;
            //float speed = 3;
            float percent = 0;
            Vector3 startPos = enemyBug.transform.position;
            while (percent < 1)
            {
                percent += Time.deltaTime * speed;
                if (enemyBug != null)
                {
                    enemyBug.transform.position = Vector3.Lerp(startPos, transform.position, percent);
                }
                yield return null;
            }
            bloodEffect.Play();
            isClinged = false;
            enemyBug.TakeBite(damage);
        }
    }
}