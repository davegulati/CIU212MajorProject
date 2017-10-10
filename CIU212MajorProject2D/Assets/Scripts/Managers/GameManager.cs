using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Load scene variables.
    private GameObject loadingScreen;
    [Header("Loading Screen Variables")]
    public Sprite[] loadingImages;
    public string[] loadingTips;
    private AsyncOperation operation;
    private string text_SceneLoadingComplete = "Loading complete. Press any key to continue.";
    private Slider slider_LoadProgress;

    private void Awake()
    {
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of ItemsManager found.");
			return;
		}
		else
		{
			instance = this;
		}
    }

    private void Start()
    {
        loadingScreen = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject;
        loadingScreen.SetActive(false);
    }

    public void LoadScene (string sceneName)
    {
        loadingScreen.SetActive(true);
        int spriteindex = Random.Range(0, loadingImages.Length);
        loadingScreen.transform.Find("Image_Art").GetComponent<Image>().sprite = loadingImages[spriteindex];
		int tipindex = Random.Range(0, loadingTips.Length);
		loadingScreen.transform.Find("Image_Art").transform.Find("Text_Tip").GetComponent<Text>().text = loadingTips[tipindex];
        slider_LoadProgress = loadingScreen.transform.Find("Slider_LoadProgress").GetComponent<Slider>();
		StartCoroutine(SceneLoad(sceneName));
    }

    IEnumerator SceneLoad(string sceneName)
    {
		operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        while (operation.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider_LoadProgress.value = progress;
            yield return null;
        }

        // When loading is finished.
        loadingScreen.transform.Find("Image_Art").transform.Find("Text_Loading").GetComponent<Text>().text = text_SceneLoadingComplete;
        loadingScreen.transform.Find("Slider_LoadProgress").gameObject.SetActive(false);
        StartCoroutine(SceneActivate());
    }

    IEnumerator SceneActivate()
    {
        while (!Input.anyKeyDown)
        {
            yield return new WaitForSeconds(0);
        }

        yield return new WaitForSeconds(0);
        operation.allowSceneActivation = true;
	}	
}