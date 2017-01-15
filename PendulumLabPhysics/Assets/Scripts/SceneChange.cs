using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {

	public void SceneChanger (string sceneName)
    {
        Application.LoadLevel(sceneName);
        
    }

    
}
