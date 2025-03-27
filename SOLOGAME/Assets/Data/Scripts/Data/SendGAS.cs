using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public static class SendGAS
{
    private const string gasUrl = "https://script.google.com/macros/s/AKfycbzZSj5g4eufHFVx8lHrUUXctVeykzFU4Bao1S1PSuI7drKRvcw243y7pkjdzbMKBYITaA/exec";
    // ルームリストを保持する静的変数
    private static List<string> roomList = new List<string>();

    static bool check = false;

    private static IEnumerator SendDataToGAS(string message)
    {
        string url = gasUrl + message;
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("GAS Response: " + responseText);

                if (responseText.Contains("Error"))
                {
                    Debug.LogError("GASエラー: " + responseText);
                }
            }
            else
            {
                Debug.LogError("GASへのデータ送信に失敗しました: " + request.error);
            }
        }
    }

    [System.Serializable]
    public class RoomListWrapper
    {
        public List<string> roomIds;
    }

    public static IEnumerator GetRoomListFromGAS()
    {
        string url = gasUrl + "?action=getRoomList";  // ルームIDリストを取得するためのURL

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("GAS Response: " + responseText);

                try
                {
                    if (!string.IsNullOrEmpty(responseText))
                    {
                        RoomListWrapper roomListWrapper = JsonUtility.FromJson<RoomListWrapper>(responseText);

                        if (roomListWrapper != null && roomListWrapper.roomIds != null)
                        {
                            roomList = roomListWrapper.roomIds;
                            Debug.Log("roomList: " + string.Join(", ", roomList));  // 確認用ログ
                        }
                        else
                        {
                            Debug.LogError("roomListWrapper が null または roomIds が null です");
                        }
                    }
                    else
                    {
                        Debug.LogError("レスポンスが空です");
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("JSONパースエラー: " + e.Message);
                }
            }
            else
            {
                Debug.LogError("GASからルームリストの取得に失敗しました: " + request.error);
            }
        }
        check = true;
    }

    // 外部からルームリストを取得するメソッド
    public static List<string> GetRoomList()
    {
        return roomList;
    }

    public static bool GetCHECK()
    {
        return check;
    }

    // コルーチンをラップするためのメソッド
    public static void StartCoroutineWrapper(MonoBehaviour caller, string message)
    {
        caller.StartCoroutine(SendDataToGAS(message));
    }
    public static void StartCoroutineWrapper(MonoBehaviour caller)
    {
        check = false;
        caller.StartCoroutine(GetRoomListFromGAS());
    }
}
