using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeManager : MonoBehaviour
{
    // �I������
    string[] SIZEMODE = { "FullScreen", "Window" };
    // ���ݑI������Ă���SIZEMODE�̃C���f�b�N�X
    private int currentIndex_SIZE = 0;
    // �C���f�b�N�X�v�Z
    IndexFunc Index;

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
    }
    /// <summary>
    /// �T�C�Y�ؑւ̉E�{�^���ł��B
    /// </summary>
    public void SIZEMODE_R()
    {
        // �C���f�b�N�X�����炵�āA�͈͓��Ɏ��߂�
        currentIndex_SIZE = Index.GetCyclicIndex(currentIndex_SIZE, +1, SIZEMODE.Length);
    }

    // �\�����X�V
    public string UpdateSIZEDisplay()
    {
        // ���݂�FPS��Text�ɕ\��
        return SIZEMODE[currentIndex_SIZE];
    }

}
