using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    // as there is not so many things in game i keep all in here, Which is not correct, 'I KNOW!!'.
public class BaseItem : ScriptableObject {

    public int ItemID; //unique identifier for this item in dbase
    public int ItemEntryNumber; // specific identifier number which matches with a specific kind of item -see seperate list for this below.
    public string ItemName; //Specific Name for this kind of item
    public int ItemTypeNr; //1 for vehicle, 2 for Energy Item, 3 for Weapon item, 4 for supplementary - Not sure thi i important 
    public string ItemDecription; // short text decription

    public bool InProduction; // true if the item is currently in production
    public int TurnsUntillFinished; //if in production it shows how many turns remaining to finish item, if zero is reached it is finished    

    public int PowerNeeded;//how much power is needed to produce this item

    public int WaterNeeded; // how much water (in tons) is needed to produce this item
    public int TitanNeeded; // 
    public int AluNeeded; // 
    public int CopperNeeded; // 
    public int SiliconNeeded; // 
    public int IronNeeded; // 
    public int SilverNeeded; // 
    public int PlatinumNeeded;
    public int UraniumNeeded;

    public bool Researched; // true if it has been researched if not true it should not be possible to produce! - not SURE
    public int TurnsUntillResearched; // It keeps track on how long time is left before research is finished. 0 means it is fully researched

    public int Weight; // this i important to count up freight limit etc.
    public bool LoadableItem; //if true, this item can be loaded into transsport ships 
    public int MaxLoad; // if this is a ship it should have a max load of freight probes have 0, grazer 250, carrack 1000, fleet carrier 1000
    public int LoadValue; //this display current weight of loaded items - 
                          // NOTE a method needss to check if something can be loaded onto this ship without excceeding load limit        

    public bool IsCrewed; //All should be set to true except for vehicles which is set to false until they are fueled

    public string Crew;
    public string ShipName; // this only applies to ships

    public bool AsteroidMiningCapabillity; // this i only true for grazer
    public bool AsteroidFound; //only for Grazer

    public bool OnMoon; // true if item is on moon
    public bool OnShip; // true if item is on ship
    public bool InTransitAsteroidField; // This is true if vehicle is in transit
    public bool InTransitMoon;
    public bool InTransitComet;
    public bool InTransitCallisto;
    public bool InTransitMars;
    public bool InAsteroidField; // if true this vehicle is in asteroid field - NOTE for this prototype we only have this other location 
    public bool OnComet; // near comet
    public int DaysUntilArrival; 
        
    // Future location status should be here

    public int ShipIdentifier; // this is zero if not on ship but otherwise it should fit a ItemID for the ship carrying it.

    public int WaterCarried; // These only applies to loadable spacecraft. 
    public int TitanCarried; 
    public int AluCarried;
    public int CopperCarried;
    public int SilicaCarried;
    public int IronCarried;
    public int SilverCarried;
    public int PlatinumCarried;
    public int UraniumCarried;

    //public GameObject TurnCounterForProductionText; // TEST TEST TEST *************** for deducting turns in GUI *********************************************************
   
    //Other functions here: like install, load on ship, destroy, etc..??

    public void Launch() // we need a method on the launchbutton to set the detination of a ship
    {
        if (InProduction == true)// if this vehicle is being produced it is not to be able to launch
            return;

        if (ItemTypeNr == 1 && OnMoon == true && InTransitAsteroidField == true)//if this is a vehicle on the moon and is set to fly to asteroid field                
            DaysUntilArrival = 17; //set the duration untill arrival for thiss object to be 17 turn/days

        if (ItemTypeNr == 1 && OnMoon == true && InTransitComet == true)//if this is a vehicle on the moon and is set to fly to asteroid field                
            DaysUntilArrival = 35; //set the duration untill arrival for thiss object to be 35 turn/days to arrive at comet


        if (ItemTypeNr == 1 && InAsteroidField == true && InTransitMoon == true)//if this is a vehicle in the asteroid field and set to fly to the moon
            DaysUntilArrival = 17; //set the duration untill arrival for thiss object to be 17 turn/days

        if (ItemTypeNr == 1 && InAsteroidField == true && InTransitComet == true)//if this is a vehicle in the asteroid field and set to fly to the comet
            DaysUntilArrival = 25; //set the duration untill arrival for this object to be 25 turn/days to arrive at comet


        if (ItemTypeNr == 1 && OnComet == true && InTransitMoon == true)//if this is a vehicle on the comet and set to fly to the moon
            DaysUntilArrival = 35; //set the duration untill arrival for thiss object to be 17 turn/days

        if (ItemTypeNr == 1 && InAsteroidField == true && InTransitAsteroidField == true)//if this is a vehicle on the comet field and set to fly to the Asteroid field
            DaysUntilArrival = 25; //set the duration untill arrival for this object to be 25 turn/days to arrive at comet

        //for multiple destinations this method need to be remade!

    }

    public void Land() //when a ship is in transit to the moon and days untill arrival reaches 0
    {
        if(InTransitMoon == true && DaysUntilArrival == 0)
        {
            OnMoon = HangarManager.Instance().InsertShip(this);

            if (!OnMoon)
            {
                //Handle no-space in bays
            }
            //For production part:
            //HangarManager.instance().InsertShip(Instantiate(Resources.Load("BaseItems/Grazer") as BaseItem));


            InTransitMoon = false;  
 
        }

    }

    public int LoadShip(BaseItem LoadItem) //this is to check if an (another unique ID) item can be loaded onto spcific instance of ship 
    {

        // Check if item is loadable
        // check if item does not outweight the weight limit of ship
        // if passing both above load item onto ship (or in this prototype case the resources like Water,Iron etc.) 

        return 0;// if it was not possible to load the item return 0 to avoid increment the load on the ship
    }

    public void ProgressBuild()
    {

        //var TextComponent3 = TurnCounterForProductionText.GetComponent<Text>();

        if (this.TurnsUntillFinished > 0 && this.InProduction == true)
        {
            this.TurnsUntillFinished--;
            Debug.Log("Turns untill finished for this object: " + this.TurnsUntillFinished);
         // TextComponent3.text = "" + this.TurnsUntillFinished; // this is for displaying the number of turns left for this object to be finished **********************************************


        }
          
        else
        {
            Debug.Log("Production is finished for this object Finished method call here");
            //this.InProduction = false;   // We also need to remove this object from the inbuild progress list and into the finished stuff list
            FinishBuild(); // call the finish build function
        }


    }

    private int count; // for counting up eleements in the finished list

    public void FinishBuild()
    {
        HangarManager.Instance().FinishedItems.Add(this); // take this object and put it into the list of finished items
                                                          /*HangarManager.Instance().InProductionItems.Remove(this);*/ // take this object and remove it from the list of items which is in production
        this.InProduction = false; // As this item is finished we set it to false 
        

        foreach (var FinishedItem in HangarManager.Instance().FinishedItems) // trying to read out the list of build things
        {
            Debug.Log("Finished Item nr " + count +" is:" + HangarManager.Instance().FinishedItems + FinishedItem);// Show in the Debug log whats on this list to show iterate through
            count++;


        }

    } 





}
