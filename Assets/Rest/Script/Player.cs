using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    public int maxHP = 20;

    public int hp;

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
    public Dictionary<int, Weapon> weapons = new Dictionary<int, Weapon>();

    public Image template;
    public Transform templateHolder;

    public List<Weapon> allWeapons;

    public List<float> levelUpCosts;

    private DamageNumbers damageNumbers;
    public HPBar hpBar;

    public Canvas GameOverScreen;

    private void Awake()
    {
        Time.timeScale = 1.0f;

        damageNumbers = GetComponentInChildren<DamageNumbers>();
        rb = GetComponent<Rigidbody>();
        levelUpSFX = GetComponent<AudioSource>();
        hpBar = GetComponentInChildren<HPBar>();

        hp = maxHP;
        hpBar.SetHealthBar(maxHP, hp);
        AddWeapon(weaponslot.GetComponentInChildren<Weapon>());
        ShowWeaponFrame();
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


        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            SceneManager.LoadScene(0);
        }
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
            var weaponInstance = Instantiate(weapon);
            AddWeapon(weaponInstance);

            levelUpSFX.Play();
            //levelUpVFX.Play();
        }
        ShowWeaponFrame();
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
                weapons.Remove(selectedWeapon.order);
                selectedWeapon = null;
            }
        }
        ShowWeaponFrame();
    }

    void SelectWeapon()
    {
        int select = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            select = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            select = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            select = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            select = 3;
        }

        if (weapons.ContainsKey(select))
        {
            selectedWeapon = weapons[select];
            ShowWeaponFrame();
        }

        if (selectedWeapon != null)
        {
           // Debug.Log($"selected weapon: {selectedWeapon.name}");
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        if (weapons.ContainsKey(weapon.order))
        {
            weapon.order = weapon.order + 1;
        }
        weapon.order = weapons.Count;
        weapons.Add(weapon.order, weapon);
        weapon.transform.position = weaponslot.position;
        weapon.transform.SetParent(weaponslot);
    }

    public void ShowWeaponFrame()
    {
        foreach (Transform t in templateHolder.transform)
        {
            if (t == null) continue;
            Destroy(t.gameObject);
        }

        for (int i = 0; i < 4; i++)
        {
            if (!weapons.ContainsKey(i)) continue;
            if (weapons[i] == weaponslot) continue;

            var instance = Instantiate(template, templateHolder.transform);
            instance.transform.SetSiblingIndex(i - 1);
            string weaponName = weapons[i].name.Replace("(Clone)", "");
            template.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = weaponName + " " + (weapons[i].order + 1);
            template.transform.Find("active").GetComponent<TextMeshProUGUI>().text = (weapons[i].GetComponent<Weapon>() == selectedWeapon) ? "Selected" : "";
        }
    }

    public void TakeDamage(int damage)
    {
        if (damageNumbers != null)
        {
            damageNumbers.PrintDamageNumber(damage, Color.red);
        }

        hp -= damage;

        if (hp <= 0)
        {
            PlayerDeath();
        }

        hpBar.SetHealthBar(maxHP, hp);
    }

    private void PlayerDeath()
    {
        GameOverScreen.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
