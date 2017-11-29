using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour {

    public void UpdateWeaponSlotImage (GameObject weapon)
    {
        gameObject.GetComponent<Image>().sprite = weapon.GetComponent<SpriteRenderer>().sprite;
    }
}
