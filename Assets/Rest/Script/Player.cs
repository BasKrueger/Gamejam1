using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;

    public Transform bulletSpawnPoint;

    public Image normalTowerHighlight;
    public Image bombTowerHighlight;

    public LayerMask mask;

    public int turretCost = 5;

    private int level = 0;

    private AudioSource levelUpSFX;

    public Transform weaponslot;

    public List<GameObject> Unlocks = new List<GameObject>();
    public Image levelProgress;
    public TextMeshProUGUI PlayerLevel;

    public ParticleSystem levelUpVFX;

    public GenericTower genericTowerTemplate;

    public Weapon selectedWeapon;
    public List<Weapon> weapons = new List<Weapon>();

    public Image template;
    public Transform templateHolder;

    public List<Weapon> allWeapons;

    public List<float> levelUpCosts;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        levelUpSFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            var position = hit.point;
            position.y = transform.position.y;
            transform.LookAt(position);
        }

        LevelUp();
        Move();
       // Attack();
        SpawnTurret();
        SelectWeapon();
        ShowWeaponFrame();
    }

    public void LevelUp()
    {
        float levelUPXP = levelUpCosts[level];
        levelProgress.fillAmount = Ressources.XP / (float)levelUPXP;
        PlayerLevel.text = $"Playerlevel: {level + 1}";

        if(Ressources.XP >= levelUPXP)
        {
            Ressources.XP = 0;

            if (Unlocks.Count > level)
            {
                //Unlocks[level].gameObject.SetActive(true);
                //Ressources.Difficulty += 1;
            }

            level++;

            var weapon = allWeapons[UnityEngine.Random.Range(0, allWeapons.Count)];
            var t = Instantiate(weapon);
            AddWeapon(t);

            levelUpSFX.Play();
            //levelUpVFX.Play();
        }
    }

    private void Move()
    {
        var direction = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            direction.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.z -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x -= 1;
        }

        rb.velocity = (direction * speed);
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
        }
    }

    void SpawnTurret()
    {
        if (Input.GetMouseButtonDown(1) && selectedWeapon != null)
        {
            RaycastHit hit;

            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, mask))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Turret")) return;

                //Ressources.value -= turretCost;

                var turret = Instantiate(genericTowerTemplate);
                var position = hit.point;
                position.y = transform.position.y;
                turret.transform.position = position;

                turret.AddWeapon(selectedWeapon);
                weapons.Remove(selectedWeapon);
                selectedWeapon = null;
            }
        }
    }

    void SelectWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = weapons[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = weapons[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = weapons[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedWeapon = weapons[3];
        }

        if(selectedWeapon != null)
        {
           // Debug.Log($"selected weapon: {selectedWeapon.name}");
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
        weapon.transform.position = weaponslot.position;
        weapon.transform.SetParent(weaponslot);
    }

    public void ShowWeaponFrame()
    {
        foreach(Transform t in templateHolder.transform)
        {
            if (t == null) continue;
            Destroy(t.gameObject);
        }

        foreach(Weapon w in weapons)
        {
            var instance = Instantiate(template);
            template.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = w.name;
            template.transform.Find("active").GetComponent<TextMeshProUGUI>().text = (w == selectedWeapon) ? "Selected" : "";

            instance.transform.SetParent(templateHolder);
        }
    }
}
