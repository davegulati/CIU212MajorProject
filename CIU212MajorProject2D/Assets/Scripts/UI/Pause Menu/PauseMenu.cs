using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        gameObject.transform.Find("PauseMenu").gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!gameObject.transform.Find("PauseMenu").gameObject.activeSelf)
            {
                PauseGame();
            }
            else if (gameObject.transform.Find("PauseMenu").gameObject.activeSelf)
            {
                
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        DisplayPauseMenu();
    }

    private void UnpauseGame ()
    {
        gameObject.transform.Find("PauseMenu").gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void DisplayPauseMenu()
    {
        gameObject.transform.Find("PauseMenu").gameObject.SetActive(true);
    }

    public void ResumeClicked()
    {
        UnpauseGame();
    }

    public void ControlsClicked()
    {
        gameObject.transform.Find("PauseMenu").transform.Find("PauseBox").gameObject.SetActive(false);
        // Display controls
    }

    public void ExitClicked ()
    {
        gameObject.transform.Find("PauseMenu").transform.Find("PauseBox").gameObject.SetActive(false);
        // Confirm exit box show
    }

    public void ExitYesClicked ()
    {
        SceneManager.LoadScene("2_MainMenu");
    }

    public void ExitNoClicked ()
    {
        // Confirm exit box hide
        gameObject.transform.Find("PauseMenu").transform.Find("PauseBox").gameObject.SetActive(true);
    }
}