using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditsRoll : MonoBehaviour {
    bool creditsRolling = true;
    Text pauseButtonText;
    // Use this for initialization
    void Start()
    {
        pauseButtonText = GameObject.Find("Pause Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (creditsRolling)
        {
            foreach (Text singleText in GameObject.FindObjectsOfType<Text>())
            {
                if (!(singleText.name == "Credits" || singleText.name == "Pause Text"))
                    singleText.transform.Translate(new Vector3(0, 1));

                if (singleText.transform.position.y > 435)
                    singleText.transform.position = new Vector2(Screen.width / 2, -10);
            }
        }
    }

    public void PauseCredits()
    {
        creditsRolling = !creditsRolling;
        
        if (creditsRolling)
            pauseButtonText.text = "Pause Credits";
        else
            pauseButtonText.text = "Resume";
    }
}

