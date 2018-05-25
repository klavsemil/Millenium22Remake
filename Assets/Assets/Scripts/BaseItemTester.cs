using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemTester : MonoBehaviour {

	// Use this for initialization
	void Start () {

        BaseItem item = BaseItemDatabase.GetItem(0);
        if(item != null)
        {
            Debug.Log(string.Format("item ID: {0}, Item Name: {1}, Item Description: {2}", item.ItemID, item.ItemName, item.ItemDecription));
         
        }
        item = BaseItemDatabase.GetItem(1);
        if (item != null)
        {
            Debug.Log(string.Format("item ID: {0}, Item Name: {1}, Item Description: {2}", item.ItemID, item.ItemName, item.ItemDecription));

        }








    }
	
	// Update is called once per frame
	void Update () {
		



	}
}
