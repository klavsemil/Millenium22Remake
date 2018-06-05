using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipManager : MonoBehaviour {

    private static ShipManager instance = null;


    public GameObject TextForNameOfShip;     //this is for the status textfield of the cockpit view for chosen ship!
    public GameObject TextForPositionOfShip;   // this is for displaying where the ship is (or where it is in destination to).






    public void UpdateShipInterface(int ShipNumber)
    {
        //Debug.Log("*********************################################################## ShipNumber parmeter received in updateShipInterface method in Shipmanager: "+ ShipNumber);

        var TextComponentShipName = TextForNameOfShip.GetComponent<Text>(); //link to the shipName textfield .. Remember to set in unity inspector!
        var TextComponentShipLocation = TextForPositionOfShip.GetComponent<Text>();// Link to ShipLocation textfield

        TextComponentShipName.text = HangarManager.Instance().ShipsInOrbit[ShipNumber].ShipName; // we insert the shipname into the testfield for name in the universal cockpit


        if(HangarManager.Instance().ShipsInOrbit[ShipNumber].InOrbit==true)// ****FIXED TO BE TRUE from being nothing!!
        TextComponentShipLocation.text = "In Orbit Moon"; // we insert the shipname into the testfield for name in the universal cockpit

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
