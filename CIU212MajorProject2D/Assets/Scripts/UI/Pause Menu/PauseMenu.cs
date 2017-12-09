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
                UnpauseGame();
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
        gameObject.transform.Find("PauseMenu").transform.Find("ControlsBox").gameObject.SetActive(false);
        gameObject.transform.Find("PauseMenu").transform.Find("ConfirmQuitBox").gameObject.SetActive(false);
        gameObject.transform.Find("PauseMenu").transform.Find("Button_Back").gameObject.SetActive(false);
    }

    public void ResumeClicked()
    {
        UnpauseGame();
    }

    public void ControlsClicked()
    {
        gameObject.transform.Find("PauseMenu").transform.Find("PauseBox").gameObject.SetActive(false);
        gameObject.transform.Find("PauseMenu").transform.Find("ControlsBox").gameObject.SetActive(true);
        gameObject.transform.Find("PauseMenu").transform.Find("Button_Back").gameObject.SetActive(true);
    }

    public void BackClicked()
    {
        gameObject.transform.Find("PauseMenu").transform.Find("PauseBox").gameObject.SetActive(true);
        gameObject.transform.Find("PauseMenu").transform.Find("ControlsBox").gameObject.SetActive(false);
        gameObject.transform.Find("PauseMenu").transform.Find("Button_Back").gameObject.SetActive(false);
    }

    public void ExitClicked ()
    {
        gameObject.transform.Find("PauseMenu").transform.Find("PauseBox").gameObject.SetActive(false);
        gameObject.transform.Find("PauseMenu").transform.Find("ConfirmQuitBox").gameObject.SetActive(true);
    }

    public void ExitYesClicked ()
    {
        SceneManager.LoadScene("2_MainMenu");
    }

    public void ExitNoClicked ()
    {
        gameObject.transform.Find("PauseMenu").transform.Find("ConfirmQuitBox").gameObject.SetActive(false);
        gameObject.transform.Find("PauseMenu").transform.Find("PauseBox").gameObject.SetActive(true);
    }
}