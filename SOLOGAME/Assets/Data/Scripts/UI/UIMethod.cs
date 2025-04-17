using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.RoomManagement;
partial class UIButton
{
    private RoomData roomData;

    #region[MenuItem]
    // ルーム作成ボタン
    private void ROOMCREATE()
    {
        // RoomDataのインスタンスを作成
        roomData = new RoomData();

        // SaveIsCreate メソッドを呼び出す
        roomData.SaveIsCreate(true);

        InputPanel.SetActive(true);
    }

    // ルーム参加ボタンの処理
    private void ROOMJOIN()
    {
        // RoomDataのインスタンスを作成
        roomData = new RoomData();

        // SaveIsCreate メソッドを呼び出す
        roomData.SaveIsCreate(false);

        InputPanel.SetActive(true);
    }

    // 設定を開く
    private void OPENOPTION()
    {
        Debug.Log("オプション画面を開く");
        Option.SetActive(true);
    }

    // 感想を送る
    private void FEEDBACK()
    {
        if(TextBox.activeSelf)
        {
            string message = "?action=feedBack"
                            + "&Text="+InputText.text;
            GAS.StartCoroutineWrapper(this, message);
            // 送信後空白に戻す
            InputText.text = "";
            TextBox.SetActive(false);
            nowSelectObj.SetActive(false);

        }
        else
        {
            TextBox.SetActive(true);
        }
    }

    // 作成もしくは参加を行う
    private void OK()
    {
        if (fieldCheck.IsCheck())
        {
            // RoomDataのインスタンスを作成
            roomData = new RoomData();

            roomId = fieldCheck.GETTEXT_room;
            playerName = fieldCheck.GETTEXT_name;

            // 作成中が表示されているオブジェクトを表示に

            WaitObj.SetActive(true);
  
            // ルームIDリストをGASから取得して、作成時にもルームIDが重複しないかチェック
            GAS.StartCoroutineWrapper(this);

            // コルーチンが終了するまで待機
            StartCoroutine(WaitForRoomList());

        }
    }

