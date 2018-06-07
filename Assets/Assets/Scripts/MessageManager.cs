using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour {

    

    public GameObject TextForHeadline;
    public GameObject TextForMessage;

    public GameObject MessagePanel;
    private static MessageManager instance = null;

    public void UpdateMessagePanel(string Headline,string Message) // This method sets the the message panel one the main canvas screen and displays the message from where it is called!! 
    {

        MessagePanel.SetActive(true); // make the message panel visible
        var TextComponentHeadline = TextForHeadline.GetComponent<Text>();
        var TextComponentMessage = TextForMessage.GetComponent<Text>();

        TextComponentHeadline.text = Headline;
        TextComponentMessage.text = Message;


    }

    public static MessageManager Instance() // 
    {
        if (instance == null)
        {

            instance = GameObject.FindObjectOfType<MessageManager>() as MessageManager;
        }

        return instance;
    }

    




}
