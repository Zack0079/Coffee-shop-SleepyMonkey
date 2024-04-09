using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CupLogic : MonoBehaviour
{
    // Reference to the panel prefab
    public GameObject panelPrefab;

    private ShowStep showStepController = null;
    public bool showStep = false;
    // Start is called before the first frame update
    void Start()
    {
        panelPrefab.SetActive(false);
        if(showStep){
            showStepController = GameObject.Find("GameController").GetComponent<ShowStep>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bean")
        {
            Debug.Log("Ball entered the cup");
            other.gameObject.SetActive(false);
            panelPrefab.SetActive(true);
            if(showStep){
                showStepController.ShowNext(0);
            }
        }
    }


}
