using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject player1, player2;
    public int damage, p1Health, p2Health;

    public Sprite[] armorSprite;

    //Canvas
    public Image armorImg1, armorImg2;
    //change this later to scroller for hp bar
    public TextMeshProUGUI p1HealthTxt, p2HealthTxt;

    private void Awake()
    {
        //Set Players' Starting Health
        p1Health = 100;
        p1HealthTxt.text = p1Health.ToString();
        p2Health = 100;
        p2HealthTxt.text = p2Health.ToString();

        //Set Players' Armor
        //GetArmor(armorImg1, player1);
        //GetArmor(armorImg2, player2);
    }

    public void GetArmor(Image img, GameObject player)
    {
        //Get Armor Type from Player
        int armor = player.GetComponent<Champion>().chArmor;

        //Set Armor Sprite Based on Number ID
        switch (armor)
        {
            case 1:
                img.sprite = armorSprite[0];
                break;
            case 2:
                img.sprite = armorSprite[1];
                break;
            case 3:
                img.sprite = armorSprite[2];
                break;
        }
    }
}