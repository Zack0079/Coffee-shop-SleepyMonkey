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
            Destroy(other.gameObject);

            // Make panel left of object
            Vector3 panelPosition = this.transform.position + new Vector3(0, 0, 0);
            GameObject panel = Instantiate(panelPrefab, panelPosition, Quaternion.identity);

            // Find the Canvas in the scene
            Canvas canvas = FindObjectOfType<Canvas>();

            // Make the panel a child of the Canvas
            if (canvas != null)
            {
                panel.transform.SetParent(canvas.transform, false);
            }
            else
            {
                Debug.LogError("No Canvas found in the scene.");
            }
        }
    }


}
