using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    public static Base Instance 
    { get;  set; }

    [SerializeField]
    public int maxHP = 500;
    
    public int hp;

    public ParticleSystem VFX;
    public Canvas GameOverScreen;

    private DamageNumbers damageNumbers;
    public HPBar hpBar;

    public bool attackPlayerInsteadOfBase = true;

    private void Awake()
    {
        damageNumbers = GetComponentInChildren<DamageNumbers>();
        hp = maxHP;
        hpBar.SetHealthBar(maxHP, hp);
    }

    public void SetInstance(bool value)
    {
        Instance = value ? this : null;
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
            BaseDeath();
        }

        hpBar.SetHealthBar(maxHP, hp);
    }

    public void BaseDeath()
    {
        // todo
        VFX.gameObject.SetActive(true);
        GameOverScreen.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        //Destroy(gameObject);
        //Destroy(Instance);
    }

    public void Looser()
    {
        SceneManager.LoadScene(0);
    }
}
