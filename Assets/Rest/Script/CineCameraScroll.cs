using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineCameraScroll : MonoBehaviour
{
    private CinemachineBrain cineCam;

    public CinemachineVirtualCamera[] virtualCameras;
    private int iterator = 0;

    void Awake()
    {
        cineCam = GetComponent<CinemachineBrain>();
        DisableAllCams();
        GoToNextCam();
    }

    private void Update()
    {
        if (virtualCameras[iterator] == null) return;
        if (transform.position == virtualCameras[iterator - 1].transform.position)
        {
            GoToNextCam();
        }
    }

    public void GoToNextCam()
    {
        if (iterator <= virtualCameras.Length)
        {
            DisableAllCams();
            virtualCameras[iterator].gameObject.SetActive(true);
            iterator++;
        }
    }

    private void DisableAllCams()
    {
        foreach (var cam in virtualCameras)
        {
            cam.gameObject.SetActive(false);
        }
    }
}
