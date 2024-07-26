using TMPro;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogueSO _dialogue;
    [SerializeField] private GameObject _showTxtPanel;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private string _playerName;

    void Start()
    {
        _showTxtPanel.SetActive(false);
        _playerName = PlayerPrefs.GetString("PlayerName");
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
