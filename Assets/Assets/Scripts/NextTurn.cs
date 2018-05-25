using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextTurn : MonoBehaviour {

    public int TurnCounter;
    

    public GameObject TurnText; 


public void ProgressTurn() // Resource increment is handled in its own Turn method in ResourceManager
    {
        TurnCounter++;

        var TextComponent = TurnText.GetComponent<Text>();
        TextComponent.text = TurnCounter + "";
    }

//
}
