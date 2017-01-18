using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PageChange : MonoBehaviour {

    int page = 1;
    public Text par1;
    public Text par2;
    public Text par3;
    public Text par4;
    bool textDisabled;
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        switch (page)
        {
            case 1:
                image1.enabled = true;
                image2.enabled = false;
                image3.enabled = true;
                image4.enabled = false;
                par1.text = "From the pendulum experience, the period of a pendulum and other quantities such as energy can be analyzed.";
                par2.text = "The pendulum period formula can be derived from considering approximate circular motion of the pendulum for small angles.";
                par3.text = "Where T is the pendulum period (s), L is the string length (m) and g is the acceleration due to gravity (m/s^2).";
                par4.text = "Notice that this formula does not include any angle value or mass. This is because the period is not affected by the mass and is not affected by angle changes (for sufficiently small angles).";
                break;
            case 2:
                image1.enabled = true;
                image2.enabled = true;
                image5.enabled = false;
                image3.enabled = false;
                image4.enabled = true;
                textDisabled = false;
                par1.text = "The bob mass and launch angle don't affect the period, but they do affect the energies.";
                par2.text = "Larger masses and larger launch angles will increase the kinetic energy (increasing speed), potential energy and total mechanical energy.";
                par3.text = "However, as you will notice in the simulation, mechanical energy is not conserved but gradually decreases.";
                par4.text = "This is because energy is lost due to friction. In a frictionless environment, mechanical energy is always conserved and is the sum of kinetic and potential energies.";
                break;
            case 3:
                image4.enabled = false;
                image1.enabled = false;
                textDisabled = true;
                image5.enabled = true;
                break;
        }

        if (textDisabled)
        {
            foreach (Text singleText in GameObject.FindObjectsOfType<Text>())
            {
                if (singleText.name != "Title" && singleText.name != "Text")
                {
                    singleText.enabled = false;
                }
            }
        }
        else
        {
            foreach (Text singleText in GameObject.FindObjectsOfType<Text>())
            {
                if (singleText.name != "Title" && singleText.name != "Text")
                {
                    singleText.enabled = true;
                }
            }
        }


	}

    public void IncrementPage ()
    {
        
        
        if (page < 3)
        {
            page++;
        }
    }

    public void DecrementPage()
    {
        
        if (page > 1)
        {
            page--;
        }
    }
}
