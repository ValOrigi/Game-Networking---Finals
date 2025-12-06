using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System.Net.Http.Headers;

public class Login : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    [SerializeField] TextMeshProUGUI usernameDisplay;
    [SerializeField] PlayerStat playerStat;

    private string registerURL = "http://localhost:5000/api/players/login/";

    public void LoginPlayerButton()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        LoginPlayer(username, password);
    }


    [System.Serializable]
    public class Player
    {
        public string _id;
        public int score;
        public string username;
        public string email;
        public string password;
    }

    [System.Serializable] //NEED TO BE SERIALIZED ADJKMAWNDMKLAWMDKLAW taenignaiwmdiawmdiawd get the root depending on the strucutre din
    public class LoginResponse
    {
        public bool success;
        public string message;
        public Player data;
    }

    public void LoginPlayer(string username, string password)
    {
        StartCoroutine(PostRegisterPlayer(new Player
        {
            username = username,
            password = password
        }));

    }

    IEnumerator PostRegisterPlayer(Player player)
    {
        string json = JsonUtility.ToJson(player);

        using (UnityWebRequest request = new UnityWebRequest(registerURL, "POST"))
        {
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

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
                playerStat.player1ID = loggedIn._id;

                usernameDisplay.text = loggedIn.username;

                playerStat.scoreInput = loggedIn.score;

                Debug.Log("Player Logged In (ID): " + loggedIn._id);
            }
        }
    }


}
