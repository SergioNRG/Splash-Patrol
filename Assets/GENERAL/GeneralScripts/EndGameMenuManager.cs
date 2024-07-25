using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.ActivateCursor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        UIManager.instance.QuitGame();
    }
}
