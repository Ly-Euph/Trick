using UnityEngine;
using UnityEngine.UI;  // UIコンポーネントにアクセスするために必要

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;  // スライダーUIをインスペクターから設定
    private const string VolumeKey = "MasterVolume";  // PlayerPrefsに保存するためのキー


    // 音量を保存するメソッド
    public void SAVE()
    {
        // 現在のスライダーの値をPlayerPrefsに保存
        PlayerPrefs.SetFloat(VolumeKey, volumeSlider.value);
        PlayerPrefs.Save();  // 保存を確定
        AudioListener.volume = volumeSlider.value;  // 読み込んだ音量を適用
        Debug.Log("音量を保存しました: " + volumeSlider.value);
    }

    // 保存された音量を読み込むメソッド
    public void LOAD()
    {
        // PlayerPrefsから保存されていた音量を読み込む（デフォルトは0.5）
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.5f);
        volumeSlider.value = savedVolume;  // スライダーに設定
        AudioListener.volume = savedVolume;  // 読み込んだ音量を適用
        Debug.Log("音量を読み込みました: " + savedVolume);
    }
}
