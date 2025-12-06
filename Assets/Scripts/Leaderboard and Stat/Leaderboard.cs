using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] GameObject playerLeaderboardPref;
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI playerScore;
    [SerializeField] Transform leaderboardContent;
    PlayerLeaderboard playerLeaderboard;

    private string baseURL = "http://localhost:5000/api/players";
    ///api/players/register
    public void LeaaderBoardButton()
    {
        StartCoroutine(GetAllPlayers());
    }

    /* IEnumerator GetAllPlayers()
     {
         using (UnityWebRequest request = UnityWebRequest.Get(baseURL))
         {
             yield return request.SendWebRequest();

             if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
             {
                 Debug.Log(request.error);
             }
             else
             {
                 Debug.Log(request.downloadHandler.text);
             }
         }
     }*/
    [System.Serializable]
    public class Players
    {
        public bool success;
        public int count;
        public List<User> data;
    }

    [System.Serializable]
    public class User
    {
        public string username;
        public int score;
    }

    IEnumerator GetAllPlayers()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
                yield break;
            }

            string json = request.downloadHandler.text;

            Players players = JsonUtility.FromJson<Players>(json);

            foreach (User user in players.data)
            {
                GameObject PlayerCont = Instantiate(playerLeaderboardPref, leaderboardContent);
                playerLeaderboard = PlayerCont.GetComponent<PlayerLeaderboard>();
                playerLeaderboard.playerName.text = user.username;
                playerLeaderboard.score.text = user.score.ToString();


                Debug.Log( //dyanmically script leaderboard
                    $"Username: {user.username}\n" +
                    $"Score: {user.score}\n"
                );

            }
        }
    }
}
