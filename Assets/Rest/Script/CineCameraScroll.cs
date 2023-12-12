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
        if (iterator < 0 || iterator > virtualCameras.Length) return;
        if (virtualCameras[iterator] == null) return;
        if (transform.position == virtualCameras[iterator - 1].transform.position)
        {

            if (virtualCameras[iterator - 1].GetComponent<VCamTimer>().isBaseFight)
            {
                virtualCameras[iterator - 1].GetComponent<VCamTimer>().SetBaseInstance(true);
            }
            else
            {
                GoToNextCam();
            }
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
