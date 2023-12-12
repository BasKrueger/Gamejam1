using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerBaseFight : MonoBehaviour
{
    TextMeshProUGUI textmesh;

    // Start is called before the first frame update
    void Awake()
    {
        textmesh = GetComponent<TextMeshProUGUI>();
    }

    public void SetTime(float time)
    {
        int floorTime = Mathf.FloorToInt(time);

        textmesh.text = floorTime.ToString();
    }

    public void KillTimer()
    {
        textmesh.text = "";
    }
}
