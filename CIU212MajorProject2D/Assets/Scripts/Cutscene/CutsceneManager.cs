using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(AudioSource))]

public class CutsceneManager : MonoBehaviour 
{
    public MovieTexture cutscene;
    private AudioSource cutsceneAudio;
    public string sceneToLoad;

    private void Start()
    {
        gameObject.GetComponent<RawImage>().texture = cutscene as MovieTexture;
        cutsceneAudio = gameObject.GetComponent<AudioSource>();
        cutsceneAudio.clip = cutscene.audioClip;
        cutscene.Play();
        cutsceneAudio.Play();
    }

    private void Update()
    {
        if (!cutscene.isPlaying)
        {
            CutsceneFinished();
        }

        if (Input.anyKeyDown)
        {
            Skip();
        }
    }

    private void CutsceneFinished()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void Skip()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
