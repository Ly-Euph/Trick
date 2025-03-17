using MiscUtil.Linq.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class UIButton
{
    // ���[���쐬�{�^��
    private void ROOMCREATE()
    {
        playerName = "NEKO";
        string url = gasUrl + "?action=createRoom"
                           + "&roomId=" + Uri.EscapeDataString(roomId)
                           + "&player1=" + Uri.EscapeDataString(playerName); Debug.Log("Sending request to GAS with URL: " + url);  // ������URL���m�F
        StartCoroutine(SendDataToGAS(url));
    }

    // ���[���Q���{�^���̏���
    private void ROOMJOIN()
    {
        playerName = "SAME";
        string url = gasUrl + "?action=joinRoom"
                           + "&roomId=" + Uri.EscapeDataString(roomId)
                           + "&player2=" + Uri.EscapeDataString(playerName);
        StartCoroutine(SendDataToGAS(url));
    }

    // �ݒ���J��
    private void OPENOPTION()
    {
        Debug.Log("�I�v�V������ʂ��J��");
    }

    // �Q�[���I��
    private void GAMEEND()
    {
        Application.Quit();

        // �G�f�B�^�ł�����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // �Q�[���ɖ߂�
    private void RETURNGAME()
    {
        Menu.SetActive(false);
    }


    #region OPTION
    /*L�͍��{�^��R�͉E�{�^�����Ӗ�*/
    // FPS�̒l�ύX
    private void FPS_NUM_L()
    {
        fpsManager.FPS_NUM_L(); // �C���f�b�N�X�v�Z
        FpsText.text = fpsManager.UpdateFPSDisplay(); // ���f
    }
    private void FPS_NUM_R()
    {
        fpsManager.FPS_NUM_R(); // �C���f�b�N�X�v�Z
        FpsText.text = fpsManager.UpdateFPSDisplay(); // ���f
    }
    // �\���؂�ւ�
    private void FPS_MODE_L()
    {
        fpsManager.FPS_MDOE_L(); // �C���f�b�N�X�v�Z
        FpsModeText.text = fpsManager.UpdateMODEDisplay(); // ���f
    }
    private void FPS_MODE_R()
    {
        fpsManager.FPS_MODE_R(); // �C���f�b�N�X�v�Z
        FpsModeText.text = fpsManager.UpdateMODEDisplay(); // ���f
    }

    #endregion
}