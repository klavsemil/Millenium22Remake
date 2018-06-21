using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

    //This class is to handle the mineral resources on the moon and return true or false to BuildItem if a build is possible and if true deduct -
    //the used resources from the mineral resource pool, each Turn/day the resources should be implemented, and resources should be added by grazers etc. unloading 
    //from the moon Hangar when returning from asteroid mining runs etc. 

    //private static ResourceManager instance = null;

    public int WaterOnBase;
    public int TitanOnBase;
    public int AluOnBase;
    public int CopperOnBase;
    public int SilicaOnBase;
    public int IronOnBase;
    public int SilverOnBase;
    public int PlatinumOnBase;
    public int UraniumOnBase;

    public int EnergyOnBase = 170; //Basic Battery = 7KW , Solagen MK1 = 170 KW
    public bool HasSolagenMK2; // Not sure on this yet 
   
    public bool HasResourcesFor(BaseItem ProductionItem) //if the object being asked for (the parameter BaseItem object) have the necessary resources//THIS also deducts resources if true
    {
        if (ProductionItem.WaterNeeded <= WaterOnBase && ProductionItem.TitanNeeded <= TitanOnBase && ProductionItem.AluNeeded <= AluOnBase && ProductionItem.CopperNeeded <= CopperOnBase && ProductionItem.SiliconNeeded <= SilicaOnBase && ProductionItem.IronNeeded <= IronOnBase && ProductionItem.SilverNeeded <= SilverOnBase && ProductionItem.PlatinumNeeded <= PlatinumOnBase && ProductionItem.UraniumNeeded <= UraniumOnBase)
        {
            //deduct resources from moonbase
             WaterOnBase = WaterOnBase - ProductionItem.WaterNeeded;
             TitanOnBase = TitanOnBase - ProductionItem.TitanNeeded;
             AluOnBase = AluOnBase - ProductionItem.AluNeeded;
             CopperOnBase = CopperOnBase - ProductionItem.CopperNeeded;
             SilicaOnBase = SilicaOnBase - ProductionItem.SiliconNeeded;
             IronOnBase = IronOnBase - ProductionItem.IronNeeded;
             SilverOnBase = SilverOnBase - ProductionItem.SilverNeeded;
             PlatinumOnBase = PlatinumOnBase - ProductionItem.PlatinumNeeded;
             UraniumOnBase = UraniumOnBase - ProductionItem.UraniumNeeded;


            //call text update
           
            UpdateResourceText();


            return true;
        }
           
        else
            return false;

    }

    public bool HasEnergyFor(BaseItem ProductionItem)
    {
        EnergyOnBase = 170;

        for(int i =0; i<HangarManager.Instance().FinishedItems.Count; i++)
        {
            if (HangarManager.Instance().FinishedItems[i].ItemID==6)
            {
                if(EnergyOnBase >=170)
                EnergyOnBase = 680;
                
            }
            if (HangarManager.Instance().FinishedItems[i].ItemID == 7)
            {
                if(EnergyOnBase >= 680)
                EnergyOnBase = 3200;
           
            }
            if (HangarManager.Instance().FinishedItems[i].ItemID == 8)
            {
                if (EnergyOnBase >= 3200)
                EnergyOnBase = 25000;
               
            }
            if (HangarManager.Instance().FinishedItems[i].ItemID == 9)
            {
                if(EnergyOnBase >=25000)
                EnergyOnBase = 37000;
              
            }

        }
        Debug.Log("ENERGY ON BASE: " + EnergyOnBase);
        if (ProductionItem.PowerNeeded <= EnergyOnBase)
        {
            return true;

        }
        else return false;


    }
    



    public static ResourceManager instance;

    public void Awake() // only for one instance of this , Google singleton!, This works Only if attached to one object!!!
    {
        instance = this;
    }
    // IF ARRAY THING GOE WRONG CHANGE IT BACK
    public GameObject[] WaterText = new GameObject[2]; // this is needed for having a object to put inside the gamedata for changing the text in a Unity GUI text
    public GameObject[] TitanText = new GameObject[2];
    public GameObject[] AluText = new GameObject[2];
    public GameObject[] CopperText = new GameObject[2];
    public GameObject[] SilicaText = new GameObject[2];
    public GameObject[] IronText = new GameObject[2];
    public GameObject[] SilverText = new GameObject[2];
    public GameObject[] PlatinumText = new GameObject[2];
    public GameObject[] UraniumText = new GameObject[2];

    public void Turn() // This method increments the resource varibles in regard to the resource incrementation and updates GUITEXT!
    {
        // This is made for the moon only, later add on needs another structure for this 

        // If PowerManager.powerSurplus > PowerNeededFor mining{ ....  // Power will always be sufficient
        
     WaterOnBase +=3; //THis Should be changed down for the prototype a there i energy enough
     TitanOnBase+=6;
     AluOnBase+=4;
     CopperOnBase+=0;
     SilicaOnBase+=4;
     IronOnBase+=4;
     SilverOnBase+=0;
     PlatinumOnBase+=0;
     UraniumOnBase+=0;

        UpdateResourceText();

    }



    public void UpdateResourceText()
    {


        for(int i = 0; i < WaterText.Length; i++) //NOT URE YET
        {
            var WaterTextComponent = WaterText[i].GetComponent<Text>(); //here we need to get the text from the GUI object chosen and insert it in this 
            WaterTextComponent.text = WaterOnBase + "";

            var TitanTextComponent = TitanText[i].GetComponent<Text>(); //as above just for Titan
            TitanTextComponent.text = TitanOnBase + "";

            var AluTextComponent = AluText[i].GetComponent<Text>(); //as above just for Alu ...
            AluTextComponent.text = AluOnBase + "";

            var CopperTextComponent = CopperText[i].GetComponent<Text>();
            CopperTextComponent.text = CopperOnBase + "";

            var SilicaTextComponent = SilicaText[i].GetComponent<Text>();
            SilicaTextComponent.text = SilicaOnBase + "";

            var IronTextComponent = IronText[i].GetComponent<Text>();
            IronTextComponent.text = IronOnBase + "";

            var SilverTextComponent = SilverText[i].GetComponent<Text>();
            SilverTextComponent.text = SilverOnBase + "";

            var PlatinumTextComponent = PlatinumText[i].GetComponent<Text>();
            PlatinumTextComponent.text = PlatinumOnBase + "";

            var UraniumTextComponent = UraniumText[i].GetComponent<Text>();
            UraniumTextComponent.text = UraniumOnBase + "";
        }





    } 



}
