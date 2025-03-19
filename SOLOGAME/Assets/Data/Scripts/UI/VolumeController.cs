using UnityEngine;
using UnityEngine.UI;  // UI�R���|�[�l���g�ɃA�N�Z�X���邽�߂ɕK�v

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;  // �X���C�_�[UI���C���X�y�N�^�[����ݒ�
    private const string VolumeKey = "MasterVolume";  // PlayerPrefs�ɕۑ����邽�߂̃L�[


    // ���ʂ�ۑ����郁�\�b�h
    public void SAVE()
    {
        // ���݂̃X���C�_�[�̒l��PlayerPrefs�ɕۑ�
        PlayerPrefs.SetFloat(VolumeKey, volumeSlider.value);
        PlayerPrefs.Save();  // �ۑ����m��
        AudioListener.volume = volumeSlider.value;  // �ǂݍ��񂾉��ʂ�K�p
        Debug.Log("���ʂ�ۑ����܂���: " + volumeSlider.value);
    }

    // �ۑ����ꂽ���ʂ�ǂݍ��ރ��\�b�h
    public void LOAD()
    {
        // PlayerPrefs����ۑ�����Ă������ʂ�ǂݍ��ށi�f�t�H���g��0.5�j
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.5f);
        volumeSlider.value = savedVolume;  // �X���C�_�[�ɐݒ�
        AudioListener.volume = savedVolume;  // �ǂݍ��񂾉��ʂ�K�p
        Debug.Log("���ʂ�ǂݍ��݂܂���: " + savedVolume);
    }
}
