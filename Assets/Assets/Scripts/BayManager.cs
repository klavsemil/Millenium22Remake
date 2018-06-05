using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayManager : MonoBehaviour {

    public BaseItem ship;

    

    public bool IsEmpty() //check to see if a bay is empty
    {
        if (ship == null)
        {           
            return true;
        }

        else
            return false;
    }

    public bool Launch() //empty this specific launchbay
    {
        if (ship == null)
            return false; //you cannot launch a non exiting ship from a bay
        ship.Launch(); //call the BaseItem method  Launch() !!!?
        ship = null; // set this bay to be empty
        return true;
    }






}
