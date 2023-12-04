using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = $"Coins: {Ressources.value}";
    }
}
