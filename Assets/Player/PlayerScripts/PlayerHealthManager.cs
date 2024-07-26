using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private PlayerEffectsManager _playerEffectsManager;
    public int CurrentHealth { get; set; }

    public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;
    public event IHealable.TakeHealEvent OnTakeHeal;

    [SerializeField] private Slider HPSlider;

    private void Awake()
    {
        _playerEffectsManager = GetComponent<PlayerEffectsManager>();
        CurrentHealth = MaxHealth;
        HPSlider.maxValue = MaxHealth;
        SetHPSlider(MaxHealth);
    }

    private void OnEnable()
    {
        OnTakeDamage += _playerEffectsManager.TakeDamageEffect;
        OnDeath += _playerEffectsManager.Die;
        OnTakeHeal += _playerEffectsManager.HealEffect;
    }

    private void OnDisable()
    {
        OnTakeDamage -= _playerEffectsManager.TakeDamageEffect;
        OnDeath -= _playerEffectsManager.Die;
        OnTakeHeal -= _playerEffectsManager.HealEffect;
    }

    public void ApplyDamage(int damage)
    {
        int damageTaken = Mathf.Clamp(damage, 0, CurrentHealth);

        CurrentHealth -= damageTaken;

        SetHPSlider(CurrentHealth);

        if (damageTaken != 0) { OnTakeDamage?.Invoke(damageTaken); }

        if (CurrentHealth == 0 && damageTaken != 0)
        {
            OnDeath?.Invoke(transform.position, gameObject);
        }
    }

    public void ApplyHeal(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        SetHPSlider(CurrentHealth);
        if (amount != 0) { OnTakeHeal?.Invoke(amount); }
    }

    private void SetHPSlider(int health)
    {
        HPSlider.value = health;
    }
}
