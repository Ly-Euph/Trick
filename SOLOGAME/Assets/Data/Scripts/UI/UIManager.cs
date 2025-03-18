using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIButton[] uiButtons; // UIButton�̔z��

    private static UIManager instance; // Singleton�C���X�^���X

    [Header("FPSManager"), SerializeField] FPSManager fps;
    [Header("ScreenSizeManager"), SerializeField] ScreenSizeManager size;

    void Awake()
    {
        // Singleton�p�^�[��: ����UIManager�����ɑ��݂���ꍇ�A���݂̃C���X�^���X���폜
        if (instance != null)
        {
            Destroy(gameObject); // �d�������C���X�^���X���폜
        }
        else
        {
            instance = this; // ����̂݃C���X�^���X��ݒ�
            DontDestroyOnLoad(gameObject); // ���̃I�u�W�F�N�g���V�[�����ύX����Ă��j�����Ȃ�
        }
    }

    void Start()
    {
        // �V�[�����̂��ׂĂ�UIButton������
        uiButtons = FindObjectsOfType<UIButton>(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            // �eUIButton��UpdateBUTTON()���Ăяo��
            foreach (var button in uiButtons)
            {
                button.UpdateButton(); // �{�^����UpdateBUTTON�����s
            }
        }
    }
}
