using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;
using Unity.Jobs.LowLevel.Unsafe;

public static class GAS
{
    private const string gasUrl = "https://script.google.com/macros/s/AKfycbzZSj5g4eufHFVx8lHrUUXctVeykzFU4Bao1S1PSuI7drKRvcw243y7pkjdzbMKBYITaA/exec";
    // ルームリストを保持する静的変数
    private static List<string> roomList = new List<string>();
    private static int roomRowNumber = -1; // ルームの行番号を保存（初期値は-1）
    private static bool isMatch = false;
    static bool check = false;

    // GASからのレスポンスデータ
    [System.Serializable]
    public class RoomListWrapper
    {
        public List<string> roomIds;
    }
    [System.Serializable]
    public class RoomRowResponse
    {
        public int rowNumber;
        public string error;
    }
    // マッチングチェックのレスポンスを格納するクラス
    [System.Serializable]
    public class MatchCheckResponse
    {
        public bool matched;
        public string error;
    }
    // GAS側に送信
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

    // ルームIDリストを取得する
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

    // ルームIDから行番号を取得する関数
    public static IEnumerator GetRoomRowFromGAS(string roomId)
    {
        string url = gasUrl + "?action=getRoomRow&roomId=" + Uri.EscapeDataString(roomId);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("GAS Response: " + responseText);  // レスポンス内容をデバッグ

                try
                {
                    // JSONのレスポンスをRoomRowResponseにパース
                    RoomRowResponse rowResponse = JsonUtility.FromJson<RoomRowResponse>(responseText);

                    // エラーメッセージがある場合、エラーをログに出力
                    if (!string.IsNullOrEmpty(rowResponse.error))
                    {
                        Debug.LogError("GAS Error: " + rowResponse.error);
                    }
                    else
                    {
                        // 行番号を保存
                        roomRowNumber = rowResponse.rowNumber;
                        Debug.Log($"ルーム {roomId} の行番号: {roomRowNumber}");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("JSONパースエラー: " + e.Message);
                }
            }
            else
            {
                Debug.LogError("GASからルーム行番号の取得に失敗しました: " + request.error);
            }
        }
        check = true;
    }

    // マッチングチェック
    public static IEnumerator CheckMatchStatus(int rowNumber)
    {
        string url = gasUrl + "?action=matchCheck&rowNumber=" + rowNumber;

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("GAS Response: " + responseText);  // レスポンス内容をデバッグ

                try
                {
                    // JSONのレスポンスをMatchCheckResponseにパース
                    MatchCheckResponse matchResponse = JsonUtility.FromJson<MatchCheckResponse>(responseText);
                    Debug.Log(matchResponse);
                    // エラーメッセージがある場合、エラーをログに出力
                    if (!string.IsNullOrEmpty(matchResponse.error))
                    {
                        Debug.LogError("GAS Error: " + matchResponse.error);
                    }
                    else
                    {
                        // マッチング結果を保存
                        isMatch = matchResponse.matched;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("JSONパースエラー: " + e.Message);
                }
            }
            else
            {
                Debug.LogError("GASからマッチングチェックの取得に失敗しました: " + request.error);
            }
        }
        check = true;
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
    public static void GetRoomRow(MonoBehaviour caller, string roomID)
    {
        check = false;
        caller.StartCoroutine(GetRoomRowFromGAS(roomID));
    }
    public static void CheckMatch(MonoBehaviour caller)
    {
        check = false;
        caller.StartCoroutine(CheckMatchStatus(roomRowNumber));
    }

    // 外部からルームリストを取得
    public static List<string> GetRoomList()
    {
        return roomList;
    }
    // マッチングチェック
    public static bool GetIsMatch()
    {
        return isMatch;
    }
    // ルームの行番号を取得
    public static int GetRoomRowNumber()
    {
        if (roomRowNumber != -1)
        {
            return roomRowNumber;
        }
        Debug.LogWarning("まだルームの行番号が取得されていません");
        return -1; // ルームが見つからなかった場合
    }

    /// <summary>
    /// タイミング調整
    /// </summary>
    /// <returns></returns>
    public static bool GetCHECK()
    {
        return check;
    }
}
