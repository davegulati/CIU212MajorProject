using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PrototypeBuild : MonoBehaviour {

    private Text text_Countdown;
    private float waitTime = 10;

	// Use this for initialization
	void Start () 
    {
        text_Countdown = GameObject.Find("Canvas").transform.Find("Text_Countdown").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        waitTime -= Time.deltaTime;
        text_Countdown.text = "Game beginning in " + waitTime.ToString("0.0") + " seconds...\nPress 'Space' to begin now.";

        if (waitTime <= 0)
        {
            SceneManager.LoadScene("2_MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
			SceneManager.LoadScene("2_MainMenu");
		}
	}
}
