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


    public GameObject Grazer; // for displaying a grazer in Hangar bay 01
    public GameObject Probe; // for displaying a probe in hanagar bay 01
    public GameObject SIOS; //

    public GameObject Grazer2; // for displaying a grazer in Hangar bay 02
    public GameObject Probe2; // for displaying a probe in hanagar bay 02
    public GameObject Grazer3; // 
    public GameObject Probe3; // 
    public GameObject Grazer4; // 
    public GameObject Probe4; //    
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

    //Objects for ship rooster // We have only room for a limited ammount of ships in this game!!!
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
    public GameObject Ship10Access;




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
        HangarManager.Instance().Bays[BayNumber].ship.InMoonBayNr = 0;   //We  set the moonbay status for the ship to zero as it is not more on moon.
        //HangarManager.Instance().ShipsInService.Add(Bays[BayNumber].ship); //add the specific ship from the baynumber where the launch button has been clicked // HOPE THIS IS LIKE A STACK SO IT ADDS FROM THE LAST PLace in the list


        if (FinishedItems.Count > 0) // We also need to remove the ship from the list of finished items at the moon
        {
            for (int i = HangarManager.Instance().FinishedItems.Count-1; i >= 0; i--)
            {
                if (HangarManager.Instance().FinishedItems[i].InMoonBayNr == BayNumber) // HERE WE NEED TO KNOW IF THEindex starts at 0 or 1 ??
                {
                    HangarManager.Instance().FinishedItems.RemoveAt(i);// We Remove the item from the Finished Items list

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
                break;
            }
            counter++;
        }
        //HangarManager.Instance().ShipsInService[ShipsInService.Count-1].InOrbit = true;
        //HangarManager.Instance().ShipsInService[ShipsInService.Count - 1].OnMoon = false;
        Destroy(HangarManager.Instance().Bays[BayNumber].ship); // we see to that the ship is removed correctly from the bay Array

        UpdateValuesInHangar(BayNumber, null);



        if (HangarManager.Instance().ShipsInService.Count>=0) //if there is any ships in the orbit list, then update the cockpitdisplay - NB REMEMBER TORESET OLD VALUES ON Ship cockpit panel
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
        if (ship != null) { 
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
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Space craft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish 
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true); // Make it possible to load/unload craft -------------Not strictly necessary at this point
            }
            if (ship.ItemID == 0 && BayNumber == 0 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a probe then display it in Hangar
            {
                Probe.SetActive(true); // Show a ptobe in Hangar 01 
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish 
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);
            }
            if (ship.ItemID == 4 && BayNumber == 0 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a SIOS Base then display it in Hangar
            {
                SIOS.SetActive(true); // Show a SIOS in Hangar 01 
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SIOS Colony Spacecraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton.SetActive(true);

            }


            if (ship.ItemID == 1 && BayNumber == 1 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
            {
                Grazer2.SetActive(true); // Show a grazer in Hangar 02     
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SpaceCraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish  
                LaunchButton2.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton2.SetActive(true);

            }
            if (ship.ItemID == 0 && BayNumber == 1 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a probe then display it in Hangar
            {
                Probe2.SetActive(true); // Show a probe in Hangar 02     
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton2.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton2.SetActive(true);

            }


            if (ship.ItemID == 1 && BayNumber == 2 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
            {
                Grazer3.SetActive(true); // Show a grazer in Hangar 03  
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SpaceCraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton3.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton3.SetActive(true);

            }
            if (ship.ItemID == 0 && BayNumber == 2 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Probe then display it in Hangar
            {
                Probe3.SetActive(true); // Show a probe in Hangar 03    
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton3.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton3.SetActive(true);



            }


            if (ship.ItemID == 1 && BayNumber == 3 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
            {
                Grazer4.SetActive(true); // Show a grazer in Hangar 04  
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SpaceCraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton4.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton4.SetActive(true);


            }
            if (ship.ItemID == 0 && BayNumber == 3 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Probe then display it in Hangar
            {
                Probe4.SetActive(true); // Show a probe in Hangar 04    
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton4.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton4.SetActive(true);

            }

            if (ship.ItemID == 1 && BayNumber == 4 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
            {
                Grazer5.SetActive(true); // Show a grazer in Hangar 05  
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SpaceCraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton5.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton5.SetActive(true);


            }
            if (ship.ItemID == 0 && BayNumber == 4 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Probe then display it in Hangar
            {
                Probe5.SetActive(true); // Show a probe in Hangar 05    
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton5.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton5.SetActive(true);

            }

            if (ship.ItemID == 1 && BayNumber == 5 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
            {
                Grazer6.SetActive(true); // Show a grazer in Hangar 06  
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SpaceCraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton6.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton6.SetActive(true);


            }
            if (ship.ItemID == 0 && BayNumber == 5 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Probe then display it in Hangar
            {
                Probe6.SetActive(true); // Show a probe in Hangar 06    
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton6.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton6.SetActive(true);

            }

            if (ship.ItemID == 1 && BayNumber == 6 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
            {
                Grazer7.SetActive(true); // Show a grazer in Hangar 07  
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SpaceCraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton7.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton7.SetActive(true);


            }
            if (ship.ItemID == 0 && BayNumber == 6 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Probe then display it in Hangar
            {
                Probe7.SetActive(true); // Show a probe in Hangar 07    
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton7.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton7.SetActive(true);

            }
            if (ship.ItemID == 1 && BayNumber == 7 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Grazer then display it in Hangar
            {
                Grazer8.SetActive(true); // Show a grazer in Hangar 08  
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " SpaceCraft ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish          
                LaunchButton8.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton8.SetActive(true);


            }
            if (ship.ItemID == 0 && BayNumber == 7 && ship.InProduction == false && ship.TurnsUntillFinished <= 0) // If this is a Probe then display it in Hangar
            {
                Probe8.SetActive(true); // Show a probe in Hangar 08    
                Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = " Probe ready in Hangar"; // This sets the ship status
                Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ""; // removes the number of turns until build is finish
                LaunchButton8.SetActive(true); //Make it possible to launch this Spacecraft
                LoadEquipmentButton8.SetActive(true);
            }

        }
        else
        {
            Bays[BayNumber].gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "XXX";
            Bays[BayNumber].gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = "XXX";
            Bays[BayNumber].gameObject.transform.GetChild(6).gameObject.GetComponent<Text>().text = "XXX";
            Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = "XXX";
            Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = "XXX";

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

        var TextComponentShip1Name = TextForShip1Name.GetComponent<Text>();
        var TextComponentShip1Status = TextForShip1Status.GetComponent<Text>();
        var TextComponentShip1ETA = TextForShip1ETA.GetComponent<Text>();

        var TextComponentShip2Name = TextForShip2Name.GetComponent<Text>();
        var TextComponentShip2Status = TextForShip2Status.GetComponent<Text>();
        var TextComponentShip2ETA = TextForShip2ETA.GetComponent<Text>();

        var TextComponentShip3Name = TextForShip3Name.GetComponent<Text>();
        var TextComponentShip3Status = TextForShip3Status.GetComponent<Text>();
        var TextComponentShip3ETA = TextForShip3ETA.GetComponent<Text>();

        var TextComponentShip4Name = TextForShip4Name.GetComponent<Text>();
        var TextComponentShip4Status = TextForShip4Status.GetComponent<Text>();
        var TextComponentShip4ETA = TextForShip4ETA.GetComponent<Text>();

        var TextComponentShip5Name = TextForShip5Name.GetComponent<Text>();
        var TextComponentShip5Status = TextForShip5Status.GetComponent<Text>();
        var TextComponentShip5ETA = TextForShip5ETA.GetComponent<Text>();

        var TextComponentShip6Name = TextForShip6Name.GetComponent<Text>();
        var TextComponentShip6Status = TextForShip6Status.GetComponent<Text>();
        var TextComponentShip6ETA = TextForShip6ETA.GetComponent<Text>();

        var TextComponentShip7Name = TextForShip7Name.GetComponent<Text>();
        var TextComponentShip7Status = TextForShip7Status.GetComponent<Text>();
        var TextComponentShip7ETA = TextForShip7ETA.GetComponent<Text>();

        var TextComponentShip8Name = TextForShip8Name.GetComponent<Text>();
        var TextComponentShip8Status = TextForShip8Status.GetComponent<Text>();
        var TextComponentShip8ETA = TextForShip8ETA.GetComponent<Text>();

        var TextComponentShip9Name = TextForShip9Name.GetComponent<Text>();
        var TextComponentShip9Status = TextForShip9Status.GetComponent<Text>();
        var TextComponentShip9ETA = TextForShip9ETA.GetComponent<Text>();

        var TextComponentShip10Name = TextForShip10Name.GetComponent<Text>();
        var TextComponentShip10Status = TextForShip10Status.GetComponent<Text>();
        var TextComponentShip10ETA = TextForShip10ETA.GetComponent<Text>();

        if (ShipsInService.Count>0)
        {
            Ship1Access.SetActive(true); // set the button for link to cockpit true
            TextComponentShip1Name.text = HangarManager.Instance().ShipsInService[0].ShipName;

            if (HangarManager.Instance().ShipsInService[0].OnMoon == true)
                TextComponentShip1Status.text = "On Moon";

            if (HangarManager.Instance().ShipsInService[0].OnComet == true)
                TextComponentShip1Status.text = "On Comet";

            if (HangarManager.Instance().ShipsInService[0].InAsteroidField == true)
                TextComponentShip1Status.text = "In Asteroid field";

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

            TextComponentShip1ETA.text = HangarManager.Instance().ShipsInService[0].DaysUntilArrival+"";

        }

        if (ShipsInService.Count > 1)
        {
            Ship2Access.SetActive(true);
            TextComponentShip2Name.text = HangarManager.Instance().ShipsInService[1].ShipName;

            if (HangarManager.Instance().ShipsInService[1].OnMoon == true)
                TextComponentShip2Status.text = "On Moon";

            if (HangarManager.Instance().ShipsInService[1].OnComet == true)
                TextComponentShip2Status.text = "On Comet";

            if (HangarManager.Instance().ShipsInService[1].InAsteroidField == true)
                TextComponentShip2Status.text = "In Asteroid field";

            if (HangarManager.Instance().ShipsInService[1].InOrbit == true)
                TextComponentShip2Status.text = "In Orbit Moon";

            if (HangarManager.Instance().ShipsInService[1].InTransitAsteroidField == true)
                TextComponentShip2Status.text = "In Transit to Asteroid Field";

            if (HangarManager.Instance().ShipsInService[1].InTransitComet == true)
                TextComponentShip2Status.text = "In Transit to Comet";

            if (HangarManager.Instance().ShipsInService[1].InTransitMoon == true)
                TextComponentShip2Status.text = "In Transit to Moon";

            if (HangarManager.Instance().ShipsInService[1].InTransitCallisto == true)
                TextComponentShip2Status.text = "In Transit to Callisto";

            TextComponentShip2ETA.text = HangarManager.Instance().ShipsInService[1].DaysUntilArrival + "";

        }

        if (ShipsInService.Count > 2)
        {
            Ship3Access.SetActive(true);
            TextComponentShip3Name.text = HangarManager.Instance().ShipsInService[2].ShipName;

            if (HangarManager.Instance().ShipsInService[2].OnMoon == true)
                TextComponentShip3Status.text = "On Moon";

            if (HangarManager.Instance().ShipsInService[2].OnComet == true)
                TextComponentShip3Status.text = "On Comet";

            if (HangarManager.Instance().ShipsInService[2].InAsteroidField == true)
                TextComponentShip3Status.text = "In Asteroid field";

            if (HangarManager.Instance().ShipsInService[2].InOrbit == true)
                TextComponentShip3Status.text = "In Orbit Moon";

            if (HangarManager.Instance().ShipsInService[2].InTransitAsteroidField == true)
                TextComponentShip3Status.text = "In Transit to Asteroid Field";

            if (HangarManager.Instance().ShipsInService[2].InTransitComet == true)
                TextComponentShip3Status.text = "In Transit to Comet";

            if (HangarManager.Instance().ShipsInService[2].InTransitMoon == true)
                TextComponentShip3Status.text = "In Transit to Moon";

            if (HangarManager.Instance().ShipsInService[2].InTransitCallisto == true)
                TextComponentShip3Status.text = "In Transit to Callisto";

            TextComponentShip3ETA.text = HangarManager.Instance().ShipsInService[2].DaysUntilArrival + "";
        }

        if (ShipsInService.Count > 3)
        {
            Ship4Access.SetActive(true);
            TextComponentShip4Name.text = HangarManager.Instance().ShipsInService[3].ShipName;

            if (HangarManager.Instance().ShipsInService[3].OnMoon == true)
                TextComponentShip4Status.text = "On Moon";

            if (HangarManager.Instance().ShipsInService[3].OnComet == true)
                TextComponentShip4Status.text = "On Comet";

            if (HangarManager.Instance().ShipsInService[3].InAsteroidField == true)
                TextComponentShip4Status.text = "In Asteroid field";

            if (HangarManager.Instance().ShipsInService[3].InOrbit == true)
                TextComponentShip4Status.text = "In Orbit Moon";

            if (HangarManager.Instance().ShipsInService[3].InTransitAsteroidField == true)
                TextComponentShip4Status.text = "In Transit to Asteroid Field";

            if (HangarManager.Instance().ShipsInService[3].InTransitComet == true)
                TextComponentShip4Status.text = "In Transit to Comet";

            if (HangarManager.Instance().ShipsInService[3].InTransitMoon == true)
                TextComponentShip4Status.text = "In Transit to Moon";

            if (HangarManager.Instance().ShipsInService[3].InTransitCallisto == true)
                TextComponentShip4Status.text = "In Transit to Callisto";

            TextComponentShip4ETA.text = HangarManager.Instance().ShipsInService[3].DaysUntilArrival + "";

        }

        if (ShipsInService.Count > 4)
        {
            Ship5Access.SetActive(true);
            TextComponentShip5Name.text = HangarManager.Instance().ShipsInService[4].ShipName;

            if (HangarManager.Instance().ShipsInService[4].OnMoon == true)
                TextComponentShip5Status.text = "On Moon";

            if (HangarManager.Instance().ShipsInService[4].OnComet == true)
                TextComponentShip5Status.text = "On Comet";

            if (HangarManager.Instance().ShipsInService[4].InAsteroidField == true)
                TextComponentShip5Status.text = "In Asteroid field";

            if (HangarManager.Instance().ShipsInService[4].InOrbit == true)
                TextComponentShip5Status.text = "In Orbit Moon";

            if (HangarManager.Instance().ShipsInService[4].InTransitAsteroidField == true)
                TextComponentShip5Status.text = "In Transit to Asteroid Field";

            if (HangarManager.Instance().ShipsInService[4].InTransitComet == true)
                TextComponentShip5Status.text = "In Transit to Comet";

            if (HangarManager.Instance().ShipsInService[4].InTransitMoon == true)
                TextComponentShip5Status.text = "In Transit to Moon";

            if (HangarManager.Instance().ShipsInService[4].InTransitCallisto == true)
                TextComponentShip5Status.text = "In Transit to Callisto";

            TextComponentShip5ETA.text = HangarManager.Instance().ShipsInService[4].DaysUntilArrival + "";

        }

        if (ShipsInService.Count > 5)
        {
            Ship6Access.SetActive(true);
            TextComponentShip1Name.text = HangarManager.Instance().ShipsInService[5].ShipName;

            if (HangarManager.Instance().ShipsInService[5].OnMoon == true)
                TextComponentShip6Status.text = "On Moon";

            if (HangarManager.Instance().ShipsInService[5].OnComet == true)
                TextComponentShip6Status.text = "On Comet";

            if (HangarManager.Instance().ShipsInService[5].InAsteroidField == true)
                TextComponentShip6Status.text = "In Asteroid field";

            if (HangarManager.Instance().ShipsInService[5].InOrbit == true)
                TextComponentShip6Status.text = "In Orbit Moon";

            if (HangarManager.Instance().ShipsInService[5].InTransitAsteroidField == true)
                TextComponentShip6Status.text = "In Transit to Asteroid Field";

            if (HangarManager.Instance().ShipsInService[5].InTransitComet == true)
                TextComponentShip6Status.text = "In Transit to Comet";

            if (HangarManager.Instance().ShipsInService[5].InTransitMoon == true)
                TextComponentShip6Status.text = "In Transit to Moon";

            if (HangarManager.Instance().ShipsInService[5].InTransitCallisto == true)
                TextComponentShip6Status.text = "In Transit to Callisto";

            TextComponentShip6ETA.text = HangarManager.Instance().ShipsInService[5].DaysUntilArrival + "";

        }
        if (ShipsInService.Count > 6)
        {
            Ship7Access.SetActive(true);
            TextComponentShip7Name.text = HangarManager.Instance().ShipsInService[6].ShipName;

            if (HangarManager.Instance().ShipsInService[6].OnMoon == true)
                TextComponentShip7Status.text = "On Moon";

            if (HangarManager.Instance().ShipsInService[6].OnComet == true)
                TextComponentShip7Status.text = "On Comet";

            if (HangarManager.Instance().ShipsInService[6].InAsteroidField == true)
                TextComponentShip7Status.text = "In Asteroid field";

            if (HangarManager.Instance().ShipsInService[6].InOrbit == true)
                TextComponentShip7Status.text = "In Orbit Moon";

            if (HangarManager.Instance().ShipsInService[6].InTransitAsteroidField == true)
                TextComponentShip7Status.text = "In Transit to Asteroid Field";

            if (HangarManager.Instance().ShipsInService[6].InTransitComet == true)
                TextComponentShip7Status.text = "In Transit to Comet";

            if (HangarManager.Instance().ShipsInService[6].InTransitMoon == true)
                TextComponentShip7Status.text = "In Transit to Moon";

            if (HangarManager.Instance().ShipsInService[6].InTransitCallisto == true)
                TextComponentShip7Status.text = "In Transit to Callisto";

            TextComponentShip7ETA.text = HangarManager.Instance().ShipsInService[6].DaysUntilArrival + "";

        }

        if (ShipsInService.Count > 7)
        {
            Ship8Access.SetActive(true);
            TextComponentShip8Name.text = HangarManager.Instance().ShipsInService[7].ShipName;

            if (HangarManager.Instance().ShipsInService[7].OnMoon == true)
                TextComponentShip8Status.text = "On Moon";

            if (HangarManager.Instance().ShipsInService[7].OnComet == true)
                TextComponentShip8Status.text = "On Comet";

            if (HangarManager.Instance().ShipsInService[7].InAsteroidField == true)
                TextComponentShip8Status.text = "In Asteroid field";

            if (HangarManager.Instance().ShipsInService[7].InOrbit == true)
                TextComponentShip8Status.text = "In Orbit Moon";

            if (HangarManager.Instance().ShipsInService[7].InTransitAsteroidField == true)
                TextComponentShip8Status.text = "In Transit to Asteroid Field";

            if (HangarManager.Instance().ShipsInService[7].InTransitComet == true)
                TextComponentShip8Status.text = "In Transit to Comet";

            if (HangarManager.Instance().ShipsInService[7].InTransitMoon == true)
                TextComponentShip8Status.text = "In Transit to Moon";

            if (HangarManager.Instance().ShipsInService[7].InTransitCallisto == true)
                TextComponentShip8Status.text = "In Transit to Callisto";

            TextComponentShip8ETA.text = HangarManager.Instance().ShipsInService[7].DaysUntilArrival + "";
        }

        if (ShipsInService.Count > 8)
        {
            Ship9Access.SetActive(true);
            TextComponentShip9Name.text = HangarManager.Instance().ShipsInService[8].ShipName;

            if (HangarManager.Instance().ShipsInService[8].OnMoon == true)
                TextComponentShip9Status.text = "On Moon";

            if (HangarManager.Instance().ShipsInService[8].OnComet == true)
                TextComponentShip9Status.text = "On Comet";

            if (HangarManager.Instance().ShipsInService[8].InAsteroidField == true)
                TextComponentShip9Status.text = "In Asteroid field";

            if (HangarManager.Instance().ShipsInService[8].InOrbit == true)
                TextComponentShip9Status.text = "In Orbit Moon";

            if (HangarManager.Instance().ShipsInService[8].InTransitAsteroidField == true)
                TextComponentShip9Status.text = "In Transit to Asteroid Field";

            if (HangarManager.Instance().ShipsInService[8].InTransitComet == true)
                TextComponentShip9Status.text = "In Transit to Comet";

            if (HangarManager.Instance().ShipsInService[8].InTransitMoon == true)
                TextComponentShip9Status.text = "In Transit to Moon";

            if (HangarManager.Instance().ShipsInService[8].InTransitCallisto == true)
                TextComponentShip9Status.text = "In Transit to Callisto";

            TextComponentShip9ETA.text = HangarManager.Instance().ShipsInService[8].DaysUntilArrival + "";

        }
        if (ShipsInService.Count > 9)
        {
            Ship10Access.SetActive(true);
            TextComponentShip10Name.text = HangarManager.Instance().ShipsInService[9].ShipName;

            if (HangarManager.Instance().ShipsInService[9].OnMoon == true)
                TextComponentShip10Status.text = "On Moon";

            if (HangarManager.Instance().ShipsInService[9].OnComet == true)
                TextComponentShip10Status.text = "On Comet";

            if (HangarManager.Instance().ShipsInService[9].InAsteroidField == true)
                TextComponentShip10Status.text = "In Asteroid field";

            if (HangarManager.Instance().ShipsInService[9].InOrbit == true)
                TextComponentShip10Status.text = "In Orbit Moon";

            if (HangarManager.Instance().ShipsInService[9].InTransitAsteroidField == true)
                TextComponentShip10Status.text = "In Transit to Asteroid Field";

            if (HangarManager.Instance().ShipsInService[9].InTransitComet == true)
                TextComponentShip10Status.text = "In Transit to Comet";

            if (HangarManager.Instance().ShipsInService[9].InTransitMoon == true)
                TextComponentShip10Status.text = "In Transit to Moon";

            if (HangarManager.Instance().ShipsInService[9].InTransitCallisto == true)
                TextComponentShip10Status.text = "In Transit to Callisto";

            TextComponentShip10ETA.text = HangarManager.Instance().ShipsInService[9].DaysUntilArrival + "";

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
