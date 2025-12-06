using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject homePhase, buildPhase, battlePhase, loginPanel, registerPanel, leaderPanel, statPanel;
    private ChampionEquipment _championEquipment;
    [SerializeField] private Champion champion;
    [SerializeField] private Transform leaderboardContent;

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

    public void Register()
    {
        homePhase.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void Login()
    {
        homePhase.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void Return()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        homePhase.SetActive(true);
    }

    public void LeaderBoard(bool boolean)
    {
        leaderPanel.SetActive(boolean);
        homePhase.SetActive(!boolean);

        if (!boolean)
        {
            foreach(Transform child in leaderboardContent.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void StatPanel(bool boolean)
    {
        statPanel.SetActive(boolean);
        homePhase.SetActive(!boolean);
    }
}
