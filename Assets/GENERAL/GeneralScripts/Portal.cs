using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private TextMeshPro _potalHPTxt;

    public event ChangePortalHPEvent OnPortalEnter;
    public delegate void ChangePortalHPEvent();

    
    void Start()
    {
        if (GameManager.Instance != null)
        {
            _potalHPTxt.text = GameManager.Instance.PortalLife.ToString();
            OnPortalEnter += GameManager.Instance.ChangePortalLife;
        }

        

    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
            OnPortalEnter -= GameManager.Instance.ChangePortalLife;
    }

    void Update()
    {
        if(Camera.main != null)
        {
            _potalHPTxt.gameObject.transform.rotation = Quaternion.LookRotation(_potalHPTxt.gameObject.transform.position - Camera.main.transform.position);
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            OnPortalEnter?.Invoke();
            if (GameManager.Instance != null) { _potalHPTxt.text = GameManager.Instance.PortalLife.ToString(); }
               
        }
    }
}
