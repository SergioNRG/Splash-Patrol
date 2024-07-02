using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private int _maxHealth;

    [SerializeField] private int _currentHealth;// just to see in inspector
    public int CurrentHealth { get => _currentHealth;  set => _currentHealth = value; }

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
        OnTakeHeal += _enemyEffectsManager.HealEffect;
    }

    private void OnDisable()
    {
        OnTakeDamage -= _enemyEffectsManager.TakeDamageEffect;
        OnDeath -= _enemyEffectsManager.Die;
        OnTakeHeal -= _enemyEffectsManager.HealEffect;
    }

    public void ApplyDamage(int damage)
    {
        int damageTaken = Mathf.Clamp(damage, 0, CurrentHealth);

        CurrentHealth -= damageTaken;

        if (damageTaken != 0) { OnTakeDamage?.Invoke(damageTaken); }

        if (CurrentHealth == 0 && damageTaken != 0) 
        {
            
           
            OnDeath?.Invoke(transform.position); 
        }
    }

    public void ApplyHeal(int amount)
    {
        int healTaken = Mathf.Clamp(amount, 0, MaxHealth);

        CurrentHealth += healTaken;

        if (healTaken != 0) { OnTakeHeal?.Invoke(-healTaken); }

      //  if (CurrentHealth == 0 && healTaken != 0) { OnDeath?.Invoke(transform.position); }
    }


}
