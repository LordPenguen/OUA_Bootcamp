using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BugGameNameSpace
{
    public class Web : MonoBehaviour
    {
        [SerializeField] private LayerMask collisionMask;
        float clingTime = .60f;
        Camera viewCamera;
        bool isClinged = false;
        float damage = 1;

        void Start()
        {
            viewCamera = Camera.main;
        }

        public bool ShootWeb(float essentialClingTime)
        {
            if (isClinged)
                return true;
            clingTime = essentialClingTime;
            Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, collisionMask))
            {
                //print(hit.collider.name);
                //Debug.DrawLine(transform.position, hit.point, Color.red);
                Vector3 dir = ray.GetPoint(100) - transform.position;
                Debug.DrawRay(transform.position, dir * 100f, Color.red);
                EnemyBug enemyBug = hit.collider.GetComponent<EnemyBug>();

                if (enemyBug != null && !isClinged)
                {
                    enemyBug.clinged = true;
                    print("enemy clinged = " + enemyBug.transform.name);
                    StartCoroutine(PullBug(enemyBug));
                    return true;
                }
            }
            return false;
        }

        IEnumerator PullBug(EnemyBug enemyBug)
        {
            isClinged = true;
            float speed = 1 / clingTime;
            float percent = 0;
            Vector3 startPos = enemyBug.transform.position;
            while (percent < 1)
            {
                percent += Time.deltaTime * speed;
                enemyBug.transform.position = Vector3.Lerp(startPos, transform.position, percent);
                yield return null;
            }
            isClinged = false;
            enemyBug.TakeBite(damage);
        }
    }
}