using System.Collections.Generic;
using UnityEngine;

public class Champion : MonoBehaviour
{
    /*
     * Note:
     * Character Weapon:
     *  1 = Sharp
     *  2 = Blunt
     *  3 = Range
     * Character Armor:
     *  1 = Leather (Weak to Sharp, Normal to Ranged, Resist Blunt)
     *  2 = Iron(Weak to Blunt, Normal to Sharp, Resit to Range)
     *  3 = Chainmail (Weak to Range, Normal to Blunt, Resist to Sharp)
     */

    [Tooltip("Character Weapon:\r\n" +
        "1 = Sharp\r\n" +
        "2 = Blunt\r\n" +
        "3 = Range")]
    [Range(1, 3)]
    public int chWeapon;

    [Tooltip("Character Armor:\r\n" +
    "1 = Leather\r\n" +
    "2 = Iron\r\n" +
    "3 = Chainmail")]
    [Range(1, 3)]
    public int chArmor;

    [SerializeField] private GameManager gameManager;

    public List<int> playerCards = new List<int>();
    public List<int> playerHand = new List<int>();

    //might need to switch this over to update if we start the game without a player 2
    //check later
    private void Awake()
    {
        //Set Player 1's Hand
        GetHand();
    }

    public void GetHand()
    {
        //Do it 3 Times
        for (int i = 0; i < 3; i++)
        {
            //Get Random from Player Deck and Put in Player Hand
            int randomNum = Random.Range(0, playerCards.Count);
            playerHand.Add(playerCards[randomNum]);
            playerCards.RemoveAt(randomNum);
        }

        //Instantiate Hand
        int count = 0;
        foreach (var cards in playerHand)
        {
            Instantiate(gameManager.cardList[cards - 1], gameManager.cardPos[count++]);
        }
    }

    public void GetCard(int cardPosition)
    {
        int randomNum = Random.Range(0, playerCards.Count);
        playerHand.RemoveAt(cardPosition);
        playerHand.Add(playerCards[randomNum]);
        playerCards.RemoveAt(randomNum);

        //Clear All Cards
        DestroyAll("Card");

        //Instantiate Hand
        int count = 0;
        foreach (var cards in playerHand)
        {
            Instantiate(gameManager.cardList[cards - 1], gameManager.cardPos[count++]);
        }
    }

    void DestroyAll(string tag)
    {
        // Find all GameObjects with the target tag and store them in an array
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(tag);

        // Iterate through the array and destroy each GameObject
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }

    public void ReplaceCard(int cardPosition)
    {
        GetCard(cardPosition);
    }
}