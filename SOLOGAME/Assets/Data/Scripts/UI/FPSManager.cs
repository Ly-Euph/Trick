using MiscUtil.Linq.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : MonoBehaviour
{
    // �I������
    string[] FPS = { "30", "60", "120" };
    // ���ݑI������Ă���FPS�̃C���f�b�N�X
    private int currentIndex_FPS = 1;

    // �I������
    string[] FPSMODE = { "��\��", "�\��" };
    // ���ݑI������Ă���FPS�̃C���f�b�N�X
    private int currentIndex_MODE = 0;

    // �C���f�b�N�X�v�Z
    IndexFunc Index;


    private void OnEnable()
    {
        if (Index != null) { return; }
        Index = new IndexFunc();
    }
    public void FPS_NUM_L()
    {
        // �C���f�b�N�X�����炵�āA�͈͓��Ɏ��߂�
        currentIndex_FPS = Index.GetCyclicIndex(currentIndex_FPS, -1, FPS.Length);
    }
    public void FPS_NUM_R()
    {
        // �C���f�b�N�X�𑝂₵�āA�͈͓��Ɏ��߂�
        currentIndex_FPS = Index.GetCyclicIndex(currentIndex_FPS, 1, FPS.Length);
    }
    public void FPS_MDOE_L()
    {
        // �C���f�b�N�X�����炵�āA�͈͓��Ɏ��߂�
        currentIndex_MODE = Index.GetCyclicIndex(currentIndex_MODE, -1, FPSMODE.Length);
    }
    public void FPS_MODE_R()
    {
        // �C���f�b�N�X�𑝂₵�āA�͈͓��Ɏ��߂�
        currentIndex_MODE = Index.GetCyclicIndex(currentIndex_MODE, 1,FPSMODE.Length);
    }

    /// <summary>
    /// FPS�̐��l�ؑ�
    /// </summary>
    /// <returns>FPS�̐��l</returns>
    public string UpdateFPSDisplay()
    {
        // ���݂�FPS��Text�ɕ\��
        return FPS[currentIndex_FPS];
    }
    /// <summary>
    /// FPS�\���ؑ�
    /// </summary>
    /// <returns>�ؑւ̗L��</returns>
    public string UpdateMODEDisplay()
    {
        // ���݂�FPS��Text�ɕ\��
        return FPSMODE[currentIndex_MODE];
    }
}
