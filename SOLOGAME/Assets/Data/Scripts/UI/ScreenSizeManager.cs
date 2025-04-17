using MiscUtil.Linq.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenSizeManager : MonoBehaviour
{
    // 選択項目
    string[] SIZEMODE = { "FullScreen", "Window" };
    // 現在選択されているSIZEMODEのインデックス
    private int currentIndex_SIZE = 0;
    // インデックス計算
    IndexFunc Index;
    // 反映先のテキスト
    [SerializeField] Text ScreenSizeText;

    private void OnEnable()
    {
        if (Index != null) { return; }
        Index = new IndexFunc();
    }

    /// <summary>
    /// サイズ切替の左ボタンです。
    /// </summary>
    public void SIZEMODE_L()
    {
        // インデックスを減らして、範囲内に収める
        currentIndex_SIZE = Index.GetCyclicIndex(currentIndex_SIZE, -1, SIZEMODE.Length);
        ScreenSizeText.text = UpdateSIZEDisplay();
    }
    /// <summary>
    /// サイズ切替の右ボタンです。
    /// </summary>
    public void SIZEMODE_R()
    {
        // インデックスを減らして、範囲内に収める
        currentIndex_SIZE = Index.GetCyclicIndex(currentIndex_SIZE, +1, SIZEMODE.Length);
        ScreenSizeText.text = UpdateSIZEDisplay();
    }

    // 表示を更新
    private string UpdateSIZEDisplay()
    {
        // 現在のFPSをTextに表示
        return SIZEMODE[currentIndex_SIZE];
    }

    /// <summary>
    /// データのロード
    /// </summary>
    public void LOAD()
    {
        // 読み込む
        currentIndex_SIZE = PlayerPrefs.GetInt("currentIndex_SIZE",0);
        // 反映させる
        ScreenSizeText.text = UpdateSIZEDisplay();

        ScreenSize();
    }

    /// <summary>
    /// 設定完了と同時に方法しておく
    /// </summary>
    public void SAVE()
    {
        PlayerPrefs.SetInt("currentIndex_SIZE", currentIndex_SIZE);
        PlayerPrefs.Save(); // 保存
        ScreenSize();
    }

    // 設定状態を取得しその後画面サイズを変更する
    private void ScreenSize()
    {
        if(PlayerPrefs.GetInt("currentIndex_SIZE", 0)==0)
        {
            FullScreenSize();
        }
        else
        {
            WindowSize();
        }
    }    

    // 画面サイズ変更
    private void FullScreenSize()
    {
        Screen.SetResolution(1920, 1080, true); // 1920x1080 のフルスクリーン
    }    
    private void WindowSize()
    {
        // モニターの解像度を取得
        int screenWidth = Screen.currentResolution.width;
        int screenHeight = Screen.currentResolution.height;

        // 16:9の比率を維持した幅と高さを計算
        float aspectRatio = 16f / 9f;
        int targetWidth = Mathf.FloorToInt(screenHeight * aspectRatio); // 高さから幅を計算
        int targetHeight = screenHeight; // 高さはそのまま

        // モニターサイズより一回り小さい値に調整
        targetWidth = Mathf.Min(targetWidth, screenWidth - 100); // 100px小さくする例
        targetHeight = Mathf.Min(targetHeight, screenHeight - 100); // 100px小さくする例

        // ウィンドウサイズを設定
        Screen.SetResolution(targetWidth, targetHeight, false); // `false` でウィンドウモード
    }
}
