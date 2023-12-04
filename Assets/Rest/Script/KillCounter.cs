using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = $"kills: {Ressources.enemyKills}";
    }
}
