using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipManager : MonoBehaviour {

    private static ShipManager instance = null;

    public GameObject TextForNameOfShip;     //This is for the status textfield of the cockpit view for chosen ship!
    public GameObject TextForPositionOfShip;   // This is for displaying where the ship is (or where it is in destination to).
    public GameObject TextForDestinationOfShip; // if destination is set then this is to be changed.
    public GameObject TextForDaysUntillArrival;

    public GameObject TextForWaterAmount;
    public GameObject TextForTitanAmount;
    public GameObject TextForAluAmount;
    public GameObject TextForCopperAmount;
    public GameObject TextForSilicaAmount;
    public GameObject TextForIronAmount;
    public GameObject TextForSilverAmount;
    public GameObject TextForPlatinumAmount;
    public GameObject TextForUraniumAmount;

    public GameObject NavButton;
    public GameObject AutoMineButton;
    public GameObject NavPanel;
    public GameObject AutominePanel;

    public int AutomineChance; // used to calculate chance of finding ore in automine method below
    public int AutomineAccident; // used to calculate risk of losing Grazer.
    public bool CometOreFound;
    public bool AsteroidOreFound;

    public GameObject CometAccidentPicture;
    public GameObject AsteroidAccidentPicture;

    public bool ProbeVisitedMars;
    public GameObject ProbeMarsScan;
    public bool ProbeVisitedCallisto;
    public GameObject ProbeCallistoScan;
    public GameObject Scientist;

    //public GameObject SetAutoMiningCometButton;
    //public GameObject SetAutoMiningAsteroidButton;

    public int ActiveShipNumber;
    
    public void UpdateShipInterface(int ShipNumber)
    {
       
        ActiveShipNumber = ShipNumber; // Activeshipnumber to be used locally to change/update values when certain buttons are pressed in cockpit in method: UpdateShip

        var TextComponentShipName = TextForNameOfShip.GetComponent<Text>(); //link to the shipName textfield .. Remember to set in unity inspector!
        var TextComponentShipLocation = TextForPositionOfShip.GetComponent<Text>();// Link to ShipLocation textfield
        var TextComponentShipDestination = TextForDestinationOfShip.GetComponent<Text>();
        var TextComponentShipDaysUntillArrival = TextForDaysUntillArrival.GetComponent<Text>();


        if(HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival == 0 && HangarManager.Instance().ShipsInService[ActiveShipNumber].ItemID==1) // If ship is on its way, the navigation choices are not available and the automine option only available for Grazer
        {
            NavButton.SetActive(true);
            AutoMineButton.SetActive(true);
            NavPanel.SetActive(false);
            AutominePanel.SetActive(false);
        }
        if(HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival == 0 && HangarManager.Instance().ShipsInService[ActiveShipNumber].ItemID != 1)
        {
            NavButton.SetActive(true);
            AutoMineButton.SetActive(false);
            NavPanel.SetActive(false);
            AutominePanel.SetActive(false);
        }
        if (HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival > 0)
        {
            NavButton.SetActive(false);
            AutoMineButton.SetActive(false);
            NavPanel.SetActive(false);
            AutominePanel.SetActive(false);
        }

        TextComponentShipName.text = HangarManager.Instance().ShipsInService[ShipNumber].ShipName; // we insert the shipname into the testfield for name in the universal cockpit

        TextComponentShipDaysUntillArrival.text = HangarManager.Instance().ShipsInService[ShipNumber].DaysUntilArrival + ""; // Insert Number of turns untill arrival in cockpittextfield


        if (HangarManager.Instance().ShipsInService[ShipNumber].InOrbit==true)// FIXED TO BE TRUE from being nothing!!
        TextComponentShipLocation.text = "In Moon Orbit!"; // we insert the shipname into the textfield for name in the universal cockpit
        if (HangarManager.Instance().ShipsInService[ShipNumber].OnMoon == true)
            TextComponentShipLocation.text = "On Moon";
        if (HangarManager.Instance().ShipsInService[ShipNumber].OnComet == true)
            TextComponentShipLocation.text = "On Comet";
        if (HangarManager.Instance().ShipsInService[ShipNumber].InorbitMars == true)
            TextComponentShipLocation.text = "In Mars Orbit";
        if (HangarManager.Instance().ShipsInService[ShipNumber].InOrbitCallisto == true)
            TextComponentShipLocation.text = "In Callisto Orbit";
        if (HangarManager.Instance().ShipsInService[ShipNumber].InAsteroidField == true)
            TextComponentShipLocation.text = "In Asteroid Field!";
        if (HangarManager.Instance().ShipsInService[ShipNumber].InTransitAsteroidField == true)
        {
            TextComponentShipLocation.text = "In Transit to Asteroid Field";
            TextComponentShipDestination.text = "Asteroid Field";
        }            
        if (HangarManager.Instance().ShipsInService[ShipNumber].InTransitComet == true)
        {
            TextComponentShipLocation.text = "In Transit to Comet";
            TextComponentShipDestination.text = "Comet";
        }         
        if (HangarManager.Instance().ShipsInService[ShipNumber].InTransitMoon == true)
        {
            TextComponentShipLocation.text = "In Transit to Moon";
            TextComponentShipDestination.text = "Moon";
        }
        if (HangarManager.Instance().ShipsInService[ShipNumber].InTransitMars == true)
        {
            TextComponentShipLocation.text = "In Transit to Mars";
            TextComponentShipDestination.text = "Mars";
        }
        if (HangarManager.Instance().ShipsInService[ShipNumber].InTransitCallisto == true)
        {
            TextComponentShipLocation.text = "In Transit to Callisto";
            TextComponentShipDestination.text = "Callisto";
        }         
        if (HangarManager.Instance().ShipsInService[ShipNumber].AutoMineRunComet == true && HangarManager.Instance().ShipsInService[ShipNumber].CurrentlyMining == false )
        {
            TextComponentShipLocation.text = "AutoMine Run to Comet";
            TextComponentShipDestination.text = "Comet (Automining)";
        }            
        if (HangarManager.Instance().ShipsInService[ShipNumber].AutoMineRunAsteroids == true && HangarManager.Instance().ShipsInService[ShipNumber].CurrentlyMining == false)
        {
            TextComponentShipLocation.text = "AutoMine Run to Asteroids";
            TextComponentShipDestination.text = "Asteroids (Automining)";
        }
        if (HangarManager.Instance().ShipsInService[ShipNumber].AutoMineRunComet == true && HangarManager.Instance().ShipsInService[ShipNumber].CurrentlyMining == true)
        {
            TextComponentShipLocation.text = "AutoMining On Comet";
            TextComponentShipDestination.text = ">>Automining<<";
        }
        if (HangarManager.Instance().ShipsInService[ShipNumber].AutoMineRunAsteroids == true && HangarManager.Instance().ShipsInService[ShipNumber].CurrentlyMining == true)
        {
            TextComponentShipLocation.text = "AutoMining In Asteroids";
            TextComponentShipDestination.text = ">>Automining<<";
        }





    }

    public void UpdateShip(int CockpitChoice) //method which is called when ship destination or mission has changed to set the values of ship instance
    {
        // CockpitChoice is corresponding to a code which fits the button pressed in the cockpit: 1 for CometAutominrun enable, 2 for Asteroid autominerun  
        //var TextComponentSolaGenMK2 = 

        //Below is checking if automine run for either comet or Asteroid has been chosen

        //SetAutoMiningCometButton.GetComponent<bool>(); // when the button for automining comet is pressed this is to be true
        //SetAutoMiningAsteroidButton.GetComponent<bool>();

        

        Debug.Log(" CockpitChoice:::::::::::::::" + CockpitChoice);
        //Automining Comet set cockpitchoice = 1
        if (CockpitChoice == 1 && HangarManager.Instance().ShipsInService[ActiveShipNumber].OnMoon != true) //We need to set this for all departure points
        {
            HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunComet = true;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].InTransitComet = true; //
            HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunAsteroids = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].CurrentlyMining = false;
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbit == true) // If InMoon ORBIT TIME TO REACH COMET IS 24 DAYS
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 24;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbit = false; // we now set the ship to not be in moon Orbit as it is not longer there!
            }
                
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InAsteroidField == true) // If In Asteroidfield  TIME TO REACH COMET IS 14 DAYS
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 14;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InAsteroidField = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InorbitMars == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 28;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InorbitMars = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbitCallisto == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 34;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbitCallisto = false;
            }

            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].OnComet == true) // If On Comet TIME TO REACH Asteroid IS 14 DAYS
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 1; // If the ship is on the comet it take one day to initiate (make it easier to program this conditional stuff)
                //HangarManager.Instance().ShipsInService[ActiveShipNumber].OnComet = false; // NOT SURE YET if this will work ok
            }
        }

        //Automining asteroids set
        if (CockpitChoice == 2 && HangarManager.Instance().ShipsInService[ActiveShipNumber].OnMoon != true)
        {
            HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunAsteroids = true;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].InTransitAsteroidField = true; //
            HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunComet = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].CurrentlyMining = false; //
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbit == true) // If InMoon ORBIT TIME TO REACH Asteroidf IS 17 DAYS
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 17;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbit = false;
            }
                
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].OnComet == true) // If On Comet TIME TO REACH Asteroid IS 14 DAYS
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 14;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].OnComet = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InorbitMars == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 8;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InorbitMars = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbitCallisto == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 18;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbitCallisto = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InAsteroidField == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 1;
                //HangarManager.Instance().ShipsInService[ActiveShipNumber].InAsteroidField = false;
            }

        }
        
        //destination moon set
        if (CockpitChoice == 3 && (HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbit != true || HangarManager.Instance().ShipsInService[ActiveShipNumber].OnMoon != true)) //if destination moon is set (3) and the ship is not in moon orbit
        {
            HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunComet = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunAsteroids = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].CurrentlyMining = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].InTransitMoon = true;
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].OnComet == true) // If OnComet TIME TO REACH Moon IS 24 DAYS
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 24;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].OnComet = false;
            }
                
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InAsteroidField == true) // If In Asteroidfield  TIME TO REACH Moon IS 17 DAYS
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 17;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InAsteroidField = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InorbitMars == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 12;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InorbitMars = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbitCallisto == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 32;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbitCallisto = false;
            }

        }
        
        //destination Asteroids set
        if (CockpitChoice == 4 && (HangarManager.Instance().ShipsInService[ActiveShipNumber].InAsteroidField != true || HangarManager.Instance().ShipsInService[ActiveShipNumber].OnMoon != true)) //if destination Asteroid is set (3) and the ship is not in asteroid field orbit
        {
            HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunComet = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunAsteroids = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].CurrentlyMining = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].InTransitAsteroidField = true;
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].OnComet == true) // If OnComet TIME TO REACH asteroids IS 14 DAYS
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 14;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].OnComet = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbit == true) // If InMoon ORBIT TIME TO REACH Asteroidf IS 17 DAYS
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 17;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbit = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InorbitMars == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 8;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InorbitMars = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbitCallisto == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 18;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbitCallisto = false;
            }
        }
        
        //dest. Comet set cockpitchoice = 5
        if (CockpitChoice == 5 && (HangarManager.Instance().ShipsInService[ActiveShipNumber].OnComet != true || HangarManager.Instance().ShipsInService[ActiveShipNumber].OnMoon != true)) //if destination Comet is set (3) and the ship is not on comet or moon surface
        {
            HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunComet = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunAsteroids = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].CurrentlyMining = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].InTransitComet = true;
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InAsteroidField == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 14;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InAsteroidField = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbit == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 24;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbit = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InorbitMars == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 28;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InorbitMars = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbitCallisto == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 34;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbitCallisto = false;
            }

        }
        //dest. Mars set
        if (CockpitChoice == 6 && (HangarManager.Instance().ShipsInService[ActiveShipNumber].InorbitMars != true || HangarManager.Instance().ShipsInService[ActiveShipNumber].OnMoon != true)) //if destination Comet is set (3) and the ship is not on comet or moon surface
        {
            HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunComet = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunAsteroids = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].CurrentlyMining = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].InTransitMars = true;
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InAsteroidField == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 8;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InAsteroidField = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbit == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 12;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbit = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbitCallisto == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 32;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbitCallisto = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].OnComet == true) // If OnComet TIME TO REACH asteroids IS 14 DAYS
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 28;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].OnComet = false;
            }



        }
        //dest. Callisto
        if (CockpitChoice == 7 && (HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbitCallisto != true || HangarManager.Instance().ShipsInService[ActiveShipNumber].OnMoon != true)) //if destination Comet is set (3) and the ship is not on comet or moon surface
        {
            HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunComet = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunAsteroids = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].CurrentlyMining = false;
            HangarManager.Instance().ShipsInService[ActiveShipNumber].InTransitCallisto = true;
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InAsteroidField == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 18;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InAsteroidField = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbit == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 32;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InOrbit = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].InorbitMars == true) // 
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 26;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].InorbitMars = false;
            }
            if (HangarManager.Instance().ShipsInService[ActiveShipNumber].OnComet == true) // If OnComet TIME TO REACH asteroids IS 14 DAYS
            {
                HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival = 34;
                HangarManager.Instance().ShipsInService[ActiveShipNumber].OnComet = false;
            }

        }



        //DEBUG:::::
        if (HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunComet)
         Debug.Log("Is On Autominerun to Comet");
        if (HangarManager.Instance().ShipsInService[ActiveShipNumber].AutoMineRunAsteroids)
         Debug.Log("Is On Autominerun to Asteroid");



        UpdateShipInterface(ActiveShipNumber); // now update the interface

    }

    public void TravelShips() //this method will decrement all ships with days left in DaysUntillArrival called from the NextTurn Class method progress turn
    {
        for(int i = 0; i < HangarManager.Instance().ShipsInService.Count; i++)
        {
            if(HangarManager.Instance().ShipsInService[i].DaysUntilArrival>0)
            {
                HangarManager.Instance().ShipsInService[i].DaysUntilArrival--; // decrementing the days untill arrival
                if (HangarManager.Instance().ShipsInService[i].DaysUntilArrival==0)
                {
                    if (HangarManager.Instance().ShipsInService[i].InTransitAsteroidField)
                    {
                        HangarManager.Instance().ShipsInService[i].InTransitAsteroidField = false;
                        HangarManager.Instance().ShipsInService[i].InAsteroidField = true;
                        // GET MESAGE ONTO LOG THAT SHIP HAs ARRIVED
                       if(HangarManager.Instance().ShipsInService[i].AutoMineRunAsteroids==true)
                             MessageManager.Instance().UpdateMessagePanel("Turn: " + NextTurn.Instance().TurnCounter + ". Spacecraft: " + HangarManager.Instance().ShipsInService[i].ShipName + "\n", "                        Arrived in Asteroid Field, Now >>AutoMining the Asteroid Field<<" + "\n");
                        else MessageManager.Instance().UpdateMessagePanel("Turn: " + NextTurn.Instance().TurnCounter + ". Spacecraft: " + HangarManager.Instance().ShipsInService[i].ShipName + "\n", "                         Arrived in Asteroid Field" + "\n");

                    }
                    if (HangarManager.Instance().ShipsInService[i].InTransitComet)
                    {
                        HangarManager.Instance().ShipsInService[i].InTransitComet = false;
                        HangarManager.Instance().ShipsInService[i].OnComet = true;
                                 if (HangarManager.Instance().ShipsInService[i].AutoMineRunComet == true)
                             MessageManager.Instance().UpdateMessagePanel("Turn: " + NextTurn.Instance().TurnCounter + ". Spacecraft: " + HangarManager.Instance().ShipsInService[i].ShipName + "\n", "                        Arrived in vicinity of Comet, Now >>AutoMining the Comet<<" + "\n");
                        else MessageManager.Instance().UpdateMessagePanel("Turn: " + NextTurn.Instance().TurnCounter + ". Spacecraft: " + HangarManager.Instance().ShipsInService[i].ShipName + "\n", "                        Arrived in vicinity of Comet" + "\n");


                    }
                    if (HangarManager.Instance().ShipsInService[i].InTransitMoon)
                    {
                        HangarManager.Instance().ShipsInService[i].InTransitMoon = false;
                        HangarManager.Instance().ShipsInService[i].InOrbit = true;
                        MessageManager.Instance().UpdateMessagePanel("Turn: " + NextTurn.Instance().TurnCounter + ". Spacecraft: " + HangarManager.Instance().ShipsInService[i].ShipName + "\n", "                        Arrived in Moon Orbit" + "\n");
                    }
                    if (HangarManager.Instance().ShipsInService[i].InTransitMars)
                    {
                        HangarManager.Instance().ShipsInService[i].InTransitMars = false;
                        HangarManager.Instance().ShipsInService[i].InorbitMars = true;
                        MessageManager.Instance().UpdateMessagePanel("Turn: " + NextTurn.Instance().TurnCounter + ". Spacecraft: " + HangarManager.Instance().ShipsInService[i].ShipName + "\n", "                        Arrived in Mars Orbit" + "\n");
                        if (HangarManager.Instance().ShipsInService[i].ItemID == 0 && ProbeVisitedMars == false) // IF a probe visit MArs first time it gets some info before getting destroyed
                        {
                            MessageManager.Instance().UpdateEncounterMessagePanel("Probe scan results from Mars","Your Probe scans the surface of mars and quickly reveals that Musk Space incorporated did indeed set up a large base " +
                                "with quite extensive production facilities, which at the moment seems to be producing small combat spacecrafts. It also looks like humanoid creatures are wandering around the base without any protective helmets on!");
                          
                            ProbeMarsScan.SetActive(true);
                            ProbeVisitedMars = true;
                        }
                            

                    }
                    if (HangarManager.Instance().ShipsInService[i].InTransitCallisto)
                    {
                        HangarManager.Instance().ShipsInService[i].InTransitCallisto = false;
                        HangarManager.Instance().ShipsInService[i].InOrbitCallisto = true;
                        MessageManager.Instance().UpdateMessagePanel("Turn: " + NextTurn.Instance().TurnCounter + ". Spacecraft: " + HangarManager.Instance().ShipsInService[i].ShipName + "\n", "                        Arrived in Callisto Orbit" + "\n");

                        if (HangarManager.Instance().ShipsInService[i].ItemID == 0 && ProbeVisitedCallisto == false) // IF a probe visit MArs first time it gets some info before getting destroyed
                        {
                            MessageManager.Instance().UpdateEncounterMessagePanel("Probe scan results from Callisto", "Your Probe scans the surface of Callisto revealing a rocky moon which has very good deposits of minerals and could be a good " +
                                "candidate for setting up base. Mineable Deposits include: \n\n Titanium \n Copper \n Water \n Iron \n Silver \n\n Inside a crater on the south pole of the moon a wreckage of an old Spacecraft carrier lies partially burried in sand and rocks \n " +
                                "Your Chief Science officer exclaims that this might give the opportunity to build a battle space craft, which could carry our fighters to Mars and defeat the martians! However we need to have a base on Callisto in order to " +
                                "examine the wrecked fleet carrier in detail.");

                            ProbeCallistoScan.SetActive(true);
                            Scientist.SetActive(true);
                            ProbeVisitedCallisto = true;
                        }


                    }
                    if (HangarManager.Instance().ShipsInService[i].AutoMineRunComet)
                    {
                        HangarManager.Instance().ShipsInService[i].CurrentlyMining = true;
                        HangarManager.Instance().ShipsInService[i].DaysOnAutomineComet = 0; // this i to be incremented pr turn elsewhere (not sure where yet)

                        //mesage on automining commencing
                    }
                    if (HangarManager.Instance().ShipsInService[i].AutoMineRunAsteroids)
                    {
                        HangarManager.Instance().ShipsInService[i].CurrentlyMining = true;
                        HangarManager.Instance().ShipsInService[i].DaysOnAutoMineAsteroids = 0; // this i to be incremented pr turn elsewhere in automine method and set to a minus value when ore is found  (not sure where yet)

                        //mesage on automining commencing + Calling Automine method
                    }

                }

                //future decrement of fuel
            }

           

        }

    }

    public void Automine() //called every round from NextTurn.Progresspacetravel
    {
        int AsteroidOreFindFrequency = 0;
        int CometOreFindFrequency = 0;
        int ResourceSetFound; // reused on all reources random calculation 
        int AccidentFrequency = 0;

      
        for (int i = 0; i < HangarManager.Instance().ShipsInService.Count; i++)
        {

            if (HangarManager.Instance().ShipsInService[i].AutoMineRunComet == true && HangarManager.Instance().ShipsInService[i].CurrentlyMining == true)
            {
                HangarManager.Instance().ShipsInService[i].DaysOnAutomineComet++;
                //here we insert chance for ship to find minerals and/or be destroyed by Comet Geyser (Message)

 
                if (HangarManager.Instance().ShipsInService[i].DaysOnAutomineComet < 10 && HangarManager.Instance().ShipsInService[i].DaysOnAutomineComet > 0) // above zero as if it is below zero it is tranporting ore found to the moonbase (remmeber mesage for this!)
                {
                    AutomineChance = 5; // 10 is lowest chance of finding minable ore on comet
                    AutomineAccident = 20;
                    CometOreFindFrequency = Random.Range(0, 100);
                    AccidentFrequency = Random.Range(0, 100);
                    CometOreFindFrequency += AutomineChance;
                    AccidentFrequency += AutomineAccident;

                }
                if (HangarManager.Instance().ShipsInService[i].DaysOnAutomineComet >= 10)
                {
                    AutomineChance = 10; // 
                    AutomineAccident = 30;
                    CometOreFindFrequency = Random.Range(0, 100);
                    AccidentFrequency = Random.Range(0, 100);
                    CometOreFindFrequency += AutomineChance;
                    AccidentFrequency += AutomineAccident;
                }
                Debug.Log(" Comet Ore frequency = "+ CometOreFindFrequency);
                Debug.Log(" accident frequency = " + AccidentFrequency);


                if (AccidentFrequency > 105)
                {
                    MessageManager.Instance().UpdateMessagePanel("Turn: " + NextTurn.Instance().TurnCounter + ". Spacecraft: " + HangarManager.Instance().ShipsInService[i].ShipName + "\n", " A Geyser on the comet errupted right under the ship, and it together with its crew was lost" + "\n"); // here more accident narrative options should be possible
                    CometOreFindFrequency = 0; // Not sure this sis NEccessary.... as we destroy the ship
                    //destroy ship, remove Correctly from ShipsinServiceList 

                    MessageManager.Instance().UpdateEncounterMessagePanel(HangarManager.Instance().ShipsInService[i].ShipName + " lost!!", "During drilling into a rich ore vein on the comet, a pressurized pocket of gas exploded, \n resulting in puncturing the ship hull, which rapidly depresurized the Grazer reulting in an implosion, killing the crew");
                    AsteroidAccidentPicture.SetActive(false);
                    CometAccidentPicture.SetActive(true);
                    HangarManager.Instance().ShipsInService.RemoveAt(i); // check if this works ...................................########%&¤####¤#%¤&%/&(%%¤""#¤%&/
                    continue; // we need to get out of the current loop as its size is now altered and would probably crassh
                }

                if (CometOreFindFrequency > 95) // this has to be balanced out using probability math.
                {
                    // Ore is found
                    HangarManager.Instance().ShipsInService[i].DaysOnAutomineComet = -48; // as it takes 24 day to travel one way between the moon and comet we need to set this back to -48 to make it make this run and not collecting more ore meanwhile (!)  
                    MessageManager.Instance().UpdateMessagePanel("Turn: " + NextTurn.Instance().TurnCounter + ". Spacecraft: " + HangarManager.Instance().ShipsInService[i].ShipName + "\n", "   Found Usable ore vein on Comet -> Autotransporting it to moonbase" + "\n");
                    // calculate what is found TOTAL 250 Tons
                    ResourceSetFound = Random.Range(0, 10);

                    if (ResourceSetFound >= 0 && ResourceSetFound < 4) 
                    {
                        HangarManager.Instance().ShipsInService[i].WaterCarried = 30;
                        HangarManager.Instance().ShipsInService[i].TitanCarried = 30;
                        HangarManager.Instance().ShipsInService[i].AluCarried = 30;
                        HangarManager.Instance().ShipsInService[i].CopperCarried = 30;
                        HangarManager.Instance().ShipsInService[i].SilicaCarried = 40;
                        HangarManager.Instance().ShipsInService[i].IronCarried = 70;
                        HangarManager.Instance().ShipsInService[i].PlatinumCarried = 20;
                    }

                    if (ResourceSetFound >= 4 && ResourceSetFound < 8)
                    {
                        HangarManager.Instance().ShipsInService[i].WaterCarried = 40;
                        HangarManager.Instance().ShipsInService[i].TitanCarried = 20;
                        HangarManager.Instance().ShipsInService[i].AluCarried = 50;
                        HangarManager.Instance().ShipsInService[i].CopperCarried = 20;
                        HangarManager.Instance().ShipsInService[i].SilicaCarried = 60;
                        HangarManager.Instance().ShipsInService[i].IronCarried = 50;
                        HangarManager.Instance().ShipsInService[i].PlatinumCarried = 10;
                    }

                    if (ResourceSetFound >= 8 && ResourceSetFound < 10)
                    {
                        HangarManager.Instance().ShipsInService[i].WaterCarried = 20;
                        HangarManager.Instance().ShipsInService[i].TitanCarried = 40;
                        HangarManager.Instance().ShipsInService[i].AluCarried = 20;
                        HangarManager.Instance().ShipsInService[i].CopperCarried = 40;
                        HangarManager.Instance().ShipsInService[i].SilicaCarried = 40;
                        HangarManager.Instance().ShipsInService[i].IronCarried = 50;
                        HangarManager.Instance().ShipsInService[i].PlatinumCarried = 40;
                    }


                    CometOreFindFrequency = 0; // resetscore to 0 ,NOT SURE THIs IS NEcessary
                }

                if (HangarManager.Instance().ShipsInService[i].DaysOnAutomineComet == -24) // if this number i reached the grazer is exactly back at moon and the resources helda are automatically ofloaded to moon resource pool
                {
                    ResourceManager.instance.WaterOnBase += HangarManager.Instance().ShipsInService[i].WaterCarried;
                    ResourceManager.instance.TitanOnBase += HangarManager.Instance().ShipsInService[i].TitanCarried;
                    ResourceManager.instance.AluOnBase += HangarManager.Instance().ShipsInService[i].AluCarried;
                    ResourceManager.instance.CopperOnBase += HangarManager.Instance().ShipsInService[i].CopperCarried;
                    ResourceManager.instance.IronOnBase += HangarManager.Instance().ShipsInService[i].IronCarried;
                    ResourceManager.instance.SilicaOnBase += HangarManager.Instance().ShipsInService[i].SilicaCarried;
                    ResourceManager.instance.SilverOnBase += HangarManager.Instance().ShipsInService[i].SilverCarried;
                    ResourceManager.instance.PlatinumOnBase += HangarManager.Instance().ShipsInService[i].PlatinumCarried;
                    ResourceManager.instance.UraniumOnBase += HangarManager.Instance().ShipsInService[i].UraniumCarried;

                    MessageManager.Instance().UpdateMessagePanel("Turn: " + NextTurn.Instance().TurnCounter + ". Spacecraft: " + HangarManager.Instance().ShipsInService[i].ShipName + "\n"+"", "  arrived at Moonbase with resources. Returning to Comet to mine! (Automining) \n");

                    HangarManager.Instance().ShipsInService[i].WaterCarried = 0; //After offloading resources Reset it!!
                    HangarManager.Instance().ShipsInService[i].TitanCarried = 0;
                    HangarManager.Instance().ShipsInService[i].AluCarried = 0;
                    HangarManager.Instance().ShipsInService[i].CopperCarried = 0;
                    HangarManager.Instance().ShipsInService[i].SilicaCarried = 0;
                    HangarManager.Instance().ShipsInService[i].IronCarried = 0;
                    HangarManager.Instance().ShipsInService[i].UraniumCarried = 0;

                }

            }

            if (HangarManager.Instance().ShipsInService[i].AutoMineRunAsteroids == true && HangarManager.Instance().ShipsInService[i].CurrentlyMining == true)
            {
                HangarManager.Instance().ShipsInService[i].DaysOnAutoMineAsteroids++;
                //here we insert chance for ship to find minerals and/or be destroyed by Comet Geyser (Message)
                if (HangarManager.Instance().ShipsInService[i].DaysOnAutoMineAsteroids < 10 && HangarManager.Instance().ShipsInService[i].DaysOnAutoMineAsteroids > 0)
                {
                    AutomineChance = 20; // 
                    AutomineAccident = 10;
                    AsteroidOreFindFrequency = Random.Range(0, 100);
                    AccidentFrequency = Random.Range(0, 100);
                    AsteroidOreFindFrequency += AutomineChance;
                    AccidentFrequency += AutomineAccident;
                }
                if (HangarManager.Instance().ShipsInService[i].DaysOnAutoMineAsteroids >= 10)
                {
                    AutomineChance = 40; // 
                    AutomineAccident = 20;
                    AsteroidOreFindFrequency = Random.Range(0, 100);
                    AccidentFrequency = Random.Range(0, 100);
                    AsteroidOreFindFrequency += AutomineChance;
                    AccidentFrequency += AutomineAccident;
                }


                if (AccidentFrequency > 105)
                {
                    MessageManager.Instance().UpdateMessagePanel("Turn: " + NextTurn.Instance().TurnCounter + ". Spacecraft: " + HangarManager.Instance().ShipsInService[i].ShipName + "\n", " A Mysterious little spaceCraft appeared on the grazers radar just before all communiction was lost. The ship crew was lost" + "\n"); // here more accident narrative options should be possible
                    CometOreFindFrequency = 0; // Not sure this sis NEccessary.... as we destroy the ship
                                               //destroy ship, remove Correctly from ShipsinServiceList 
                    MessageManager.Instance().UpdateEncounterMessagePanel(HangarManager.Instance().ShipsInService[i].ShipName+" lost!!", "During prospecting for minable asteroids , a strange looking probe-sized Spacecraft suddenly appeared out of nowhere, \n and started to cut the grazer into pieces very rapidly, Before the crew was able to react they were killed");
                    CometAccidentPicture.SetActive(false);
                    AsteroidAccidentPicture.SetActive(true);
                    HangarManager.Instance().ShipsInService.RemoveAt(i); // check if this works ...................................########%&¤####¤#%¤&%/&(%%¤""#¤%&/NOT SURE Yet
                    continue; // we need to get out of the current loop as its size is now altered and would probably crassh
                }


                if (AsteroidOreFindFrequency > 95) // this has to be balanced out using probability math.
                {
                    // Ore is found
                    HangarManager.Instance().ShipsInService[i].DaysOnAutoMineAsteroids = -34; // as it takes 24 day to travel one way between the moon and asteroids, we need to set this back to -34 to make it make this run and not collecting more ore meanwhile (!)  
                    MessageManager.Instance().UpdateMessagePanel("Turn: " + NextTurn.Instance().TurnCounter + ". Spacecraft: " + HangarManager.Instance().ShipsInService[i].ShipName + "\n", "   Found Usable ore on Asteroid -> Autotransporting it to moonbase" + "\n");
                    // calculate what is found
                    ResourceSetFound = Random.Range(0, 10);

                    if (ResourceSetFound >= 0 && ResourceSetFound < 4)
                    {
                        HangarManager.Instance().ShipsInService[i].WaterCarried = 30;
                        HangarManager.Instance().ShipsInService[i].TitanCarried = 30;
                        HangarManager.Instance().ShipsInService[i].AluCarried = 30;
                        HangarManager.Instance().ShipsInService[i].CopperCarried = 30;
                        HangarManager.Instance().ShipsInService[i].SilicaCarried = 40;
                        HangarManager.Instance().ShipsInService[i].IronCarried = 70;
                        HangarManager.Instance().ShipsInService[i].UraniumCarried = 20;
                    }

                    if (ResourceSetFound >= 4 && ResourceSetFound < 8)
                    {
                        HangarManager.Instance().ShipsInService[i].WaterCarried = 40;
                        HangarManager.Instance().ShipsInService[i].TitanCarried = 20;
                        HangarManager.Instance().ShipsInService[i].AluCarried = 50;
                        HangarManager.Instance().ShipsInService[i].CopperCarried = 20;
                        HangarManager.Instance().ShipsInService[i].SilicaCarried = 60;
                        HangarManager.Instance().ShipsInService[i].IronCarried = 50;
                        HangarManager.Instance().ShipsInService[i].UraniumCarried = 10;
                    }

                    if (ResourceSetFound >= 8 && ResourceSetFound < 10)
                    {
                        HangarManager.Instance().ShipsInService[i].WaterCarried = 20;
                        HangarManager.Instance().ShipsInService[i].TitanCarried = 40;
                        HangarManager.Instance().ShipsInService[i].AluCarried = 20;
                        HangarManager.Instance().ShipsInService[i].CopperCarried = 40;
                        HangarManager.Instance().ShipsInService[i].SilicaCarried = 40;
                        HangarManager.Instance().ShipsInService[i].IronCarried = 50;
                        HangarManager.Instance().ShipsInService[i].UraniumCarried = 40;
                    }
                    //AsteroidOreFindFrequency = 0; // NOT URE IF ¨this reset is necesary
                }

                if (HangarManager.Instance().ShipsInService[i].DaysOnAutoMineAsteroids == -17) // if this number i reached the grazer is exactly back at moon and the resources helda are automatically ofloaded to moon resource pool
                {
                    ResourceManager.instance.WaterOnBase += HangarManager.Instance().ShipsInService[i].WaterCarried;
                    ResourceManager.instance.TitanOnBase += HangarManager.Instance().ShipsInService[i].TitanCarried;
                    ResourceManager.instance.AluOnBase += HangarManager.Instance().ShipsInService[i].AluCarried;
                    ResourceManager.instance.CopperOnBase += HangarManager.Instance().ShipsInService[i].CopperCarried;
                    ResourceManager.instance.IronOnBase += HangarManager.Instance().ShipsInService[i].IronCarried;
                    ResourceManager.instance.SilicaOnBase += HangarManager.Instance().ShipsInService[i].SilicaCarried;
                    ResourceManager.instance.SilverOnBase += HangarManager.Instance().ShipsInService[i].SilverCarried;
                    ResourceManager.instance.PlatinumOnBase += HangarManager.Instance().ShipsInService[i].PlatinumCarried;
                    ResourceManager.instance.UraniumOnBase += HangarManager.Instance().ShipsInService[i].UraniumCarried;

                    MessageManager.Instance().UpdateMessagePanel("Turn: " + NextTurn.Instance().TurnCounter + ". Spacecraft: " + HangarManager.Instance().ShipsInService[i].ShipName + "\n" + "", "  arrived at Moonbase with resources. Returning to asteroid belt to mine! (Automining) \n");


                    HangarManager.Instance().ShipsInService[i].WaterCarried = 0;
                    HangarManager.Instance().ShipsInService[i].TitanCarried = 0;
                    HangarManager.Instance().ShipsInService[i].AluCarried = 0;
                    HangarManager.Instance().ShipsInService[i].CopperCarried = 0;
                    HangarManager.Instance().ShipsInService[i].SilicaCarried = 0;
                    HangarManager.Instance().ShipsInService[i].IronCarried = 0;
                    HangarManager.Instance().ShipsInService[i].UraniumCarried = 0;


                }





            }

            var TextComponentWater = TextForWaterAmount.GetComponent<Text>();
            var TextComponentTitan = TextForTitanAmount.GetComponent<Text>();
            var TextComponentAlu = TextForAluAmount.GetComponent<Text>();
            var TextComponentCopper = TextForCopperAmount.GetComponent<Text>();
            var TextComponentSilica = TextForSilicaAmount.GetComponent<Text>();
            var TextComponentIron = TextForIronAmount.GetComponent<Text>();
            var TextComponentSilver = TextForSilverAmount.GetComponent<Text>();
            var TextComponentPlatinum = TextForPlatinumAmount.GetComponent<Text>();
            var TextComponentUranium = TextForUraniumAmount.GetComponent<Text>();

            TextComponentWater.text = HangarManager.Instance().ShipsInService[i].WaterCarried + "";
            TextComponentTitan.text = HangarManager.Instance().ShipsInService[i].TitanCarried + "";
            TextComponentAlu.text = HangarManager.Instance().ShipsInService[i].AluCarried + "";
            TextComponentCopper.text = HangarManager.Instance().ShipsInService[i].CopperCarried + "";
            TextComponentSilica.text = HangarManager.Instance().ShipsInService[i].SilicaCarried + "";
            TextComponentIron.text = HangarManager.Instance().ShipsInService[i].IronCarried + "";
            TextComponentSilver.text = HangarManager.Instance().ShipsInService[i].SilverCarried + "";
            TextComponentPlatinum.text = HangarManager.Instance().ShipsInService[i].PlatinumCarried + "";
            TextComponentUranium.text = HangarManager.Instance().ShipsInService[i].UraniumCarried + "";

        }





    }


    public static ShipManager Instance() // THIS IS NOT HTE ONLY SINGLETON SO MIGHT BE BAD... (SINGLETON ALSO IN HANGARMANAGER!!!)
    {
        if (instance == null)
        {

            instance = GameObject.FindObjectOfType<ShipManager>() as ShipManager;
        }

        return instance;
    }






}
