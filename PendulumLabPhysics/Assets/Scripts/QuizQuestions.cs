/****************************************************************************
*
* Created by: Chris Karpinski
* Created on: Jan 2017
* This scene is a quiz to test the knowledge of each of the labs
* I'm using an imported MessageBox asset in this scene that I purchased online
*
****************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using CorruptedSmileStudio.MessageBox;

public class QuizQuestions : MonoBehaviour {
    
    // fields 
    string optionSelected;
    List<int> levels = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
    const int OPTION_NUMBER = 4;
    const int INITIAL_QUESTION_NUMBER = 15;
    string [] textArray  = new string [OPTION_NUMBER];
    int score = 0;
    int questionsAnswered = 0;
    int currentLevel;
    string correctOption;
    Text questionTitle;
    float originalHeight = 768;
    float originalWidth = 1024;
    // original screen resolution for re-scaling GUI buttons
    Vector3 scale;

    // Use this for initialization

    enum Options
    {
        // enum for the options 
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
        // display all the text and score
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
        // all of the code done with scales and matrices is to re-scale the GUI to any screen size
        scale.x = Screen.width / originalWidth; // calculate hor scale
        scale.y = Screen.height / originalHeight; // calculate vert scale
        scale.z = 1;
        Matrix4x4 svMat = GUI.matrix; // save current matrix
                                // substitute matrix - only scale is altered from standard
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
        GUI.skin.button.fontSize = 20;

        // GUI buttons for the options
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
            if (questionsAnswered < INITIAL_QUESTION_NUMBER)
            {
                questionsAnswered++;
            }
            if (CheckCorrect(optionSelected, correctOption))
            {
                MessageBox.Show(SelectNextQuestion, "You got the question correct! + 1 score!", "Correct!", MessageBoxButtons.OK);
                // message box for correct answer
            }
            else
            {
                MessageBox.Show(SelectNextQuestion, "You answered incorrectly, the correct answer was: "
                    + correctOption, "Incorrect", MessageBoxButtons.OK); // message box for incorrect answer
            }
            GameObject.Find("Title").GetComponent<AudioSource>().Play();
        }

        // restore matrix before returning
        GUI.matrix = svMat; // restore matrix
        

    }

	void Shuffle (ref string [] array)
    {
        // shuffles the options so that they dont always appear in the same position
            int lastArrayValue = array.Length;
            while (lastArrayValue > 1)
            {
                new Random();
                int newIndex = Random.Range(0, lastArrayValue--);
                string temp = array[lastArrayValue];
                array[lastArrayValue] = array[newIndex];
                array[newIndex] = temp;
            }
    
    }
	
    bool CheckCorrect (string optionSelected, string correctOption)
    {
        // checks if the level is correct
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
        // selects the next question and is a callback for the MessageBox
        if (result == DialogResult.Ok)
        {
            SelectNextQuestion();
        }
    }

    void SelectNextQuestion()
    {
        // select the next question
        if (levels.Count > 0)
        {
            new Random();
            currentLevel = levels[Random.Range(0, levels.Count)];


            switch (currentLevel)
            {
                // sets the question and options for each question
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
                case 11:
                    questionTitle.text = "The normal force on an object can effectively be thought of as what?";
                    textArray[0] = "The force of the surface pushing against the object";
                    textArray[1] = "The force of friction";
                    textArray[2] = "The net force on the object";
                    textArray[3] = "The force of gravity";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "The force of the surface pushing against the object")).ToString();
                    break;
                case 12:
                    questionTitle.text = "What is the relationship between the force of friction (Ff) and the normal force (Fn)?";
                    textArray[0] = "Ff = FN*g";
                    textArray[1] = "Ff = FN*Fg";
                    textArray[2] = "Ff = FN * coeff of friction";
                    textArray[3] = "Ff = FN";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "Ff = FN * coeff of friction")).ToString();
                    break;
                case 13:
                    questionTitle.text = "Which pairs of forces are always directed opposite to each other?";
                    textArray[0] = "Fg and FN";
                    textArray[1] = "Ff and FNet";
                    textArray[2] = "Ff and FNet";
                    textArray[3] = "Fg and Ff";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "Ff and FNet")).ToString();
                    break;
                case 14:
                    questionTitle.text = "Which of Newton's laws states that F = ma?";
                    textArray[0] = "1st";
                    textArray[1] = "2nd";
                    textArray[2] = "3rd";
                    textArray[3] = "4th";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "2nd")).ToString();
                    break;
                case 15:
                    questionTitle.text = "What happens to the frictional force as the incline angle increases?";
                    textArray[0] = "Increases";
                    textArray[1] = "Stays the same because the coefficient of friction is unchanged";
                    textArray[2] = "Decreases because the coefficient of friction decreases";
                    textArray[3] = "Decreases because the normal force decreases";
                    Shuffle(ref textArray);
                    correctOption = ((Options)System.Array.IndexOf(textArray, "Decreases because the normal force decreases")).ToString();
                    break;

            }

            // adjust the font size of questions that are longer than others
            if (currentLevel == 7 || currentLevel == 8 || currentLevel == 9 || currentLevel == 10 || currentLevel == 12)
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
            MessageBox.Show(ExitCallBack, "You have finished all the levels. Your result is: " + score + " / " + INITIAL_QUESTION_NUMBER, "Finished", MessageBoxButtons.OK);
            // display the message box saying the levels have been finished
        }
    }

    void ExitCallBack (DialogResult result) 
    {
        // callback to exit the scene once all of the questions have been completed
        if (result == DialogResult.Ok)
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
