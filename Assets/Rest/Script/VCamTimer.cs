using UnityEngine;

public class VCamTimer : MonoBehaviour
{
    public bool isBaseFight;

    public float time = 10.0f;
    private float internalTimer;

    private TimerBaseFight timerBaseFight;
    private Base playerBase;

    private void Start()
    {
        timerBaseFight = FindObjectOfType<TimerBaseFight>();
        if (transform.parent != null)
        {
            playerBase = GetComponentInParent<Base>();
        }
        internalTimer = time;
    }

    private void Update()
    {
        if (Base.Instance == null) return;

        Debug.Log(internalTimer);
        internalTimer -= Time.deltaTime;

        timerBaseFight.SetTime(internalTimer);

        if (internalTimer <= 0.0f) 
        {
            internalTimer = time;
            isBaseFight = false;
            Base.Instance.SetInstance(false);
            timerBaseFight.KillTimer();
            Camera.main.GetComponent<CineCameraScroll>().GoToNextCam();
        }
    }

    public void SetBaseInstance(bool value)
    {
        playerBase.SetInstance(value);
    }
}
