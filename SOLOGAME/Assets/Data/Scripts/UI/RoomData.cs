using System.IO;
using UnityEngine;

namespace MyGame.RoomManagement
{
    [System.Serializable]
    public class ROOMData
    {
        public int roomRow;
        public string roomID;
        public string playerName;
        public bool IsCreate = false;
    }

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
            filePath = Path.Combine(Application.persistentDataPath, "ROOMDATA.json");
            LoadPlayerData();
        }

        public void SaveRoomRow(int roomRow)
        {
            roomData.roomRow = roomRow; // 🔹 既存のデータを上書き
            SaveToFile();
        }

        public void SaveRoomID(string roomID)
        {
            roomData.roomID = roomID; // 🔹 既存のデータを上書き
            SaveToFile();
        }

        public void SavePlayerName(string playerName)
        {
            roomData.playerName = playerName; // 🔹 既存のデータを上書き
            SaveToFile();
        }

        public void SaveIsCreate(bool isCreate)
        {
            roomData.IsCreate = isCreate; // 🔹 既存のデータを上書き
            SaveToFile();
        }

        private void SaveToFile()
        {
            try
            {
                string json = JsonUtility.ToJson(roomData);
                File.WriteAllText(filePath, json);
                // Debug.Log("Room data saved!");
            }
            catch (IOException ex)
            {
                // Debug.LogError("Error saving room data: " + ex.Message);
            }
        }

        public void LoadPlayerData()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    roomData = JsonUtility.FromJson<ROOMData>(json);
                }
                else
                {
                    roomData = new ROOMData();
                }
            }
            catch (IOException ex)
            {
                // Debug.LogError("Error loading room data: " + ex.Message);
            }
        }
    }
}
