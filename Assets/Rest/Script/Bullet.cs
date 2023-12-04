using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    Vector3 direction;
    public void Fire(Vector3 direction, bool lockY = false, float bonusSpeed = 0)
    {
        speed += bonusSpeed;
        direction = direction.normalized;
        if (lockY)
        {
            direction.y = 0;
        }
        this.direction = direction;
        Destroy(this.gameObject, 10);
    }

    private void Update()
    {
        GetComponent<Rigidbody>().velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null ) 
        {
            other.GetComponent<Enemy>().TakeDamage(1);
            Destroy(this.gameObject);
        }
    }
}
