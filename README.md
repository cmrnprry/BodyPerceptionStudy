# BodyPerceptionStudy
# HOW TO USE THE STATS MANAGER

## INSTANCE
While scripting, to retrieve the static instace of the `StatsManager` object, call:
`StatsManager.Instance`

## ENUMS
There are two enum types to use:

`orderType.APP` and `orderType.FRIDGE` are used for indicating where a food item originated

`questionResult.AGREE`, `questionResult.DISAGREE`, and `questionResult.INDIFFERENT` are used to indicate the user response to a magazine quiz

## FUNCTIONS
While scripting, keep the following functions in mind:

To be used whenever a piece of food is consumed:
`public void addFood(string foodName, orderType orderType, int calories);` 

Where calories is the number of calories of that food

To be used whenever the player exercises:
`public void addExercise(string exerciseName, int calories);`

Where calories is A NEGATIVE NUMBER indicating the number of calories burnt in the exercise

To be used when a quiz is completed:
`public void addQuiz(string quizQuestion, questionResult quizResult);`

To be used whenever the player reads a book:
`public void addBook(string bookName, questionResult bookResult);`

To be used whenever the phone is opened:
`public void checkedPhone();`

To be used whenever the player checks calories:
`public void checkedCals();`

To be used whenever the player opens the food app:
`public void openedFoodApp();`

To be used whenever the fridge is opened:
`public void openedFridge();`

# How to Set Up Different Meals in Fridge
Click on the Refrigerator game object in the hierarchy (Room -> Kitchen Cabinet -> Refigerator). Under Choose A Meal (Script) there will be a label titled Foods, click that and in size, enter how many foods the participent will be able to see when they open the fridge. This will then bring up lists that can be populated with the food name and amount of calories.



