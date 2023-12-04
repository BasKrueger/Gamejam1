using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : MonoBehaviour
{
    public float cooldown;
    private float active;
    public ParticleSystem vfx;

    private void Update()
    {
        active -= Time.deltaTime;
        if(active <= 0)
        {
            active = cooldown;

            foreach(var enemy in FindObjectsOfType<Enemy>())
            {
                vfx.Play();
                enemy.TakeDamage(10000);
            }
        }
    }
}
