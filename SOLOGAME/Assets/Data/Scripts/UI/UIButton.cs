using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Reflection;
using System;
using UnityEngine.UI;
using static GAS; // データやり取り
public partial class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region enumData
    // ボタンの種類を設定
    // タイトルシーンでのボタン
    enum TitleButton
    {
        ROOMCREATE, // 部屋作成
        ROOMJOIN,   // 部屋参加
        OPENOPTION, // 設定を開く
        GAMEEND,    // ゲーム終了
        FEEDBACK,   // 感想
        None,
        // 追加
        OK,          // 決定
        RETURN,      // 戻る
    }
    // メニューでのボタン
    enum MenuButton
    {
        RETURNGAME, // ゲームに戻る
        OPENOPTION, // 設定を開く
        EXIT,       // 部屋から退出
        GAMEEND,    // ゲーム終了
        None
    }
    // 設定項目に使うボタン
    enum LRButton
    { 
        /*FPS値のボタン*/
        FPS_NUM_L,  // FPSの左ボタン
        FPS_NUM_R,  // FPSの右ボタン
        /*FPS値の表示のボタン*/
        FPS_MODE_L, // FPSの左ボタン
        FPS_MODE_R, // FPSの右ボタン
        /*画面サイズ切り替えのボタン*/
        SIZEMODE_L, // 切替の左ボタン
        SIZEMODE_R, // 切替の右ボタン

        FIN,        // 設定完了
        None,
    }
    // 基本的にはNoneで設定して使うときに変更一種類のみに設定すること
    [Header("ボタンの種類選択"), SerializeField] TitleButton titleButton=TitleButton.None;
    [Header("ボタンの種類選択"), SerializeField] MenuButton  menuButton=MenuButton.None;
    [Header("ボタンの種類選択"), SerializeField] LRButton lrButton = LRButton.None;
    #endregion

    // 触れたときに分かりやすくするために表示する
    [Header("選択中に表示するオブジェクト"), SerializeField]
    GameObject nowSelectObj;

    /*ものによって使う*/
    // メニューオブジェクトの表示、非表示切替に
    [Header("Menuオブジェクト"),SerializeField]
    GameObject Menu;
    [Header("Optionオブジェクト"),SerializeField]
    GameObject Option;
    [Header("TextBoxのオブジェクト"), SerializeField] 
    GameObject TextBox;
    [Header("TextBoxと合わせて文字を取得するため"),SerializeField]
    InputField InputText;  // InputFieldをInspectorでアタッチ
    [Header("作成もしくは参加ボタンを押したときに表示するオブジェクト"),SerializeField]
    GameObject InputPanel;
    [Header("Waitパネル"), SerializeField]
    GameObject WaitObj;
    // アニメーションの設定
    [SerializeField] Animator anim;

    // 外部スクリプト
    [SerializeField] FPSManager fpsManager;
    [SerializeField] ScreenSizeManager screenSizeManager;
    [SerializeField] VolumeController volumeController;
    [SerializeField] FieldCheck fieldCheck;

    // プレイヤー名やルームIDを設定
    private string playerName = "Player1";  // 例としてプレイヤー名を設定
    private string roomId = "Room001";  // 例としてルームIDを設定

    private bool IsCreate = false;
    private string message;
    // UI接触のインターフェース処理
    #region InterFace
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 表示
        nowSelectObj.SetActive(true);
        if (anim != null)
        {
            anim.Play("Button_0");
        }
        Debug.Log("UIに触れた");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 非表示
        nowSelectObj.SetActive(false);
        if (anim != null)
        {
            anim.Play("IdleUI");
        }
        Debug.Log("UIから離れた");
    }
    #endregion

    // 各種UI一つ一つにアップデートを行うと処理速度が心配のため
    // UIManagerにて一括管理しておく
    public void UpdateButton()
    {
        // 触れた状態で左クリックを押したら処理
        if (nowSelectObj.activeSelf)
        {
            if (titleButton != TitleButton.None)
            {
                // Enumの値ごとに対応する関数を実行
                InvokeMatchingMethod(titleButton);
            }
            else if(menuButton != MenuButton.None)
            {
                // Enumの値ごとに対応する関数を実行
                InvokeMatchingMethod(menuButton);
            }
            else if (lrButton != LRButton.None)
            {
                // Enumの値ごとに対応する関数を実行
                InvokeMatchingMethod(lrButton);
            }
        }
    }

    #region MatchingMethod
    private void InvokeMatchingMethod(TitleButton kButton)
    {
        string methodName = kButton.ToString(); // Enumの名前を文字列化

        // メソッド情報を取得
        MethodInfo method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (method != null)
        {
            method.Invoke(this, null);
        }
        else
        {
            Debug.LogWarning($"メソッド '{methodName}' が見つかりません。");
        }
    }
    private void InvokeMatchingMethod(LRButton kButton)
    {
        string methodName = kButton.ToString(); // Enumの名前を文字列化

        // メソッド情報を取得
        MethodInfo method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (method != null)
        {
            method.Invoke(this, null);
        }
        else
        {
            Debug.LogWarning($"メソッド '{methodName}' が見つかりません。");
        }
    }
    private void InvokeMatchingMethod(MenuButton kButton)
    {
        string methodName = kButton.ToString(); // Enumの名前を文字列化

        // メソッド情報を取得
        MethodInfo method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (method != null)
        {
            method.Invoke(this, null);
        }
        else
        {
            Debug.LogWarning($"メソッド '{methodName}' が見つかりません。");
        }
    }
    #endregion
}
