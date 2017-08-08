using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDataBase : MonoBehaviour
{

    public List<ITEM> items = new List<ITEM>();
}

[System.Serializable]

public class ITEM
{
    public int ID;
    public string name;
    public Sprite image;
}
