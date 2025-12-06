using UnityEngine;
using UnityEngine.EventSystems;

public class CardBehavior : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Champion champ;
    public int cardType, cardSlot;

    private void Awake()
    {
        champ = GameObject.FindGameObjectWithTag("Player").GetComponent<Champion>();
        cardSlot = int.Parse(transform.parent.name);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Will Change the Player's Weapon Type
        champ.chWeapon = cardType;

        //If there are Cards in Deck, Replace Card
        //Else, just Destroy
        if (champ.playerCards.Count != 0)
        {
            champ.ReplaceCard(cardSlot);
        }
        else
        {
            Destroy(gameObject);
            champ.playerHand.RemoveAt(cardSlot);
        }
    }
}