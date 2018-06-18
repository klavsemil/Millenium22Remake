using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour {

    public int MessageCounter; // shall be used to count the messages so the oldest will be moved outside the textfield  
    public int MessagePageCounter=1;
    public GameObject TextForHeadline;
    public GameObject TextForMessage;

    public GameObject MessagePanel;

    //

    public GameObject EncounterMessagePanel;
    public GameObject TextForEncounterHeadline;
    public GameObject TextForEncounterMessage;



    private static MessageManager instance = null;

    public void UpdateMessagePanel(string Headline,string Message) // This method sets the the message panel one the main canvas screen and displays the message from where it is called!! 
    {
        MessageCounter++;

        MessagePanel.SetActive(true); // make the message panel visible
        var TextComponentHeadline = TextForHeadline.GetComponent<Text>();
        var TextComponentMessage = TextForMessage.GetComponent<Text>();

        if (MessageCounter > 25)
        {
            MessagePageCounter++;
            TextComponentHeadline.text = "Message page: "+MessagePageCounter+"\n"; // TEST Trying to Start text all over by reset- Works kinda ok for the prototypetesting (*)
            TextComponentMessage.text = "\n";
            MessageCounter = 0;
            MessagePageCounter++;
        }

        TextComponentHeadline.text += Headline; //  with += 'instead if just =
        TextComponentMessage.text += Message;

    }

    public void UpdateEncounterMessagePanel(string Headline, string Message) // This method sets the the EncounterMessage panel one the main canvas screen and displays the message from where it is called!! 
    {

        EncounterMessagePanel.SetActive(true);// make the message panel visible
        var TextComponentEncounterHeadline = TextForEncounterHeadline.GetComponent<Text>();
        var TextComponentEncounterMessage = TextForEncounterMessage.GetComponent<Text>();

        TextComponentEncounterHeadline.text = Headline; // 
        TextComponentEncounterMessage.text = Message;

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
