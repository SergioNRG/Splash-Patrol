using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public event ChangeLvlEvent OnPortalEnter;
    public delegate void ChangeLvlEvent();


 /*   private void OnEnable()
    {
        if (GameManager.Instance != null)
            OnPortalEnter += GameManager.Instance.ChangePortalLife;
    }*/

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            OnPortalEnter -= GameManager.Instance.ChangePortalLife;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
            OnPortalEnter += GameManager.Instance.ChangePortalLife;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(OnPortalEnter);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            Debug.Log("entrou portal");
            Debug.Log(OnPortalEnter);
            OnPortalEnter?.Invoke();
        }
    }
}
