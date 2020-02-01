using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;


//  The code for initializing the class and deleting any duplicates was used from
//  https://gamedev.stackexchange.com/questions/116009/in-unity-how-do-i-correctly-implement-the-singleton-pattern
public class StatsManager : MonoBehaviour
{
    //  Class getter and static instance
    private static StatsManager managerInstance;
    public static StatsManager Instance {  get { return managerInstance;} }

    //  STATS TO TRACK:
    //  Calorie Stats:
    private int caloriesTracked = 2000;
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
    // private int booksRead = bookResults.count;


    //  Lists
    private List<(string, int, orderType)> foodsEaten = new List<(string, int, orderType)>();
    private List<(string, int)> exercises = new List<(string, int)>();
    private List<(string, questionResult)> quizResults = new List<(string, questionResult)>();
    private List<(string, questionResult)> bookResults = new List<(string, questionResult)>();
    

    //  Enums:
    public enum orderType { APP, FRIDGE };
    public enum questionResult { AGREE, DISAGREE, INDIFFERENT };

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

    //  Methods for adjusting stats from outside
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
        caloriesTracked += calories;
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

    public void addQuiz(string quizQuestion, questionResult quizResult)
    {
        quizResults.Add((quizQuestion, quizResult));
    }

    public void addBook(string bookName, questionResult bookResult)
    {
        bookResults.Add((bookName, bookResult));
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

    public int getCurCalories()
    {
        return caloriesTracked;
    }



    // Private methods
    private int numOfType(orderType orderType)
    {
        return foodsEaten.FindAll(item => item.Item3 == orderType).Count;
    }

    //  Code from https://sushanta1991.blogspot.com/2015/02/how-to-write-data-to-csv-file-in-unity.html
    public void saveToCSV(string fileName)
    {
        Debug.Log("Saving Results...");
        List<string[]> rowData = new List<string[]>();
        string[] colTitles = new string[17];
        colTitles[0] = "Total Calroies : Net Calroies";
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
        colTitles[12] = "Books Read";
        colTitles[13] = "Foods : Calories";
        colTitles[14] = "Exercises : Calories";
        colTitles[15] = "Quiz Questions : Results";
        colTitles[16] = "Book Names : Results";

        rowData.Add(colTitles);

        int maxLength = Mathf.Max(foodsEaten.Count, exercises.Count, quizResults.Count, bookResults.Count);
        int rowCounter = 0;
        while (rowCounter <= maxLength)
        {
            string[] resultsRow = new string[17];
            if (rowCounter == 0)
            {
                resultsRow[0] = caloriesTracked.ToString() + " : " + caloriesNet.ToString();
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
                resultsRow[12] = bookResults.Count.ToString();
            }
            if (rowCounter < foodsEaten.Count)
            {
                (string, int, orderType) food = foodsEaten[rowCounter];
                resultsRow[13] = food.Item1.ToString() + " : " + food.Item2.ToString();
            }
            if (rowCounter < exercises.Count)
            {
                (string, int) exercise = exercises[rowCounter];
                resultsRow[14] = exercise.Item1.ToString() + " : " + exercise.Item2.ToString();
            }
            if (rowCounter < quizResults.Count)
            {
                (string, questionResult) quiz = quizResults[rowCounter];
                resultsRow[15] = quiz.Item1.ToString() + " : " + quiz.Item2.ToString();
            }
            if (rowCounter < bookResults.Count)
            {
                (string, questionResult) book = bookResults[rowCounter];
                resultsRow[16] = book.Item1.ToString() + " : " + book.Item2.ToString();
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
        Debug.Log("Results Saved!");
    }


}
