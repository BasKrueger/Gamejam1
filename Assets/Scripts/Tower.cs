using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTower : MonoBehaviour
{
    public Weapon weapon;

    public Transform weaponSlot;
    private Player player;

    public void AddWeapon(Weapon weapon)
    {
        weapon.transform.SetParent(weaponSlot);
        weapon.transform.position = weaponSlot.position;
        this.weapon = weapon;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<Player>() == null)
        {
            return;
        }
        player = other.gameObject.GetComponent<Player>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            player = null;
        }
    }

    private void Update()
    {
        if(player != null && weapon != null && Input.GetKeyDown(KeyCode.E))
        {
            player.AddWeapon(weapon);
            weapon = null;
            Destroy(this.gameObject);
        }
    }
}
