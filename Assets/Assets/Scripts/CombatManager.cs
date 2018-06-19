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
    public bool FightersIncludedInFight;

    public int CombatOutcome; // if plus player wins, if minus martians wins


    public GameObject TextForIncomingFighters;

    public GameObject TextForAvailableFighters;
    public GameObject TextForAvailableLasers;

    public GameObject TextForFightersLaunchedSet;
    public GameObject TextForOrbitalLaserSet;

    public GameObject TextForEnemyPower;
    public GameObject TextForPlayerPower;
    public GameObject TextforOutcome;




    public void SetupEnemy(int AttackNr) // attack number parameter defines what number of enemyfighters are attacking
    {

        var TextComponentEnemyFighters = TextForIncomingFighters.GetComponent<Text>();


        if (AttackNr < 10) // this hould resemble the original game somewhat, where the number of fighters increase overtime multiplied by a randomizer
            NrOfEnemyFighters = (AttackNr * 2)-1; // to be adjusted
        if (AttackNr >=10)
            NrOfEnemyFighters = (AttackNr * 3) - 1; // making it harder after attack 10

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
        FightersIncludedInFight = true; //  For the attackressolve panel text it is!!
        for (int i = 0; i < HangarManager.Instance().FinishedItems.Count; i++)
        {

            if (HangarManager.Instance().FinishedItems[i].ItemID == 10) // if fighter
            {
                NrOfFightersReady++;               
            }
        }
        TextComponentChosenFighters.text = "Fighters launched: "+NrOfFightersReady+"";
    }

    public void SetupPlayerForces()// Alternatively (int NrOfFighters, int NrOfOrbitalLasers) // at some point number of corvettes at battle scene could be interesting to inert as a 3rd parameter 
    {
        if (NrOfFightersReady == 0 && OrbitalLasersChosenForFight == 0)
        {
            PlayerAttackBonus = -10; // If there is no fighters or Orbital laser then the enemy attacker will get open run on player base!!
        }
        else
        PlayerAttackBonus = NrOfFightersReady + (OrbitalLasersChosenForFight * 2);
    }

    public void ResolveCombat()
    {
        var TextComponentPlayerPower = TextForPlayerPower.GetComponent<Text>();
        var TextComponentEnemyPower = TextForEnemyPower.GetComponent<Text>();
        var TextComponentOutcome = TextforOutcome.GetComponent<Text>();

        SetupPlayerForces(); // method call give us the playerAttackbonus


        //Combat Calculations
        //To start with a really simple system is setup !!!!!!!!!!!!!

        CombatOutcome = Random.Range(-10, 10) + PlayerAttackBonus - EnemyAttackBonus; // this is to be changed/balanced

        if (CombatOutcome < 0)
        {

        }
            //player looses some assets

            if (CombatOutcome > 0)
            {

            }
                //Enemy Looses its fighters, but player also looses some fighters etc.

                TextComponentPlayerPower.text = "Moonbase forces strike delivers: " + PlayerAttackBonus+ " attackforce ";
                TextComponentEnemyPower.text = "Enemyforces strike delivers: " + EnemyAttackBonus+ " attackforce";
                TextComponentOutcome.text = ": " + CombatOutcome;

        NextTurn.Instance().AttackResolvePanel.SetActive(true); // thi panel will how outcome of combat


        // when combat is finished reset values
        NrOfFightersReady = 0;
        NrOfEnemyFighters = 0;

    }

    public void BombingBase()
    {






    }

    public void DisplayavailableForces()
    {

        var TextComponentAvailableFighters = TextForAvailableFighters.GetComponent<Text>();
        var TextComponentAvailableLasers = TextForAvailableLasers.GetComponent<Text>();
        TextComponentAvailableFighters.text = HangarManager.Instance().NrOfFighters + "";
        TextComponentAvailableLasers.text = HangarManager.Instance().NrOfOrbitalLasers + "";
    }

    public static CombatManager Instance() // THIS IS NOT HTE ONLY SINGLETON SO MIGHT BE BAD... (SINGLETON ALSO IN HANGARMANAGER!!!)
    {
        if (instance == null)
        {

            instance = GameObject.FindObjectOfType<CombatManager>() as CombatManager;
        }

        return instance;
    }












}
