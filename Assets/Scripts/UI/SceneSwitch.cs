using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public string sceneName;  // 씬 이름：Inspector에 지정

    public void Switch() 
	{
        SceneManager.LoadScene(sceneName);
    }
}
