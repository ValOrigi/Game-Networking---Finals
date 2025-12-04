using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject player1, player2;
    [SerializeField] private ChampionEquipment[] _championEquipment;
    [SerializeField] private SceneManager _sceneManager;

    public void StartGameReady()
    {
        if(player1.GetComponent<Player>()._isReady && player2.GetComponent<Player>()._isReady)
        {
            for (int count = 0; count < 2; count++)
            {
                for (int i = 0; i < _championEquipment[count]._sharpWeaponVal; i++)
                {
                    _championEquipment[count].Deck.Add(1);
                }

                for (int i = 0; i < _championEquipment[count]._bluntWeaponVal; i++)
                {
                    _championEquipment[count].Deck.Add(2);
                }

                for (int i = 0; i < _championEquipment[count]._rangeWeaponVal; i++)
                {
                    _championEquipment[count].Deck.Add(3);
                }
            }

            //change this when multiplayer is on
            _sceneManager.BuildToBattle();
        }
        else
        {
            Debug.Log("READY UP FIRST");
        }
    }
}
