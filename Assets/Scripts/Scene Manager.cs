using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject player1, player2, homePhase, buildPhase, battlePhase, champ1, champ2, loginPanel, registerPanel, leaderPanel, statPanel;
    private ChampionEquipment _championEquipment1, _championEquipment2;
    [SerializeField] private Champion champion1, champion2;

    [SerializeField] private Transform leaderboardContent;

    public void HomeToBuild()
    {
        player1.GetComponent<Player>().playerScore = 0;
        player2.GetComponent<Player>().playerScore = 0;
        player1.GetComponent<PlayerChampionCustomize>().playerScoreText.text = "Score: 0";
        player2.GetComponent<PlayerChampionCustomize>().playerScoreText.text = "Score: 0";

        homePhase.SetActive(false);
        buildPhase.SetActive(true);

        GetComponent<HomeManager>().enabled = false;
        player1.GetComponent<PlayerChampionCustomize>().enabled = true;
        player2.GetComponent<PlayerChampionCustomize>().enabled = true;
    }

    public void BuildToBattle()
    {
        _championEquipment1 = player1.GetComponent<ChampionEquipment>();
        _championEquipment2 = player2.GetComponent<ChampionEquipment>();

        battlePhase.SetActive(true);
        champion1.playerCards.Clear();
        champion1.playerHand.Clear();

        champion2.playerCards.Clear();
        champion2.playerHand.Clear();

        foreach (int card in _championEquipment1.Deck)
        {
            champion1.playerCards.Add(card);
        }
        foreach (int card in _championEquipment2.Deck)
        {
            champion2.playerCards.Add(card);
        }

        //Get Armor
        champion1.chArmor = _championEquipment1._championArmor;
        champion2.chArmor = _championEquipment2._championArmor;

        //Set Player 1's Hand
        champion1.GetHand();
        champion2.GetHand();

        buildPhase.SetActive(false);

        player1.GetComponent<PlayerChampionCustomize>().enabled = false;
        player2.GetComponent<PlayerChampionCustomize>().enabled = false;
        GameManager gm = GetComponent<GameManager>();
        gm.enabled = true;
        gm.p1Health = 100;
        gm.p2Health = 100;
        gm.p1HealthTxt.text = gm.p1Health.ToString();
        gm.p2HealthTxt.text = gm.p2Health.ToString();
        gm.GetArmor(gm.armorImg1, champ1);
        gm.GetArmor(gm.armorImg2, champ2);
    }

    public void BattleToBuild()
    {
        battlePhase.SetActive(false);
        GetComponent<GameManager>().enabled = false;

        buildPhase.SetActive(true);
        player1.GetComponent<PlayerChampionCustomize>().enabled = true;
        player2.GetComponent<PlayerChampionCustomize>().enabled = true;
    }

    public void BuildToHome()
    {
        homePhase.SetActive(true);
        buildPhase.SetActive(false);

        GetComponent<HomeManager>().enabled = true;
        player1.GetComponent<PlayerChampionCustomize>().enabled = false;
        player2.GetComponent<PlayerChampionCustomize>().enabled = false;
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
