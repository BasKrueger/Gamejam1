using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookWeapon : Weapon
{
    public override void Fire()
    {
        GetComponent<Animator>().Play("BookSpin");
    }
}
