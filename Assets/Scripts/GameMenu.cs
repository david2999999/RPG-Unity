using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public static GameMenu instance;

    public GameObject theMenu;
    public GameObject[] windows;
    public GameObject[] statusButtons;

    private CharStats[] playerStats;

    [Header("Player Information")]
    public Text[] nameText;
    public Text[] hpText;
    public Text[] mpText;
    public Text[] lvText;
    public Text[] expText;
    public Slider[] expSlider;
    public Image[] charImages;
    public GameObject[] charStatHolder;

    [Header("Player Status")]
    public Text statusName;
    public Text statusHP;
    public Text statusMP;
    public Text statusStr;
    public Text statusDef;
    public Text statusWpnEqpd;
    public Text statusWpnPwr;
    public Text statusArmrEqpd;
    public Text statusArmrPwr;
    public Text statusExp;
    public Image statusImage;

    [Header("List of Item Holders")]
    public ItemButton[] itemButtons;

    [Header("Selected Item")]
    public string selectedItem;
    public Item activeItem;
    public Text itemName;
    public Text itemDescription;
    public Text useButtonText;

    [Header("Pressing the Use Button for Items")]
    public GameObject itemCharChoiceMenu;
    public Text[] itemCharChoiceNames;
        
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (theMenu.activeInHierarchy)
            {
                CloseMenu();
            } else
            {
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);
                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHP +  "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvText[i].text = "Lvl: " + playerStats[i].playerLevel;
                expText[i].text = "" + playerStats[i].currentExp + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentExp;
                charImages[i].sprite = playerStats[i].charImage; 
            } else
            {
                charStatHolder[i].SetActive(false);
            }
        }
    }

    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();

        for (int i = 0; i < windows.Length; i++)
        {
            if (i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            } else
            {
                windows[i].SetActive(false);
            }
        }

        itemCharChoiceMenu.SetActive(false);
    }

    public void OpenStatus()
    {
        UpdateMainStats();
        StatusChar(0);

        for (int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }

    public void CloseMenu()
    {
        foreach (GameObject window in windows) window.SetActive(false);

        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
        itemCharChoiceMenu.SetActive(false);
    }

    public void StatusChar(int selected)
    {
        statusName.text = playerStats[selected].charName;
        statusHP.text = playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        statusMP.text = playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        statusStr.text = playerStats[selected].strength.ToString();
        statusDef.text = playerStats[selected].defense.ToString();

        if (playerStats[selected].equippedWeapon != "")
        {
            statusWpnEqpd.text = playerStats[selected].equippedWeapon;
        }

        statusWpnPwr.text = playerStats[selected].weaponPower.ToString();

        if (playerStats[selected].equippedArmor != "")
        {
            statusWpnEqpd.text = playerStats[selected].equippedArmor;
        }

        statusArmrPwr.text = playerStats[selected].armorPower.ToString();
        statusExp.text = (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel] - playerStats[selected].currentExp).ToString();
        statusImage.sprite = playerStats[selected].charImage;
    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();

        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            } else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item item)
    {
        activeItem = item;

        if (activeItem.isItem)
        {
            useButtonText.text = "Use";
        } 

        if (activeItem.isWeapon || activeItem.isArmor)
        {
            useButtonText.text = "Equip";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }

    public void DiscardItem()
    {
        if (activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void OpenItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(true);

        for (int i = 0; i < itemCharChoiceNames.Length; i++)
        {
            itemCharChoiceNames[i].text = GameManager.instance.playerStats[i].charName;
            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }
    }

    public void CloseItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(false);
    }
}
