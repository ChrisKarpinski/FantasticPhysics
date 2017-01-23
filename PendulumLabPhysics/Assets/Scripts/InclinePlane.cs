/****************************************************************************
*
* Created by: Chris Karpinski
* Created on: Jan 2017
* This scene simulates an inclined plane experience
*
****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InclinePlane : MonoBehaviour {

    // fields
    float gravityForce;
    float normalForce;
    float frictionForce;
    float netForce;
    float acceleration;
    
	// Use this for initialization
	void Start () {

        
        Physics2D.gravity = new Vector2(0, -9.81f); // reset the acceleration due to gravity 

	}
	
	// Update is called once per frame
	void Update () {


        

        gravityForce = GameObject.Find("Mass").GetComponent<Slider>().value
           * GameObject.Find("Gravity").GetComponent<Slider>().value;

        normalForce = gravityForce * Mathf.Cos(Mathf.PI / 4);

        frictionForce = GameObject.Find("Frictional Coeff").GetComponent<Slider>().value * normalForce;

        netForce = gravityForce * Mathf.Sin(Mathf.PI / 4) - frictionForce;

        acceleration = netForce / GameObject.Find("block").GetComponent<Rigidbody2D>().mass;
        // above - calculating all of the forces and net acceleration
       
        GameObject.Find("Accel val").GetComponent<Text>().text = acceleration.ToString("F2") + " m/s^2";
        GameObject.Find("FF val").GetComponent<Text>().text = frictionForce.ToString("F2") + " N";
        GameObject.Find("GF val").GetComponent<Text>().text = gravityForce.ToString("F2") + " N";
        GameObject.Find("NF val").GetComponent<Text>().text = normalForce.ToString("F2") + " N";
        GameObject.Find("Net val").GetComponent<Text>().text = netForce.ToString("F2") + " N";
        // displaying those values as text

        
	}

    public void EnablePhysics()
    {
        // Enable and disable the physics on the object
        if (GameObject.Find("Start Text").GetComponent<Text>().text == "Start Demo" || GameObject.Find("Start Text").GetComponent<Text>().text == "Continue")
        {

            GameObject.Find("block").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; 
            // enable the physics on the object by making it dynamic (responding to forces)

            GameObject.Find("Start Text").GetComponent<Text>().text = "Pause";
        }
        else
        {

            GameObject.Find("block").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; 
            // disable the physics to pause by making the object static (not responding to forces)
            GameObject.Find("Start Text").GetComponent<Text>().text = "Continue";
        }
    }

    public void AdjustGravity ()
    {
        // adjusts the acceleration due to gravity
        float grav = GameObject.Find("Gravity").GetComponent<Slider>().value;
        Physics2D.gravity = new Vector2(0, -grav);
        GameObject.Find("Gravity Txt").GetComponent<Text>().text = grav.ToString("F2") + " m/s^2";
    }

    public void AdjustMass()
    {
        // adjusts the mass
        float blockMass = GameObject.Find("Mass").GetComponent<Slider>().value;
        GameObject.Find("block").GetComponent<Rigidbody2D>().mass = blockMass;
        GameObject.Find("Mass Val").GetComponent<Text>().text = blockMass.ToString("F2") + " kg";
    }

    public void AdjustFriction ()
    {
        // adjusts the frictional force
        float frictionValue = GameObject.Find("Frictional Coeff").GetComponent<Slider>().value;
        GameObject.Find("hyp").GetComponent<BoxCollider2D>().sharedMaterial.friction = frictionValue;
        GameObject.Find("Mu").GetComponent<Text>().text = frictionValue.ToString("F2");
    }

    public void Reset ()
    {
        // resets the simulation
        GameObject.Find("block").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // turns off physics
        GameObject.Find("block").GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0.4f, 0.9f); // resets rotation
        GameObject.Find("block").GetComponent<RectTransform>().position = new Vector3(1.1f, 1.7f, 90); // resets position

        
        GameObject.Find("Start Text").GetComponent<Text>().text = "Start Demo";

        foreach (Slider singleSlider in GameObject.FindObjectsOfType<Slider>())
        {
            singleSlider.value = singleSlider.minValue;
        }

    }
}
