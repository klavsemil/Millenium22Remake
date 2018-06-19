using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangarManager : MonoBehaviour {

    //In this class we aslo need to make a method that keeps track on all Ships in the game (using location variable!)

    public BayManager[] Bays = new BayManager[8];   // this is needed to create an array of baymanagers to be able to fill and empty bays when producing and launching etc.
    private static HangarManager instance = null;
    public List<BaseItem> InProductionItems; // This is a list of baseitems which has been chosen for production by player and been accepted as for base having resources enough... 
    public List<BaseItem> FinishedItems; // This is a list of baseitems which is finished.. *****************************

    public List<BaseItem> ShipsInService; //Not sure about this list yet.

    public bool AnythingLaunched = false;
    public GameObject MarsWarningPanel;

    public GameObject Grazer; // for displaying a grazer in Hangar bay 01
    public GameObject Probe; // for displaying a probe in hanagar bay 01
    public GameObject SIOS; //
    public GameObject Waverider;
    public GameObject Carrack; 

    public GameObject Grazer2; // for displaying a grazer in Hangar bay 02
    public GameObject Probe2; // for displaying a probe in hanagar bay 02
    public GameObject Waverider2;
    public GameObject Carrack2;
    public GameObject Grazer3; // 
    public GameObject Probe3; // 
    public GameObject Waverider3;
    public GameObject Carrack3;
    public GameObject Grazer4; // 
    public GameObject Probe4; //   
    public GameObject Waverider4;
    public GameObject Carrack4;
    public GameObject Grazer5; // 
    public GameObject Probe5; //    
    public GameObject Grazer6; // 
    public GameObject Probe6; //    
    public GameObject Grazer7; // 
    public GameObject Probe7; //    
    public GameObject Grazer8; // 
    public GameObject Probe8; //   

    public GameObject LaunchButton; //
    public GameObject LoadEquipmentButton; // 
    public GameObject LaunchButton2;
    public GameObject LoadEquipmentButton2; //
    public GameObject LaunchButton3;
    public GameObject LoadEquipmentButton3; //
    public GameObject LaunchButton4;
    public GameObject LoadEquipmentButton4; //
    public GameObject LaunchButton5;
    public GameObject LoadEquipmentButton5;
    public GameObject LaunchButton6;
    public GameObject LoadEquipmentButton6; //
    public GameObject LaunchButton7;
    public GameObject LoadEquipmentButton7; //
    public GameObject LaunchButton8;
    public GameObject LoadEquipmentButton8; //


    public GameObject TextForNrOfSolagenMk2;
    public GameObject TextForNrOfSolagenMk3;
    public GameObject TextForNrOfSolagenMk4;
    public GameObject TextForNrOfFusionPowerGenerators;
    public GameObject TextForNrOfFighters;
    public GameObject TextForNrOfOrbitalLasers;
    public GameObject TextForNrOfLivingQuarters;
    public GameObject TextForNrOfBunkers;
    public GameObject TextForNrOfVaccines;
    public GameObject TextForNrOfTerraFormers;
    public GameObject TextForNrOfRadars;

    public int NrOfSolaGenMk2;
    public int NrOfSolaGenMk3;
    public int NrOfSolaGenMk4;
    public int NrOfFusionPowerGenerators;
    public int NrOfFighters;
    public int NrOfOrbitalLasers;
    public int NrOfLivingQuarters;
    public int NrOfBunkers;
    public int NrOfVaccines;
    public int NrOfTerraformers;
    public int NrOfRadars;

    public GameObject SolagenMK2; // object for diplaying solarpanels when they are finished build
    public GameObject SolagenMK3;
    public GameObject SolagenMK4;


    public GameObject shipDisplayPrefab;
    public GameObject shipListDisplay;

    //Objects for ship rooster // We have only room for a limited ammount of ships in this game!!!
    /*public GameObject[] TextForShipName;
    public GameObject[] TextForShipStatus;
    public GameObject[] TextForShipETA;
    public GameObject TextForShip1Name; 
    public GameObject TextForShip1Status;
    public GameObject TextForShip1ETA;
    public GameObject TextForShip2Name;
    public GameObject TextForShip2Status;
    public GameObject TextForShip2ETA;
    public GameObject TextForShip3Name;
    public GameObject TextForShip3Status;
    public GameObject TextForShip3ETA;
    public GameObject TextForShip4Name;
    public GameObject TextForShip4Status;
    public GameObject TextForShip4ETA;
    public GameObject TextForShip5Name;
    public GameObject TextForShip5Status;
    public GameObject TextForShip5ETA;
    public GameObject TextForShip6Name;
    public GameObject TextForShip6Status;
    public GameObject TextForShip6ETA;
    public GameObject TextForShip7Name;
    public GameObject TextForShip7Status;
    public GameObject TextForShip7ETA;
    public GameObject TextForShip8Name;
    public GameObject TextForShip8Status;
    public GameObject TextForShip8ETA;
    public GameObject TextForShip9Name;
    public GameObject TextForShip9Status;
    public GameObject TextForShip9ETA;
    public GameObject TextForShip10Name;
    public GameObject TextForShip10Status;
    public GameObject TextForShip10ETA;

    public GameObject Ship1Access;
    public GameObject Ship2Access;
    public GameObject Ship3Access;
    public GameObject Ship4Access;
    public GameObject Ship5Access;
    public GameObject Ship6Access;
    public GameObject Ship7Access;
    public GameObject Ship8Access;
    public GameObject Ship9Access;
    public GameObject Ship10Access;*/


    public GameObject[] HangarPanelInsertion; // An array for insertion of information into the right hangar panel

    public bool InsertShip(BaseItem ship) //
    {
        for(int i = 0 ; i< Bays.Length ; i++)
        {
            if (Bays[i].IsEmpty())
            {
                ship.InMoonBayNr = i+1; //we count up Moon bays from 1 not zero
                Bays[i].ship = ship;

                SetValuesInHangarWindow(i,ship); //here we need to set the values of the Panel textfield gameobjects

                return true;
            }
          
        }
        return false; 
    }

    public void LaunchShipFromSurface(int BayNumber) //This will empty the hangarbay selected by pressing launch button and should Return the Finished itemnumber so its information can be displayed in the cockpit 
    {

        if (AnythingLaunched == false) // This makes the threat message from the martian appear when launcing the first time
        {
            // display warning sign from Martians
            MarsWarningPanel.SetActive(true);
            AnythingLaunched = true; // make it so this message does not appear again
        }

        HangarManager.Instance().Bays[BayNumber].ship.InMoonBayNr = 0;   //We  set the moonbay status for the ship to zero as it is not more on moon.
        //HangarManager.Instance().ShipsInService.Add(Bays[BayNumber].ship); //add the specific ship from the baynumber where the launch button has been clicked // HOPE THIS IS LIKE A STACK SO IT ADDS FROM THE LAST PLace in the list

        // Problem Problem Problem... This erases the equipmentlist because a method in Nexturn (I believe) get outof bound...  
        if (FinishedItems.Count > 0) // We also need to remove the ship from the list of finished items at the moon
        {
            for (int i = HangarManager.Instance().FinishedItems.Count-1; i >= 0; i--)
            {
                if (HangarManager.Instance().FinishedItems[i].InMoonBayNr == BayNumber) // HERE WE NEED TO KNOW IF THEindex starts at 0 or 1 ??
                {
                   // HangarManager.Instance().FinishedItems.RemoveAt(i);// We Remove the item from the Finished Items list

                }
            }
        }


        int counter = 0;
        foreach(BaseItem obj in ShipsInService)
        {

            // if something breaks, check if the shipidentifer actually is in the list
            if (obj.ShipIdentifier == Bays[BayNumber].ship.ShipIdentifier)
            {
                obj.InOrbit = true;
                obj.OnMoon = false;
                /*obj.DaysUntilArrival = 0;
                obj.InOrbitCallisto = false;
                obj.InorbitMars = false;
                obj.InAsteroidField = false;
                obj.OnComet = false;
                obj.AutoMineRunAsteroids = false;
                obj.AutoMineRunComet = false;
                */
                break;
            }
            counter++;
        }
        //HangarManager.Instance().ShipsInService[ShipsInService.Count-1].InOrbit = true;
        //HangarManager.Instance().ShipsInService[ShipsInService.Count - 1].OnMoon = false;
        Destroy(HangarManager.Instance().Bays[BayNumber].ship); // we see to that the ship is removed correctly from the bay Array

        UpdateValuesInHangar(BayNumber, null);



        if (HangarManager.Instance().ShipsInService.Count>=0) //if there is any ships in the orbit list, then update the cockpitdisplay - NB REMEMBER TO RESET OLD VALUES ON Ship cockpit panel
        ShipManager.Instance().UpdateShipInterface(counter); // Set the textfields that fits the last index in ShipsInOrbit list



    }

    //here we make a method that returns an integer which corresponds to the number of first vacant HangarBay -This is not used..
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

        Bays[BayNumber].panel.transform.GetChild(2).gameObject.GetComponent<Text>().text = ship.ItemName ;
        Bays[BayNumber].panel.transform.GetChild(4).gameObject.GetComponent<Text>().text = ship.ShipName;
        Bays[BayNumber].panel.transform.GetChild(6).gameObject.GetComponent<Text>().text = ship.Crew ;

        if(ship.InProduction == true)//if the object in hangar i in production it i informed to the player here
        {
            Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = "SpaceCraft in production! Finish in     turns"; // This Sees to that this bay is just reserved to be in production
            Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ship.TurnsUntillFinished + ""; // setting the number of turns until build is finish            
        }

        //Below is for setting 3D models in of chosen vehicles in hangars
 
        // Below is for displaying this objects loaded resources if there is resources onboard spaceCraft else displaying there is nothing
        if (ship.WaterCarried > 0 || ship.TitanCarried > 0 || ship.IronCarried>0 || ship.AluCarried > 0 || ship.CopperCarried >0 || ship.SilicaCarried > 0 || ship.SilverCarried > 0 || ship.PlatinumCarried > 0 || ship.UraniumCarried > 0)
        {
         Bays[BayNumber].panel.transform.GetChild(10).gameObject.GetComponent<Text>().text = "Water: " + ship.WaterCarried + ", " + "Titan: " + ship.TitanCarried + ", " + "Alu: " + ship.AluCarried + ", " + "Copper: " + ship.CopperCarried + ", " + "Silica: " + ship.SilicaCarried + ", " + "Iron: " + ship.IronCarried + ", " + "Silver: " + ship.SilverCarried + ", " + "Platinum: " + ship.PlatinumCarried + ", " + "Uranium: " + ship.UraniumCarried; // plus the other components
        }
        else
        {
            Bays[BayNumber].panel.transform.GetChild(10).gameObject.GetComponent<Text>().text = "No Cargo";
        }
    } 

    public void UpdateValuesInHangar(int BayNumber, BaseItem ship) // this method is used for updating information in the bay panels if there is a ship or a ship has reserved space there
    {
        if (ship != null) { 
            Bays[BayNumber].panel.transform.GetChild(2).gameObject.GetComponent<Text>().text = ship.ItemName;
            Bays[BayNumber].panel.transform.GetChild(4).gameObject.GetComponent<Text>().text = ship.ShipName;
            Bays[BayNumber].panel.transform.GetChild(6).gameObject.GetComponent<Text>().text = ship.Crew;

            if (ship.InProduction == true)//if the object in hangar i in production it i informed to the player here
            {
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = "SpaceCraft in production! Finish in    turns"; // This Sees to that this bay is just reserved to be in production
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ship.TurnsUntillFinished + ""; // setting the number of turns until build is finish            
            }

            if (ship.ItemID == 1 && BayNumber == 0 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a finished Grazer then display it in Hangar
            {
                Grazer.SetActive(true); // Show a grazer in Hangar 01 
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Space craft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish 
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true); // Make it possible to load/unload craft -------------Not strictly necessary at this point
            }
            if (ship.ItemID == 0 && BayNumber == 0 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a probe then display it in Hangar
            {
                Probe.SetActive(true); // Show a ptobe in Hangar 01 
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish 
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);
            }
            if (ship.ItemID == 4 && BayNumber == 0 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a SIOS Base then display it in Hangar
            {
                SIOS.SetActive(true); // Show a SIOS in Hangar 01 
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SIOS Colony Spacecraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);

            }
            if (ship.ItemID == 3 && BayNumber == 0 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a SIOS Base then display it in Hangar
            {
                Carrack.SetActive(true); // Show a Carrack in Hangar 01 
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Carrack Spacecraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);

            }


            if (ship.ItemID == 1 && BayNumber == 1 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
            {
                Grazer2.SetActive(true); // Show a grazer in Hangar 02     
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SpaceCraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton2.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton2.SetActive(true);

            }
            if (ship.ItemID == 0 && BayNumber == 1 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a probe then display it in Hangar
            {
                Probe2.SetActive(true); // Show a probe in Hangar 02     
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton2.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton2.SetActive(true);

            }

            if (ship.ItemID == 4 && BayNumber == 1 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a SIOS Base then display it in Hangar
            {
                SIOS.SetActive(true); // Show a SIOS in Hangar 01 ! THIS IS actually howed in the middle of the hangar and therefore can be applied to all hangarbays 
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SIOS Colony Spacecraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);

            }
            if (ship.ItemID == 3 && BayNumber == 1 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a SIOS Base then display it in Hangar
            {
                Carrack2.SetActive(true); // Show a Carrack in Hangar 02 
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Carrack Spacecraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);

            }

            if (ship.ItemID == 1 && BayNumber == 2 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
            {
                Grazer3.SetActive(true); // Show a grazer in Hangar 03  
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SpaceCraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton3.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton3.SetActive(true);

            }
            if (ship.ItemID == 0 && BayNumber == 2 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Probe then display it in Hangar
            {
                Probe3.SetActive(true); // Show a probe in Hangar 03    
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton3.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton3.SetActive(true);
            }

            if (ship.ItemID == 4 && BayNumber == 2 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a SIOS Base then display it in Hangar
            {
                SIOS.SetActive(true); // Show a SIOS in Hangar 
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SIOS Colony Spacecraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);

            }
            if (ship.ItemID == 3 && BayNumber == 2 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a SIOS Base then display it in Hangar
            {
                Carrack3.SetActive(true); // Show a Carrack in Hangar 3
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Carrack Spacecraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);

            }

            if (ship.ItemID == 1 && BayNumber == 3 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
            {
                Grazer4.SetActive(true); // Show a grazer in Hangar 04  
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SpaceCraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton4.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton4.SetActive(true);
            }
            if (ship.ItemID == 0 && BayNumber == 3 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Probe then display it in Hangar
            {
                Probe4.SetActive(true); // Show a probe in Hangar 04    
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton4.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton4.SetActive(true);

            }
            if (ship.ItemID == 4 && BayNumber == 3 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a SIOS Base then display it in Hangar
            {
                SIOS.SetActive(true); // Show a SIOS in Hangar 
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SIOS Colony Spacecraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);
            }
            if (ship.ItemID == 3 && BayNumber == 3 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a SIOS Base then display it in Hangar
            {
                Carrack4.SetActive(true); // Show a Carrack in Hangar 02 
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Carrack Spacecraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);

            }

            if (ship.ItemID == 1 && BayNumber == 4 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
            {
                Grazer5.SetActive(true); // Show a grazer in Hangar 05  
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SpaceCraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton5.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton5.SetActive(true);
            }
            if (ship.ItemID == 0 && BayNumber == 4 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Probe then display it in Hangar
            {
                Probe5.SetActive(true); // Show a probe in Hangar 05    
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton5.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton5.SetActive(true);
            }

            if (ship.ItemID == 4 && BayNumber == 4 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a SIOS Base then display it in Hangar
            {
                SIOS.SetActive(true); // Show a SIOS in Hangar 
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SIOS Colony Spacecraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);
            }

            if (ship.ItemID == 1 && BayNumber == 5 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
            {
                Grazer6.SetActive(true); // Show a grazer in Hangar 06  
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SpaceCraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton6.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton6.SetActive(true);


            }
            if (ship.ItemID == 0 && BayNumber == 5 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Probe then display it in Hangar
            {
                Probe6.SetActive(true); // Show a probe in Hangar 06    
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton6.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton6.SetActive(true);
            }
            if (ship.ItemID == 4 && BayNumber == 5 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a SIOS Base then display it in Hangar
            {
                SIOS.SetActive(true); // Show a SIOS in Hangar 
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SIOS Colony Spacecraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);

            }



            if (ship.ItemID == 1 && BayNumber == 6 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
            {
                Grazer7.SetActive(true); // Show a grazer in Hangar 07  
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SpaceCraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton7.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton7.SetActive(true);


            }
            if (ship.ItemID == 0 && BayNumber == 6 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Probe then display it in Hangar
            {
                Probe7.SetActive(true); // Show a probe in Hangar 07    
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton7.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton7.SetActive(true);
            }
            if (ship.ItemID == 4 && BayNumber == 6 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a SIOS Base then display it in Hangar
            {
                SIOS.SetActive(true); // Show a SIOS in Hangar 
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SIOS Colony Spacecraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);
            }

            if (ship.ItemID == 1 && BayNumber == 7 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
            {
                Grazer8.SetActive(true); // Show a grazer in Hangar 08  
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SpaceCraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton8.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton8.SetActive(true);
            }
            if (ship.ItemID == 0 && BayNumber == 7 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Probe then display it in Hangar
            {
                Probe8.SetActive(true); // Show a probe in Hangar 08    
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish
                LaunchButton8.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton8.SetActive(true);
            }
            if (ship.ItemID == 4 && BayNumber == 7 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a SIOS Base then display it in Hangar
            {
                SIOS.SetActive(true); // Show a SIOS in Hangar 
                Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SIOS Colony Spacecraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);

            }







        }
        else
        {
            Bays[BayNumber].panel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "XXX";
            Bays[BayNumber].panel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "XXX";
            Bays[BayNumber].panel.transform.GetChild(6).gameObject.GetComponent<Text>().text = "XXX";
            Bays[BayNumber].panel.transform.GetChild(8).gameObject.GetComponent<Text>().text = "XXX";
            Bays[BayNumber].panel.transform.GetChild(11).gameObject.GetComponent<Text>().text = "XXX";

        }

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
            instance.ShipsInService = new List<BaseItem>(); // this list is for ships in orbit
        }

        return instance;
    }

    public void DisplayShipsInList() // This method displays finished items in the Spacecraft roosterlist
    {

        List<GameObject> toBeDeleted = new List<GameObject>();

        for(int i = 0; i < shipListDisplay.transform.childCount; i++)
        {
            if (shipListDisplay.transform.GetChild(i).CompareTag("ShipListItem")) toBeDeleted.Add(shipListDisplay.transform.GetChild(i).gameObject);
        }

        while(toBeDeleted.Count > 0)
        {
            GameObject obj = toBeDeleted[0];
            toBeDeleted.Remove(obj);
            Destroy(obj);
        }

        for(int i = 0; i < ShipsInService.Count; i++)
        {
            GameObject currDisplay = Object.Instantiate(shipDisplayPrefab, shipListDisplay.transform);
            Vector3 pos = currDisplay.GetComponent<RectTransform>().localPosition;
            pos.y = 9 - i * 5;
            currDisplay.GetComponent<RectTransform>().localPosition = pos;

            ShipListDisplayItem currDisp = currDisplay.GetComponent<ShipListDisplayItem>();
            currDisp.number = i;
            currDisp.shipName.text = HangarManager.Instance().ShipsInService[i].ShipName;

            if (HangarManager.Instance().ShipsInService[i].OnMoon == true)
                currDisp.shipStatus.text = "On Moon";

            if (HangarManager.Instance().ShipsInService[i].OnComet == true)
                currDisp.shipStatus.text = "On Comet";

            if (HangarManager.Instance().ShipsInService[i].OnComet == true && HangarManager.Instance().ShipsInService[i].AutoMineRunComet == true && HangarManager.Instance().ShipsInService[i].CurrentlyMining == true)
                currDisp.shipStatus.text = ">>Automining Comet<<";

            if (HangarManager.Instance().ShipsInService[i].InAsteroidField == true)
                currDisp.shipStatus.text = "In Asteroid field";

            if (HangarManager.Instance().ShipsInService[i].InAsteroidField == true && HangarManager.Instance().ShipsInService[i].AutoMineRunAsteroids == true && HangarManager.Instance().ShipsInService[i].CurrentlyMining == true)
                currDisp.shipStatus.text = ">>Automining Asteroids<<";

            if (HangarManager.Instance().ShipsInService[i].InOrbit == true)
                currDisp.shipStatus.text = "In Orbit Moon";

            if (HangarManager.Instance().ShipsInService[i].InTransitAsteroidField == true)
                currDisp.shipStatus.text = "In Transit to Asteroid Field";

            if (HangarManager.Instance().ShipsInService[i].InTransitComet == true)
                currDisp.shipStatus.text = "In Transit to Comet";

            if (HangarManager.Instance().ShipsInService[i].InTransitMoon == true)
                currDisp.shipStatus.text = "In Transit to Moon";

            if (HangarManager.Instance().ShipsInService[i].InTransitCallisto == true)
                currDisp.shipStatus.text = "In Transit to Callisto";


            currDisp.shipETA.text = HangarManager.Instance().ShipsInService[i].DaysUntilArrival + "";
        }


        if (ShipsInService.Count>0)
        {
            /*Ship1Access.SetActive(true); // set the button for link to cockpit true
            TextComponentShip1Name.text = HangarManager.Instance().ShipsInService[0].ShipName;

            if (HangarManager.Instance().ShipsInService[0].OnMoon == true)
                TextComponentShip1Status.text = "On Moon";

            if (HangarManager.Instance().ShipsInService[0].OnComet == true)
                TextComponentShip1Status.text = "On Comet";
///////////////////
            if (HangarManager.Instance().ShipsInService[0].OnComet == true && HangarManager.Instance().ShipsInService[0].AutoMineRunComet==true && HangarManager.Instance().ShipsInService[0].CurrentlyMining == true)
                TextComponentShip1Status.text = ">>Automining Comet<<";


            if (HangarManager.Instance().ShipsInService[0].InAsteroidField == true)
                TextComponentShip1Status.text = "In Asteroid field";
///////////////////
            if (HangarManager.Instance().ShipsInService[0].InAsteroidField == true && HangarManager.Instance().ShipsInService[0].AutoMineRunAsteroids==true && HangarManager.Instance().ShipsInService[0].CurrentlyMining == true)
                TextComponentShip1Status.text = ">>Automining Asteroids<<";




            if (HangarManager.Instance().ShipsInService[0].InOrbit == true)
                TextComponentShip1Status.text = "In Orbit Moon";

            if (HangarManager.Instance().ShipsInService[0].InTransitAsteroidField == true)
                TextComponentShip1Status.text = "In Transit to Asteroid Field";

            if (HangarManager.Instance().ShipsInService[0].InTransitComet == true)
                TextComponentShip1Status.text = "In Transit to Comet";



            if (HangarManager.Instance().ShipsInService[0].InTransitMoon == true)
                TextComponentShip1Status.text = "In Transit to Moon";

            if (HangarManager.Instance().ShipsInService[0].InTransitCallisto == true)
                TextComponentShip1Status.text = "In Transit to Callisto";

            */

        }




    }



    //for (int i = HangarManager.Instance().ShipsInService.Count - 1; i >= 0; i--)
    //{
    //here we need to set the text elements and the link to ship interface button for all possible ships 
    //}




















    public void DisplayEquipmentList() //updating equipmentlist
    {

        var TextComponentSolaGenMK2 = TextForNrOfSolagenMk2.GetComponent<Text>();
        var TextComponentSolaGenMK3 = TextForNrOfSolagenMk3.GetComponent<Text>();
        var TextComponentSolaGenMK4 = TextForNrOfSolagenMk4.GetComponent<Text>();
        var TextComponentFusionPower = TextForNrOfFusionPowerGenerators.GetComponent<Text>();
        var TextComponentFighters = TextForNrOfFighters.GetComponent<Text>();
        var TextComponentOrbitalLasers = TextForNrOfOrbitalLasers.GetComponent<Text>();
        var TextComponentLivingQuarters = TextForNrOfLivingQuarters.GetComponent<Text>();
        var TextComponentBunkers = TextForNrOfBunkers.GetComponent<Text>();
        var TextComponentVaccines = TextForNrOfVaccines.GetComponent<Text>();
        var TextComponentTerraFormers = TextForNrOfTerraFormers.GetComponent<Text>();
        var TextComponentRadars = TextForNrOfRadars.GetComponent<Text>();


            NrOfSolaGenMk2 = 0; // we reset this as it is called every turn and should count the pieces of Solagenmk2 in the finished itemlist up from zero
            NrOfSolaGenMk3 = 0;
            NrOfSolaGenMk4 = 0;
            NrOfFusionPowerGenerators = 0;
            NrOfFighters = 0;
            NrOfOrbitalLasers = 0;
            NrOfLivingQuarters = 0;
            NrOfBunkers = 0;
            NrOfVaccines = 0;
            NrOfTerraformers = 0;
            NrOfRadars = 0;


        for (int k = 0; k < HangarManager.Instance().FinishedItems.Count; k++) // We count through the items in the finisheditemslist 
        {
            if (HangarManager.Instance().FinishedItems[k].ItemID == 6)// && HangarManager.Instance().InProductionItems.Count==1 // finding thoe that are spacecraft + LATER: also we need to know where the ship position is!!!! All clones should be set to the moon when being created 
                NrOfSolaGenMk2++;

            if (HangarManager.Instance().FinishedItems[k].ItemID == 7)// && HangarManager.Instance().InProductionItems.Count==1 // finding thoe that are spacecraft + LATER: also we need to know where the ship position is!!!! All clones should be set to the moon when being created 
                NrOfSolaGenMk3++;

            if (HangarManager.Instance().FinishedItems[k].ItemID == 8)// && HangarManager.Instance().InProductionItems.Count==1 // finding thoe that are spacecraft + LATER: also we need to know where the ship position is!!!! All clones should be set to the moon when being created 
                NrOfSolaGenMk4++;

            if (HangarManager.Instance().FinishedItems[k].ItemID == 9)// && HangarManager.Instance().InProductionItems.Count==1 // finding thoe that are spacecraft + LATER: also we need to know where the ship position is!!!! All clones should be set to the moon when being created 
                NrOfFusionPowerGenerators++;

            if (HangarManager.Instance().FinishedItems[k].ItemID == 10)// && HangarManager.Instance().InProductionItems.Count==1 // finding thoe that are spacecraft + LATER: also we need to know where the ship position is!!!! All clones should be set to the moon when being created 
                NrOfFighters++;

            if (HangarManager.Instance().FinishedItems[k].ItemID == 11)// && HangarManager.Instance().InProductionItems.Count==1 // finding thoe that are spacecraft + LATER: also we need to know where the ship position is!!!! All clones should be set to the moon when being created 
                NrOfOrbitalLasers++;

            if (HangarManager.Instance().FinishedItems[k].ItemID == 13)// && HangarManager.Instance().InProductionItems.Count==1 // finding thoe that are spacecraft + LATER: also we need to know where the ship position is!!!! All clones should be set to the moon when being created 
                NrOfLivingQuarters++;

            if (HangarManager.Instance().FinishedItems[k].ItemID == 14)// && HangarManager.Instance().InProductionItems.Count==1 // finding thoe that are spacecraft + LATER: also we need to know where the ship position is!!!! All clones should be set to the moon when being created 
                NrOfBunkers++;

            if (HangarManager.Instance().FinishedItems[k].ItemID == 15)
                NrOfVaccines++;

            if (HangarManager.Instance().FinishedItems[k].ItemID == 16)// 
                NrOfTerraformers++;

            if (HangarManager.Instance().FinishedItems[k].ItemID == 17)// 
                NrOfRadars++;


        }

        TextComponentSolaGenMK2.text = NrOfSolaGenMk2 + "";  // display the counted number of equipment
        TextComponentSolaGenMK3.text = NrOfSolaGenMk3 + "";
        TextComponentSolaGenMK4.text = NrOfSolaGenMk4 + "";
        TextComponentFusionPower.text = NrOfFusionPowerGenerators + "";
        TextComponentFighters.text = NrOfFighters + "";
        TextComponentOrbitalLasers.text = NrOfOrbitalLasers + "";
        TextComponentLivingQuarters.text = NrOfLivingQuarters + "";
        TextComponentBunkers.text = NrOfBunkers + "";
        TextComponentVaccines.text = NrOfVaccines + "";
        TextComponentTerraFormers.text = NrOfTerraformers + "";
        TextComponentRadars.text = NrOfRadars + "";


    }




}
