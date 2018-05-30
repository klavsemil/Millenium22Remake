using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

    //This class is to handle the mineral resources on the moon and return true or false to BuildItem if a build is possible and if true deduct -
    //the used resources from the mineral resource pool, each Turn/day the resources should be implemented, and resources should be added by grazers etc. unloading 
    //from the moon Hangar when returning from asteroid mining runs etc. 

    public int WaterOnBase;
    public int TitanOnBase;
    public int AluOnBase;
    public int CopperOnBase;
    public int SilicaOnBase;
    public int IronOnBase;
    public int SilverOnBase;
    public int PlatinumOnBase;
    public int UraniumOnBase;
   
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

    public static ResourceManager instance;

    public void Awake() // only for one instance of this , Google singleton!, This works Only if attached to one object!!!
    {
        instance = this;
    }

    public GameObject WaterText; // this is needed for having a object to put inside the gamedata for changing the text in a Unity GUI text
    public GameObject TitanText;
    public GameObject AluText;
    public GameObject CopperText;
    public GameObject SilicaText;
    public GameObject IronText;
    public GameObject SilverText;
    public GameObject PlatinumText;
    public GameObject UraniumText;

    public void Turn() // This method increments the resource varibles in regard to the resource incrementation and updates GUITEXT!
    {
        // This is made for the moon only, later add on needs another structure for this 

        // If PowerManager.powerSurplus > PowerNeededFor mining{ ....  // Power will always be sufficient
        
     WaterOnBase +=3;
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

        var WaterTextComponent = WaterText.GetComponent<Text>(); //here we need to get the text from the GUI object chosen and insert it in this 
        WaterTextComponent.text = WaterOnBase + "";

        var TitanTextComponent = TitanText.GetComponent<Text>(); //as above just for Titan
        TitanTextComponent.text = TitanOnBase + "";

        var AluTextComponent = AluText.GetComponent<Text>(); //as above just for Alu ...
        AluTextComponent.text = AluOnBase + "";

        var CopperTextComponent = CopperText.GetComponent<Text>();
        CopperTextComponent.text = CopperOnBase + "";

        var SilicaTextComponent = SilicaText.GetComponent<Text>();
        SilicaTextComponent.text = SilicaOnBase + "";

        var IronTextComponent = IronText.GetComponent<Text>();
        IronTextComponent.text = IronOnBase + "";

        var SilverTextComponent = SilverText.GetComponent<Text>();
        SilverTextComponent.text = SilverOnBase + "";

        var PlatinumTextComponent = PlatinumText.GetComponent<Text>();
        PlatinumTextComponent.text = PlatinumOnBase + "";

        var UraniumTextComponent = UraniumText.GetComponent<Text>();
        UraniumTextComponent.text = UraniumOnBase + "";



    } 



}
