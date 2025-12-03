using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject homePhase, buildPhase, battlePhase;
    private ChampionEquipment _championEquipment;
    [SerializeField] private Champion champion;

    public void HomeToBuild()
    {
        homePhase.SetActive(false);
        buildPhase.SetActive(true);

        GetComponent<HomeManager>().enabled = false;
        GetComponent<PlayerChampionCustomize>().enabled = true;
    }

    public void BuildToBattle()
    {
        _championEquipment = GameObject.Find("Champion 1(Clone)").GetComponent<ChampionEquipment>();

        battlePhase.SetActive(true);
        champion.playerCards.Clear();
        champion.playerHand.Clear();
        foreach (int card in _championEquipment.Deck)
        {
            champion.playerCards.Add(card);
        }

        //Set Player 1's Hand
        champion.GetHand();

        buildPhase.SetActive(false);
        _championEquipment.Deck.Clear();

        GetComponent<PlayerChampionCustomize>().enabled = false;
        GetComponent<GameManager>().enabled = true;
    }
}
