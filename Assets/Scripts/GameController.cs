using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameController : MonoBehaviour
{
    public UnityEvent _espresso;
    public UnityEvent _latte;
    public UnityEvent _americano;

    public GameObject cupPrefab;
    private GameObject _cupSpawn;
    public GameObject _canvas;

    Vector3 beanPsoition;
    Vector3 platePosition;
    public GameObject bean;
    public GameObject plate;

    // array to store current order, stores amount of milk, espresso, water
    private int[] currentOrder = new int[3];
    private int[] correctOrder = new int[3];

    private int currentEspresso = 0;
    private int currentWater = 0;
    private int currentMilk = 0;

    private ShowStep showStepController = null;
    public bool showStep = false;

    public TMP_Text mikeNumber;
    public TMP_Text espressoNumber;
    public TMP_Text waterNumber;
    public TMP_Text currentMikeNumber;
    public TMP_Text currentEspressoNumber;
    public TMP_Text currentWaterNumber;
    public bool showOrder = false;

    // Start is called before the first frame update
    void Start()
    {
        _espresso = new UnityEvent();
        _espresso.AddListener(chooseEspresso);

        _cupSpawn = GameObject.Find("cupSpawn");
        beanPsoition = bean.transform.position;

        // set plate position the very first time
        platePosition = plate.transform.position;

        // make sure the current order is empty
        // 0 = milk, 1 = espresso, 2 = water
        currentOrder[0] = 0;
        currentOrder[1] = 0;
        currentOrder[2] = 0;

        currentEspresso = 0;
        currentWater = 0;
        currentMilk = 0;

        // IF CURRENT SCENE IS LEVEL 2 THEN GENERATE ORDER
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level2")
        {
            generateOrder();
        }

        if (showStep)
        {
            showStepController = gameObject.GetComponent<ShowStep>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void resetBean()
    {
        GameObject newBean = Instantiate(bean, beanPsoition, Quaternion.identity);
        Destroy(bean);
        bean = newBean;
        newBean.SetActive(true);
    }

    public void resetPlate()
    {
        GameObject newPlate = Instantiate(plate, platePosition, Quaternion.identity);
        if (showStep)
        {
            newPlate.GetComponent<PlateScript>().showStep = true;
        }
    }


    public void chooseEspresso()
    {
        Debug.Log("Espresso");
        // Instantiate cupPrefab at (Machine) GameObject
        // Find gameobject called cupSpawn
        // Instantiate cupPrefab at cupSpawn position
        Instantiate(cupPrefab, _cupSpawn.transform.position, Quaternion.identity);
        if (showStep)
        {
            showStepController.ShowNext(1);
        }
        resetBean();
        resetPlate();
        // panel setactive false
        _canvas.SetActive(false);
    }

    //!!! LEVEL 2 CODE !!!
    // function to generate 3 random orders
    public void generateOrder()
    {
        // make it so that the current order is empty
        currentOrder[0] = 0;
        currentOrder[1] = 0;
        currentOrder[2] = 0;
        // generate 3 random numbers between 0 and 2
        int milk = Random.Range(0, 3);
        int water = Random.Range(0, 3);

        // coffee random numbers between 1 and 2
        int espresso = Random.Range(1, 3);

        // set the current order to the random numbers
        correctOrder[0] = milk;
        correctOrder[1] = espresso;
        correctOrder[2] = water;

        if (showOrder)
        {
            mikeNumber.text = milk.ToString();
            espressoNumber.text = espresso.ToString();
            waterNumber.text = water.ToString();
        }
        Debug.Log("Correct Order: Milk: " + correctOrder[0] + " Espresso:" + correctOrder[1] + " Water: " + correctOrder[2]);
    }



    // function to add a certain amount of espresso
    public void addEspresso()
    {
        currentEspresso++;
        currentOrder[1] = currentEspresso;
        if (showOrder)
        {
            currentEspressoNumber.text = "E:" + currentEspresso.ToString();
        }
        Debug.Log("Current Order: Milk: " + currentOrder[0] + " Espresso:" + currentOrder[1] + " Water: " + currentOrder[2]);

    }

    public void addMilk()
    {
        currentMilk++;
        currentOrder[0] = currentMilk;
        if (showOrder)
        {
            currentMikeNumber.text = "M:" + currentMilk.ToString();
        }
        Debug.Log("Current Order: Milk: " + currentOrder[0] + " Espresso:" + currentOrder[1] + " Water: " + currentOrder[2]);
    }

    public void addWater()
    {
        currentWater++;
        currentOrder[2] = currentWater;
        if (showOrder)
        {
            currentWaterNumber.text = "W:" + currentWater.ToString();
        }
        Debug.Log("Current Order: Milk: " + currentOrder[0] + " Espresso:" + currentOrder[1] + " Water: " + currentOrder[2]);
    }

    public void clearOrder()
    {
        currentOrder[0] = 0;
        currentOrder[1] = 0;
        currentOrder[2] = 0;
        currentEspresso = 0;
        currentWater = 0;
        currentMilk = 0;
    }

    public void createCup()
    {
        // check if the current order is correct
        if (currentOrder.SequenceEqual(correctOrder))
        {
            Debug.Log("Correct Order");
            chooseEspresso();
            // generate a new order
            generateOrder();
            if (showOrder)
            {
                currentMikeNumber.text = "M:0";
                currentEspressoNumber.text = "E:0";
                currentWaterNumber.text = "W:0";
            }
        }
        else
        {
            Debug.Log("Incorrect Order");
        }
        clearOrder();
    }
}
