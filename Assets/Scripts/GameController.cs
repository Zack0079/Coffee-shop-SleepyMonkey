using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent _espresso;
    public UnityEvent _latte;
    public UnityEvent _americano;
    // Start is called before the first frame update
    void Start()
    {
        _espresso = new UnityEvent();
        _espresso.AddListener(chooseEspresso);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void chooseEspresso() {
        Debug.Log("Espresso");
    }
}
