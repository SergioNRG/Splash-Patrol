using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] private DialogueSO _dialogue;
    [SerializeField] private GameObject _showTxtPanel;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    private string _playerName;
    // Start is called before the first frame update
    void Start()
    {
        _showTxtPanel.SetActive(false);
        _playerName = PlayerPrefs.GetString("PlayerName");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _showTxtPanel.SetActive(true);
            _textMeshProUGUI.text = "   Hi " + _playerName + " " + _dialogue.DialogueText;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _showTxtPanel.SetActive(false);
        }
        
    }
}
