using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInvoker : MonoBehaviour
{

    public GameController _gameController;
    // Start is called before the first frame update
    void Start()
    {
        // assign via GameController
        _gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InvokeEspresso() {
        _gameController._espresso.Invoke();
    }
}
