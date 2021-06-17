using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "GameScene";

    public void clickStart()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ClickLoad()
    {

    }

    public void ClickExit()
    {

    }
}
