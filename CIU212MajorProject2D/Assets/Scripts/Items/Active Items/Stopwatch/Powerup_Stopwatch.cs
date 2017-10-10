using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Stopwatch : MonoBehaviour {
    
    private float normalTimeScale = 1.0f;
    private float slowMotionTimeScale = 0.4f;
    private float resetAfterSeconds = 3.0f;

    public void Use ()
    {
        StartCoroutine(Initiate());
    }

    IEnumerator Initiate ()
    {
        Time.timeScale = slowMotionTimeScale;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(resetAfterSeconds);
        Time.timeScale = normalTimeScale;
    }
}
