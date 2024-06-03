using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyEffectsManager : MonoBehaviour
{
    [SerializeField] private GameObject _floatingTxt;
    [SerializeField] private EnemyHealthManager _healthManager;
     private Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _healthManager = GetComponent<EnemyHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamageEffect(int damage)
    {
        if (_healthManager.CurrentHealth != 0 )
        {
            ShowText();
            Debug.Log("enemy taking damage"); 
        }
        
    }

    public void Die(Vector3 position)
    {
        Debug.Log("enemy morreu");
        Destroy(gameObject);
    }

    public void ShowText()
    {
        _offset = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(1f,2f),0);
        var go =  Instantiate(_floatingTxt, transform.position + _offset, Quaternion.identity,transform);
        go.transform.forward = Camera.main.transform.forward;
        go.GetComponent<TextMeshPro>().text = _healthManager.CurrentHealth.ToString();
    }
}
