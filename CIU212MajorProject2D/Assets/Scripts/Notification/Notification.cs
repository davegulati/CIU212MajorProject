﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {

    private Text text_Sign;
    private Text text_Title;
    private Text text_Message;
    private GameObject notification;
    private Animation anim;
    public AnimationClip start;
    public AnimationClip finish;

    public static Notification instance;

    private void Awake()
    {
        instance = this;
        notification = transform.Find("Notification").gameObject;
        text_Sign = notification.transform.Find("Image_Border").transform.Find("Image_Color").transform.Find("Text_Sign").GetComponent<Text>();
        text_Title = notification.transform.Find("BG").transform.Find("Text_Title").GetComponent<Text>();
        text_Message = notification.transform.Find("BG").transform.Find("Text_Message").GetComponent<Text>();
        anim = notification.GetComponent<Animation>();
        notification.SetActive(false);
    }

    private void Start()
    {
        //notification.SetActive(false);
    }

    // Update is called once per frame
    private void Update () 
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Display("!", "Testing Title", "Testing message wow!\nNewline test!", 3.0f);
        }
	}

    public void Display (string sign, string title, string message, float duration)
    {
        notification.SetActive(true);
        anim.CrossFade(start.name);
        text_Sign.text = sign;
        text_Title.text = title;
        text_Message.text = message;
        notification.SetActive(true);

        duration = Mathf.Clamp(duration, start.length, float.MaxValue);
        StartCoroutine(Stop(duration));
    }

    IEnumerator Stop (float duration)
    {
        yield return new WaitForSeconds(duration);
        anim.CrossFade(finish.name);
        StartCoroutine(End());
    }

    IEnumerator End ()
    {
        yield return new WaitForSeconds(finish.length);
        notification.SetActive(false);
    }
}
