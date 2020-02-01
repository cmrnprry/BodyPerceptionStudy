using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The code for initializing the class and deleting any duplicates was used from
//https://gamedev.stackexchange.com/questions/116009/in-unity-how-do-i-correctly-implement-the-singleton-pattern
public class StatsManager : MonoBehaviour
{
    //Class getter and static instance
    private static StatsManager managerInstance;
    public static StatsManager Instance {  get { return managerInstance;} }

    //STATS TO TRACK:
    //Calorie Stats:
    private int caloriesNet = 0;
    private int caloriesGained = 0;
    private int caloriesBurned = 0;

    //Checks stats
    private int numPhoneChecks = 0;
    private int numCalChecks = 0;
    private int numFoodAppOpens = 0;
    // private int numFoodAppOrders = numOfType(orderType.APP);
    private int numFridgeOpens = 0;
    // private int numFridgeTakes = numOfType(orderType.FRIDGE);
    // private int totalFoodTakes = foodsEaten.count;
    // private int exercisesDone = exercises.count;
    // private int quizzesDone = quizResults.count;


    //Lists
    private List<(string, int, orderType)> foodsEaten = new List<(string, int, orderType)>();
    private List<(string, int)> exercises = new List<(string, int)>();
    private List<(string, quizResult)> quizResults = new List<(string, quizResult)>();
    

    //Enums:
    public enum orderType { APP, FRIDGE };
    public enum quizResult { AGREE, DISAGREE, INDIFFERENT };
  
    // Start is called before the first frame update
    void Start()
    {
        if(managerInstance != null && managerInstance != this)
        {
            Debug.Log("Multiple stats managers!");
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Initializing Stats Manager!");
            managerInstance = this;
            Debug.Log("Stat Manager Loaded!");
        }
        
    }

    //Methods for adjusting stats from outside
    public void addCalories(int calories)
    {
        if(calories <= 0)
        {
            caloriesBurned += (calories * -1);
        }
        else
        {
            caloriesGained += calories;
        }
        caloriesNet += calories;
    }

    public void addFood(string foodName, orderType orderType, int calories)
    {
        addCalories(calories);
        foodsEaten.Add((foodName, calories, orderType));
    }

    public void addExercise(string exerciseName, int calories)
    {
        addCalories(calories);
        exercises.Add((exerciseName, calories));
    }

    public void addQuiz(string quizQuestion, quizResult quizResult)
    {
        quizResults.Add((quizQuestion, quizResult));
    }

    public void checkedPhone()
    {
        numPhoneChecks++;
    }
    public void checkedCals()
    {
        numCalChecks++;
    }
    public void openedFoodApp()
    {
        numFoodAppOpens++;
    }
    public void openedFridge()
    {
        numFridgeOpens++;
    }



    // Private methods
    private int numOfType(orderType orderType)
    {
        return foodsEaten.FindAll(item => item.Item3 == orderType).Count;
    }
}
