using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Pistol : Weapon
{
    public Bullet projectile;

    public override void Fire()
    {
        if (ClosestEnemy() == null) return;

        var bullet = Instantiate(projectile);

        bullet.transform.position = transform.position;

        bullet.Fire(ClosestEnemy().transform.position - transform.position, lockY, 20);
    }

    Enemy ClosestEnemy()
    {
        Enemy closest = null;
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            if (enemy == null) continue;

            if (closest == null)
            {
                closest = enemy;
                continue;
            }

            if (closest != null && Vector3.Distance(enemy.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position))
            {
                closest = enemy;
            }
        }

        return closest;
    }
}
