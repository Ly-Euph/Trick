using System.IO;
using UnityEngine;

namespace MyGame.RoomManagement
{
    [System.Serializable]
    public class ROOMData
    {
        public string roomID;
        public string playerName;
        public bool IsCreate = false;
    }

    // MonoBehaviour を継承しないクラスに変更
    public class RoomData
    {
        private string filePath;
        private ROOMData roomData;

        public ROOMData ReturnroomData
        {
            get { return roomData; }
        }

        public RoomData()
        {
            // 初期化の際にファイルパスを設定
            filePath = Path.Combine(Application.persistentDataPath, "ROOMDATA.json");

            // 初回ロード
            LoadPlayerData();
        }

        public void SaveRoomID(string roomID)
        {
            roomData = new ROOMData { roomID = roomID };
            string json = JsonUtility.ToJson(roomData);
            try
            {
                File.WriteAllText(filePath, json);
                //Debug.Log("Room ID saved to JSON!");
            }
            catch (IOException ex)
            {
                //Debug.LogError("Error saving Room ID: " + ex.Message);
            }
        }

        public void SavePlayerName(string playerName)
        {
            roomData = new ROOMData { playerName = playerName };
            string json = JsonUtility.ToJson(roomData);
            try
            {
                File.WriteAllText(filePath, json);
               // Debug.Log("Player Name saved to JSON!");
            }
            catch (IOException ex)
            {
               // Debug.LogError("Error saving Player Name: " + ex.Message);
            }
        }

        public void SaveIsCreate(bool isCreate)
        {
            roomData = new ROOMData { IsCreate = isCreate };
            string json = JsonUtility.ToJson(roomData);
            try
            {
                File.WriteAllText(filePath, json);
               // Debug.Log("IsCreate status saved to JSON!");
            }
            catch (IOException ex)
            {
               // Debug.LogError("Error saving IsCreate status: " + ex.Message);
            }
        }

        public void LoadPlayerData()
        {
            try
            {
                // ファイルが存在する場合のみ読み込む
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    roomData = JsonUtility.FromJson<ROOMData>(json);
                }
                else
                {
                   // Debug.LogWarning("Save file does not exist, creating a new one...");
                    // ファイルがない場合は新たにデータを作成
                    roomData = new ROOMData();
                }
            }
            catch (IOException ex)
            {
                //Debug.LogError("Error loading room data: " + ex.Message);
            }
        }
    }
}
