using UnityEngine;

public class UpgradeRange : MonoBehaviour
{
    public Player player;
    public GenericTower turret;

    public float bonusSpeed = 0.1f;
    public int levelUpCost = 1;

    public ParticleSystem levelUpVFX;

    private AudioSource levelUpSFX;

    private void Awake()
    {
        levelUpSFX = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            player = other.GetComponent<Player>();
        }
    }

    private void Update()
    {
        if(player != null && Input.GetKeyDown(KeyCode.E) && Ressources.value >= levelUpCost)
        {
            Ressources.value -= levelUpCost;
            Debug.LogWarning("Turret got Upgraded");
           // turret.fireCooldown_ = Mathf.Clamp(turret.fireCooldown_ - bonusSpeed, 0.1f, 10000);
            levelUpVFX.gameObject.SetActive(true);
            levelUpVFX.Play();
            levelUpSFX.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            player = null;
        }
    }
}
