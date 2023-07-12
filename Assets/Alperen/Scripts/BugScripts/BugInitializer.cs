using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BugGameNameSpace
{
    public class BugInitializer : MonoBehaviour
    {
        [Header("Instantiate Objects")]
        [SerializeField] private GameObject reptileSpawner;
        [SerializeField] private GameObject flySpawner;
        [SerializeField] private GameObject player;
        [SerializeField] private Transform targetCameraPosition;

        Camera mainCamera;

        [Header("Camera Movement Values")]
        [SerializeField] private float movementSeconds = 1;
        [SerializeField] private float screenWaitSeconds = 2f;

        private void Start()
        {
            mainCamera = Camera.main;
            FindObjectOfType<PongGameManager>().OnGameChange += InitializeBugGame;
            //StartCoroutine(MoveCamera());
            //Init();
        }

        //private void Update()
        //{
        //    if (Input.GetMouseButton(1))
        //    {
        //        StartCoroutine(MoveCamera());
        //    }
        //}

        [ContextMenu("StartGame")]
        public void Init()
        {
            string holderName = "Spawners";
            if (transform.Find(holderName))
            {
                DestroyImmediate(transform.Find(holderName).gameObject);
            }
            Transform spawnerHolder = new GameObject(holderName).transform;
            spawnerHolder.parent = transform;

            Instantiate(player, transform.position, transform.rotation, spawnerHolder);
            Instantiate(reptileSpawner, transform.position, transform.rotation, spawnerHolder);
            Instantiate(flySpawner, transform.position, transform.rotation, spawnerHolder);
        }

        IEnumerator MoveCamera()
        {
            float moveSpeed = 1 / movementSeconds;
            float percent = 0;
            Vector3 startPosition = mainCamera.transform.position;
            Quaternion startRotation = mainCamera.transform.rotation;
            Vector3 targetPosition = targetCameraPosition.position;
            Quaternion targetRotation = targetCameraPosition.rotation;

            yield return new WaitForSeconds(screenWaitSeconds);

            while (percent <= 1)
            {
                percent += moveSpeed * Time.deltaTime;
                mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, percent);
                mainCamera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, percent);
                yield return null;
            }
            Init();
        }

        void InitializeBugGame()
        {
            StartCoroutine(MoveCamera());
        }
    }
}