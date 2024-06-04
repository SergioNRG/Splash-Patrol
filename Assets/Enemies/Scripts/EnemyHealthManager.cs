using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;

    [SerializeField] private int _currentHealth;// just to see in inspector
    public int CurrentHealth { get => _currentHealth; private set => _currentHealth = value; }

    public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;

    [SerializeField] private EnemyEffectsManager _enemyEffectsManager;
    private void OnEnable()
    {
        _enemyEffectsManager = GetComponent<EnemyEffectsManager>();
        CurrentHealth = MaxHealth;
        OnTakeDamage += _enemyEffectsManager.TakeDamageEffect;
        OnDeath += _enemyEffectsManager.Die;
    }

    private void OnDisable()
    {
        OnTakeDamage -= _enemyEffectsManager.TakeDamageEffect;
        OnDeath -= _enemyEffectsManager.Die;
    }

    public void ApplyDamage(int damage)
    {
        int damageTaken = Mathf.Clamp(damage, 0, CurrentHealth);

        CurrentHealth -= damageTaken;

        if (damageTaken != 0) { OnTakeDamage?.Invoke(damageTaken); }

        if (CurrentHealth == 0 && damageTaken != 0) { OnDeath?.Invoke(transform.position); }
    }
}
