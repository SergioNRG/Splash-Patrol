using TMPro;
using UnityEngine;


public class Portal : MonoBehaviour
{
    [Header("Portal HP Text")]
    [SerializeField] private TextMeshPro _potalHPTxt;

    public event ChangePortalHPEvent OnPortalEnter;
    public delegate void ChangePortalHPEvent(Vector3 pos, GameObject go);

    
    void Start()
    {
        if (GameManager.Instance != null)
        {
            _potalHPTxt.text = GameManager.Instance.PortalLife.ToString();
            OnPortalEnter += GameManager.Instance.ChangePortalLife;
            
        }

        if (EnemySpawner.instance != null)
            OnPortalEnter += EnemySpawner.instance.HandleEnemyKilled;
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
            OnPortalEnter -= GameManager.Instance.ChangePortalLife;

        if (EnemySpawner.instance != null)
            OnPortalEnter -= EnemySpawner.instance.HandleEnemyKilled;
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
            if (other.transform.parent != null)
            {
                Transform parentTrans = other.transform.parent;
                GameObject parent = parentTrans.gameObject;
                OnPortalEnter?.Invoke(other.transform.position,parent);
                parent.SetActive(false);
            }else
            {
                OnPortalEnter?.Invoke(other.transform.position,other.gameObject);
                other.gameObject.SetActive(false);
            }
            
            if (GameManager.Instance != null) { _potalHPTxt.text = GameManager.Instance.PortalLife.ToString(); }
               
        }
    }
}
