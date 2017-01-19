/****************************************************************************
*
* Created by: Chris Karpinski
* Created on: Jan 2017
* This class models the pendulum behavior in the scene and controls the scene
* functionality
*
****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetAngleRotation : MonoBehaviour {

    // fields 
    bool simulationOn = false;
    float timeValue = 0;
    float period = 0;
    float savedSpeed = 0;
    Vector2 savedVelocity = Vector2.zero;
    float originalHeight;
    bool restart;
    HingeJoint2D hinge;
    float kineticEnergy = 0;
    float potentialEnergy = 0;
    float mechanicalEnergy = 0;
    public GameObject angle;
    // Use this for initialization
    void Start() {
        hinge = GameObject.Find("string").GetComponent<HingeJoint2D>();
        angle = GameObject.Find("Angle Slider");
        originalHeight = GameObject.Find("pendulum").GetComponent<RectTransform>().position.y;
        DisablePhysics();
    }

    void Update() {
        Text timeText = GameObject.Find("Time Text").GetComponent<Text>();
        Text periodText = GameObject.Find("Period Text").GetComponent<Text>();
        Text periodLabel = GameObject.Find("Period").GetComponent<Text>();
        Text kineticText = GameObject.Find("Kinetic Text").GetComponent<Text>();
        Text potentialText = GameObject.Find("Potential Text").GetComponent<Text>();
        Text totalEnergy = GameObject.Find("Total Energy").GetComponent<Text>();

        //Debug.Log(Input.mousePosition.x + ", " + Input.mousePosition.y);
        //Debug.Log(GameObject.Find("string").GetComponent<Rigidbody2D>().velocity + ", " + savedVelocity);
        //Debug.Log(originalHeight + ", " + Input.mousePosition.y + ", " + GameObject.Find("pendulum").GetComponent<RectTransform>().position.y.ToString("F5"));
        //Debug.Log(GameObject.Find("pendulum").GetComponent<Rigidbody2D>().mass);
        //Debug.Log(GameObject.Find("string").GetComponent<Rigidbody2D>().velocity.magnitude + ", " + angle.GetComponent<Slider>().value);
        Debug.Log(Time.timeScale);
        //Debug.Log((GameObject.Find("pendulum").GetComponent<RectTransform>().position.y - originalHeight)*124/232);
        //Debug.Log(GameObject.Find("string").GetComponent<Rigidbody2D>().velocity.sqrMagnitude);
        //Debug.Log(0.5*GameObject.Find("pendulum").GetComponent<Rigidbody2D>().angularVelocity);
        //Debug.Log(Input.mousePosition.y);
        //Debug.Log((Mathf.Pow(124,2)/Mathf.Pow(232,2))/2);
        //Debug.Log(GameObject.Find("string").GetComponent<HingeJoint2D>().)

        

        if (simulationOn)
        {
            timeValue += Time.deltaTime;
            // increment the time scale each frame to make a timer
        }

        timeText.text = timeValue.ToString("F2") + " s";

        // below calculating the kinetic, potential and net mechanical energies 

        if (GameObject.Find("Demo Text").GetComponent<Text>().text == "Continue")
        {
            kineticEnergy = GameObject.Find("Mass Slider").GetComponent<Slider>().value * savedVelocity.sqrMagnitude * (Mathf.Pow(155, 2) / Mathf.Pow(232, 2)) / 2 / 18 / 1.8f;

        }

        else
        {
            kineticEnergy = GameObject.Find("Mass Slider").GetComponent<Slider>().value * GameObject.Find("string").GetComponent<Rigidbody2D>()
                .velocity.sqrMagnitude * (Mathf.Pow(155, 2) / Mathf.Pow(232, 2)) / 2 / 18 / 1.8f;
        }


        if (timeValue > 0)
        {
            potentialEnergy = GameObject.Find("Mass Slider").GetComponent<Slider>().value * Physics2D.gravity.magnitude / 155 * (GameObject.Find("pendulum").GetComponent<RectTransform>
                ().position.y - originalHeight) * 155 / 232;


            mechanicalEnergy = potentialEnergy + kineticEnergy;
        }



        if ((GameObject.Find("string").GetComponent<Rigidbody2D>().velocity.magnitude/100 < 0.1 || (GameObject.Find("string").GetComponent<Rigidbody2D>().velocity.magnitude < 30 
            && angle.GetComponent<Slider>().value > 45)) && period == 0
            && GameObject.Find("string").GetComponent<RectTransform>().rotation.z > 0 && timeValue > 1
            && GameObject.Find("Demo Text").GetComponent<Text>().text != "Continue" && simulationOn)
        {
            // record the period
            period = timeValue;
        }

        kineticText.text = kineticEnergy.ToString("F2") + " J";
        potentialText.text = potentialEnergy.ToString("F2") + " J";
        totalEnergy.text = mechanicalEnergy.ToString("F2") + " J";

        if (period != 0)
        {
            periodLabel.text = "Period: ";
            periodText.text = period.ToString("F2") + " s";
        }
        else
        {
            periodText.text = "";
            periodLabel.text = "";
        }

        if (GameObject.Find("string").GetComponent<RectTransform>().transform.eulerAngles.z < 10
                && GameObject.Find("string").GetComponent<RectTransform>().transform.eulerAngles.z > -10
                && GameObject.Find("string").GetComponent<Rigidbody2D>().velocity.magnitude > 5)
        {
            GameObject.Find("pendulum").GetComponent<AudioSource>().Play();
        }

        if (GameObject.Find("Demo Text").GetComponent<Text>().text == "Pause Demo")
        {
            simulationOn = true;
        }
        else
        {
            simulationOn = false;
        }

        if (hinge.useLimits)
        {
            timeValue = 0;
            potentialEnergy = 0;
            period = 0;
        }

    }

    public void OnApply ()
    {
        // change the launch angle of the pendulum
        hinge.useLimits = false;
        GameObject fixedJoint = GameObject.Find("string");
        Text releaseAngle = GameObject.Find("Release Angle").GetComponent<Text>();
        float anglePendulum = GameObject.Find("Angle Slider").GetComponent<Slider>().value;
        fixedJoint.transform.rotation = Quaternion.AngleAxis(anglePendulum, Vector3.forward);
        releaseAngle.text = anglePendulum.ToString("F2");
    }

    public void EnablePhysics ()
    {
        // this enables the physics and controls the on and off phases of the simulation
        Text demoButtonText = GameObject.Find("Demo Text").GetComponent<Text>();
        DisableSliders();
        GameObject.Find("No Slider").GetComponent<Text>().enabled = true;

        if (demoButtonText.text == "Start Demo" || demoButtonText.text == "Continue")
        {
            
            // re-enable the motion of the pendulum if its just starting or if it's continuing
            // after being paused
            GameObject.Find("string").GetComponent<Rigidbody2D>().gravityScale = 1;
            GameObject.Find("pendulum").GetComponent<Rigidbody2D>().gravityScale = 1;
            GameObject.Find("string").GetComponent<Rigidbody2D>().velocity = savedVelocity;
            GameObject.Find("string").GetComponent<Rigidbody2D>().angularVelocity = savedSpeed;
            demoButtonText.text = "Pause Demo";
        }
        else
        {
            DisablePhysics();
            
            simulationOn = false;
            demoButtonText.text = "Continue";
        }

        
    }

    public void SlowDownSimulation ()
    {
        float slowFactor = GameObject.Find("Speed Slider").GetComponent<Slider>().value;
        Time.timeScale = 1 / slowFactor;
    }

    void DisableSliders()
    {
        // disables each slider
        foreach (Slider singleSlider in GameObject.FindObjectsOfType<Slider>())
        {
            if (singleSlider.name != "Speed Slider")
            {
                singleSlider.enabled = false;
            }
        }
    }

    void EnableSliders()
    {
        // enables the sliders 
        foreach (Slider singleSlider in GameObject.FindObjectsOfType<Slider>())
        {
            singleSlider.enabled = true;
            
        }
    }

    public void DisablePhysics ()
    {
        // disable the gravity and speed of the pendulum if the simulation is paused
        GameObject.Find("string").GetComponent<Rigidbody2D>().gravityScale = 0;
        GameObject.Find("pendulum").GetComponent<Rigidbody2D>().gravityScale = 0;
        // save the velocity and angular speed of the pendulum so that we know what speed
        // it should be moving once the simulation is restarted.
        savedSpeed = GameObject.Find("string").GetComponent<Rigidbody2D>().angularVelocity;
        savedVelocity = GameObject.Find("string").GetComponent<Rigidbody2D>().velocity;
        GameObject.Find("string").GetComponent<Rigidbody2D>().angularVelocity = 0;
        GameObject.Find("string").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //GameObject.Find("pendulum").GetComponent<AudioSource>().loop = false;

    }

    public void SetLength ()
    {
        // adjust the pendulum string length
        GameObject stringLength = GameObject.Find("string");
        GameObject pendulum = GameObject.Find("pendulum");
        Text lengthText = GameObject.Find("Length val").GetComponent<Text>();
        float lengthSlider = GameObject.Find("Length Slider").GetComponent<Slider>().value;
        //stringLength.transform.DetachChildren();
        stringLength.transform.localScale = new Vector3(1, lengthSlider, 0);
        // stretches out the string length by the slider factor
        pendulum.transform.localScale = new Vector3(1, 1/lengthSlider);
        // stretches the pendulum bob by the inverse of the length slider to preserve its shape
        // (since we only need the string to be stretched and not the mass).
        lengthText.text = lengthSlider.ToString("F2") + " m";
        originalHeight = 295.6f - (225.5f * lengthSlider - 225.5f);
        // get the starting height of the pendulum by subtracting the change in length of the string
        // from the height of the mass at string length of 1 m for potential energy calculations.
    }

    public void SetMass ()
    {
        // adjusts the mass of the pendulum bob
        Text massText = GameObject.Find("Mass val").GetComponent<Text>();
        float massSlider = GameObject.Find("Mass Slider").GetComponent<Slider>().value;
        GameObject.Find("pendulum").GetComponent<Rigidbody2D>().mass = massSlider;
        massText.text = massSlider.ToString("F2") + " kg"; 
    }

    public void SetGravity ()
    {
        // this procedure sets the acceleration due to gravity using the gravity acceleration slider
        Text accelerationGravity = GameObject.Find("Acceleration val").GetComponent<Text>();
        float gravitySlider = GameObject.Find("Gravity Slider").GetComponent<Slider>().value;
        Physics2D.gravity = new Vector2(0, -gravitySlider*190);
        // the gravity slider value is being multiplied by 124 to compensate for the string length difference 
        // (if i leave gravity at 9.8, the period will give a string length of about 124 m (at min length), so since i want a minimum length of 
        // 1 m, I have to multiply gravity by this factor to 124 to give accurate pendulum motion for 1 m string length
        accelerationGravity.text = gravitySlider.ToString("F2") + " m / s^2 [DOWNWARDS]";
    }

    public void ResetDemo ()
    {
        // this procedure resets the pendulum demo back to its initial state
        EnableSliders();
        // enable all the sliders
        foreach (Slider singleSlider in GameObject.FindObjectsOfType<Slider>()) // reset the slider values back to their minimum (starting values)
        {
            singleSlider.value = singleSlider.minValue;
            // reset all the sliders to their minimum value
        }

        DisablePhysics(); // disable the physics
       
        JointAngleLimits2D limits = hinge.limits;
        
        
        hinge.useLimits = true;
        limits.min = 0;
        limits.max = 0;
        // set the pendulum angle to 0
        GameObject.Find("No Slider").GetComponent<Text>().enabled = false;

        Text demoButtonText = GameObject.Find("Demo Text").GetComponent<Text>();
        simulationOn = false;
        demoButtonText.text = "Start Demo";
        period = 0;
        timeValue = 0;
        kineticEnergy = 0;
        potentialEnergy = 0;
        mechanicalEnergy = 0;
        savedVelocity = Vector2.zero;
        savedSpeed = 0;
        // reset all the above fields to their initial values
    }
}


