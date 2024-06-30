using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testespawn : MonoBehaviour
{

    public GameObject GameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        for (int i = 0; i <= 10; i++)
        {
            Instantiate(gameObject);
        }
    }
}
