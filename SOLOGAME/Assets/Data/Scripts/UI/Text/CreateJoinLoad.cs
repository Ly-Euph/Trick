using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class CreateJoinLoad : MonoBehaviour
{

    [SerializeField] bool isCreate=true;
    // 出力先のテキスト
    [SerializeField] Text text;
    string create = "作成中";
    string join = "ルームを検索中";

    string createErr = "このルームIDはすでに存在します。";
    string joinErr = "指定されたルームIDは存在しません。";
    string addString = ".";

    // タイマー
    float timer = 0;
    float Ctimer = 2.0f;

    // メソッドを何回呼び出したかカウント
    int countfunc = 0;
    private void OnEnable()
    {
        countfunc = 0;
        IsCreate();
    }
    void Update()
    {
        if (countfunc == -1) { return; }
        timer += Time.deltaTime;
        if(timer>=Ctimer)
        {
            timer = 0;
            AddText();
        }
    }

    // 文字の最後に...を付けてプレイヤーに
    // 作成中ですこし待ってもらうことを伝える
    private void AddText()
    {
        text.text += addString;
        countfunc++;
        if(countfunc>=3)
        {
            countfunc = 0;
            IsCreate();
        }
    }

    // セットするテキストを判断
    private void IsCreate()
    {
        if (isCreate)
        {
            text.text = create;
        }
        else
        {
            text.text = join;
        }
    }

    // 作成失敗、入室失敗時の関数
    public void Error()
    {
        if (isCreate)
        {
            text.text = createErr;
        }
        else
        {
            text.text = joinErr;
        }
        // 例外
        countfunc = -1;
    }
}
