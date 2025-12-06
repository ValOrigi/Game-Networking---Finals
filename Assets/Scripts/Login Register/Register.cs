using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.Rendering;

public class Register : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    private string registerURL = "http://localhost:5000/api/players/register/";

    public void RegisterPlayerButton()
    {
        string username = usernameInput.text;
        string email = emailInput.text;
        string password = passwordInput.text;

        RegisterPlayer(username, email, password);
    }

    public class Player
    {
        public string username;
        public string email;
        public string password;
    }

    public void RegisterPlayer(string username, string email, string password)
    {
        StartCoroutine(PostRegisterPlayer(new Player {
        username = username,
        email = email,
        password = password
        } ));

    }

    IEnumerator PostRegisterPlayer(Player player)
    {
        string json = JsonUtility.ToJson(player);
        Debug.Log("player json file: " + json);

        using (UnityWebRequest request = new UnityWebRequest(registerURL, "POST"))
        {
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            Debug.Log("yeild return fginished should do condition");
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
                //register error panel
            }
            else
            {
                Debug.Log("Player Registered" + request.downloadHandler.text);
                //lagay rito panel na user has been registered
            }
        }
    }
}
