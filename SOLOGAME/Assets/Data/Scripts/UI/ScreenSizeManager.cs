using MiscUtil.Linq.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenSizeManager : MonoBehaviour
{
    // �I������
    string[] SIZEMODE = { "FullScreen", "Window" };
    // ���ݑI������Ă���SIZEMODE�̃C���f�b�N�X
    private int currentIndex_SIZE = 0;
    // �C���f�b�N�X�v�Z
    IndexFunc Index;
    // ���f��̃e�L�X�g
    [SerializeField] Text ScreenSizeText;

    private void OnEnable()
    {
        if (Index != null) { return; }
        Index = new IndexFunc();
    }

    /// <summary>
    /// �T�C�Y�ؑւ̍��{�^���ł��B
    /// </summary>
    public void SIZEMODE_L()
    {
        // �C���f�b�N�X�����炵�āA�͈͓��Ɏ��߂�
        currentIndex_SIZE = Index.GetCyclicIndex(currentIndex_SIZE, -1, SIZEMODE.Length);
        ScreenSizeText.text = UpdateSIZEDisplay();
    }
    /// <summary>
    /// �T�C�Y�ؑւ̉E�{�^���ł��B
    /// </summary>
    public void SIZEMODE_R()
    {
        // �C���f�b�N�X�����炵�āA�͈͓��Ɏ��߂�
        currentIndex_SIZE = Index.GetCyclicIndex(currentIndex_SIZE, +1, SIZEMODE.Length);
        ScreenSizeText.text = UpdateSIZEDisplay();
    }

    // �\�����X�V
    private string UpdateSIZEDisplay()
    {
        // ���݂�FPS��Text�ɕ\��
        return SIZEMODE[currentIndex_SIZE];
    }

    /// <summary>
    /// �f�[�^�̃��[�h
    /// </summary>
    public void LOAD()
    {
        // �ǂݍ���
        currentIndex_SIZE = PlayerPrefs.GetInt("currentIndex_SIZE",0);
        // ���f������
        ScreenSizeText.text = UpdateSIZEDisplay();

        ScreenSize();
    }

    /// <summary>
    /// �ݒ芮���Ɠ����ɕ��@���Ă���
    /// </summary>
    public void SAVE()
    {
        PlayerPrefs.SetInt("currentIndex_SIZE", currentIndex_SIZE);
        PlayerPrefs.Save(); // �ۑ�
        ScreenSize();
    }

    // �ݒ��Ԃ��擾�����̌��ʃT�C�Y��ύX����
    private void ScreenSize()
    {
        if(PlayerPrefs.GetInt("currentIndex_SIZE", 0)==0)
        {
            FullScreenSize();
        }
        else
        {
            WindowSize();
        }
    }    

    // ��ʃT�C�Y�ύX
    private void FullScreenSize()
    {
        Screen.SetResolution(1920, 1080, true); // 1920x1080 �̃t���X�N���[��
    }    
    private void WindowSize()
    {
        // ���j�^�[�̉𑜓x���擾
        int screenWidth = Screen.currentResolution.width;
        int screenHeight = Screen.currentResolution.height;

        // 16:9�̔䗦���ێ��������ƍ������v�Z
        float aspectRatio = 16f / 9f;
        int targetWidth = Mathf.FloorToInt(screenHeight * aspectRatio); // �������畝���v�Z
        int targetHeight = screenHeight; // �����͂��̂܂�

        // ���j�^�[�T�C�Y�����菬�����l�ɒ���
        targetWidth = Mathf.Min(targetWidth, screenWidth - 100); // 100px�����������
        targetHeight = Mathf.Min(targetHeight, screenHeight - 100); // 100px�����������

        // �E�B���h�E�T�C�Y��ݒ�
        Screen.SetResolution(targetWidth, targetHeight, false); // `false` �ŃE�B���h�E���[�h
    }
}
