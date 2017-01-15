using UnityEngine;
using System.Collections;

public class SceneChangeLogo : MonoBehaviour {

    IEnumerator ChangingScene()
    {
        Debug.Log("Before waiting 2 seconds");
        yield return new WaitForSeconds(2);
        Debug.Log("After waiting 2 seconds");
        Application.LoadLevel("MainMenu");

    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine(ChangingScene());


    }
}
