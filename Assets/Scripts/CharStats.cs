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
    public int[] mpLvlBonus;

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
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseExp;

        for (int i = 2; i < maxLevel; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }

        mpLvlBonus = new int[maxLevel + 1];

        for (int i = 1; i <= maxLevel; i++)
        {
            mpLvlBonus[i] = Random.Range(0, 6);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExp(1000);
        }
    }

    public void AddExp(int expToAdd)
    {
        currentExp += expToAdd;

        if (playerLevel < maxLevel && currentExp > expToNextLevel[playerLevel])
        {
            currentExp -= expToNextLevel[playerLevel++];

            // determine whether to add to str or def based on odd or even
            if (playerLevel % 2 == 0)
            {
                strength++;
            } else
            {
                defense++;
            }

            maxHP = Mathf.FloorToInt(maxHP * 1.05f);
            currentHP = maxHP;

            maxMP += mpLvlBonus[playerLevel];
            currentMP = maxMP;
        }

        if (playerLevel >= maxLevel)
        {
            currentExp = 0;
        }
    }
}
