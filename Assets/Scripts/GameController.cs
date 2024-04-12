using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

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
    private int[] correctSetOrder = new int[3];

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

    // !!!LEVEL 3 SLIDERS!!!
    public Slider espressoSlider;
    public Slider milkSlider;
    public Slider waterSlider;

    private string correctSetOrderName;

    [Header("Mission States")]
    public TMP_Text finishedNumber;
    public GameObject nextButton;
    public int requestOfCups = 3;
    private int amountOfCups = -1;

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

        // !!!LEVEL 3 SLIDERS!!!
        if (espressoSlider != null && milkSlider != null && waterSlider != null)
        {
            espressoSlider.onValueChanged.AddListener(setEspresso);
            milkSlider.onValueChanged.AddListener(setMilk);
            waterSlider.onValueChanged.AddListener(setWater);
        }
        // IF CURRENT SCENE IS LEVEL 3 THEN GENERATE ORDER
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level3")
        {
            correctSetOrderName = "";
            correctSetOrder = new int[3];
            generateSetOrder();
            Debug.Log("Correct Set Order Name: " + correctSetOrderName);
            Debug.Log("Correct Set Order: Milk: " + correctSetOrder[0] + " Espresso:" + correctSetOrder[1] + " Water: " + correctSetOrder[2]);
        }
        CompleteOrder();
    }

    // Update is called once per frame
    void Update()
    {
        // Find current order text and set it to correctSetOrderName
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level3")
        {
            GameObject.Find("Order").GetComponent<TMP_Text>().text = "Order: " + correctSetOrderName;
        }
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
        // Instantiate cupPrefab at (Machine) GameObject
        // Find gameobject called cupSpawn
        // Instantiate cupPrefab at cupSpawn position
        // Generate a new correct order
        generateSetOrder();

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
        // RESET Sliders
        if(espressoSlider != null && milkSlider != null && waterSlider != null)
        {
            espressoSlider.value = 0;
            milkSlider.value = 0;
            waterSlider.value = 0;
        }
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
        }
        else
        {
            Debug.Log("Incorrect Order");
        }
        
        if (showOrder)
        {
            currentMikeNumber.text = "M:0";
            currentEspressoNumber.text = "E:0";
            currentWaterNumber.text = "W:0";
        }

        clearOrder();
    }

    // !!! LEVEL 3 CODE !!!
    // set esoressi, milk, water sliders
    public void setEspresso(float value)
    {
        currentOrder[1] = (int)value;
        Debug.Log("Espresso: " + value);
    }

    public void setMilk(float value)
    {
        currentOrder[0] = (int)value;
        Debug.Log("Milk: " + value);
    }

    public void setWater(float value)
    {
        currentOrder[2] = (int)value;
        Debug.Log("Water: " + value);
    }


    public void generateSetOrder()
    {
        // create 3 preset orders and then randomly choose one and set as correctSetOrder
        // Americano = 2/3 water, 1/3 espresso
        // Latte = 2/3 milk, 1/3 espresso
        // Espresso = 1/3 espresso
        // make sure the current order is empty
        correctSetOrder[0] = 0;
        correctSetOrder[1] = 0;
        correctSetOrder[2] = 0;


        int[] americano = new int[3];
        americano[0] = 0;
        americano[1] = 1;
        americano[2] = 2;

        int[] latte = new int[3];
        latte[0] = 2;
        latte[1] = 1;
        latte[2] = 0;

        int[] espresso = new int[3];
        espresso[0] = 0;
        espresso[1] = 3;
        espresso[2] = 0;

        // randomly choose one of the 3 orders
        int randomOrder = Random.Range(0, 3);

        if (randomOrder == 0)
        {
            correctSetOrderName = "Americano";
            correctSetOrder = americano;
        }
        else if (randomOrder == 1)
        {
            correctSetOrderName = "Latte";
            correctSetOrder = latte;
        }
        else
        {
            correctSetOrderName = "Espresso";
            correctSetOrder = espresso;
        }
    }

    public void createCupLevel3(){
        // check if the current order is correct
        if (currentOrder.SequenceEqual(correctSetOrder))
        {
            Debug.Log("Correct Order");
            chooseEspresso();
        }
        else
        {
            Debug.Log("Incorrect Order");
        }
        clearOrder();
    }

    public void CompleteOrder(){
        if (finishedNumber)
        {
            amountOfCups++;
            finishedNumber.text =  amountOfCups.ToString() + " / " + requestOfCups.ToString();
            if(amountOfCups == requestOfCups){
                nextButton.SetActive(true);
            }
        }
    }

}
