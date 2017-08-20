using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Inventory : MonoBehaviour
{
    public InventoryDataBase data;
    public List<ItemInventory> items = new List<ItemInventory>();

    public GameObject Gameobjectshow;
    public GameObject InventoryMainObject;

    public currencySystem currency;

    public int Maxcount;

    public Camera cam;
    public EventSystem ES;

    public int CurrentID;
    public ItemInventory currentitem;

    public RectTransform MovingObject;
    public Vector3 offset;

    public GameObject Background;

    void Start()
    {
        if (items.Count == 0)
            AddGraphics();

        UpdateInventory();
    }

    void Update()
    {
        if (CurrentID != -1)
            MoveObject();

        if (Input.GetKeyDown(KeyCode.I))
        {
            Background.SetActive(!Background.activeSelf);
            if (Background.activeSelf)
                UpdateInventory();
        }  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Item")
        {
            ItemSelf newitem = other.transform.GetComponent<ItemSelf>();
            //add new variables in itemself and update here for item stats
            SearchForSameItem(data.items[newitem.ID], newitem.count, newitem.value);
            UpdateInventory();
            Destroy(other.transform.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        ItemSelf newitem = collision.transform.GetComponent<ItemSelf>();

        if (collision.gameObject.tag == "ShopItem" && Input.GetKey(KeyCode.E))
        {
            //does the player have enough mulla?
            if (currency.money < newitem.value)
            {
                Debug.Log("Not Enough Money");
            }
            //if they do add item in inventory and subtract mulla
            else if (currency.money > newitem.value)
            {
                SearchForSameItem(data.items[newitem.ID], newitem.count, newitem.value);
                UpdateInventory();
                Destroy(collision.transform.gameObject);
                currency.subtractMoney(newitem.value);
            }
        }
    }

    public void SearchForSameItem(ITEM item, int count, int value)
    {
        for(int i = 0; i < Maxcount; i++)
        {
            if (items[i].ID == item.ID)
            {
                if(items[i].count < 64)
                {
                    items[i].count += count;
                    if (items[i].count > 64)
                    {
                        count = items[i].count - 64;
                        items[1].count = 64;
                    }
                    else
                    {
                        count = 0;
                        i = Maxcount;
                    }
                }
            }
        }

        if(count > 0)
        {
            for (int i = 0; i < Maxcount; i++)
            {
                if (items[i].ID == 0)
                {
                    AddItem(i, item, count, value);
                    i = Maxcount;
                }
            }
        }
    }

    public void AddItem(int ID, ITEM item, int count, int value)
    {
        items[ID].ID = item.ID;
        items[ID].value = item.value;
        items[ID].count = count;
        items[ID].ItemGameObj.GetComponent<Image>().sprite = item.image;

        if(count > 1 && item.ID != 0)
        {
            items[ID].ItemGameObj.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[ID].ItemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddInventoryItem (int ID, ItemInventory inv_item)
    {
        items[ID].ID = inv_item.ID;
        items[ID].value = inv_item.value;
        items[ID].count = inv_item.count;
        items[ID].ItemGameObj.GetComponent<Image>().sprite = data.items[inv_item.ID].image;

        if (inv_item.count > 1 && inv_item.ID != 0)
        {
            items[ID].ItemGameObj.GetComponentInChildren<Text>().text = inv_item.count.ToString();
        }
        else
        {
            items[ID].ItemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddGraphics()
    {
        for(int i = 0; i < Maxcount; i++)
        {
            GameObject newItem = Instantiate(Gameobjectshow, InventoryMainObject.transform) as GameObject;
            newItem.name = i.ToString();

            ItemInventory II = new ItemInventory();
            II.ItemGameObj = newItem;

            RectTransform RT = newItem.GetComponent<RectTransform>();
            RT.localPosition = new Vector3(0, 0, 0);
            RT.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelecObject(); });

            items.Add(II);
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < Maxcount; i++)
        {
            if(items[i].ID != 0 && items[i].count > 1)
            {
                items[i].ItemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].ItemGameObj.GetComponentInChildren<Text>().text = "";
            }

            items[i].ItemGameObj.GetComponent<Image>().sprite = data.items[items[i].ID].image;
        }
    }

    public void SelecObject()
    {
        if(CurrentID == -1)
        {
            CurrentID = int.Parse(ES.currentSelectedGameObject.name);
            currentitem = CopyInventoryItem(items[CurrentID]);

            MovingObject.gameObject.SetActive(true);
            MovingObject.GetComponent<Image>().sprite = data.items[currentitem.ID].image;

            AddItem(CurrentID, data.items[0], 0,0);
        }
        else
        {
            ItemInventory II = items[int.Parse(ES.currentSelectedGameObject.name)];

            if (currentitem.ID != II.ID)
            {
                AddInventoryItem(CurrentID, II);
                AddInventoryItem(int.Parse(ES.currentSelectedGameObject.name), currentitem);
            }
            else
            {
                if (II.count + currentitem.count <= 64)
                {
                    II.count += currentitem.count;
                }
                else
                {
                    AddItem(CurrentID, data.items[II.ID], II.count + currentitem.count - 64, II.value);

                    II.count = 64;
                }

                II.ItemGameObj.GetComponentInChildren<Text>().text = II.count.ToString();
            }

            CurrentID = -1;

            MovingObject.gameObject.SetActive(false);
        }
    }

    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z;
        MovingObject.position = cam.ScreenToWorldPoint(pos);
    }

    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();

        New.ID = old.ID;
        New.ItemGameObj = old.ItemGameObj;
        New.value = old.value;
        New.count = old.count;

        return New;
    }
}

[System.Serializable]

public class ItemInventory
{
    public int ID;
    public GameObject ItemGameObj;
    public int value;
    public int count;
}