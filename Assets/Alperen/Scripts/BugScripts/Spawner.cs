using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BugGameNameSpace
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Wave[] waves;

        [SerializeField] private int maxEnemyCount;
        [SerializeField] private float secondsToMaxDifficulty = 60;

        [Header ("Models")]
        [SerializeField] private EnemyBug enemyBug;

        [Header("Positions")]
        [SerializeField] private Transform enemySpawnTranformParent;
        [SerializeField] private Transform[] enemyBugSpawnTranforms;

        Wave currentWave;
        int currentWaveIndex;
        int enemiesRemainingToSpawn;
        float nextSpawnTime;
        int enemiesRemainingAlive = 0;
        Vector2 timeBetweenSpawnsMinMax;

        float gameStartTime;

        private void Start()
        {
            gameStartTime = Time.time;
            enemyBugSpawnTranforms = new Transform[enemySpawnTranformParent.childCount];

            for (int i = 0; i < enemyBugSpawnTranforms.Length; i++)
            {
                enemyBugSpawnTranforms[i] = enemySpawnTranformParent.GetChild(i).transform;
            }

            NextWave();
        }

        private void Update()
        {
            if (enemiesRemainingAlive < maxEnemyCount)
            {
                if ((enemiesRemainingToSpawn > 0 || currentWave.infinite) && Time.time > nextSpawnTime)
                {
                    float timeBetweenSpawns = Mathf.Lerp(timeBetweenSpawnsMinMax.y, timeBetweenSpawnsMinMax.x, GetDifficultyPercent());
                    nextSpawnTime = Time.time + timeBetweenSpawns;
                    int spawnTransformIndex = Random.Range(0, enemyBugSpawnTranforms.Length);
                    EnemyBug newBug = Instantiate(enemyBug, enemyBugSpawnTranforms[spawnTransformIndex].position, Quaternion.identity) as EnemyBug;
                    newBug.OnDeath += EnemyDeath;
                    enemiesRemainingAlive++;
                    enemiesRemainingToSpawn--;
                }
            }
        }

        void NextWave()
        {
            if (currentWaveIndex+1 <= waves.Length)
            {
                currentWaveIndex++;
                currentWave = waves[currentWaveIndex - 1];
                enemiesRemainingToSpawn = currentWave.enemyCount;
                timeBetweenSpawnsMinMax = currentWave.timeBetweenSpawnsMinMax;
            }
        }

        void EnemyDeath()
        {
            enemiesRemainingAlive--;
        }

        float GetDifficultyPercent()
        {
            return Mathf.Clamp01(gameStartTime / secondsToMaxDifficulty);
        }

        [System.Serializable]
        public class Wave
        {
            public bool infinite;
            public int enemyCount;
            public Vector2 timeBetweenSpawnsMinMax;
            public Color enemySkinColor;
        }
    }

}