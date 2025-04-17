using UnityEngine;
using UnityEngine.UI;

public class AlphanumericInput : MonoBehaviour
{
    [SerializeField] InputField inputField;

    void Start()
    {
        // InputFieldのonValueChangedイベントに関数を登録
        inputField.onValueChanged.AddListener(ValidateInput);
    }

    // 入力値をチェックし、英数字以外を削除
    private void ValidateInput(string input)
    {
        // 正規表現で英数字以外の文字を削除
        string validatedInput = System.Text.RegularExpressions.Regex.Replace(input, "[^a-zA-Z0-9]", "");

        // 入力フィールドに英数字だけを再設定
        inputField.text = validatedInput;
    }

    void OnDestroy()
    {
        // イベント解除
        inputField.onValueChanged.RemoveListener(ValidateInput);
    }
}
