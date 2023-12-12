using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject EnemyPrefab;

    [SerializeField]
    public float startCooldown = 3.0f;

    private float cooldown;
    public float startDelay;

    public float time;
    public float startTimer;

    private void Awake()
    {
        RandomizeTimer();
        cooldown = startCooldown;
    }

    private void Update()
    {
        startTimer += Time.deltaTime;
        if (startTimer < startDelay) return;

        time += Time.deltaTime;
        cooldown = startCooldown - (Ressources.Difficulty * 0.1f);

        if (time >= cooldown)
        {
            time = 0;
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        var spawnedEnemy = Instantiate(EnemyPrefab);

        var pos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        spawnedEnemy.GetComponent<NavMeshAgent>().Warp(pos);
    }

    private void RandomizeTimer()
    {
        float randomTime = Random.Range(0.0f, cooldown);

        time = randomTime;
    }
}
