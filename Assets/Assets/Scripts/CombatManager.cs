using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour {

    private static CombatManager instance = null;


    public int NrOfEnemyFighters;
    public int EnemyAttackBonus;
    public int PlayerAttackBonus;
    public int EnemyBombingPower;
    public int NrOfFightersReady; // thi variable might need to be renamed
    public int NrOfOrbitalLasers;
    public int OrbitalLasersChosenForFight;
    public bool FightersIncludedInFight=false;
    public bool AllShipInHangarDestroyed; // for removing graphical representation of ships in hangar to be removed...

    public int CombatOutcome; // if plus player wins, if minus martians wins
    public int BombingOutcome;
    public int FightersTobeDestroyed;

    public GameObject SpaceCombatWonPic;
    public GameObject SpaceCombatLostPic;

    public GameObject TextForIncomingFighters;

    public GameObject TextForAvailableFighters;
    public GameObject TextForAvailableLasers;

    public GameObject TextForFightersLaunchedSet;
    public GameObject TextForOrbitalLaserSet;

    public GameObject TextForEnemyPower;
    public GameObject TextForPlayerPower;
    public GameObject TextforOutcome;

    public GameObject TextForResult;
    public GameObject TextForBombingResult;
    public GameObject GameOverPanel; // for if the base is completely bombed. especially if there is no defence

    //public GameObject LaunchButtonRemovedAfterBombing;

    // Below i all graphical ship Game objects to be destroyed + Solar panel
    /*
    public GameObject Grazer; // 
    public GameObject Probe; // 
    public GameObject SIOS; //
    public GameObject Waverider;
    public GameObject Carrack;

    public GameObject Grazer2; // for displaying a grazer in Hangar bay 02
    public GameObject Probe2; // for displaying a probe in hanagar bay 02
    public GameObject Waverider2;
    public GameObject Carrack2;
    public GameObject Grazer3; // 
    public GameObject Probe3; // 
    public GameObject Waverider3;
    public GameObject Carrack3;
    public GameObject Grazer4; // 
    public GameObject Probe4; //   
    public GameObject Waverider4;
    public GameObject Carrack4;
    public GameObject Grazer5; // 
    public GameObject Probe5; //    
    public GameObject Grazer6; // 
    public GameObject Probe6; //    
    public GameObject Grazer7; // 
    public GameObject Probe7; //    
    public GameObject Grazer8; // 
    public GameObject Probe8; //   

    */









    public void SetupEnemy(int AttackNr) // attack number parameter defines what number of enemyfighters are attacking
    {

        var TextComponentEnemyFighters = TextForIncomingFighters.GetComponent<Text>();


        if (AttackNr < 10) // this hould resemble the original game somewhat, where the number of fighters increase overtime multiplied by a randomizer
            NrOfEnemyFighters = (AttackNr * 2)-1; // to be adjusted
        if (AttackNr >=10)
            NrOfEnemyFighters = (AttackNr * 3) - 1; // making it harder after attack 10
        if (NrOfEnemyFighters > 20 && NextTurn.Instance().TurnCounter<150) // After round 150 it has to become more difficult 
            NrOfEnemyFighters = 20;
        if (NrOfEnemyFighters > 30 && NextTurn.Instance().TurnCounter<350) // After round 350 it has to become increasingly more difficult 
            NrOfEnemyFighters = 30;




        EnemyAttackBonus = NrOfEnemyFighters; // To be adjusted later
        TextComponentEnemyFighters.text = "Number of incomming fighters: " + NrOfEnemyFighters;

    }

    public void AddOrbitalLaserToFight()
    {
        var TextComponentChosenLasers = TextForOrbitalLaserSet.GetComponent<Text>();
        for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++)
        {

            if (HangarManager.Instance().FinishedItems[i].ItemID == 11) // if orbital lasers
            {
                OrbitalLasersChosenForFight++;
                HangarManager.Instance().FinishedItems.RemoveAt(i); // a the Orbital laser is single use only it is removed...
                break;
            }
        }
        TextComponentChosenLasers.text = "Number of Orbital Lasers activated: " + OrbitalLasersChosenForFight + "";

    }

    public void AddFightersToFight()
    {
        var TextComponentChosenFighters = TextForFightersLaunchedSet.GetComponent<Text>();
        NrOfFightersReady = 0;
        if (FightersIncludedInFight==false)
        {
            for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++)
            {

                if (HangarManager.Instance().FinishedItems[i].ItemID == 10) // if fighter
                {
                    NrOfFightersReady++;
                }
            }
            TextComponentChosenFighters.text = "Fighters launched: " + NrOfFightersReady + "";
            FightersIncludedInFight = true; //  For the attackressolve panel text it is!!
        }
  
    }

    public void SetupPlayerForces()// Alternatively (int NrOfFighters, int NrOfOrbitalLasers) // at some point number of corvettes at battle scene could be interesting to inert as a 3rd parameter 
    {
        if (NrOfFightersReady == 0 && OrbitalLasersChosenForFight == 0)
        {
            PlayerAttackBonus = -9; // If there is no fighters or Orbital laser then the enemy attacker will get open bombing run on player base!!
        }
        else
        PlayerAttackBonus = 2 + NrOfFightersReady + (OrbitalLasersChosenForFight * 3);
    }

    public void ResolveCombat()
    {
        var TextComponentPlayerPower = TextForPlayerPower.GetComponent<Text>();
        var TextComponentEnemyPower = TextForEnemyPower.GetComponent<Text>();
        var TextComponentOutcome = TextforOutcome.GetComponent<Text>();
        var TextComponentResult = TextForResult.GetComponent<Text>();

        SetupPlayerForces(); // method call give us the playerAttackbonus

        //Combat Calculations
        //To start with a really simple system is setup !!!!!!!!!!!!!

        CombatOutcome = Random.Range(-10, 10) + PlayerAttackBonus - EnemyAttackBonus; // this is to be changed/balanced

        if (CombatOutcome < 0)
        {
            SpaceCombatLostPic.SetActive(true);
            if (CombatOutcome >= -2 && CombatOutcome < 0)
            {
                BombingBase(1);
                // losing upto 5 fighters
                FightersTobeDestroyed = 5;
               
                for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++)
                {
                    if (HangarManager.Instance().FinishedItems[i].ItemID == 10)
                    {
                        HangarManager.Instance().FinishedItems.RemoveAt(i);
                        NrOfFightersReady--;
                        FightersTobeDestroyed--;
                    }
                    if (NrOfFightersReady <= 0 || FightersTobeDestroyed <=0)
                        break;
                }
                HangarManager.Instance().NrOfFighters -= 5;
                TextComponentResult.text = "Your fighters almost fended of the Martian attack, but a single bomber made it through and bombed the moonbase. You lost 5 fighters";
                if (HangarManager.Instance().NrOfFighters <= 0)
                {
                    HangarManager.Instance().NrOfFighters = 0; // If the damage makes numbers of fighters negative it is set to zero!!
                    TextComponentResult.text = "Your fighters almost fended of the Martian attack, but a single bomber made it through and bombed the moonbase. You lost all your fighters";
                }
                    
            }

            //player looses some assets
            if (CombatOutcome >= -4 && CombatOutcome <-2)
            {
                BombingBase(2);
                // +losing fighters
                FightersTobeDestroyed = 7;

                for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++)
                {
                    if (HangarManager.Instance().FinishedItems[i].ItemID == 10)
                    {
                        HangarManager.Instance().FinishedItems.RemoveAt(i);
                        NrOfFightersReady--;
                        FightersTobeDestroyed--;
                    }
                    if (NrOfFightersReady <= 0 || FightersTobeDestroyed <= 0)
                        break;
                }
                HangarManager.Instance().NrOfFighters -= 7;
                TextComponentResult.text = "Your fighters got outmaneuvered by the Martian fighters and around 50% of their bombers made it through to bomb your base. You lost 7 fighters";
                if (HangarManager.Instance().NrOfFighters <= 0)
                {
                    HangarManager.Instance().NrOfFighters = 0; // If the damage makes numbers of fighters negative it is set to zero!!
                    TextComponentResult.text = "Your fighters got outmaneuvered by the Martian fighters and around 50% of their bombers made it through to bomb your base. You lost all your fighters";
                }

            }

            if(CombatOutcome >= -8 && CombatOutcome <-4)
            {
                BombingBase(3);
                // +Losing fighters
                FightersTobeDestroyed = 12;

                for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++)
                {
                    if (HangarManager.Instance().FinishedItems[i].ItemID == 10)
                    {
                        HangarManager.Instance().FinishedItems.RemoveAt(i);
                        NrOfFightersReady--;
                        FightersTobeDestroyed--;
                    }
                    if (NrOfFightersReady <= 0 || FightersTobeDestroyed <= 0)
                        break;
                }
                HangarManager.Instance().NrOfFighters -= 12;
                TextComponentResult.text = "Your fighters got outmaneuvered and hard hit by the Martian fighters, subsequently bombing your base heavily . You lost 12 fighters";
                if (HangarManager.Instance().NrOfFighters <= 0)
                {
                    HangarManager.Instance().NrOfFighters = 0; // If the damage makes numbers of fighters negative it is set to zero!!
                    TextComponentResult.text = "Your fighters got outmaneuvered and hard hit by the Martian fighters, subsequently bombing your base heavily. You lost all your fighters";
                }
            }

            if (CombatOutcome >= -12 && CombatOutcome < -8)
            {
                BombingBase(4);
                // +Losing fighters
                FightersTobeDestroyed = 20;

                for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++)
                {
                    if (HangarManager.Instance().FinishedItems[i].ItemID == 10)
                    {
                        HangarManager.Instance().FinishedItems.RemoveAt(i);
                        NrOfFightersReady--;
                        FightersTobeDestroyed--;
                    }
                    if (NrOfFightersReady <= 0 || FightersTobeDestroyed <= 0)
                        break;
                }
                HangarManager.Instance().NrOfFighters -= 20;
                TextComponentResult.text = "Your fighters got totally outmaneuvered and very hard hit by the Martian fighters, ending in an almost apocalyptical bombing of your base. You lost 20 fighters";
                if (HangarManager.Instance().NrOfFighters <= 0)
                {
                    HangarManager.Instance().NrOfFighters = 0; // If the damage makes numbers of fighters negative it is set to zero!!
                    TextComponentResult.text = "Your fighters got totally outmaneuvered and very hard hit by the Martian fighters , ending in an almost apocalyptical bombing of your base. You lost all your fighters";
                }
            }

            if (CombatOutcome < -12)
            {
                BombingBase(5);
                // +Losing fighters
                FightersTobeDestroyed = 40;

                for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++)
                {
                    if (HangarManager.Instance().FinishedItems[i].ItemID == 10)
                    {
                        HangarManager.Instance().FinishedItems.RemoveAt(i);
                        NrOfFightersReady--;
                        FightersTobeDestroyed--;
                    }
                    if (NrOfFightersReady <= 0 || FightersTobeDestroyed <= 0)
                        break;
                }
                TextComponentResult.text = "Your fighters got completely demolished by the Martian fighters, Ending in the complete and utter destruction of the moon base . You lost all your fighters";
            }

        }
            
            if (CombatOutcome >= 0) //if player won battle
            {
            SpaceCombatWonPic.SetActive(true);
            TextComponentResult.text = "Your Fighters Fended of the Martian attack " + "" + "" ; 
            }
                //Enemy Looses its fighters, but player also looses some fighters etc.s

                TextComponentPlayerPower.text = "Moonbase forces strike delivers: " + PlayerAttackBonus+ " attackforce ";
                TextComponentEnemyPower.text = "Enemyforces strike delivers: " + EnemyAttackBonus+ " attackforce";
                TextComponentOutcome.text = ": " + CombatOutcome;

        NextTurn.Instance().AttackResolvePanel.SetActive(true); // thi panel will how outcome of combat


        // when combat is finished reset values
        NrOfFightersReady = 0;
        NrOfEnemyFighters = 0;
        FightersIncludedInFight = false;

    }

    public void BombingBase(int BombingDamage)
    {
        int DestroyCounter=0;
        var TextComponentBombingResult = TextForBombingResult.GetComponent<Text>();

        AllShipInHangarDestroyed = false;

        if (BombingDamage==1) //with low damage only ressources are affected 
        {
            BombingOutcome = Random.Range(1, 5);
            if(BombingOutcome == 1)
            {
                ResourceManager.instance.WaterOnBase = 0; // 
                TextComponentBombingResult.text = "Your base water tank was hit and you lost all water stored!";
            }
            if(BombingOutcome == 2)
            {
                ResourceManager.instance.TitanOnBase = 0; // 
                TextComponentBombingResult.text = "Your base titanium storage was hit and you lost all titan stored!";
            }
            if(BombingOutcome == 3)
            {
                ResourceManager.instance.AluOnBase = 0; //
                TextComponentBombingResult.text = "Your base aluminium storage was hit and you lost all aluminium stored!";
            }
            if(BombingOutcome == 4)
            {
                ResourceManager.instance.IronOnBase = 0; // 
                TextComponentBombingResult.text = "Your base iron storage was hit and you lost all aluminium stored!";
            }
            if(BombingOutcome == 5)
            {
                ResourceManager.instance.SilicaOnBase = 0; // 
              
                TextComponentBombingResult.text = "Your base silica storage was hit and you lost all aluminium stored!";
            }

        }

        if (BombingDamage == 2) //
        {
            BombingOutcome = Random.Range(1, 5);
            if (BombingOutcome == 1)
            {
                ResourceManager.instance.WaterOnBase = 0; // 
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for(int i = 0; i< HangarManager.Instance().FinishedItems.Count;i++) //find first instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + "was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            break; // when one instance is found break out of loop
                        }                        
                    }
                }               
                TextComponentBombingResult.text += "Your base water tank was hit and you lost all water stored!";
            }
            if (BombingOutcome == 2)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + "was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            break;
                        }
                    }
                }
                ResourceManager.instance.TitanOnBase = 0; // 
                TextComponentBombingResult.text += "Your base titanium storage was hit and you lost all titan stored!";
            }
            if (BombingOutcome == 3)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + "was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            break; //
                        }
                    }
                }
                ResourceManager.instance.AluOnBase = 0; //
                TextComponentBombingResult.text += "Your base aluminium storage was hit and you lost all aluminium stored!";
            }
            if (BombingOutcome == 4)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + "was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            break; //
                        }
                    }
                }
                ResourceManager.instance.IronOnBase = 0; // 
                TextComponentBombingResult.text += "Your base iron storage was hit and you lost all Iron stored!";
            }
            if (BombingOutcome == 5)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + "was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            break; //
                        }
                    }
                }
                ResourceManager.instance.SilicaOnBase = 0; // 
                ResourceManager.instance.IronOnBase = 0; // 
                TextComponentBombingResult.text += "Your base silica and iron storage was hit and you lost all of these stored resources!";
            }

        }

        if (BombingDamage == 3) //
        {
            BombingOutcome = Random.Range(1, 5);
            if (BombingOutcome == 1)
            {
                ResourceManager.instance.PlatinumOnBase = 0; // 
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if(DestroyCounter>1)
                            break; //
                        }
                    }

                }
                TextComponentBombingResult.text += "Your base platinium storage was hit and you lost all platinium stored!";
            }
            if (BombingOutcome == 2)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if (DestroyCounter > 1)
                                break; //
                        }
                    }
                }
                ResourceManager.instance.TitanOnBase = 0; // 
                TextComponentBombingResult.text += "Your base titanium storage was hit and you lost all titan stored!";
            }
            if (BombingOutcome == 3)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if (DestroyCounter > 1)
                                break; //
                        }
                    }
                }
                ResourceManager.instance.AluOnBase = 0; //
                TextComponentBombingResult.text += "Your base aluminium storage was hit and you lost all aluminium stored!";
            }
            if (BombingOutcome == 4)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if (DestroyCounter > 1)
                                break; //
                        }
                    }
                }
                ResourceManager.instance.IronOnBase = 0; // 
                TextComponentBombingResult.text += "Your base iron storage was hit and you lost all iron stored!";
            }
            if (BombingOutcome == 5)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if (DestroyCounter > 1)
                                break; //
                        }
                    }
                }
                ResourceManager.instance.SilicaOnBase = 0; // 
                ResourceManager.instance.IronOnBase = 0; // 
                TextComponentBombingResult.text += "Your base silica and iron storage was hit and you lost all stored of these!";
            }

        }

        if (BombingDamage == 4) //
        {
            BombingOutcome = Random.Range(1, 5);
            if (BombingOutcome == 1)
            {
                ResourceManager.instance.PlatinumOnBase = 0; // 
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if (DestroyCounter > 2)
                                break; //
                        }
                    }
                }
                for(int i = 0; i < HangarManager.Instance().ShipsInService.Count;i++) // this loop destroys all ships in hangar!!
                {
                    if (HangarManager.Instance().ShipsInService[i].OnMoon)
                        HangarManager.Instance().ShipsInService.RemoveAt(i);// 
                        AllShipInHangarDestroyed = true;
                }
                TextComponentBombingResult.text += "the hangar got severely bombed destroying all spacecraft parked there (if any). Finally ";
                ResourceManager.instance.PlatinumOnBase = 0; // 
                TextComponentBombingResult.text += "Your base platinium storage was hit and you lost all platinium stored!";
            }
            if (BombingOutcome == 2)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if (DestroyCounter > 2)
                                break; //
                        }
                    }
                }
                for (int i = 0; i < HangarManager.Instance().ShipsInService.Count; i++) // this loop destroys all ships in hangar!!
                {
                    if (HangarManager.Instance().ShipsInService[i].OnMoon)
                        HangarManager.Instance().ShipsInService.RemoveAt(i);// 
                        AllShipInHangarDestroyed = true;
                }
                TextComponentBombingResult.text += "the hangar got severely bombed destroying all spacecraft parked there (if any). Finally ";
                ResourceManager.instance.PlatinumOnBase = 0; // 
                TextComponentBombingResult.text += "Your base platinium storage was hit and you lost all platinium stored!";
                ResourceManager.instance.TitanOnBase = 0; // 
                TextComponentBombingResult.text += ",Your base titanium storage was hit and you lost all of this stored!";
            }
            if (BombingOutcome == 3)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if (DestroyCounter > 2)
                                break; //
                        }
                    }
                }
                for (int i = 0; i < HangarManager.Instance().ShipsInService.Count; i++) // this loop destroys all ships in hangar!!
                {
                    if (HangarManager.Instance().ShipsInService[i].OnMoon)
                        HangarManager.Instance().ShipsInService.RemoveAt(i);// 
                        AllShipInHangarDestroyed = true;
                }
                TextComponentBombingResult.text += "the hangar got severely bombed destroying all spacecraft parked there (if any). Finally ";
                ResourceManager.instance.PlatinumOnBase = 0; //                  
                ResourceManager.instance.AluOnBase = 0; //
                TextComponentBombingResult.text += "Your base aluminium storage and platinium storage was hit and you lost all of these stored!";
            }
            if (BombingOutcome == 4)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if (DestroyCounter > 2)
                                break; //
                        }
                    }
                }
                for (int i = 0; i < HangarManager.Instance().ShipsInService.Count; i++) // this loop destroys all ships in hangar!!
                {
                    if (HangarManager.Instance().ShipsInService[i].OnMoon)
                        HangarManager.Instance().ShipsInService.RemoveAt(i);// 
                        AllShipInHangarDestroyed = true;
                }
                TextComponentBombingResult.text += "the hangar got severely bombed destroying all spacecraft parked there (if any). Finally ";
                ResourceManager.instance.IronOnBase = 0; // 
                TextComponentBombingResult.text += "Your base iron storage was hit and you lost all this stored!";
            }
            if (BombingOutcome == 5)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if (DestroyCounter > 2)
                                break; //
                        }
                    }
                }
                for (int i = 0; i < HangarManager.Instance().ShipsInService.Count; i++) // this loop destroys all ships in hangar!!
                {
                    if (HangarManager.Instance().ShipsInService[i].OnMoon)
                        HangarManager.Instance().ShipsInService.RemoveAt(i);// 
                        AllShipInHangarDestroyed = true;
                }
                TextComponentBombingResult.text += "the hangar got severelybombed destroying all spacecraft parked there (if any). Finally ";
                ResourceManager.instance.SilicaOnBase = 0; // 
                ResourceManager.instance.IronOnBase = 0; // 
                TextComponentBombingResult.text += "Your base silica and Iron storage was hit and you lost all of this stored!";
            }

        }

        if (BombingDamage >= 5 && NrOfEnemyFighters <=10)
        {
            BombingOutcome = Random.Range(1, 5);
            if (BombingOutcome == 1)
            {
                ResourceManager.instance.PlatinumOnBase = 0; // 
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if (DestroyCounter > 3)
                                break; //
                        }
                    }
                }
                for (int i = 0; i < HangarManager.Instance().ShipsInService.Count; i++) // this loop destroys all ships in hangar!!
                {
                    if (HangarManager.Instance().ShipsInService[i].OnMoon)
                        HangarManager.Instance().ShipsInService.RemoveAt(i);// 
                        AllShipInHangarDestroyed = true;
                }
                TextComponentBombingResult.text += "the hangar got severely bombed destroying all spacecraft parked there (if any). Finally ";
                ResourceManager.instance.PlatinumOnBase = 0; // 
                TextComponentBombingResult.text += "Your base platinium storage was hit and you lost all platinium stored!";
            }
            if (BombingOutcome == 2)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if (DestroyCounter > 3)
                                break; //
                        }
                    }
                }
                for (int i = 0; i < HangarManager.Instance().ShipsInService.Count; i++) // this loop destroys all ships in hangar!!
                {
                    if (HangarManager.Instance().ShipsInService[i].OnMoon)
                        HangarManager.Instance().ShipsInService.RemoveAt(i);// 
                        AllShipInHangarDestroyed = true;
                }
                TextComponentBombingResult.text += "the hangar got severely bombed destroying all spacecraft parked there (if any). Finally ";
                ResourceManager.instance.PlatinumOnBase = 0; // 
                TextComponentBombingResult.text += "Your base platinium storage was hit and you lost all platinium stored!";
                ResourceManager.instance.TitanOnBase = 0; // 
                TextComponentBombingResult.text += ",Your base titanium storage was hit and you lost all of this stored!";
            }
            if (BombingOutcome == 3)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if (DestroyCounter > 3)
                                break; //
                        }
                    }
                }
                for (int i = 0; i < HangarManager.Instance().ShipsInService.Count; i++) // this loop destroys all ships in hangar!!
                {
                    if (HangarManager.Instance().ShipsInService[i].OnMoon)
                        HangarManager.Instance().ShipsInService.RemoveAt(i);// 
                         AllShipInHangarDestroyed = true;
                }
                TextComponentBombingResult.text += "the hangar got severely bombed destroying all spacecraft parked there (if any). Finally ";
                ResourceManager.instance.PlatinumOnBase = 0; //                  
                ResourceManager.instance.AluOnBase = 0; //
                TextComponentBombingResult.text += "Your base aluminium storage and platinium storage was hit and you lost all of these stored!";
            }
            if (BombingOutcome == 4)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if (DestroyCounter > 3)
                                break; //
                        }
                    }
                }
                for (int i = 0; i < HangarManager.Instance().ShipsInService.Count; i++) // this loop destroys all ships in hangar!!
                {
                    if (HangarManager.Instance().ShipsInService[i].OnMoon)
                        HangarManager.Instance().ShipsInService.RemoveAt(i);// 
                    AllShipInHangarDestroyed = true;
                }
                TextComponentBombingResult.text += "the hangar got severely bombed destroying all spacecraft parked there (if any). Finally ";
                ResourceManager.instance.IronOnBase = 0; // 
                TextComponentBombingResult.text += "Your base iron storage was hit and you lost all this stored!";
            }
            if (BombingOutcome == 5)
            {
                if (HangarManager.Instance().FinishedItems.Count > 0)
                {
                    for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++) //find first and second instance of an item on moon and destroy it
                    {
                        if (HangarManager.Instance().FinishedItems[i].OnMoon == true) // check for ifOnMoon== true
                        {
                            TextComponentBombingResult.text = "A " + HangarManager.Instance().FinishedItems[i].ItemName + " was destroyed and ";
                            HangarManager.Instance().FinishedItems.RemoveAt(i); //
                            DestroyCounter++;
                            if (DestroyCounter > 3)
                                break; //
                        }
                    }
                }
                for (int i = 0; i < HangarManager.Instance().ShipsInService.Count; i++) // this loop destroys all ships in hangar!!
                {
                    if (HangarManager.Instance().ShipsInService[i].OnMoon)
                        HangarManager.Instance().ShipsInService.RemoveAt(i);// 
                         AllShipInHangarDestroyed = true;
                }
                TextComponentBombingResult.text += "the hangar got severelybombed destroying all spacecraft parked there (if any). Finally ";
                ResourceManager.instance.SilicaOnBase = 0; // 
                ResourceManager.instance.IronOnBase = 0; // 
                TextComponentBombingResult.text += "Your base silica and Iron storage was hit and you lost all of this stored!";
            }

        }

        if (BombingDamage >= 5 && NrOfEnemyFighters > 10) // GAME OVER
        {
            GameOverPanel.SetActive(true); // the Martians have won...
        }

        if (AllShipInHangarDestroyed == true)
        { 
            HangarManager.Instance().Grazer.SetActive(false);
            HangarManager.Instance().Probe.SetActive(false);
            HangarManager.Instance().SIOS.SetActive(false);
            HangarManager.Instance().Waverider.SetActive(false);
            HangarManager.Instance().Carrack.SetActive(false);

            if (HangarManager.Instance().Bays[0].ship !=null && HangarManager.Instance().Bays[0].ship.InProduction == false) // we only remove finished Ships..
            {
                Destroy(HangarManager.Instance().Bays[0].ship);                
                HangarManager.Instance().UpdateValuesInHangar(0, null);
            }
            //HangarManager.Instance().UpdateValuesInHangar(0, null);

            HangarManager.Instance().Grazer2.SetActive(false);
            HangarManager.Instance().Probe2.SetActive(false);
            HangarManager.Instance().Waverider2.SetActive(false);
            HangarManager.Instance().Carrack2.SetActive(false);

            if (HangarManager.Instance().Bays[1].ship != null && HangarManager.Instance().Bays[1].ship.InProduction == false) // we only remove finished Ships..
            {
                Destroy(HangarManager.Instance().Bays[1].ship);
                HangarManager.Instance().UpdateValuesInHangar(1, null);
            }
            //HangarManager.Instance().UpdateValuesInHangar(1, null);

            HangarManager.Instance().Grazer3.SetActive(false);
            HangarManager.Instance().Probe3.SetActive(false);
            HangarManager.Instance().Waverider3.SetActive(false);
            HangarManager.Instance().Carrack3.SetActive(false);

            if (HangarManager.Instance().Bays[2].ship != null && HangarManager.Instance().Bays[2].ship.InProduction == false) // we only remove finished Ships..
            {
                Destroy(HangarManager.Instance().Bays[2].ship);
                HangarManager.Instance().UpdateValuesInHangar(2, null);
            }
            //HangarManager.Instance().UpdateValuesInHangar(2, null);

            HangarManager.Instance().Grazer4.SetActive(false);
            HangarManager.Instance().Probe4.SetActive(false);
            HangarManager.Instance().Waverider4.SetActive(false);
            HangarManager.Instance().Carrack4.SetActive(false);

            if (HangarManager.Instance().Bays[3].ship != null && HangarManager.Instance().Bays[3].ship.InProduction == false) // we only remove finished Ships..
            {
                Destroy(HangarManager.Instance().Bays[3].ship);
                HangarManager.Instance().UpdateValuesInHangar(3, null);
            }
            //HangarManager.Instance().UpdateValuesInHangar(3, null);

            HangarManager.Instance().Grazer5.SetActive(false);
            HangarManager.Instance().Probe5.SetActive(false);

            if (HangarManager.Instance().Bays[4].ship != null && HangarManager.Instance().Bays[4].ship.InProduction == false) // we only remove finished Ships..
            {
                Destroy(HangarManager.Instance().Bays[4].ship);
                HangarManager.Instance().UpdateValuesInHangar(4, null);
            }
            //HangarManager.Instance().UpdateValuesInHangar(4, null);

            HangarManager.Instance().Grazer6.SetActive(false);
            HangarManager.Instance().Probe6.SetActive(false);

            if (HangarManager.Instance().Bays[5].ship != null && HangarManager.Instance().Bays[5].ship.InProduction == false) // we only remove finished Ships..
            {
                Destroy(HangarManager.Instance().Bays[5].ship);
                HangarManager.Instance().UpdateValuesInHangar(5, null);
            }
            //HangarManager.Instance().UpdateValuesInHangar(5, null);

            HangarManager.Instance().Grazer7.SetActive(false);
            HangarManager.Instance().Probe7.SetActive(false);
            if (HangarManager.Instance().Bays[6].ship != null && HangarManager.Instance().Bays[6].ship.InProduction == false) // we only remove finished Ships..
            {
                Destroy(HangarManager.Instance().Bays[6].ship);
                HangarManager.Instance().UpdateValuesInHangar(6, null);
            }
            //HangarManager.Instance().UpdateValuesInHangar(6, null);

            HangarManager.Instance().Grazer8.SetActive(false);
            HangarManager.Instance().Probe8.SetActive(false);

            if (HangarManager.Instance().Bays[7].ship != null && HangarManager.Instance().Bays[7].ship.InProduction == false) // we only remove finished Ships..
            {
                Destroy(HangarManager.Instance().Bays[7].ship);
                HangarManager.Instance().UpdateValuesInHangar(7, null);
            }
            //HangarManager.Instance().UpdateValuesInHangar(7, null);

            for(int i =0;i < HangarManager.Instance().ShipsInService.Count ;i++)
            {
                if (HangarManager.Instance().ShipsInService[i].OnMoon && HangarManager.Instance().ShipsInService[i].InProduction==false)
                {
                    HangarManager.Instance().ShipsInService.RemoveAt(i);
                    //Destroy(HangarManager.Instance().ShipsInService[i]);
                }                  
            }


            /*
            for(int i = 0;i<8; i++) //Actually remove all finished ship objects in bays...
            {
                if (HangarManager.Instance().Bays[i].ship && HangarManager.Instance().Bays[i].ship.InProduction == false) // we only remove finished Ships..
                {
                    Destroy(HangarManager.Instance().Bays[i].ship);
                    //HangarManager.Instance().UpdateValuesInHangar(i, null);
                }
                HangarManager.Instance().UpdateValuesInHangar(i, null);
            }
            */
            AllShipInHangarDestroyed = false;
        }












    }

    public void DisplayavailableForces()
    {

        var TextComponentAvailableFighters = TextForAvailableFighters.GetComponent<Text>();
        var TextComponentAvailableLasers = TextForAvailableLasers.GetComponent<Text>();
        TextComponentAvailableFighters.text = HangarManager.Instance().NrOfFighters + "";
        TextComponentAvailableLasers.text = HangarManager.Instance().NrOfOrbitalLasers + "";
    }

    public static CombatManager Instance() // 
    {
        if (instance == null)
        {

            instance = GameObject.FindObjectOfType<CombatManager>() as CombatManager;
        }

        return instance;
    }












}
