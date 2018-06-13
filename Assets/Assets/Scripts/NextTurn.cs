using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextTurn : MonoBehaviour {

    public int TurnCounter;
    public int AlreadyOccupiedBaysNr;
    public int NrOfFinishedShipsInHangar; // thi number i to make sure that already finished ships dont get overwritten by new production items


    private static NextTurn instance = null;

    public GameObject TurnText;

    public GameObject InproductionText;

    public GameObject TurnTextForGUI;

    public GameObject ProbeInProduction;
    public GameObject GrazerInProduction;
    public GameObject SolaGenInProduction;
    

    public int LengthOfInProductionList; // variable for the length of inproduction list.
    public int LengthOfShipsInProduction; // a way to count up Ships in production As these need for counting up nr of in production items correctly

    public void ProgressTurn() // Resource increment is handled in its own Turn method in ResourceManager
    {
        TurnCounter++;

        HangarManager.Instance().DisplayEquipmentList();

        var TextComponent = TurnText.GetComponent<Text>(); // For incrementing of the turn
        TextComponent.text = TurnCounter + "";  // 

        var TextComponent2 = InproductionText.GetComponent<Text>(); // we need this to append and subtract items on the build stack (reverse of que)

        var TextComponent3 = TurnTextForGUI.GetComponent<Text>(); // Turns left..


        for (int i = 0; i < 8; i++)
        {
            // if the bay is empty (nothing is in production/finished or the spacecraft has launched)  show the defaualt empty thing
            HangarManager.Instance().UpdateValuesInHangar(i, HangarManager.Instance().Bays[i].ship);
        }


        foreach (BaseItem obj in HangarManager.Instance().ShipsInService)
        {

            //HERE WE NEED TO DECREMENT DAYSUNTILLARRIVAL FOR ALL SHIPS THAT ARE ENROUTE UNTILL THEY REACH 0



            // if something breaks, check if the shipidentifer actually is in the list
            Debug.Log( obj.ShipName + " is on moon : "+obj.OnMoon);
        }

        if (HangarManager.Instance().InProductionItems.Count > 0)
        {
            LengthOfInProductionList = HangarManager.Instance().InProductionItems.Count - 1; // THIS DOES NOT SEEM RIGHT WE might also need a LENGTHOFSHIP BUILD -Not sure though
            Debug.Log(LengthOfInProductionList + ";" + HangarManager.Instance().InProductionItems.Count);
            HangarManager.Instance().InProductionItems[LengthOfInProductionList].ProgressBuild();// PROGRESSING BUILD -> in here we also handle what todo when build is finished

            //Below Metod call on specific item deals with the transition from InProduction ships to finished ones and displays them when they are finished in the Hangar
            //**************************##########################
            //AlreadyOccupiedBaysNr = HangarManager.Instance().BayVacancyNrCheck(); //we need to know how many bay are alredy occupied before inserting new items 

            // under here we adjust the baynumber for already finished ships, if there are such

            if (HangarManager.Instance().InProductionItems[LengthOfInProductionList].ItemTypeNr==1) // NOT SURE we only want to update the hangar ships in production and not update items which is not ships
            {
                LengthOfShipsInProduction = 0; // reset it for a new count
                for (int i =0; i<LengthOfInProductionList;i++)
                {
                    if (HangarManager.Instance().InProductionItems[i].ItemTypeNr == 1) //if it is a ship 
                        LengthOfShipsInProduction++;
                }

                //HangarManager.Instance().UpdateValuesInHangar(LengthOfInProductionList + NrOfFinishedShipsInHangar, HangarManager.Instance().InProductionItems[LengthOfInProductionList]); // TEST.Changing the last item in the production TRYING TO MAKE THIS change display WORKS!!!

            }

            //*!?
            //HangarManager.Instance().UpdateValuesInHangar(LengthOfShipsInProduction + NrOfFinishedShipsInHangar, HangarManager.Instance().InProductionItems[LengthOfShipsInProduction]); // TEST.Changing the last item in the production TRYING TO MAKE THIS change display WORKS!!!
            // go into each hangar bay , and update the UI with the necessary information depending on the status
          

            TextComponent3.text = "" + HangarManager.Instance().InProductionItems[LengthOfInProductionList].TurnsUntillFinished; // HERE WE SET THE TURNSLEFT in the production panel

            TextComponent2.text = ""; // Clear the text for the production list -- Sorta Works
            //foreach(var Item in HangarManager.Instance().InProductionItems) // here we check the list of items set into production - because it is possible to set several things into production at the same time


            for (int i = HangarManager.Instance().InProductionItems.Count - 1; i >= 0; i--) //counting backwards to avoid problems when removing an element(baseitem) from the list
            {
                //for each item in production we progress build -NOTE THIS SHOULD ONLY BE DONE FOR LAST ITEM IN THE LIST!!! 
                Debug.Log("items curently in production mode: " + HangarManager.Instance().InProductionItems[i]); // this should be put into a list in the Production Canvas - Attempt on this just below

                TextComponent2.text += "/ " + HangarManager.Instance().InProductionItems[i] + "\n"; // Here we add all the current objects inside the ProductionItems to the text which should display what has been chosen for the production stack!! 

                if (HangarManager.Instance().InProductionItems[i].InProduction == false)
                {
                    // We need to remove the graphical representation of this object in the production module(false). And set the next in line to be represented:
                    if (HangarManager.Instance().InProductionItems.Count > 1)
                    {
                        if (HangarManager.Instance().InProductionItems[i].ItemID == 0 && HangarManager.Instance().InProductionItems[i - 1].ItemID != 0) // If probe then remove the graphical representation of it unless next in production is also probe
                        {
                            ProbeInProduction.SetActive(false);
                            if (HangarManager.Instance().InProductionItems[i - 1].ItemID == 1)
                                GrazerInProduction.SetActive(true);
                            if (HangarManager.Instance().InProductionItems[i - 1].ItemID == 6 || HangarManager.Instance().InProductionItems[i - 1].ItemID == 7 || HangarManager.Instance().InProductionItems[i - 1].ItemID == 8)
                                SolaGenInProduction.SetActive(true);
                        }

                        if (HangarManager.Instance().InProductionItems[i].ItemID == 1 && HangarManager.Instance().InProductionItems[i - 1].ItemID != 1) // If grazer then remove the graphical representation of it
                        {
                            GrazerInProduction.SetActive(false);
                            if (HangarManager.Instance().InProductionItems[i - 1].ItemID == 0)
                                ProbeInProduction.SetActive(true);
                            if (HangarManager.Instance().InProductionItems[i - 1].ItemID == 6 || HangarManager.Instance().InProductionItems[i - 1].ItemID == 7 || HangarManager.Instance().InProductionItems[i - 1].ItemID == 8)
                                SolaGenInProduction.SetActive(true);
                        }

                        if ((HangarManager.Instance().InProductionItems[i].ItemID == 6 || HangarManager.Instance().InProductionItems[i].ItemID == 7 || HangarManager.Instance().InProductionItems[i].ItemID == 8) && (HangarManager.Instance().InProductionItems[i - 1].ItemID != 6 || HangarManager.Instance().InProductionItems[i - 1].ItemID != 7) || HangarManager.Instance().InProductionItems[i - 1].ItemID != 8) // If solagenMK2 or MK3 or MK3 then remove the graphical representation of it
                        {
                            SolaGenInProduction.SetActive(false);
                            if (HangarManager.Instance().InProductionItems[i - 1].ItemID == 0)
                                ProbeInProduction.SetActive(true);
                            if (HangarManager.Instance().InProductionItems[i - 1].ItemID == 1)
                                GrazerInProduction.SetActive(true);
                        }


                    }
                    if(HangarManager.Instance().InProductionItems.Count <= 1)
                    {
                        ProbeInProduction.SetActive(false); // if no production is left after this, then erase all graphical representation of production items in the production module
                        GrazerInProduction.SetActive(false);
                        SolaGenInProduction.SetActive(false);
                    }





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

        ProgressSpaceTravel(); //at the end of the progre turn method we 'move' all ships


    }

    public void ProgressSpaceTravel()
    {
        ShipManager.Instance().TravelShips();

    }


    public static NextTurn Instance() // 
    {
        if (instance == null)
        {

            instance = GameObject.FindObjectOfType<NextTurn>() as NextTurn;
        }

        return instance;
    }












    //
}
