using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ParticleSystem pling;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Player>() != null)
        {
            Ressources.value += 1;
            Ressources.XP += 1;

            var pl = Instantiate(pling);
            pl.transform.position = transform.position;

            Destroy(this.gameObject);
        }
    }
}
