using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string charName;
    public int playerLevel = 1;
    public int currentExp;

    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseExp = 1000;

    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int maxMP = 30;

    public int strength;
    public int defense;

    public int weaponPower;
    public int armorPower;

    public string equipedWeapon;
    public string equipedArmor;

    public Sprite charImage;

    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel = new int[maxLevel + 1];
        expToNextLevel[1] = baseExp;

        for (int i = 2; i <= maxLevel; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
