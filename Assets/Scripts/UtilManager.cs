using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; ////
using UnityEngine.SceneManagement; ////

public class UtilManager : MonoBehaviour
{
    public static UtilManager instance; 
    public Text text; // the Text object on our Canvas that displays the utils
    public int utils;
    private int numOfPizzas;
    public int utilValue;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        text.text = "X0";
    }

    public void ChangeUtil()
    {
        numOfPizzas++;
        utilValue = 110 - 10*numOfPizzas;
        utils += utilValue;

        text.text = "X" + utils.ToString();
    }

    public void ResetUtil()
    {
        utils = 0;
        numOfPizzas = 0;
    }
}
