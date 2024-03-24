using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cup"))
        {
            Debug.Log("Cup entered the plate");
            //remove layer draggable from the cup
            other.gameObject.layer = 0;
            //glue the cup to the plate
            other.gameObject.transform.parent = this.transform;
        }
    }
}
