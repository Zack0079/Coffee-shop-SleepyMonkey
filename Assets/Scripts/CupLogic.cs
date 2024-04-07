using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CupLogic : MonoBehaviour
{
    // Reference to the panel prefab
    public GameObject panelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        panelPrefab.SetActive(false);
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
        }
    }


}
