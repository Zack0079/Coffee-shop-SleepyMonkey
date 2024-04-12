using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExitLogic : MonoBehaviour
{
    public GameController gameController ;
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
        if(other.gameObject.CompareTag("Espresso"))
        {
            Debug.Log("COMPLETED ORDER");
            Destroy(other.gameObject);
            if(gameController != null){
                gameController.CompleteOrder();
            }
        }
    }
}
