using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextTurn : MonoBehaviour {

    public int TurnCounter;
    public int AlreadyOccupiedBaysNr;
    public int NrOfFinishedShipsInHangar; // thi number i to make sure that already finished ships dont get overwritten by new production items
    

    public GameObject TurnText;

    public GameObject InproductionText;

    public GameObject TurnTextForGUI;

    public int LengthOfInProductionList; // variable for the length of inproduction list.

    public void ProgressTurn() // Resource increment is handled in its own Turn method in ResourceManager
    {
        TurnCounter++;

        HangarManager.Instance().DisplayEquipmentList();

        var TextComponent = TurnText.GetComponent<Text>(); // this is a text component for showing the incrementing of the turn
        TextComponent.text = TurnCounter + "";  // 

        var TextComponent2 = InproductionText.GetComponent<Text>(); // we need this to append and subtract items on the build stack (reverse of que)

        var TextComponent3 = TurnTextForGUI.GetComponent<Text>(); // This is for displaying number of turns left..

        if (HangarManager.Instance().InProductionItems.Count > 0)
        {
            LengthOfInProductionList = HangarManager.Instance().InProductionItems.Count - 1;
            Debug.Log(LengthOfInProductionList + ";" + HangarManager.Instance().InProductionItems.Count);
            HangarManager.Instance().InProductionItems[LengthOfInProductionList].ProgressBuild();


            //Below Metod call on specific item deals with the transition from InProduction ships to finished ones and displays them when they are finished in the Hangar
            //**************************##########################
            //AlreadyOccupiedBaysNr = HangarManager.Instance().BayVacancyNrCheck(); //we need to know how many bay are alredy occupied before inserting new items 

            // under here we adjust the baynumber for already finished ships, if there are such


            NrOfFinishedShipsInHangar = 0; //reset the number for a new count of actual finished ships in hangar
            for (int k = 0; k < HangarManager.Instance().FinishedItems.Count; k++) // We count through the items in the finisheditemslist 
            {
                if (HangarManager.Instance().FinishedItems[k].ItemTypeNr == 1)// && HangarManager.Instance().InProductionItems.Count==1 // finding thoe that are spacecraft + LATER: also we need to know where the ship position is!!!! All clones should be set to the moon when being created 
                    NrOfFinishedShipsInHangar++;
            }






            if (HangarManager.Instance().InProductionItems[LengthOfInProductionList].ItemTypeNr==1) // NOT SURE we only want to update the hangar ships in production and not update items which is not ships
            HangarManager.Instance().UpdateValuesInHangar(LengthOfInProductionList + NrOfFinishedShipsInHangar, HangarManager.Instance().InProductionItems[LengthOfInProductionList]); // TEST.Chamging the last item in the production TRYING TO MAKE THIS change display WORKS!!!








            TextComponent3.text = "" + HangarManager.Instance().InProductionItems[LengthOfInProductionList].TurnsUntillFinished; // HERE WE SET THE TURNSLEFT in the production panel

            TextComponent2.text = ""; //TEST TEST Clear the text for the production list -- Sorta Works
            //foreach(var Item in HangarManager.Instance().InProductionItems) // here we check the list of items set into production - because it is possible to set several things into production at the same time


            for (int i = HangarManager.Instance().InProductionItems.Count - 1; i >= 0; i--) //counting backwards to avoid problems when removing an element(baseitem) from the list
            {
                //for each item in production we progress build -NOTE THIS SHOULD ONLY BE DONE FOR LAST ITEM IN THE LIST!!! 
                Debug.Log("items curently in production mode: " + HangarManager.Instance().InProductionItems[i]); // this should be put into a list in the Production Canvas - Attempt on this just below

                TextComponent2.text += "/ " + HangarManager.Instance().InProductionItems[i] + "\n"; // Here we add all the current objects inside the ProductionItems to the text which should display what has been chosen for the production stack!! 

                if (HangarManager.Instance().InProductionItems[i].InProduction == false)
                {
                    HangarManager.Instance().InProductionItems.RemoveAt(i); // remove this BaseItem from the inproduction list
                    TextComponent2.text = ""; // empty the textfield
                    for (int j = HangarManager.Instance().InProductionItems.Count - 1; j >= 0; j--) //refill the text again (which now should be without the removed text part)
                        TextComponent2.text += "/ " + HangarManager.Instance().InProductionItems[j] + "\n";                         
                }  
                
            }

            NrOfFinishedShipsInHangar = 0; //reset the number for a new count of actual finished ships in hangar
            for (int k = 0; k < HangarManager.Instance().FinishedItems.Count; k++) // We count through the items in the finisheditemslist 
            {
                if (HangarManager.Instance().FinishedItems[k].ItemTypeNr == 1)// && HangarManager.Instance().InProductionItems.Count==1 // finding thoe that are spacecraft + LATER: also we need to know where the ship position is!!!! All clones should be set to the moon when being created 
                    NrOfFinishedShipsInHangar++;
            }



        }

     





        /*foreach (var FinishedItem in HangarManager.Instance().FinishedItems) // here we check the list of items which is finished-
        {
            
            Debug.Log("item"+" curently in the finished list: " + FinishedItem); // want to have a counter for each item in the list!!!
        }
        */


    }

    public void ProgressSpaceTravel()
    {


    }





    //
}
