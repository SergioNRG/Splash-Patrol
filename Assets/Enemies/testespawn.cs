using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testespawn : MonoBehaviour
{

    public GameObject GameObject;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= 100; i++)
        {
            Instantiate(GameObject, transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
      
    }
}
