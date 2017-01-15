using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using CorruptedSmileStudio.MessageBox;

public class QuizQuestions : MonoBehaviour {
    
    string optionSelected;
    List<int> levels = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    const int OPTION_NUMBER = 4;
    string [] textArray  = new string [OPTION_NUMBER];
    int score = 0;
    int questionsAnswered = 0;
    int currentLevel;
    string correctOption;
    Text questionTitle;
    

    // Use this for initialization

    enum Options
    {
        A,
        B,
        C,
        D
    }

	void Start () {

        questionTitle = GameObject.Find("Question").GetComponent<Text>();
        SelectNextQuestion();
        
    }
    
    void Update()
    {
        Text option1 = GameObject.Find("Option 1").GetComponent<Text>();
        Text option2 = GameObject.Find("Option 2").GetComponent<Text>();
        Text option3 = GameObject.Find("Option 3").GetComponent<Text>();
        Text option4 = GameObject.Find("Option 4").GetComponent<Text>();
        Text questionNum = GameObject.Find("Question Number").GetComponent<Text>();
        Text scoreValue = GameObject.Find("Score Value").GetComponent<Text>();
        Debug.Log(optionSelected + ", " + correctOption + ", " + score);

        scoreValue.text = score + " / " + questionsAnswered;
        
        questionNum.text = (questionsAnswered + 1).ToString();

        option1.text = textArray[0];
        option2.text = textArray[1];
        option3.text = textArray[2];
        option4.text = textArray[3];


    }

    void OnGUI ()
    {
        GUI.skin.button.fontSize = 20;
        if (GUI.Button(new Rect(50, 350, 100, 70), "A"))
        {

            optionSelected = "A";

        }

        if (GUI.Button(new Rect(50, 450, 100, 70), "B"))
        {
            optionSelected = "B";

        }

        if (GUI.Button(new Rect(50, 550, 100, 70), "C"))
        {
            optionSelected = "C";

        }

        if (GUI.Button(new Rect(50, 650, 100, 70), "D"))
        {
            optionSelected = "D";


        }

        if (GUI.changed)
        {
            if (questionsAnswered < 10)
            {
                questionsAnswered++;
            }
            if (CheckCorrect(optionSelected, correctOption))
            {
                MessageBox.Show(SelectNextQuestion, "You got the question correct! + 1 score!", "Correct!", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(SelectNextQuestion, "You answered incorrectly, the correct answer was: "
                    + correctOption, "Incorrect", MessageBoxButtons.OK);
            }
        }


    }

	void Shuffle (ref string [] array)
    {
            int n = array.Length;
            while (n > 1)
            {
                new Random();
                int k = Random.Range(0, n--);
                string temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
    
    }
	
    bool CheckCorrect (string optionSelected, string correctOption)
    {
        levels.Remove(currentLevel);
        if (optionSelected == correctOption)
        {
            score++;
            return true;
        }
        else
        {
            return false;
        }

    }

    void SelectNextQuestion (DialogResult result)
    {
        if (result == DialogResult.Ok)
        {
            SelectNextQuestion();
        }
    }

    void SelectNextQuestion()
    {

        if (levels.Count > 0)
        {
            new Random();
            currentLevel = levels[Random.Range(0, levels.Count)];


            switch (currentLevel)
            {
                case 1:
                    questionTitle.text = "Which of the following is correct for a pendulum?";
                    textArray[0] = "Period is proportional to length";
                    textArray[1] = "Period is proportional to the square root of length";
                    textArray[2] = "Period is proportional to the square root of bob mass";
                    textArray[3] = "Period is inversely proportional to the acceleration due to gravity";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "Period is proportional to the square root of length")).ToString();
                    break;

                case 2:
                    questionTitle.text = "Where is kinetic energy maximum in the pendulum's motion?";
                    textArray[0] = "Bottom of swing arc";
                    textArray[1] = "Top of swing arc";
                    textArray[2] = "Quarter of the way through swing";
                    textArray[3] = "Kinetic energy is constant throughout the motion";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "Bottom of swing arc")).ToString();
                    break;

                case 3:
                    questionTitle.text = "Where is potential energy maximum in the pendulum's motion?";
                    textArray[0] = "Bottom of swing arc";
                    textArray[1] = "Top of swing arc";
                    textArray[2] = "Quarter of the way through swing";
                    textArray[3] = "Potential energy is constant throughout the motion";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "Top of swing arc")).ToString();
                    break;

                case 4:
                    questionTitle.text = "How does total mechanical energy change during the motion?";
                    textArray[0] = "Decreases from top to bottom";
                    textArray[1] = "Increases from top to bottom";
                    textArray[2] = "Mechanical Energy is conserved throughout the motion";
                    textArray[3] = "Increases then decreases";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "Mechanical Energy is conserved throughout the motion")).ToString();
                    break;
                case 5:
                    questionTitle.text = "The pendulum formula for period is an approximation for...";
                    textArray[0] = "Small launch angles (less than around 10 degrees)";
                    textArray[1] = "Small total mechanical energy";
                    textArray[2] = "Large launch angles (around 90 degrees)";
                    textArray[3] = "Small bob mass";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "Small launch angles (less than around 10 degrees)")).ToString();
                    break;
                case 6:
                    questionTitle.text = "Which of the quantities has NO EFFECT on the pendulum's period?";
                    textArray[0] = "Launch angle";
                    textArray[1] = "String length";
                    textArray[2] = "Acceleration due to gravity";
                    textArray[3] = "Bob mass";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "Bob mass")).ToString();
                    break;
                case 7:
                    questionTitle.text = "The string length on a pendulum is increased by a factor of 4. What is the effect on the period?";
                    textArray[0] = "Increases by a factor of 4";
                    textArray[1] = "Decreases by a factor of 4";
                    textArray[2] = "Increases by a factor of 2";
                    textArray[3] = "Decreases by a factor of 2";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "Increases by a factor of 2")).ToString();
                    break;
                case 8:
                    questionTitle.text = "The length of a pendulum string is 1 m, what is its approximate period?";
                    textArray[0] = "1 s";
                    textArray[1] = "2 s";
                    textArray[2] = "3 s";
                    textArray[3] = "4 s";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "2 s")).ToString();
                    break;
                case 9:
                    questionTitle.text = "What is the period of a pendulum with string length 1 m on Mars (where g = 3.73 m/s^2)?";
                    textArray[0] = "2 s";
                    textArray[1] = "1 s";
                    textArray[2] = "0.5 s";
                    textArray[3] = "0.2 s";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "0.5 s")).ToString();
                    break;
                case 10:
                    questionTitle.text = "What effect does increasing the mass of the bob on a pendulum have?";
                    textArray[0] = "Increases the period";
                    textArray[1] = "Increases the maximum kinetic energy";
                    textArray[2] = "Increases the total mechanical energy";
                    textArray[3] = "Increases both the maximum kinetic energy AND mechanical energy";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "Increases both the maximum kinetic energy AND mechanical energy")).ToString();
                    break;

            }

            if (currentLevel == 7 || currentLevel == 8 || currentLevel == 9 || currentLevel == 10)
            {
                questionTitle.fontSize = 22;
            }
            else
            {
                questionTitle.fontSize = 30;
            }

        }
        else
        {
            MessageBox.Show(ExitCallBack, "You have finished all the levels. Your result is: " + score + " / " + levels.Count, "Finished", MessageBoxButtons.OK);
        }
    }

    void ExitCallBack (DialogResult result) 
    {
        if (result == DialogResult.Ok)
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
