using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldCheck : MonoBehaviour
{
    [Header("ルームID"), SerializeField] InputField roomText;
    [Header("プレイヤー名"), SerializeField] InputField nameText;

    // 入力エラーのときに知らせる機能
    [SerializeField] GameObject Itemroom;
    [SerializeField] GameObject Itemname;

    /// <summary>
    /// 未入力がないかチェック
    /// </summary>
    /// <returns>問題なければtrueを返します</returns>
    public bool IsCheck()
    {
        // 未入力がないかチェック
        if (roomText.text == "")
        {
            Itemroom.SetActive(true);
        }
        else
        {
            Itemroom.SetActive(false);
        }
        if (nameText.text == "")
        {
            Itemname.SetActive(true);
        }
        else
        {  
            Itemname.SetActive(false);
        }
        if (Itemname.activeSelf || Itemroom.activeSelf)
        {
            return false;
        }
        return true;
    }

    public string GETTEXT_room
    {
        get { return roomText.text; }
    }
    public string GETTEXT_name
    {
        get { return nameText.text; }
    }
}

