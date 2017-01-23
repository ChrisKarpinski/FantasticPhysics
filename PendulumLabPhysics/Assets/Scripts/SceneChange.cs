/****************************************************************************
*
* Created by: Chris Karpinski
* Created on: Jan 2017
* This script changes scenes
*
****************************************************************************/

using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {

	public void SceneChanger (string sceneName)
    {
        Application.LoadLevel(sceneName);
        
    }

    
}
