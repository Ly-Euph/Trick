using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeManager : MonoBehaviour
{
    // 選択項目
    string[] SIZEMODE = { "FullScreen", "Window" };
    // 現在選択されているSIZEMODEのインデックス
    private int currentIndex_SIZE = 0;
    // インデックス計算
    IndexFunc Index;

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
    }
    /// <summary>
    /// サイズ切替の右ボタンです。
    /// </summary>
    public void SIZEMODE_R()
    {
        // インデックスを減らして、範囲内に収める
        currentIndex_SIZE = Index.GetCyclicIndex(currentIndex_SIZE, +1, SIZEMODE.Length);
    }

    // 表示を更新
    public string UpdateSIZEDisplay()
    {
        // 現在のFPSをTextに表示
        return SIZEMODE[currentIndex_SIZE];
    }

}
