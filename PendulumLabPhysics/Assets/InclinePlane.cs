using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InclinePlane : MonoBehaviour {


    public Slider gravitySlider;
    public Slider massSlider;
    public Slider angleSlider;
    public Slider frictionSlider;
	// Use this for initialization
	void Start () {

        gravitySlider = GameObject.Find("Gravity").GetComponent<Slider>();
        massSlider = GameObject.Find("Mass").GetComponent<Slider>();
        angleSlider = GameObject.Find("Angle").GetComponent<Slider>();
        frictionSlider = GameObject.Find("Frictional Coeff").GetComponent<Slider>();

        Physics2D.gravity = new Vector2(0, -9.81f);

	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(Input.mousePosition.x + ", " + Input.mousePosition.y);
        //Debug.Log(Physics2D.gravity.magnitude);gra
        Debug.Log(gravitySlider.value);

	}

    public void EnablePhysics()
    {
        if (GameObject.Find("Start Text").GetComponent<Text>().text == "Start Demo" || GameObject.Find("Start Text").GetComponent<Text>().text == "Continue")
        {

            GameObject.Find("block").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            GameObject.Find("Start Text").GetComponent<Text>().text = "Pause";
        }
        else
        {

            GameObject.Find("block").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GameObject.Find("Start Text").GetComponent<Text>().text = "Continue";
        }
    }

    public void AdjustGravity ()
    {
        Physics2D.gravity = new Vector2(0, -gravitySlider.value);
    }

    public void AdjustMass()
    {
        GameObject.Find("block").GetComponent<Rigidbody2D>().mass = massSlider.value;
    }

    public void AdjustAngle ()
    {
        float angle = angleSlider.value;
        float height = GameObject.Find("opp").GetComponent<RectTransform>().rect.y;
        height = angleSlider.value;
        GameObject.Find("Triangle").transform.rotation = Quaternion.AngleAxis(angleSlider.value, Vector3.forward);
    }

    public void AdjustFriction ()
    {
        GameObject.Find("hyp").GetComponent<PhysicsMaterial2D>().friction = frictionSlider.value;
    }

    public void Reset ()
    {
        GameObject.Find("block").GetComponent<RectTransform>().position = new Vector3(600, 529, 0);
        GameObject.Find("block").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GameObject.Find("Start Text").GetComponent<Text>().text = "Start Demo";

        foreach (Slider singleSlider in GameObject.FindObjectsOfType<Slider>())
        {
            singleSlider.value = singleSlider.minValue;
        }



    }
}
