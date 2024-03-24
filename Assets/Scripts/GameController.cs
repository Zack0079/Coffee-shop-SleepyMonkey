using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent _espresso;
    public UnityEvent _latte;
    public UnityEvent _americano;

    public GameObject cupPrefab;
    private GameObject _cupSpawn;
    public GameObject _canvas;
    // Start is called before the first frame update
    void Start()
    {
        _espresso = new UnityEvent();
        _espresso.AddListener(chooseEspresso);

        _cupSpawn = GameObject.Find("cupSpawn");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chooseEspresso() {
        Debug.Log("Espresso");
        // Instantiate cupPrefab at (Machine) GameObject
        // Find gameobject called cupSpawn
        // Instantiate cupPrefab at cupSpawn position


        Instantiate(cupPrefab, _cupSpawn.transform.position, Quaternion.identity);
    }
}
