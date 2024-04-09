using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    
    private ShowStep showStepController = null;
    public bool showStep = false;
    // Start is called before the first frame update
    void Start()
    {
        if(showStep){
            showStepController =  GameObject.Find("GameController").GetComponent<ShowStep>();
        }
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
            //set other tag to espresso/latte/americano
            this.gameObject.tag = "Espresso";
            if(showStep){
                showStepController.ShowNext(2);
            }
        }

        if(other.gameObject.CompareTag("Exit"))
        {

            if(this.gameObject.CompareTag("Espresso"))
            {
                Debug.Log("COMPLETED ORDER");
            }

            if(showStep){
                showStepController.end();
            }
        }
    }
}
