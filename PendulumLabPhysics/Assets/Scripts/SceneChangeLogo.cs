/****************************************************************************
*
* Created by: Chris Karpinski
* Created on: Jan 2017
* This script displays the app logo for 2 seconds 
*
****************************************************************************/

using UnityEngine;
using System.Collections;

public class SceneChangeLogo : MonoBehaviour {

    IEnumerator ChangingScene()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel("MainMenu");

    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine(ChangingScene());


    }
}
