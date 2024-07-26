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
            _potalHPTxt.gameObject.transform.LookAt(Camera.main.transform.position);
            _potalHPTxt.gameObject.transform.Rotate(0,180,0);
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
