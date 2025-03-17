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


    public void FPS_NUM_L()
    {
        // �C���f�b�N�X�����炵�āA�͈͓��Ɏ��߂�
        currentIndex_FPS = GetCyclicIndex(currentIndex_FPS, -1, FPS.Length);
    }
    public void FPS_NUM_R()
    {
        // �C���f�b�N�X�𑝂₵�āA�͈͓��Ɏ��߂�
        currentIndex_FPS = GetCyclicIndex(currentIndex_FPS, 1, FPS.Length);
    }
    public void FPS_MDOE_L()
    {
        // �C���f�b�N�X�����炵�āA�͈͓��Ɏ��߂�
        currentIndex_MODE = GetCyclicIndex(currentIndex_MODE, -1, FPSMODE.Length);
    }
    public void FPS_MODE_R()
    {
        // �C���f�b�N�X�𑝂₵�āA�͈͓��Ɏ��߂�
        currentIndex_MODE = GetCyclicIndex(currentIndex_MODE, 1,FPSMODE.Length);
    }

    // FPS�̕\�����X�V
    public string UpdateFPSDisplay()
    {
        // ���݂�FPS��Text�ɕ\��
        return FPS[currentIndex_FPS];
    }
    public string UpdateMODEDisplay()
    {
        // ���݂�FPS��Text�ɕ\��
        return FPSMODE[currentIndex_MODE];
    }

    // �C���f�b�N�X��͈͓��ŏz������i��������j
    private int GetCyclicIndex(int currentIndex, int direction,int length)
    {
        // �C���f�b�N�X�𑝌�
        int nextIndex = currentIndex + direction;

        // �͈͓��Ɏ��߂鏈��
        if (nextIndex < 0)
        {
            nextIndex = length - 1; // �͈͊O�ɂȂ�����ő�C���f�b�N�X�ɖ߂�
        }
        else if (nextIndex >= length)
        {
            nextIndex = 0; // �͈͊O�ɂȂ�����ŏ��C���f�b�N�X�ɖ߂�
        }

        return nextIndex;
    }
}
