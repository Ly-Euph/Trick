using MiscUtil.Linq.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : MonoBehaviour
{
    // 選択項目
    string[] FPS = { "30", "60", "120" };
    // 現在選択されているFPSのインデックス
    private int currentIndex_FPS = 1;

    // 選択項目
    string[] FPSMODE = { "非表示", "表示" };
    // 現在選択されているFPSのインデックス
    private int currentIndex_MODE = 0;

    // インデックス計算
    IndexFunc Index;


    private void OnEnable()
    {
        if (Index != null) { return; }
        Index = new IndexFunc();
    }
    public void FPS_NUM_L()
    {
        // インデックスを減らして、範囲内に収める
        currentIndex_FPS = Index.GetCyclicIndex(currentIndex_FPS, -1, FPS.Length);
    }
    public void FPS_NUM_R()
    {
        // インデックスを増やして、範囲内に収める
        currentIndex_FPS = Index.GetCyclicIndex(currentIndex_FPS, 1, FPS.Length);
    }
    public void FPS_MDOE_L()
    {
        // インデックスを減らして、範囲内に収める
        currentIndex_MODE = Index.GetCyclicIndex(currentIndex_MODE, -1, FPSMODE.Length);
    }
    public void FPS_MODE_R()
    {
        // インデックスを増やして、範囲内に収める
        currentIndex_MODE = Index.GetCyclicIndex(currentIndex_MODE, 1,FPSMODE.Length);
    }

    /// <summary>
    /// FPSの数値切替
    /// </summary>
    /// <returns>FPSの数値</returns>
    public string UpdateFPSDisplay()
    {
        // 現在のFPSをTextに表示
        return FPS[currentIndex_FPS];
    }
    /// <summary>
    /// FPS表示切替
    /// </summary>
    /// <returns>切替の有無</returns>
    public string UpdateMODEDisplay()
    {
        // 現在のFPSをTextに表示
        return FPSMODE[currentIndex_MODE];
    }
}
