using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    Quaternion rotation;
    Vector3 position;

    private void Awake()
    {
        rotation = transform.rotation;
        position = transform.position;
    }

    private void LateUpdate()
    {
        transform.rotation = rotation;
        var player = FindObjectOfType<Player>();
        Vector3 pos = transform.position;
        pos.y = position.y;
        pos.x = player.transform.position.x;
        pos.z = player.transform.position.z;
        transform.position = pos;
    }
}
