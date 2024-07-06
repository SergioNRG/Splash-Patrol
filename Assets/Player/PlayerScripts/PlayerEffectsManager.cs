using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsManager : MonoBehaviour
{

    [SerializeField] private PlayerHealthManager _healthManager;

    void Start()
    {
        _healthManager = GetComponent<PlayerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RoarEffect()
    {
        
    }
    public void Idleffect()
    {
       
    }
    public void MoveEffect()
    {
       
    }
    public void ChaseEffect()
    {

    }

    public void AttackEffect()
    {
       
    }

    public void TakeDamageEffect(int damage)
    {

        Debug.Log("taking Damage " + _healthManager.CurrentHealth);
    }

    public void Die(Vector3 position)
    {
        Debug.Log("player DIE");
    }


    public void HealEffect(int amount)
    {
        Debug.Log("healing");
    }
}
