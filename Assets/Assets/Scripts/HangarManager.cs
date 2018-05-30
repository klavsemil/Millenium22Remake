using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangarManager : MonoBehaviour {

    //In this class we aslo need to make a method that keeps track on all Ships in the game (using location variable!)

    public BayManager[] Bays = new BayManager[8];   // this is needed to create an array of baymanagers to be able to fill and empty bays when producing and launching etc.
    private static HangarManager instance = null;
    public List<BaseItem> InProductionItems; // This is a list of baseitems which has been chosen for production by player and been accepted as for base having resources enough... 
    public List<BaseItem> FinishedItems; // This is a list of baseitems which is finished.. *****************************


    public GameObject Grazer; // for displaying a grazer in Hangar bay 01
    public GameObject Probe; // for displaying a probe in hanagar bay 01
    public GameObject SIOS; //

    public GameObject Grazer2; // for displaying a grazer in Hangar bay 02
    public GameObject Probe2; // for displaying a probe in hanagar bay 02
    public GameObject Grazer3; // 
    public GameObject Probe3; // 
    public GameObject Grazer4; // 
    public GameObject Probe4; //    

    public GameObject[] HangarPanelInsertion; // An array for insertion of information into the right hangar panel

    public bool InsertShip(BaseItem ship) //
    {
        for(int i = 0 ; i< Bays.Length ; i++)
        {
            if (Bays[i].IsEmpty())
            {
                Bays[i].ship = ship;

                SetValuesInHangarWindow(i,ship); //here we need to set the values of the Panel textfield gameobjects

                return true;
            }
          


        }
        return false; 
    }

    //here we make a method that returns an integer which corresponds to the number of first vacant HangarBay
    public int BayVacancyNrCheck() // not sure if it needs some parameter on allready reserved places from the InproductionITems.length!!! - NOTE . IS NOT USED AT PRESENT
    {
        int FirstvacancyNr = 10;

        for (int j = 0; j < Bays.Length; j++)
        {
            if (Bays[j].IsEmpty()==false)
                FirstvacancyNr = 10; // this is out of range of the 8 places... A magic number (i know!!)
            else
                FirstvacancyNr = j;
        }

        return FirstvacancyNr;
    }

    public void InsertEquipment(BaseItem Equipment) // not sure this needs to be public!!
    {
        InProductionItems.Add(Equipment);
    }

    public void SetValuesInHangarWindow(int BayNumber, BaseItem ship) // this Method fill in the textfields on the hangarbaypanel for chosen vacant Bay
    {

        Bays[BayNumber].gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = ship.ItemName ;
        Bays[BayNumber].gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = ship.ShipName;
        Bays[BayNumber].gameObject.transform.GetChild(6).gameObject.GetComponent<Text>().text = ship.Crew ;

        if(ship.InProduction == true)//if the object in hangar i in production it i informed to the player here
        {
            Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = "SpaceCraft in production! Finish in     turns"; // This Sees to that this bay is just reserved to be in production
            Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ship.TurnsUntillFinished + ""; // setting the number of turns until build is finish            
        }

        //Below is for setting 3D models in of chosen vehicles in hangars
 
        // Below is for displaying this objects loaded resources if there is resources onboard spaceCraft else displaying there is nothing
        if (ship.WaterCarried > 0 || ship.TitanCarried > 0 || ship.IronCarried>0 || ship.AluCarried > 0 || ship.CopperCarried >0 || ship.SilicaCarried > 0 || ship.SilverCarried > 0 || ship.PlatinumCarried > 0 || ship.UraniumCarried > 0)
        {
         Bays[BayNumber].gameObject.transform.GetChild(10).gameObject.GetComponent<Text>().text = "Water: " + ship.WaterCarried + ", " + "Titan: " + ship.TitanCarried + ", " + "Alu: " + ship.AluCarried + ", " + "Copper: " + ship.CopperCarried + ", " + "Silica: " + ship.SilicaCarried + ", " + "Iron: " + ship.IronCarried + ", " + "Silver: " + ship.SilverCarried + ", " + "Platinum: " + ship.PlatinumCarried + ", " + "Uranium: " + ship.UraniumCarried; // plus the other components
        }
        else
        {
            Bays[BayNumber].gameObject.transform.GetChild(10).gameObject.GetComponent<Text>().text = "No Cargo";
        }



    } 

    public void UpdateValuesInHangar(int BayNumber, BaseItem ship) // this method is used for updating information in the bay panels if there is a ship or a ship has reserved space there
    {
        Bays[BayNumber].gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = ship.ItemName;
        Bays[BayNumber].gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = ship.ShipName;
        Bays[BayNumber].gameObject.transform.GetChild(6).gameObject.GetComponent<Text>().text = ship.Crew;

        if (ship.InProduction == true)//if the object in hangar i in production it i informed to the player here
        {
            Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = "SpaceCraft in production! Finish in    turns"; // This Sees to that this bay is just reserved to be in production
            Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ship.TurnsUntillFinished + ""; // setting the number of turns until build is finish            
        }

        if (ship.ItemID == 1 && BayNumber == 0 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a finished Grazer then display it in Hangar
        {
            Grazer.SetActive(true); // Show a grazer in Hangar 01 
        }
        if (ship.ItemID == 0 && BayNumber == 0 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a probe then display it in Hangar
        {
            Probe.SetActive(true); // Show a ptobe in Hangar 01 
        }
        if (ship.ItemID == 4 && BayNumber == 0 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a SIOS Base then display it in Hangar
        {
            SIOS.SetActive(true); // Show a ptobe in Hangar 01 
        }


        if (ship.ItemID == 1 && BayNumber == 1 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
        {
            Grazer2.SetActive(true); // Show a grazer in Hangar 02             
        }
        if (ship.ItemID == 0 && BayNumber == 1 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a probe then display it in Hangar
        {
            Probe2.SetActive(true); // Show a probe in Hangar 02             
        }


        if (ship.ItemID == 1 && BayNumber == 2 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
        {
            Grazer3.SetActive(true); // Show a grazer in Hangar 03             
        }
        if (ship.ItemID == 0 && BayNumber == 2 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Probe then display it in Hangar
        {
            Probe3.SetActive(true); // Show a probe in Hangar 03             
        }


        if (ship.ItemID == 1 && BayNumber == 3 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
        {
            Grazer4.SetActive(true); // Show a grazer in Hangar 04            
        }
        if (ship.ItemID == 0 && BayNumber == 3 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Probe then display it in Hangar
        {
            Probe4.SetActive(true); // Show a probe in Hangar 04             
        }









        // needs to do this for all 8 hangars!
    }










    public bool HasEmptySpot()
    {
        for (int i = 0; i < Bays.Length; i++)
        {
            if (Bays[i].IsEmpty())
            {
                return true;
            }
        }
        return false;
    }
    

    public static HangarManager Instance ()
    {
        if (instance == null)
        {
            
            instance = GameObject.FindObjectOfType<HangarManager>() as HangarManager;
            instance.InProductionItems = new List<BaseItem>();
            instance.FinishedItems = new List<BaseItem>(); // this is for instansiated things that are finished and ready to use ******************************
        }

        return instance;
    }

    public void DisplayEquipmentList()
    {
     





    }




}
