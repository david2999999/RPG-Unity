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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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
}
