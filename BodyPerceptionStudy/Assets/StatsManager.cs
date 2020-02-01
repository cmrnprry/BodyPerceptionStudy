using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;


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
        saveToCSV("runResults");   
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

    //Code from https://sushanta1991.blogspot.com/2015/02/how-to-write-data-to-csv-file-in-unity.html
    public void saveToCSV(string fileName)
    {
        List<string[]> rowData = new List<string[]>();
        string[] colTitles = new string[15];
        colTitles[0] = "Net Calroies";
        colTitles[1] = "Calories Gained";
        colTitles[2] = "Calories Burned";
        colTitles[3] = "Phone Checks";
        colTitles[4] = "Calorie Checks";
        colTitles[5] = "Food App Checks";
        colTitles[6] = "Food App Orders";
        colTitles[7] = "Fridge Checks";
        colTitles[8] = "Fridge Takes";
        colTitles[9] = "Total Food Eaten";
        colTitles[10] = "Exercises Done";
        colTitles[11] = "Quizzes Taken";
        colTitles[12] = "Foods : Calories";
        colTitles[13] = "Exercises : Calories";
        colTitles[14] = "Quiz Questions : Results";

        rowData.Add(colTitles);

        int maxLength = Mathf.Max(foodsEaten.Count, exercises.Count, quizResults.Count);
        int rowCounter = 0;
        while (rowCounter <= maxLength)
        {
            string[] resultsRow = new string[15];
            if (rowCounter == 0)
            {
                resultsRow[0] = caloriesNet.ToString();
                resultsRow[1] = caloriesGained.ToString();
                resultsRow[2] = caloriesBurned.ToString();
                resultsRow[3] = numPhoneChecks.ToString();
                resultsRow[4] = numCalChecks.ToString();
                resultsRow[5] = numFoodAppOpens.ToString();
                resultsRow[6] = numOfType(orderType.APP).ToString();
                resultsRow[7] = numFridgeOpens.ToString();
                resultsRow[8] = numOfType(orderType.FRIDGE).ToString();
                resultsRow[9] = foodsEaten.Count.ToString();
                resultsRow[10] = exercises.Count.ToString();
                resultsRow[11] = quizResults.Count.ToString();
            }
            if (rowCounter < foodsEaten.Count)
            {
                (string, int, orderType) food = foodsEaten[rowCounter];
                resultsRow[12] = food.Item1.ToString() + " : " + food.Item2.ToString();
            }
            if (rowCounter < exercises.Count)
            {
                (string, int) exercise = exercises[rowCounter];
                resultsRow[13] = exercise.Item1.ToString() + " : " + exercise.Item2.ToString();
            }
            if (rowCounter < quizResults.Count)
            {
                (string, quizResult) quiz = quizResults[rowCounter];
                resultsRow[14] = quiz.Item1.ToString() + " : " + quiz.Item2.ToString();
            }

            rowData.Add(resultsRow);
            rowCounter++;
        }


        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

        string filePath = Application.dataPath + "/RESULTS/" + fileName + ".csv";

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }


}
