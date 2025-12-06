using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using static Leaderboard;
using static System.Net.WebRequestMethods;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI usernameDisplay;
    [SerializeField] TextMeshProUGUI usernameDisplayStat;
    public int scoreInput;

    [System.Serializable]
    public class Player
    {
        public string _id;
        public string username;
        public int score;
    }

    [System.Serializable]
    public class LoginResponse
    {
        public bool success;
        public string message;
        public Player data;
    }

    public string player1ID;

    public void DeletePlayerButton()
    {
       StartCoroutine(DeletePlayer());
    }
    public void StatButton()
    {
        StartCoroutine(GetPlayer());
    }

    public void LogOut()
    {
        player1ID = "";
        usernameDisplay.text = "Not Logged In";
        usernameDisplayStat.text = "Not Logged In";
        score.text = "0";
    }

    IEnumerator GetPlayer()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:5000/api/players/" + player1ID))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
                yield break;
            }
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string response = request.downloadHandler.text;
                Debug.Log("Raw Login Response: " + response);

                LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(response);
                Player loggedIn = loginResponse.data;
                /*                string loginResponseTxt = loginResponse.ToString();*/

                /*                Debug.Log(loggedIn);*/

                score.text = loggedIn.score.ToString();
                if (usernameDisplay != null)
                {
                    usernameDisplayStat.text = loggedIn.username.ToString();
                }
                else
                {
                    usernameDisplayStat.text = "Not Logged In";
                }

                Debug.Log("Player Logged In (ID): " + loggedIn._id);
            }
        }
    }

    IEnumerator DeletePlayer()
    {
        using (UnityWebRequest request = new UnityWebRequest("http://localhost:5000/api/players/delete/" + player1ID, "DELETE"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
                yield break;
            }
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                LogOut();
                string response = request.downloadHandler.text;

                Debug.Log("Delete Response: " + response);

                Debug.Log("Player deleted successfully!");
            }
        }
    }
}
