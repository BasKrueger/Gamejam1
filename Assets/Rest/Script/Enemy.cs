using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public int maxHP = 5;

    public int hp;

    [SerializeField]
    private int damage = 1;

    private NavMeshAgent agent;
    private HPBar hpBar;

    public Coin coin;

    public GameObject deathVfxPrefab;

    [HideInInspector]
    public bool isBaseInRange = false;

    public DamageNumbers numbers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Base>() != null)
        {
            isBaseInRange = true;
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        hpBar = GetComponentInChildren<HPBar>();

        if(Ressources.Difficulty > 1)
        {
            maxHP += (Ressources.Difficulty / 2) * 10;
        }

        hp = maxHP;
        hpBar.SetHealthBar(maxHP, hp);

        
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        numbers.PrintDamageNumber(damage, Color.red);

        if (hp <= 0)
        {
            var coins = Instantiate(coin);
            coins.transform.position = transform.position;
            Ressources.enemyKills += 1;
            EnemyDeath();
        }

        hpBar.SetHealthBar(maxHP, hp);
    }

    public void AITick()
    {
        if (isBaseInRange)
        {
            AttackTarget();
            return;
        }

        WalkTowardsTarget();
    }

    public void EnemyDeath()
    {
        var deathVfx = Instantiate(deathVfxPrefab);
        deathVfx.transform.position = transform.position;
        Destroy(gameObject);
    }

    public void WalkTowardsTarget()
    {
        agent.SetDestination(Base.Instance.transform.position);
    }

    public void AttackTarget()
    {
        Base.Instance.TakeDamage(damage);
        EnemyDeath();
    }

}
