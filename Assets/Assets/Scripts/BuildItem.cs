using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildItem : MonoBehaviour {

    //public int BuildNumber; // this indicates what is chosen to be build - see  below in parameter for InstantiateBiuild method!

    public bool EnergySufficient; // NOTE: THESE HAS BEEN SET TO TRUE IN THE UNITY EDITOR !!
    public bool ResourceSufficient;
    public bool PeopleSufficient;

    public GameObject NotenoughResourcesPanel; // this is for telling the player that there is not resources enough

    //TEST TEST
    public GameObject FillInHangarPanel; // this is for setting the information of this vehicle into the hanagar bay panel
    public GameObject TypeTextObject; //For transfering text

    //maybe an array to save stuff that is overriden by new production so that the production keeps memory of partially finished stuff for later completion 

    //maybe I need a BaseItem object instantiation for the object being builded !?!?

    public int DaysUntilFinished;
    public string TypeName; //TEST TEST THIS is for sending info to the panel corresponding to the hangarbay which holds this

    public void InstantiateBuild(int BuildNumber) // HERE SHOULD ALSO BE A PARAMETER FOR BUILDTYPE to determine if it is a ship or not!
    {

        BaseItem newObject = BaseItemDatabase.GetItem(BuildNumber); //NOTE: THIS HAS BEEN MOVED OUTSIDE THE IF condition BELOW!!!

        if (HangarManager.Instance().HasEmptySpot()) //Is there an empty spot in the hangar?
        {//If yes

            //Debug.Log(ResourceManager.instance == null);
            if (ResourceManager.instance.HasResourcesFor(newObject)) //If there is enough resources // is instance with lower caser or uppercase start letter!
            {

                //ResourceManager.instance.HasResourcesFor(newObject); //
                newObject.InProduction = true;
                HangarManager.Instance().InsertShip(newObject); //Insert Item BuildNumber (e.g. 0 = probe)
                DaysUntilFinished = newObject.TurnsUntillFinished;
                //newObject.InProduction = true;

                Debug.Log(newObject.PowerNeeded + " PowerNeeded ");

                // Below a call to a method to set the data from the selected object for production into a specific hangar. MIGHT not belong here!!

                TypeName = newObject.ItemName; 
                //HangarManager.

            }
            else
            {
                Destroy(newObject); //Ødelæg objectet

                NotenoughResourcesPanel.SetActive(true); // This sees to it that the chosen object (which in Unity Editor is set to the NoteEnoughResourcesPanel) is displayed 

                //Fortæl spilleren at der ikke er resourcer nok.
            }
            
        }
        else //If no:
        {
            //fortæl spilleren at der ikke er plads.



        }

    }

    public void FillInfoInPanel(int BayNumber) // this method is for filling the Hangar form with the info on the actual object. NOTE This method should be in hanagarmanager 
    {
        
        //?????


    }

    public void FinishBuild() // when 0 days left for production is reached this method sees to change status of object to finished 
    {                         // Removes the InProduction == trueand increments the equipment list etc..
        if (DaysUntilFinished == 0)
        {
            //remove GUI indicating build in progress and also emptying graphical representation in build bay
        }

    }




}
