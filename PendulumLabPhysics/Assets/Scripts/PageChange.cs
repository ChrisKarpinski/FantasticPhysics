/****************************************************************************
*
* Created by: Chris Karpinski
* Created on: Jan 2017
* This scene displays information about each experiment
*
****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PageChange : MonoBehaviour {

    // fields
    int page = 1;
    const int PAGES = 5;
    public Text title;
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
    public Image image6;
    public Image image7;

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
                par4.text = "This is because energy is lost due to friction. In a closed frictionless environment, mechanical energy is always conserved and is the sum of kinetic and potential energies.";
                break;
            case 3:
                image4.enabled = false;
                textDisabled = true;
                image5.enabled = true;
                image6.enabled = false;
                break;
            case 4:
                textDisabled = false;
                image5.enabled = false;
                image6.enabled = true;
                image7.enabled = false;
                title.text = "Inclined Plane Lab";
                par1.text = "From the inclined plane experience, various forces acting on the mass on the plane can be analyzed.";
                par2.text = "The diagram below illustrates the main forces acting on the mass on the plane.";
                par3.text = "The net force (the sum total of all the forces) acts in a direction downwards along the plane.";
                par4.text = "Friction is a rubbing force between the two surfaces that opposes the sliding motion of the mass.";
                break;
            case 5:
                textDisabled = false;
                image6.enabled = false;
                image7.enabled = true;
                image1.enabled = false;
                title.text = "Inclined Plane force analysis";
                par1.text = "The forces acting on the mass can be analyzed using a force diagram called a free body diagram.";
                par2.text = "From vector analysis of the free body diagram, the following conclusions can be made:";
                
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
        // increment the page
        
        if (page < PAGES)
        {
            page++;
            GameObject.Find("Front").GetComponent<AudioSource>().Play();
        }
    }

    public void DecrementPage()
    {
        // decrement the page
        if (page > 1)
        {
            page--;
            GameObject.Find("Front").GetComponent<AudioSource>().Play();
        }
        
        
    }
}
