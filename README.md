# BodyPerceptionStudy
# HOW TO USE THE STATS MANAGER

## INSTANCE
While scripting, to retrieve the static instace of the `StatsManager` object, call:
`StatsManager.Instance`

## ENUMS
There are two enum types to use:

`orderType.APP` and `orderType.FRIDGE` are used for indicating where a food item originated

`quizResult.AGREE`, `quizResult.DISAGREE`, and `quizResult.INDIFFERENT` are used to indicate the user response to a magazine quiz

## FUNCTIONS
While scripting, keep the following functions in mind:

To be used whenever a piece of food is consumed:
`public void addFood(string foodName, orderType orderType, int calories);` 

Where calories is the number of calories of that food

To be used whenever the player exercises:
`public void addExercise(string exerciseName, int calories);`

Where calories is A NEGATIVE NUMBER indicating the number of calories burnt in the exercise

To be used when a quiz is completed:
`public void addQuiz(string quizQuestion, quizResult quizResult);`

To be used whenever the phone is opened:
`public void checkedPhone();`

To be used whenever the player checks calories:
`public void checkedCals();`

To be used whenever the player opens the food app:
`public void openedFoodApp();`

To be used whenever the fridge is opened:
`public void openedFridge();`

# How to Set Up Choose-A-Meal Pop-ups

Attach script ChooseAMeal.cs to the GameObject which orders meals, a pop-up with choices will appear when the player/camera curses over it. In the Inspector, serialize the list of meals offfered with name + calorie count. TBC...

