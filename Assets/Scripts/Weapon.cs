using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float cooldown;
    private float activeCooldown;

    public bool lockY;

    private void Start()
    {
        activeCooldown = cooldown;
    }

    private void Update()
    {
        activeCooldown -= Time.deltaTime;
        if(activeCooldown < 0)
        {
            activeCooldown = cooldown;
            Fire();
        }
    }

    public abstract void Fire();
}
