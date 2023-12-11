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

    public Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (Base.Instance.attackPlayerInsteadOfBase)
        {
            if (other.GetComponent<Player>() != null)
            {
                FindObjectOfType<Player>().TakeDamage(damage);
                EnemyDeath();
            }
        }
        else
        {
            if (other.GetComponent<Base>() != null)
            {
                isBaseInRange = true;
            }
        }
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
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

        if (Base.Instance.attackPlayerInsteadOfBase)
        {
            WalkTowardsPlayer();
        }
        else
        {
            WalkTowardsTarget();
        }

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

    public void WalkTowardsPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    public void AttackTarget()
    {
        Base.Instance.TakeDamage(damage);
        EnemyDeath();
    }

}
