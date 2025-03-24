using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class SendGAS
{
    // GASのURL
    private const string gasUrl =
    "https://script.google.com/macros/s/AKfycbzZSj5g4eufHFVx8lHrUUXctVeykzFU4Bao1S1PSuI7drKRvcw243y7pkjdzbMKBYITaA/exec";
    // Start is called before the first frame update
    // GASにデータを送信するコルーチン
    private static IEnumerator SendDataToGAS(string message)
    {
        string url = gasUrl + message;
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            // リクエストの結果をチェック
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("GASにデータを送信しました: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("GASへのデータ送信に失敗しました: " + request.error);
            }
        }
    }

    public static void StartCoroutineWrapper(MonoBehaviour caller,string message)
    {
        caller.StartCoroutine(SendDataToGAS(message));
    }
}
