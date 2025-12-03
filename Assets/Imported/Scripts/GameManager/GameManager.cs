using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManagerIC : MonoBehaviour
{
    [SerializeField] Player[] players;

/*    [SerializeField] GameObject _player1Champion, _player2Champion;*/

    [SerializeField] Transform _player1ChampPos, _player2ChampPos;

    [SerializeField] GameObject _championPref;


    public enum gameState //put in a diff script as gameState
    {
        WaitingForPlayers,
        Building,
        Attacking,
        RoundEnd
    }

    public gameState _gamestate;

    [SerializeField] GameObject _buildingPanel;
    [SerializeField] GameObject _roundOverPanel;

    private void Start()
    {

    }



    private void BuildingState()
    {
        //idk how to make this a multilayer ui thing. tas ownership din for the instantiated champ.
        /*        _buildingPanel.SetActive(true);
                _roundOverPanel.SetActive(false);*/


        _gamestate = gameState.Building; // after login and 2 players has joined
        //load Building Scene
        players[0]._champion = InstantiateChampion(players[0]._champion, _player1ChampPos, _championPref);
        players[1]._champion = InstantiateChampion(players[1]._champion, _player2ChampPos, _championPref);
    }

    private GameObject InstantiateChampion(GameObject playerChamp, Transform champInstantiatePos, GameObject championPrefab)
    {
        if (playerChamp == null)
        {
            playerChamp = Instantiate(championPrefab, champInstantiatePos);
        }

        return playerChamp;
    }
}
