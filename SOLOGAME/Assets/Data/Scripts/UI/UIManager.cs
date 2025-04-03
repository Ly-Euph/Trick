using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private UIButton[] uiButtons; // UIButtonの配列

    FadeInOut fade; // フェード機能

    private static UIManager instance; // Singletonインスタンス

    [Header("FPSManager"), SerializeField] FPSManager fps;
    [Header("ScreenSizeManager"), SerializeField] ScreenSizeManager size;
    [Header("VolumeController"), SerializeField] VolumeController volume;

    private float timer = 0;

    void Awake()
    {
        // Singletonパターン: 他のUIManagerが既に存在する場合、現在のインスタンスを削除
        if (instance != null)
        {
            Destroy(gameObject); // 重複したインスタンスを削除
        }
        else
        {
            instance = this; // 初回のみインスタンスを設定
            DontDestroyOnLoad(gameObject); // このオブジェクトをシーンが変更されても破棄しない
        }
    }

    void Start()
    {
        // シーン内のすべてのUIButtonを検索
        uiButtons = FindObjectsOfType<UIButton>(true);
        fade = FadeInOut.CreateInstance();
        // データの読み込み
        fps.LOAD();
        size.LOAD();
        volume.LOAD();
    }

    void Update()
    {
        //　計算処理
        fps.FPSdelta();

        // ボタン入力
        if (Input.GetMouseButtonDown(0)) {
            // 各UIButtonのUpdateBUTTON()を呼び出す
            foreach (var button in uiButtons)
            {
                button.UpdateButton(); // ボタンがUpdateBUTTONを実行
            }
        }   
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 5.0f)
        {
            foreach (var button in uiButtons)
            {
                if (button.ReturnNowMatch())
                {
                    // コルーチンが終了するまで待機
                    StartCoroutine(WaitForMatch());
                }
            }
        }
    }

    private IEnumerator WaitForMatch()
    {
        timer = 0;
        GAS.CheckMatch(this);
        // `check` が `true` になるまで待機
        while (!GAS.GetCHECK())
        {
            yield return null; // 次のフレームまで待機
        }

        // マッチングしたらここでシーン遷移
        if (GAS.GetIsMatch())
        {
            fade.LoadScene("Game");
        }
    }
}
