using UnityEngine;

public class EnemyHealthManager : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private int _maxHealth;

    public int CurrentHealth { get; set; }
    public int MaxHealth { get =>_maxHealth; private set =>_maxHealth = value; }

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;
    public event IHealable.TakeHealEvent OnTakeHeal;

    [SerializeField] private EnemyEffectsManager _enemyEffectsManager;

    private void Awake()
    {
        _enemyEffectsManager = GetComponent<EnemyEffectsManager>();
        CurrentHealth = MaxHealth;
    }

    private void OnEnable()
    {       
        OnTakeDamage += _enemyEffectsManager.TakeDamageEffect;
        OnDeath += _enemyEffectsManager.Die;
        OnDeath += EnemySpawner.instance.HandleEnemyKilled;
        OnTakeHeal += _enemyEffectsManager.HealEffect;
    }

    private void OnDisable()
    {
        OnTakeDamage -= _enemyEffectsManager.TakeDamageEffect;
        OnDeath -= _enemyEffectsManager.Die;
        OnDeath -= EnemySpawner.instance.HandleEnemyKilled;
        OnTakeHeal -= _enemyEffectsManager.HealEffect;
    }

    public void ApplyDamage(int damage)
    {
        int damageTaken = Mathf.Clamp(damage, 0, CurrentHealth);

        CurrentHealth -= damageTaken;

        if (damageTaken != 0) { OnTakeDamage?.Invoke(damageTaken); }

        if (CurrentHealth == 0 && damageTaken != 0) 
        {   
            if (gameObject.transform.parent != null)
            {
                Transform parentTrans = gameObject.transform.parent;
                GameObject parent = parentTrans.gameObject;
                OnDeath?.Invoke(transform.position, parent);
            }else
            {
                OnDeath?.Invoke(transform.position, gameObject);
            }
           
        }
    }

    public void ApplyHeal(int amount)
    {
        int healTaken = Mathf.Clamp(amount, 0, MaxHealth);

        CurrentHealth += healTaken;

        if (healTaken != 0) { OnTakeHeal?.Invoke(healTaken); }

    }


}
