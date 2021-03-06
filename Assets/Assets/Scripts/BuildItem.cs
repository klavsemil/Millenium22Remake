﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildItem : MonoBehaviour {

    //public int BuildNumber; // this indicates what is chosen to be build - see  below in parameter for InstantiateBiuild method!

    public bool EnergySufficient; // NOTE: THESE HAS BEEN SET TO TRUE IN THE UNITY EDITOR !!
    public bool ResourceSufficient;
    public bool PeopleSufficient;
    public int ItemCounter;

    public GameObject NotenoughResourcesPanel; // this is for telling the player that there is not resources enough
    public GameObject NotEnoughRoomInHangarPanel; // this is for telling the player that there is not room in hangar
    public GameObject NotEnoughEnergyPanel;
    //TEST TEST
    //public GameObject FillInHangarPanel; // this is for setting the information of this vehicle into the hanagar bay panel
    //public GameObject TypeTextObject; //For transfering text

    //maybe an array to save stuff that is overriden by new production so that the production keeps memory of partially finished stuff for later completion - DONE!


    public int DaysUntilFinished;
    public string TypeName; 

    public void InstantiateBuild(int BuildNumber) // HERE SHOULD ALSO BE A PARAMETER FOR BUILDTYPE to determine if it is a ship or not!
    {

        BaseItem newObject = BaseItemDatabase.GetItem(BuildNumber); //NOTE: THIS HAS BEEN MOVED OUTSIDE THE IF condition BELOW!!!
        HangarManager.Instance().InProductionItems.Add(newObject);
        
            if (ResourceManager.instance.HasResourcesFor(newObject) && ResourceManager.instance.HasEnergyFor(newObject)) // Do we have the required resources ANd Energy
            {//If yes

           
                if (newObject.ItemTypeNr==1 && HangarManager.Instance().HasEmptySpot()) //Is there an empty spot in the hangar?
                {

                ItemCounter++; //every time we build something this is incremented to give unique names etc.
                newObject.InProduction = true;
                newObject.ShipIdentifier = ItemCounter;
                newObject.ShipName = newObject.ItemName + "0" + ItemCounter;
                HangarManager.Instance().InsertShip(newObject); //Insert Item BuildNumber (e.g. 0 = probe)
                DaysUntilFinished = newObject.TurnsUntillFinished;
               

                Debug.Log(newObject.PowerNeeded + " PowerNeeded ");

                // Below a call to a method to set the data from the selected object for production into a specific hangar. MIGHT not belong here!!
                TypeName = newObject.ItemName;                
                }
                else if(newObject.ItemTypeNr ==1 && HangarManager.Instance().HasEmptySpot()==false)
                {
                 //Message player that there is no room in Hangar
                 Destroy(newObject); //Ødelæg objectet

                NotEnoughRoomInHangarPanel.SetActive(true);
                }



                //All other non SpaceCraftObjects handled here 
                if (newObject.ItemTypeNr == 2) // if this is an energyitem
                {
                newObject.InProduction = true; // set the item to be in production
                HangarManager.Instance().InsertEquipment(newObject);
                DaysUntilFinished = newObject.TurnsUntillFinished; // not sure if this is used....                
                }
                if (newObject.ItemTypeNr == 3) // if this is an Weaponitem
                {
                 newObject.InProduction = true; // set the item to be in production
                 HangarManager.Instance().InsertEquipment(newObject);
                 DaysUntilFinished = newObject.TurnsUntillFinished; // not sure if this is used....                
                }
                if (newObject.ItemTypeNr == 4) // if this is an Supplementaryitem
                {
                newObject.InProduction = true; // set the item to be in production
                HangarManager.Instance().InsertEquipment(newObject);
                DaysUntilFinished = newObject.TurnsUntillFinished; // not sure if this is used....                
                }
               /* else // if the type = 1 and there is no space it should be destroyed 
                {
                Destroy(newObject); //Ødelæg objectet
                NotenoughResourcesPanel.SetActive(true);
                } */


        }
            else //If no:
            {
            //fortæl spilleren at der ikke er resourcer nok
             if (ResourceManager.instance.HasResourcesFor(newObject)== false)
             {
                NotenoughResourcesPanel.SetActive(true);
             }
           
             if (ResourceManager.instance.HasEnergyFor(newObject) == false)
             {
                NotEnoughEnergyPanel.SetActive(true);
             }
                Destroy(newObject); //Ødelæg objectet
            }

    }











 




}
