using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SmartSpawner : MonoBehaviour
{
    public float targetEnemies = 100;

    private float averageEnemies = 0;

    public float maxBoostPercent = 5;

    private void Start()
    {
        StartCoroutine(UpdateAverageEnemies());
    }

    private void Update()
    {
        float difference = targetEnemies - averageEnemies;

        foreach(var spawner in GetComponentsInChildren<EnemySpawner>())
        {
            spawner.time += spawner.startCooldown * ((difference / targetEnemies) * (maxBoostPercent / 100)); 
        }
    }

    IEnumerator UpdateAverageEnemies()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            var count = FindObjectsOfType<Enemy>().Length;
            averageEnemies = count;
        }
    }
}
