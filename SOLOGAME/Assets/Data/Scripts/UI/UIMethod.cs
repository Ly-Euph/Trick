using MiscUtil.Linq.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class UIButton
{
    // ルーム作成ボタン
    private void ROOMCREATE()
    {
        playerName = "NEKO";
        string url = gasUrl + "?action=createRoom"
                           + "&roomId=" + Uri.EscapeDataString(roomId)
                           + "&player1=" + Uri.EscapeDataString(playerName); Debug.Log("Sending request to GAS with URL: " + url);  // ここでURLを確認
        StartCoroutine(SendDataToGAS(url));
    }

    // ルーム参加ボタンの処理
    private void ROOMJOIN()
    {
        playerName = "SAME";
        string url = gasUrl + "?action=joinRoom"
                           + "&roomId=" + Uri.EscapeDataString(roomId)
                           + "&player2=" + Uri.EscapeDataString(playerName);
        StartCoroutine(SendDataToGAS(url));
    }

    // 設定を開く
    private void OPENOPTION()
    {
        Debug.Log("オプション画面を開く");
        Option.SetActive(true);
    }

    // ゲーム終了
    private void GAMEEND()
    {
        Application.Quit();

        // エディタでも反応
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // ゲームに戻る
    private void RETURNGAME()
    {
        // キャンバスごと非表示にするので個別に非表示にしなければならない
        // 非表示
        nowSelectObj.SetActive(false);

        Menu.SetActive(false);
    }


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