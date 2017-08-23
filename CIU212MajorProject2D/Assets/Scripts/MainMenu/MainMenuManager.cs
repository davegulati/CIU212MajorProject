using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public void Button_Play_Clicked ()
    {
        SceneManager.LoadScene("SafeZoneGreybox");
    }

    public void Button_Options_Clicked ()
    {
        
    }

    public void Button_Quit_Clicked ()
    {
        Application.Quit();
    }
}
