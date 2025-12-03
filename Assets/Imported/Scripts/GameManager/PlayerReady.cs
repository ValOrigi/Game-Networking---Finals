using UnityEngine;
using static GameManagerIC;

public class PlayerReady : MonoBehaviour
{
    GameManagerIC gameManager;
    Player[] playerReady; 


    private void CheckIfReady() //if const, change logic completely
    {
        if (playerReady[0]._isReady && playerReady[1]._isReady)
        {
            gameManager._gamestate = gameState.Attacking;
        }
    }
}
