using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region FIELDS

    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;

    private const int MELEE_COUNT = 3;
    private const int RANGED_COUNT = 3;
    private const float SPAWN_DELAY = 2f;

    #endregion FIELDS

    #region UNITY METHODS

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    #endregion UNITY METHODS

    #region METHODS

    private IEnumerator SpawnEnemies()
    {
        yield return StartCoroutine(SpawnWithDelay(meleeEnemyPrefab, MELEE_COUNT));
        yield return StartCoroutine(SpawnWithDelay(rangedEnemyPrefab, RANGED_COUNT));
    }

    private IEnumerator SpawnWithDelay(GameObject enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy(enemyPrefab);
            yield return new WaitForSeconds(SPAWN_DELAY);
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        EnemyController enemyController = enemyInstance.GetComponent<EnemyController>();

        if (enemyController)
        {
            enemyController.patrolPoints = GetPatrolPoints();
        }
    }

    private Transform[] GetPatrolPoints()
    {
        Transform[] points = new Transform[100];
        int count = 0;

        for (int i = 1; i <= 100; i++)
        {
            Transform point = transform.Find($"SpawnPoint{transform.GetSiblingIndex() + 1}PP{i}");
            if (point)
            {
                points[count] = point;
                count++;
            }
            else
            {
                break;
            }
        }

        System.Array.Resize(ref points, count);
        //randomize the order of the points
        for (int i = 0; i < points.Length; i++)
        {
            Transform temp = points[i];
            int randomIndex = Random.Range(i, points.Length);
            points[i] = points[randomIndex];
            points[randomIndex] = temp;
        }
        return points;
    }

    #endregion METHODS
}