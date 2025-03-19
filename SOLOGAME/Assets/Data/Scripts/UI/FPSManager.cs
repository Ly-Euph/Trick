using UnityEngine;
using UnityEngine.UI;
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

    // FPSの表示をしているテキスト
    [SerializeField] Text FpsText;     // 設定画面の文字
    [SerializeField] Text FpsModeText; // 設定画面の文字
    [SerializeField] Text NowFPSText;  // FPS値を表示する
    // FPSの平均を求めて表示する
    private float deltaTime = 0.0f;

    private void OnEnable()
    {
        if (Index != null) { return; }
        Index = new IndexFunc();
    }
    public void FPS_NUM_L()
    {
        // インデックスを減らして、範囲内に収める
        currentIndex_FPS = Index.GetCyclicIndex(currentIndex_FPS, -1, FPS.Length);
        FpsText.text = UpdateFPSDisplay();
    }
    public void FPS_NUM_R()
    {
        // インデックスを増やして、範囲内に収める
        currentIndex_FPS = Index.GetCyclicIndex(currentIndex_FPS, 1, FPS.Length);
        FpsText.text = UpdateFPSDisplay();
    }
    public void FPS_MDOE_L()
    {
        // インデックスを減らして、範囲内に収める
        currentIndex_MODE = Index.GetCyclicIndex(currentIndex_MODE, -1, FPSMODE.Length);
        FpsModeText.text = UpdateMODEDisplay();
    }
    public void FPS_MODE_R()
    {
        // インデックスを増やして、範囲内に収める
        currentIndex_MODE = Index.GetCyclicIndex(currentIndex_MODE, 1,FPSMODE.Length);
        FpsModeText.text = UpdateMODEDisplay();
    }

    /// <summary>
    /// FPSの数値切替
    /// </summary>
    /// <returns>FPSの数値</returns>
    private string UpdateFPSDisplay()
    {
        // 現在のFPSをTextに表示
        return FPS[currentIndex_FPS];
    }
    /// <summary>
    /// FPS表示切替
    /// </summary>
    /// <returns>切替の有無</returns>
    private string UpdateMODEDisplay()
    {
        // 現在のFPSをTextに表示
        return FPSMODE[currentIndex_MODE];
    }

    /// <summary>
    /// データのロード
    /// </summary>
    public void LOAD()
    {
        // 読み込む
        currentIndex_FPS = PlayerPrefs.GetInt("currentIndex_FPS", 1);
        currentIndex_MODE = PlayerPrefs.GetInt("currentIndex_MODE", 0);
        // 反映させる
        FpsText.text = UpdateFPSDisplay();
        FpsModeText.text = UpdateMODEDisplay();

        // FPSの設定
        Application.targetFrameRate = int.Parse(FPS[PlayerPrefs.GetInt("currentIndex_FPS", 0)]);
    }

    /// <summary>
    /// 設定完了と同時に方法しておく
    /// </summary>
    public void SAVE()
    {
        PlayerPrefs.SetInt("currentIndex_FPS",currentIndex_FPS);
        PlayerPrefs.SetInt("currentIndex_MODE", currentIndex_MODE);
        PlayerPrefs.Save(); // 保存

        // 非表示ならテキストは空白にしておく
        if (PlayerPrefs.GetInt("currentIndex_MODE", 0) == 0)
        {
            // 空白
            NowFPSText.text = "";
        }

        // FPSの設定
        Application.targetFrameRate = int.Parse(FPS[PlayerPrefs.GetInt("currentIndex_FPS", 0)]);
    }

    /// <summary>
    /// FPS値を計算
    /// </summary>
     public void FPSdelta()
    {
        // 表示状態の時のみ計算させるので非表示状態はreturn
        if(PlayerPrefs.GetInt("currentIndex_MODE", 0) == 0)
        {
            return;
        }
        else
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f; // 過去の値と平均化
            float fps = 1.0f / deltaTime;
            NowFPSText.text = "FPS:" + fps.ToString("F0");
        }
    }
}
