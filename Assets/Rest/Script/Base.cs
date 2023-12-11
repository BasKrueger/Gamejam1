using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    public static Base Instance 
    { get; private set; }

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
        Instance = this;
        damageNumbers = GetComponentInChildren<DamageNumbers>();
        hp = maxHP;
        hpBar.SetHealthBar(maxHP, hp);
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
        //Destroy(gameObject);
        //Destroy(Instance);
    }

    public void Looser()
    {
        SceneManager.LoadScene(0);
    }
}
