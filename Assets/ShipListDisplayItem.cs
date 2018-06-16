using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipListDisplayItem : MonoBehaviour {

    public GameObject button;

    public Text shipName;
    public Text shipStatus;
    public Text shipETA;

    public int number;

    public void EnterCockpit ()
    {
        Debug.Log("Aaaaah!");
        GameObject.Find("SpaceCraftRooosterMenu").SetActive(false);
        GameObject.Find("Main_Canvas").SetActive(false);
        Camera.main.gameObject.SetActive(false);
        GameObject.Find("CockPitCamera").GetComponent<Camera>().enabled = true;
        GameObject.Find("CockpitCanvas").GetComponent<Canvas>().enabled = true;

        ShipManager.Instance().UpdateShipInterface(number);

    }

}
