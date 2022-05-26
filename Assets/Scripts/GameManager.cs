using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CharStats[] playerStats;

    [Header("Stop Player Movement")]
    public bool gameMenuOpen;
    public bool dialogActive;
    public bool fadingBetweenAreas;

    [Header("Player Items")]
    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    public int currentGold;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        SortItems();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogActive || fadingBetweenAreas)
        {
            PlayerController.instance.canMove = false;
        } else
        {
            PlayerController.instance.canMove = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            AddItem("Iron Armor");
            AddItem("Invalid Item");

            RemoveItem("Health Potion");
            RemoveItem("Blue Potion");
        }
    }

    public Item GetItemDetails(string itemToGrab)
    {
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }

        return null;
    }

    public void SortItems()
    {
        int i = 0;

        for (int j = 0; j < itemsHeld.Length; j++)
        {
            if (itemsHeld[j] != "")
            {
                itemsHeld[i] = itemsHeld[j];
                numberOfItems[i] = numberOfItems[j];
                i++;
            }
        }

        while (i < itemsHeld.Length)
        {
            itemsHeld[i] = "";
            numberOfItems[i] = 0;
            i++;
        }
    }

    public void AddItem(string item)
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == "" || itemsHeld[i] == item)
            {
                newItemPosition = i;
                foundSpace = true;
                break;
            }
        }

        if (foundSpace && ItemExists(item))
        {
            itemsHeld[newItemPosition] = item;
            numberOfItems[newItemPosition]++;
        }

        GameMenu.instance.ShowItems();
    }

    private bool ItemExists(string itemName)
    {
        foreach (Item item in referenceItems)
        {
            if (item.itemName == itemName) return true;
        }

        Debug.Log(itemName + " does not exists!!");
        return false;
    }

    public void RemoveItem(string item)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == item)
            {
                foundItem = true;
                itemPosition = i;
                break;
            }
        }

        if (foundItem)
        {
            numberOfItems[itemPosition]--;

            if (numberOfItems[itemPosition] <= 0)
            {
                numberOfItems[itemPosition] = 0;
                itemsHeld[itemPosition] = "";
            }

            GameMenu.instance.ShowItems();
        } else
        {
            Debug.Log(item + " could not be found to be removed");
        }
    }
}