    // ルームリスト取得完了を待つコルーチン
    private IEnumerator WaitForRoomList()
    {
        // `check` が `true` になるまで待機
        while (!GAS.GetCHECK())
        {
            yield return null; // 次のフレームまで待機
        }
        List<string> roomList = GAS.GetRoomList(); // 一度だけ取得
        if (roomList == null)
        {
            Debug.LogError("roomList is null");
        }
        else if (roomList.Count == 0)
        {
            Debug.LogError("roomList is empty");
        }
        else
        {
            Debug.Log("Room List: " + string.Join(", ", roomList));
        }
        // `check` が `true` になったら次の処理に進む
        if (roomData.ReturnroomData.IsCreate)
        {
            // ルーム作成の場合、ルームIDの重複チェック
            if (roomList.Contains(roomId))
            {
                // ルームIDがすでに存在する場合のエラーメッセージ
                Debug.LogError("このルームIDはすでに存在します。別のIDを選択してください。");
                WaitObj.GetComponent<CreateJoinLoad>().Error();
                // 遅延させる
                yield return new WaitForSeconds(3f);
                WaitObj.SetActive(false);
                yield break;
            }
            else
            {
                // 重複がなければルームを作成
                string message = "?action=createRoom"
                                 + "&roomId=" + Uri.EscapeDataString(roomId)
                                 + "&player1=" + Uri.EscapeDataString(playerName);

                // 保存する
                roomData.SaveRoomID(roomId);
                roomData.SavePlayerName(playerName);
                Debug.Log("Sending request to GAS with URL: " + message);
                GAS.StartCoroutineWrapper(this, message);

                // 遅延させる
                yield return new WaitForSeconds(8f);

                // 行数取得のコルーチン
                // 入室のタイミングで部屋の名前にプラスで_occupiedが入るので
                // 文字列を追加することでさらに検索出来ない仕様
                GAS.GetRoomRow(this, roomData.ReturnroomData.roomID);
            }
        }
        else
        {
            // ルーム参加の場合、ルームIDが存在しない場合はエラーを表示
            if (!roomList.Contains(roomId))
            {
                // ルームIDが存在しない場合のエラーメッセージ
                Debug.LogError("指定されたルームIDは存在しません。");
                WaitObj.GetComponent<CreateJoinLoad>().Error();
                // 遅延させる
                yield return new WaitForSeconds(3f);
                WaitObj.SetActive(false);
                yield break;
            }
            else
            {
                // ルームが存在する場合、参加処理を進める
                string message = "?action=joinRoom"
                                 + "&roomId=" + Uri.EscapeDataString(roomId)
                                 + "&player2=" + Uri.EscapeDataString(playerName);

                // 保存する
                roomData.SaveRoomID(roomId);
                roomData.SavePlayerName(playerName);
                Debug.Log("Sending request to GAS with URL: " + message);
                GAS.StartCoroutineWrapper(this, message);

                // 遅延させる
                yield return new WaitForSeconds(8f);

                // 行数取得のコルーチン
                // 入室のタイミングで部屋の名前にプラスで_occupiedが入るので
                // 文字列を追加することでさらに検索出来ない仕様
                GAS.GetRoomRow(this, roomData.ReturnroomData.roomID + "_occupied");
            }
        }
        // コルーチンが終了するまで待機
        StartCoroutine(WaitForRoomRow());
    }
    private IEnumerator WaitForRoomRow()
    {
        // `check` が `true` になるまで待機
        while (!GAS.GetCHECK())
        {
            yield return null; // 次のフレームまで待機
        }

        // 作成中が表示されているオブジェクトを非表示に
        WaitObj.SetActive(false);

        roomData.SaveRoomRow(GAS.GetRoomRowNumber());

        Debug.Log(roomData.ReturnroomData.roomRow);

        // プレイヤーは作成もしくは参加を行ったので
        // マッチングチェックを行いゲームシーンに切り替える
        // これはマッチングチェックを行うタイミング
        NowMatch = true;

        nowSelectObj.SetActive(false);
        InputPanel.SetActive(false);
    }

    // 参加、作成のパネルから戻る
    private void RETURN()
    {
        nowSelectObj.SetActive(false);
        InputPanel.SetActive(false);
    }
    // ほかでも共通で使う
    // ゲーム終了
    private void GAMEEND()
    {
        Application.Quit();

        // エディタでも反応
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion


    #region [MENUItem]
    // ゲームに戻る
    private void RETURNGAME()
    {
        // キャンバスごと非表示にするので個別に非表示にしなければならない
        // 非表示
        nowSelectObj.SetActive(false);

        Menu.SetActive(false);
    }
    #endregion

    #region OPTION
    /*Lは左ボタンRは右ボタンを意味*/
    // FPSの値変更
    private void FPS_NUM_L()
    {
        fpsManager.FPS_NUM_L(); // インデックス計算
    }
    private void FPS_NUM_R()
    {
        fpsManager.FPS_NUM_R(); // インデックス計算
    }
    // 表示切り替え
    private void FPS_MODE_L()
    {
        fpsManager.FPS_MDOE_L(); // インデックス計算
    }
    private void FPS_MODE_R()
    {
        fpsManager.FPS_MODE_R(); // インデックス計算
    }
    // サイズ変更
    private void SIZEMODE_L()
    {
        screenSizeManager.SIZEMODE_L(); // インデックス計算
    }
    private void SIZEMODE_R()
    {
        screenSizeManager.SIZEMODE_R(); // インデックス計算
    }
    // 設定完了
    private void FIN()
    {
        // データの保存
        fpsManager.SAVE();
        screenSizeManager.SAVE();
        volumeController.SAVE();
        // 非表示
        nowSelectObj.SetActive(false);
        Option.SetActive(false);
    }    
    #endregion
}