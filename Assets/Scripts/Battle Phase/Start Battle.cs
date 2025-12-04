using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class StartBattle : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SceneManager sceneManager;
    [SerializeField] private GameObject champ1, champ2, winnerText, player1, player2;

    //this will be an event when both players ready up their weapon
    void Ready()
    {
        Champion c1 = champ1.GetComponent<Champion>();
        Champion c2 = champ2.GetComponent<Champion>();
        
        if (c1.hasPlayedCard && c2.hasPlayedCard)
        {
            //Evaluate Player 1 Health
            gameManager.p1Health -= EvaluateHealth(gameManager.player1, gameManager.player2);
            gameManager.p1HealthTxt.text = gameManager.p1Health.ToString();

            //Evaluate Player 2 Health
            gameManager.p2Health -= EvaluateHealth(gameManager.player2, gameManager.player1);
            gameManager.p2HealthTxt.text = gameManager.p2Health.ToString();

            c1.hasPlayedCard = false;
            c2.hasPlayedCard = false;
            c1.readyText.text = "NOT READY!";
            c2.readyText.text = "NOT READY!";

            if ((gameManager.p1Health <= 0 || gameManager.p2Health <= 0) && gameManager.p1Health != gameManager.p2Health)
            {
                TextMeshProUGUI winText = winnerText.GetComponent<TextMeshProUGUI>();

                if (gameManager.p1Health > gameManager.p2Health)
                {
                    winText.text = "PLAYER 1 WINS";
                    player1.GetComponent<Player>().playerScore++;
                    player1.GetComponent<PlayerChampionCustomize>().playerScoreText.text = "Score: " + player1.GetComponent<Player>().playerScore;
                    player2.GetComponent<Player>()._isReady = false;
                }
                else
                {
                    winText.text = "PLAYER 2 WINS";
                    player2.GetComponent<Player>().playerScore++;
                    player2.GetComponent<PlayerChampionCustomize>().playerScoreText.text = "Score: " + player2.GetComponent<Player>().playerScore;
                    player1.GetComponent<Player>()._isReady = false;
                }

                winnerText.SetActive(true);

                //add wait

                winnerText.SetActive(false);

                sceneManager.BattleToBuild();
            }
        }
        else
        {
            Debug.Log("PLAYERS READY UP FIRST");
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