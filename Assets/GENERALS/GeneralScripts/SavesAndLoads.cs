using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SavesAndLoads : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameTMPro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("PlayerName", null);
        PlayerPrefs.SetString("PlayerName",_nameTMPro.text.ToString());
    }
}
