using UnityEngine;
using UnityEngine.EventSystems;

public class CardBehavior : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Champion champ;
    public int cardType, cardSlot, playerNum;

    private void Awake()
    {
        champ = GameObject.Find("Champion" + playerNum).GetComponent<Champion>();
        gameObject.tag = "Card" + playerNum;
        cardSlot = int.Parse(transform.parent.name);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Will Change the Player's Weapon Type
        champ.chWeapon = cardType;

        //If there are Cards in Deck, Replace Card
        //Else, just Destroy
        if(!champ.hasPlayedCard)
        {
            if (champ.playerCards.Count != 0)
            {
                champ.hasPlayedCard = true;
                champ.ReplaceCard(cardSlot, playerNum);
            }
            else
            {
                champ.hasPlayedCard = true;
                Destroy(gameObject);
                champ.playerHand.RemoveAt(cardSlot);
            }
        }
        else
        {
            Debug.Log("PLAYED CARD ALREADY");
        }
    }
}