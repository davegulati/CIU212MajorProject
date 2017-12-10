using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour {

    private Image icon;
    private Image fill;

    private Color highDurabilityColor = new Color(0, 0.7f, 0);
    private Color mediumDurabilityColor = new Color(255, 0.5f, 0);
    private Color lowDurabilityColor = new Color(255, 0, 0);

    private void Awake()
    {
        icon = gameObject.transform.Find("Icon").GetComponent<Image>();
        fill = gameObject.transform.Find("Fill").GetComponent<Image>();
    }

    public void UpdateWeaponSlotImage (GameObject weapon, float durability)
    {
        icon.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        fill.fillAmount = durability / 100;
        if (fill.fillAmount > 0.7)
        {
            fill.color = highDurabilityColor;
        }

        if (fill.fillAmount < 0.7 && fill.fillAmount > 0.3)
        {
            fill.color = mediumDurabilityColor;
        }

        if (fill.fillAmount < 0.3)
        {
            fill.color = lowDurabilityColor;
        }
    }

    public void UpdateWeaponDurabilityUI_Axe (float durability)
    {
        fill.fillAmount = durability / 100;
        if (fill.fillAmount > 0.7)
        {
            fill.color = highDurabilityColor;
        }

        if (fill.fillAmount < 0.7 && fill.fillAmount > 0.3)
        {
            fill.color = mediumDurabilityColor;
        }

        if (fill.fillAmount < 0.3)
        {
            fill.color = lowDurabilityColor;
        }
    }

    public void UpdateWeaponDurabilityUI_Bow(float durability)
    {
        fill.fillAmount = durability / 100;
        if (fill.fillAmount > 0.7)
        {
            fill.color = highDurabilityColor;
        }

        if (fill.fillAmount < 0.7 && fill.fillAmount > 0.3)
        {
            fill.color = mediumDurabilityColor;
        }

        if (fill.fillAmount < 0.3)
        {
            fill.color = lowDurabilityColor;
        }
    }
}
