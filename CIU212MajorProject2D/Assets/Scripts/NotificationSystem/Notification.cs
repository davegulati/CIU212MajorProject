using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {

    // Normal notification
    private Text text_Sign;
    private Text text_Title;
    private Text text_Heading;
    private Text text_Description;
    private Text text_Message;
    private GameObject notification;
    private Animation anim;
    public AnimationClip start;
    public AnimationClip finish;

    // Small notification
    private Text notification_Small;

    public static Notification instance;

    private void Awake()
    {
        instance = this;
        notification = transform.Find("Notification").gameObject;
        text_Sign = notification.transform.Find("Image_Border").transform.Find("Image_Color").transform.Find("Text_Sign").GetComponent<Text>();
        text_Title = notification.transform.Find("BG_Title").transform.Find("Text_Title").GetComponent<Text>();
        text_Heading = notification.transform.Find("Image_Body").transform.Find("Text_Heading").GetComponent<Text>();
        text_Description = notification.transform.Find("Image_Body").transform.Find("Text_Description").GetComponent<Text>();
        text_Message = notification.transform.Find("Image_Body").transform.Find("Text_Message").GetComponent<Text>();
        anim = notification.GetComponent<Animation>();
        notification.SetActive(false);
        notification_Small = transform.Find("Notification_Small").GetComponent<Text>();
        notification_Small.gameObject.SetActive(false);
    }

    public void Display (string sign, string title, string heading, string description, string message, float duration)
    {
        if (notification.activeSelf)
        {
            StopAllCoroutines();
        }

		notification.SetActive(true);
        anim.CrossFade(start.name);
        text_Sign.text = sign;
        text_Title.text = title;
        text_Heading.text = heading;
        text_Description.text = description;
        text_Message.text = message;
        notification.SetActive(true);

        duration = Mathf.Clamp(duration, start.length, float.MaxValue);
        StartCoroutine(Stop(duration));
    }

    public void DisplaySmallNotification(string text)
    {
		if (notification_Small.gameObject.activeSelf)
		{
			StopAllCoroutines();
		}

        notification_Small.gameObject.SetActive(true);
        notification_Small.text = text;
        StartCoroutine(NotificationSmall());
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

    IEnumerator NotificationSmall ()
    {
        yield return new WaitForSeconds(2);
        notification_Small.text = "";
        notification_Small.gameObject.SetActive(false);
    }
}
