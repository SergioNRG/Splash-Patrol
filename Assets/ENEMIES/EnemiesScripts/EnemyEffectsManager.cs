using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class EnemyEffectsManager : MonoBehaviour
{
    [SerializeField] private GameObject _floatingTxt;
    [SerializeField] private EnemyHealthManager _healthManager;
    [SerializeField] private Image _crosshair;
    [SerializeField] private EnemyBase enemy;
     private Vector3 _offset;


    public static ObjectPool<GameObject> _popUPPool;

    

    /*private void OnEnable()
    {
        enemy = GetComponent<EnemyBase>();
        _healthManager = GetComponent<EnemyHealthManager>();
        _crosshair = Camera.main.GetComponentInChildren<Image>();
        _popUPPool = new ObjectPool<GameObject>(CreatePopUp);
    }*/
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyBase>();
        _healthManager = GetComponent<EnemyHealthManager>();
        _crosshair = Camera.main.GetComponentInChildren<Image>();
        _popUPPool = new ObjectPool<GameObject>(CreatePopUp);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RoarEffect()
    {
        enemy.AnimControllerInstance.Playanimation(enemy._animator, enemy.RoarAnim);       
    }
    public void Idleffect()
    {
        enemy.AnimControllerInstance.Playanimation(enemy._animator, enemy.IdleAnim);
    }
    public void MoveEffect()
    {
        enemy.AnimControllerInstance.Playanimation(enemy._animator, enemy.MoveAnim);
    }
    public void ChaseEffect()
    {
        enemy.AnimControllerInstance.Playanimation(enemy._animator, enemy.ChaseAnim);
    }

    public void AttackEffect()
    {
        enemy.AnimControllerInstance.Playanimation(enemy._animator, enemy.AttackAnim);
    }

    public void TakeDamageEffect(int damage)
    {
        if (_healthManager.CurrentHealth != 0 )
        {
            if ( _healthManager.CurrentHealth >= _healthManager.MaxHealth/2)
            {
                ShowDamagePopUp(Color.green);
            }
            if (_healthManager.CurrentHealth < _healthManager.MaxHealth/2 && _healthManager.CurrentHealth > _healthManager.MaxHealth / 4)
            {
                ShowDamagePopUp(Color.yellow);
            }
            if (_healthManager.CurrentHealth <= _healthManager.MaxHealth / 4)
            {
                ShowDamagePopUp(Color.red);
            }
        }
        
    }

    public void Die(Vector3 position, GameObject go)
    {
        enemy.AnimControllerInstance.Playanimation(enemy._animator, enemy.DieAnim);
    }
   

    public void HealEffect(int amount)
    {
        Debug.Log("healing");
    }

    public void ShowDamagePopUp(Color color)
    {
        var popText = _popUPPool.Get();
        popText.SetActive(true);
        popText.GetComponent<TextMeshProUGUI>().faceColor = color;
        popText.GetComponent<TextMeshProUGUI>().text = _healthManager.CurrentHealth.ToString();
    }



    private GameObject CreatePopUp()
    {
        _offset = new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), 0);
        var popUp = Instantiate(_floatingTxt, _crosshair.transform.position + _offset, Quaternion.identity, _crosshair.transform);
        popUp.GetComponent<SelfReturnToPool>().SetPool(_popUPPool);
        return popUp;
    }

 
}
