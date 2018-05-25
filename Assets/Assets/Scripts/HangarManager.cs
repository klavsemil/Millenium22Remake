using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangarManager : MonoBehaviour {

    //In this class we aslo need to make a method that keeps track on all Ships in the game (using location variable!)

    public BayManager[] Bays = new BayManager[8];   // this is needed to create an array of baymanagers to be able to fill and empty bays when producing and launching etc.
    private static HangarManager instance = null;

    public GameObject[] HangarPanelInsertion; // TESTING !!!

    public bool InsertShip(BaseItem ship) // not sure this needs to be public!!
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

   public void SetValuesInHangarWindow(int BayNumber, BaseItem ship) // thi Method fill in the tetfields on the hangarbaypanel for chosen vacant Bay
    {

        Bays[BayNumber].gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text= ship.ItemName ;
        Bays[BayNumber].gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = ship.ItemName + "0"+ship.ItemID;
        Bays[BayNumber].gameObject.transform.GetChild(6).gameObject.GetComponent<Text>().text = ship.Crew ;

        if (ship.InProduction == true)//if the object in hangar i in production it i informed to the player here
        {
            Bays[BayNumber].gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = "SpaceCraft in production! Finish in        turns"; // This Sees to that this bay is just reserved to be in production
            Bays[BayNumber].gameObject.transform.GetChild(11).gameObject.GetComponent<Text>().text = ship.TurnsUntillFinished + ""; // setting the number of turns until build is finish
        }

        




        Bays[BayNumber].gameObject.transform.GetChild(10).gameObject.GetComponent<Text>().text = "Water: " + ship.WaterCarried + ", " + "Titan: " + ship.TitanCarried + ", " + "Alu: " + ship.AluCarried + ", " + "Copper: " + ship.CopperCarried + ", " + "Silica: " + ship.SilicaCarried + ", " + "Iron: " + ship.IronCarried + ", " + "Silver: " + ship.SilverCarried + ", " + "Platinum: " + ship.PlatinumCarried + ", " + "Uranium: " + ship.UraniumCarried; // plus the other components

        

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
        }

        return instance;
    }






}
