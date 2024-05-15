using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 100;

    [SerializeField] private int _currentHealth;// just to see in inspector
    public int CurrentHealth { get => _currentHealth; private set => _currentHealth = value; }

    public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;


    private void OnEnable()
    {
        CurrentHealth = MaxHealth;
        OnTakeDamage += TakeDamage;
        OnDeath += Die;
    }

    

    private void TakeDamage(int damage)
    {
        Debug.Log("Taking Damage");
        //apply effects
    }

    private void Die(Vector3 position)
    {
        Debug.Log("enemy morreu");
        //apply effects
    }

    public void ApplyDamage(int damage)
    {
        int damageTaken = Mathf.Clamp(damage, 0, CurrentHealth);

        CurrentHealth -= damageTaken;

        if (damageTaken != 0) { OnTakeDamage?.Invoke(damageTaken); }

        if (CurrentHealth == 0 && damageTaken != 0) { OnDeath?.Invoke(transform.position); }
    }
}
