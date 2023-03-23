using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MainMenu
{
    public void OnButtonPress()
    {
        SceneManager.LoadScene(2);
        Debug.Log("loading play");
    }
}
