using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Abfall : MonoBehaviour
{
    List<Enemy> enemies = new List<Enemy>();

    public Transform bulletSpawnPoint;
    public Bullet projectile;
    public float fireCooldown_;
    private float fireCooldown = 1;

    public bool lockY = true;

    private void Update()
    {
        while (enemies.Contains(null))
        {
            enemies.Remove(null);
        }

        if (fireCooldown < 0)
        {
            Fire();
            fireCooldown = fireCooldown_;
        }

        fireCooldown -= Time.deltaTime;
    }

    void Fire()
    {
        var closest = ClosestEnemy();
        if (closest == null) return;

        var bullet = Instantiate(projectile);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.Fire(closest.transform.position - transform.position, lockY, 20);
    }

    Enemy ClosestEnemy()
    {
        Enemy closest = null;
        foreach(Enemy enemy in enemies)
        {
            if(enemy == null) continue;

            if (closest == null)
            {
                closest = enemy; 
                continue;
            }
            
            if(closest != null && Vector3.Distance(enemy.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position))
            {
                closest = enemy;
            }
        }

        return closest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>() != null)
        {
            enemies.Add(other.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            enemies.Remove(other.GetComponent<Enemy>());
        }
    }
}
