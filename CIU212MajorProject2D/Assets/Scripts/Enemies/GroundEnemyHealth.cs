using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundEnemyHealth : MonoBehaviour {

    public GameObject coin;

    private Animator anim;
    private float currentHealth = 100;
    private bool isHurting = false;
	private SpriteRenderer spriteRenderer;
	private int hurtTime = 2;
    private float flashDelay = 0.1f;
	private Color isHurtingColor = new Color(255, 0, 0);
	private Color normalColor = new Color(255, 255, 255);

    //private GameObject healthBarCanvas;
    private Slider healthSlider;

	private void Awake () 
    {
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //healthBarCanvas = gameObject.transform.Find("HealthBarCanvas").gameObject;
		healthSlider = transform.Find("HealthBarCanvas").transform.Find("HealthBarSlider").GetComponent<Slider>();
        healthSlider.value = 100;
	}

    public void FlipHealthBarCanvas ()
    {
        //Vector3 newScale = healthBarCanvas.transform.localScale;
        //newScale.x *= -1;
        //healthBarCanvas.transform.localScale = newScale;
    }

    public void DamageEnemy (float damage)
    {
        currentHealth = currentHealth - damage;
        healthSlider.value = currentHealth / 100;
        if (currentHealth <= 0)
        {
            //anim.SetTrigger("DIE TRIGGER NAME");
            Destroy(gameObject);
            Instantiate(coin, transform.position, transform.rotation);
        }
        else
        {
            StartCoroutine(TimerDamageColor());
        }
    }

	private IEnumerator TimerDamageColor()
	{
		isHurting = true;
		StartCoroutine(FlashDamageColor());
		yield return new WaitForSeconds(hurtTime);
		isHurting = false;
	}
	private IEnumerator FlashDamageColor()
	{
		isHurting = true;
		while (isHurting)
		{
			spriteRenderer.color = isHurtingColor;
			yield return new WaitForSeconds(flashDelay);
			spriteRenderer.color = normalColor;
			yield return new WaitForSeconds(flashDelay);
		}
	}
}