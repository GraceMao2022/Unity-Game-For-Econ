using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; ////
using TMPro;
using UnityEngine.SceneManagement; ////

public class GiveGems : MonoBehaviour
{
    public int gemAmt = 5;
    private bool received;
    private bool opossumReceived;
    private bool receiveFromOpossum;
    public TextMeshProUGUI startOpossumText;
    public TextMeshProUGUI endOpossumText;

    void Update()
    {
        if(GlobalManager.giveOpossumGem && SceneManager.GetActiveScene().buildIndex == 10)
        {
            endOpossumText.text = "Great news! I borrowed money at a fixed rate, so now I've benefited from inflation. Here is my thanks for your gem (press \"E\")!";
            //ScoreManager.instance.ChangeScore(5);
        }
    }

    public void GiveGemAmt()
    {
        if(!received)
            ScoreManager.instance.ChangeScore(gemAmt);
        received = true;
    }

    public void giveOpossum()
    {
        if(!opossumReceived)
            ScoreManager.instance.ChangeScore(-1);
        GlobalManager.giveOpossumGem = true;
        opossumReceived = true;
        startOpossumText.text = "Thanks lad!";
    }

    public void receiveOpossum()
    {
        if(!receiveFromOpossum && GlobalManager.giveOpossumGem)
            ScoreManager.instance.ChangeScore(5);
        receiveFromOpossum = true;
    }
}
