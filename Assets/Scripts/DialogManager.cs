using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;

    public string[] dialogLines;

    public int currentLine;
    private bool justStarted;

    public static DialogManager instance;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                if (!justStarted)
                {
                    if (++currentLine < dialogLines.Length)
                    {
                        dialogText.text = dialogLines[currentLine];
                    }
                    else
                    {
                        dialogBox.SetActive(false);
                    }
                } else
                {
                    justStarted = false;
                }
            }
        }
    }

    public void showDialog(string[] lines)
    {
        dialogLines = lines;
        currentLine = 0;
        dialogText.text = dialogLines[currentLine];
        justStarted = true;
        dialogBox.SetActive(true);
    }
}
