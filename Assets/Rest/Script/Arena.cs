using UnityEngine;

public class Arena : MonoBehaviour
{
    Base playerBase;

    private void Awake()
    {
        playerBase = FindObjectOfType<Base>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            playerBase.attackPlayerInsteadOfBase = false;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            playerBase.attackPlayerInsteadOfBase = true;
        }

    }
}
