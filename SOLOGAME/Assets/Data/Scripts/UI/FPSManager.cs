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


    public void FPS_NUM_L()
    {
        // インデックスを減らして、範囲内に収める
        currentIndex_FPS = GetCyclicIndex(currentIndex_FPS, -1, FPS.Length);
    }
    public void FPS_NUM_R()
    {
        // インデックスを増やして、範囲内に収める
        currentIndex_FPS = GetCyclicIndex(currentIndex_FPS, 1, FPS.Length);
    }
    public void FPS_MDOE_L()
    {
        // インデックスを減らして、範囲内に収める
        currentIndex_MODE = GetCyclicIndex(currentIndex_MODE, -1, FPSMODE.Length);
    }
    public void FPS_MODE_R()
    {
        // インデックスを増やして、範囲内に収める
        currentIndex_MODE = GetCyclicIndex(currentIndex_MODE, 1,FPSMODE.Length);
    }

    // FPSの表示を更新
    public string UpdateFPSDisplay()
    {
        // 現在のFPSをTextに表示
        return FPS[currentIndex_FPS];
    }
    public string UpdateMODEDisplay()
    {
        // 現在のFPSをTextに表示
        return FPSMODE[currentIndex_MODE];
    }

    // インデックスを範囲内で循環させる（増減する）
    private int GetCyclicIndex(int currentIndex, int direction,int length)
    {
        // インデックスを増減
        int nextIndex = currentIndex + direction;

        // 範囲内に収める処理
        if (nextIndex < 0)
        {
            nextIndex = length - 1; // 範囲外になったら最大インデックスに戻す
        }
        else if (nextIndex >= length)
        {
            nextIndex = 0; // 範囲外になったら最小インデックスに戻す
        }

        return nextIndex;
    }
}
