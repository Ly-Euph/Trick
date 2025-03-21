using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIButton[] uiButtons; // UIButtonの配列

    private static UIManager instance; // Singletonインスタンス

    [Header("FPSManager"), SerializeField] FPSManager fps;
    [Header("ScreenSizeManager"), SerializeField] ScreenSizeManager size;
    [Header("VolumeController"), SerializeField] VolumeController volume;

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
}
