using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTurret : Bullet
{
    public ParticleSystem explosion;

    private void Start()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            if (enemy == null) continue;

            if (Vector3.Distance(enemy.transform.position, transform.position) < FindObjectOfType<SphereCollider>().radius)
            {
                enemy.TakeDamage(3);
            }
        }

        var boom = Instantiate(explosion);
        boom.transform.position = transform.position;
        Destroy(boom, 2);

        Destroy(this.gameObject);
    }
}
