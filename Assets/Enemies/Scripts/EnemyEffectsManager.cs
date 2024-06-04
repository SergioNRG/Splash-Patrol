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

     private Vector3 _offset;


    public static ObjectPool<GameObject> _popUPPool;
    // Start is called before the first frame update
    void Start()
    {
        _healthManager = GetComponent<EnemyHealthManager>();
        _crosshair = Camera.main.GetComponentInChildren<Image>();
        _popUPPool = new ObjectPool<GameObject>(CreatePopUp);
    }

    // Update is called once per frame
    void Update()
    {

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

            Debug.Log("enemy taking damage"); 
        }
        
    }

    public void Die(Vector3 position)
    {
        Debug.Log("enemy morreu");
        //ShowDamagePopUp(); 
        Destroy(gameObject);
    }

    public void ShowDamagePopUp(Color color)
    {
        _offset = new Vector3(Random.Range(-30f, 60f), Random.Range(-30f,60f),0);
        var popText = _popUPPool.Get();//Instantiate(_floatingTxt, _crosshair.transform.position + _offset, Quaternion.identity,_crosshair.transform);
        popText.SetActive(true);
        //popText.transform.forward = Camera.main.transform.forward;
        popText.GetComponent<TextMeshProUGUI>().faceColor = color;
        popText.GetComponent<TextMeshProUGUI>().text = _healthManager.CurrentHealth.ToString();
    }

    private GameObject CreatePopUp()
    {
        var popUp = Instantiate(_floatingTxt, _crosshair.transform.position + _offset, Quaternion.identity, _crosshair.transform);
        //popUp.SetActive(false);
        popUp.GetComponent<SelfReturnToPool>().SetPool(_popUPPool);
        return popUp;
    }
}
