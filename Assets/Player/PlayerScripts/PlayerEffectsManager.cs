using UnityEngine;


public class PlayerEffectsManager : MonoBehaviour
{

    [SerializeField] private PlayerHealthManager _healthManager;// just to see


    void Start()
    {
        _healthManager = GetComponent<PlayerHealthManager>();
       UIManager.instance.DeactivateBloodEffect();
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
        SoundManager.instance.PlayFXSound(SoundManager.instance.HurtSound, 0.25f);
        StartCoroutine(UIManager.instance.DamageEffect());
    }

    public void Die(Vector3 position, GameObject pl)
    {
        UIManager.instance.ActivateCursor();
        GameManager.Instance.ReStart();
        SceneLoaderManager.instance.LoadSceneByName("GameOver");
        
    }


    public void HealEffect(int amount)
    {
    }
}
