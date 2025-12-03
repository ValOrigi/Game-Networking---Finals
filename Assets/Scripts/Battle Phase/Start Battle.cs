using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class StartBattle : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject winnerText;

    //this will be an event when both players ready up their weapon
    void Ready()
    {
        //Evaluate Player 1 Health
        gameManager.p1Health -= EvaluateHealth(gameManager.player1, gameManager.player2);
        gameManager.p1HealthTxt.text = gameManager.p1Health.ToString();

        //Evaluate Player 2 Health
        gameManager.p2Health -= EvaluateHealth(gameManager.player2, gameManager.player1);
        gameManager.p2HealthTxt.text = gameManager.p2Health.ToString();

        if ((gameManager.p1Health < 0 || gameManager.p2Health < 0) && gameManager.p1Health != gameManager.p2Health)
        {
            TextMeshProUGUI winText = winnerText.GetComponent<TextMeshProUGUI>();

            if (gameManager.p1Health > gameManager.p2Health)
            {
                winText.text = "PLAYER 1 WINS";
            }
            else
            {
                winText.text = "PLAYER 2 WINS";
            }

            winnerText.SetActive(true);
        }
    }

    int EvaluateHealth(GameObject defence, GameObject offence)
    {
        /*
         * Note:
         * Character Weapon:
         *  1 = Sharp
         *  2 = Blunt
         *  3 = Range
         * Character Armor:
         *  1 = Leather (Weak to Sharp, Normal to Ranged, Resist to Blunt)
         *  2 = Iron (Weak to Blunt, Normal to Sharp, Resit to Range)
         *  3 = Chainmail (Weak to Range, Normal to Blunt, Resist to Sharp)
        */

        //Initialize Armor
        int defensive = defence.GetComponent<Champion>().chArmor;
        //Lock in Weapon Chosen
        int offensive = offence.GetComponent<Champion>().chWeapon;

        //ARMOR is WEAK to WEAPON
        if (defensive == offensive)
        {
            return gameManager.damage * 2;
        }

        //ARMOR is NEUTRAL to WEAPON
        else if ((defensive == 1 && offensive == 2) || (defensive == 2 && offensive == 3) || (defensive == 3 && offensive == 1))
        {
            return gameManager.damage;
        }
        //ARMOR is RESISTANT to WEAPON
        else
        {
            return gameManager.damage / 2;
        }
    }
}