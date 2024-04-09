using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStep : MonoBehaviour
{
    public GameObject[] steps;
    private int numStep = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject step in steps)
        {
            step.SetActive(false);
        }
        steps[0].SetActive(true);
    }

    // Update is called once per frame
    public void ShowNext(int num)
    {
        if( num == numStep && numStep < steps.Length){
            steps[numStep].SetActive(false);
            numStep++;
            steps[numStep].SetActive(true);
        }
    }

    public void end()
    {
        foreach (GameObject step in steps)
        {
            step.SetActive(false);
        }
        steps[(steps.Length -1)].SetActive(true);
        numStep = steps.Length+1;
    }
}
