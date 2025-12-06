using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;

public class UpdatePlayerScore : MonoBehaviour
{
    [SerializeField] PlayerStat playerStat;

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
    [System.Serializable]
    public class ScoreUpdateBody
    {
        public int score;
    }


    public void UpdatePlayerScoreBackend()
    {
        StartCoroutine(PostRegisterPlayer(playerStat.scoreInput));
    }

    IEnumerator PostRegisterPlayer(int score)
    {
        using (UnityWebRequest request = new UnityWebRequest("http://localhost:5000/api/players/score/" + playerStat.player1ID, "POST"))
        {
            /*            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);
                        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                        request.downloadHandler = new DownloadHandlerBuffer();
                        request.SetRequestHeader("Content-Type", "application/json");*/
            ScoreUpdateBody body = new ScoreUpdateBody();
            body.score = score;
            string json = JsonUtility.ToJson(body);

            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            Debug.Log("Sending JSON: " + json);

            yield return request.SendWebRequest();

            Debug.Log("yeild return fginished should do condition");
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
                //register error panel
            }
            else
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("Score Update Response: " + responseText);

                LoginResponse logResponse = JsonUtility.FromJson<LoginResponse>(responseText);

                Player updatedPlayer = logResponse.data;

                Debug.Log("Player Updated - ID: " + updatedPlayer._id + " Score: " + updatedPlayer.score);
            }
        }
    }
}
