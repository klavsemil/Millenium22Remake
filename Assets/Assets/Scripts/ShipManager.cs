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

    public GameObject NavButton;
    public GameObject AutoMineButton;
    public GameObject NavPanel;
    public GameObject AutominePanel;



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


        if(HangarManager.Instance().ShipsInService[ActiveShipNumber].DaysUntilArrival == 0) // If ship is on it way the navigation choices are not available
        {
            NavButton.SetActive(true);
            AutoMineButton.SetActive(true);
            NavPanel.SetActive(true);
            AutominePanel.SetActive(true);
        }
        else
        {
            NavButton.SetActive(false);
            AutoMineButton.SetActive(false);
            NavPanel.SetActive(false);
            AutominePanel.SetActive(false);
        }

        TextComponentShipName.text = HangarManager.Instance().ShipsInService[ShipNumber].ShipName; // we insert the shipname into the testfield for name in the universal cockpit

        TextComponentShipDaysUntillArrival.text = HangarManager.Instance().ShipsInService[ShipNumber].DaysUntilArrival + ""; // Insert Number of turns untill arrival in cockpittextfield


        if (HangarManager.Instance().ShipsInService[ShipNumber].InOrbit==true)// ****FIXED TO BE TRUE from being nothing!!
        TextComponentShipLocation.text = "In Moon Orbit!"; // we insert the shipname into the testfield for name in the universal cockpit
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
                        // VERY IMPORTANT TO GET MESAGE ONTO LOG THAT SHIP HAs ARRIVED

                    }
                    if (HangarManager.Instance().ShipsInService[i].InTransitComet)
                    {
                        HangarManager.Instance().ShipsInService[i].InTransitComet = false;
                        HangarManager.Instance().ShipsInService[i].OnComet = true;
                    }
                    if (HangarManager.Instance().ShipsInService[i].InTransitMoon)
                    {
                        HangarManager.Instance().ShipsInService[i].InTransitMoon = false;
                        HangarManager.Instance().ShipsInService[i].InOrbit = true;
                    }
                    if (HangarManager.Instance().ShipsInService[i].InTransitMars)
                    {
                        HangarManager.Instance().ShipsInService[i].InTransitMars = false;
                        HangarManager.Instance().ShipsInService[i].InorbitMars = true;
                    }
                    if (HangarManager.Instance().ShipsInService[i].InTransitCallisto)
                    {
                        HangarManager.Instance().ShipsInService[i].InTransitCallisto = false;
                        HangarManager.Instance().ShipsInService[i].InOrbitCallisto = true;
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
                        HangarManager.Instance().ShipsInService[i].DaysOnAutoMineAsteroids = 0; // this i to be incremented pr turn elsewhere (not sure where yet)

                        //mesage on automining commencing
                    }





                }

                //future decrement of fuel
            }

            if(HangarManager.Instance().ShipsInService[i].AutoMineRunComet == true && HangarManager.Instance().ShipsInService[i].CurrentlyMining == true)
            {
                HangarManager.Instance().ShipsInService[i].DaysOnAutomineComet++;
                //here we insert chance for ship to find minerals and/or be destroyed by Comet Geyser (Message)
            }

            if (HangarManager.Instance().ShipsInService[i].AutoMineRunAsteroids == true && HangarManager.Instance().ShipsInService[i].CurrentlyMining == true)
            {
                HangarManager.Instance().ShipsInService[i].DaysOnAutoMineAsteroids++;
                //here we insert chance for ship to find minerals and/or be destroyed by Comet Geyser (Message)
            }







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
