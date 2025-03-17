using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using System.Reflection;
using System;
using UnityEngine.UI;
public partial class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // �{�^���̎�ނ�ݒ�
    // �^�C�g���V�[���ł̃{�^��
    enum TitleButton
    {
        ROOMCREATE, // �����쐬
        ROOMJOIN,   // �����Q��
        OPENOPTION, // �ݒ���J��
        GAMEEND,    // �Q�[���I��
        None
    }
    // ���j���[�ł̃{�^��
    enum MenuButton
    {
        RETURNGAME, // �Q�[���ɖ߂�
        OPENOPTION, // �ݒ���J��
        EXIT,       // ��������ޏo
        GAMEEND,    // �Q�[���I��
        None
    }
    // �ݒ荀�ڂɎg���{�^��
    enum LRButton
    { 
        /*FPS�l�̃{�^��*/
        FPS_NUM_L,  // FPS�̍��{�^��
        FPS_NUM_R,  // FPS�̉E�{�^��
        /*FPS�l�̕\���̃{�^��*/
        FPS_MODE_L, // FPS�̍��{�^��
        FPS_MODE_R, // FPS�̉E�{�^��
        None
    }

    // ��{�I�ɂ�None�Őݒ肵�Ďg���Ƃ��ɕύX���ނ݂̂ɐݒ肷�邱��
    [Header("�{�^���̎�ޑI��"), SerializeField] TitleButton titleButton=TitleButton.None;
    [Header("�{�^���̎�ޑI��"), SerializeField] MenuButton  menuButton=MenuButton.None;
    [Header("�{�^���̎�ޑI��"), SerializeField] LRButton lrButton = LRButton.None;

    // �A�j���[�V�����̐ݒ�
    [SerializeField] Animator anim;

    // �G�ꂽ�Ƃ��ɕ�����₷�����邽�߂ɕ\������
    [Header("�I�𒆂ɕ\������I�u�W�F�N�g"), SerializeField]
    GameObject nowSelectObj;


    /*���̂ɂ���Ďg��*/
    // ���j���[�I�u�W�F�N�g
    GameObject Menu;
    // FPS�̕\�������Ă���e�L�X�g
    Text FpsText;
    Text FpsModeText;
    FPSManager fpsManager;


    // �v���C���[���⃋�[��ID��ݒ�
    private string playerName = "Player1";  // ��Ƃ��ăv���C���[����ݒ�
    private string roomId = "Room001";  // ��Ƃ��ă��[��ID��ݒ�

    private string gasUrl = "https://script.google.com/macros/s/AKfycbzZSj5g4eufHFVx8lHrUUXctVeykzFU4Bao1S1PSuI7drKRvcw243y7pkjdzbMKBYITaA/exec";  // GAS��URL

    #region OPTIONSTRING
    string[] SIZEMODE = { "FullScreen","Window"};
    #endregion

    // UI�ڐG�̃C���^�[�t�F�[�X����
    #region InterFace
    public void OnPointerEnter(PointerEventData eventData)
    {
        // �\��
        nowSelectObj.SetActive(true);
        if (anim != null)
        {
            anim.Play("Button_0");
        }
        Debug.Log("UI�ɐG�ꂽ");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ��\��
        nowSelectObj.SetActive(false);
        if (anim != null)
        {
            anim.Play("IdleUI");
        }
        Debug.Log("UI���痣�ꂽ");
    }
    #endregion

    void Start()
    {
        if (menuButton == MenuButton.OPENOPTION)
        {
            Menu = GameObject.Find("Menu");
        }
        // FPS�̒l��\������I�v�V�����̃e�L�X�g
        if(lrButton!=LRButton.None)
        {
            fpsManager=GameObject.Find("SystemManager").GetComponent<FPSManager>();
            FpsText = GameObject.Find("NowFPSnum").GetComponent<Text>();
            FpsModeText = GameObject.Find("NowFPSMode").GetComponent<Text>();

        }
    }
    public void UpdateBUTTON()
    {
        // �G�ꂽ��Ԃō��N���b�N���������珈��
        if (nowSelectObj.activeSelf)
        {
            if (titleButton != TitleButton.None)
            {
                // Enum�̒l���ƂɑΉ�����֐������s
                InvokeMatchingMethod(titleButton);
            }
            else if(menuButton != MenuButton.None)
            {
                // Enum�̒l���ƂɑΉ�����֐������s
                InvokeMatchingMethod(menuButton);
            }
            else if (lrButton != LRButton.None)
            {
                // Enum�̒l���ƂɑΉ�����֐������s
                InvokeMatchingMethod(lrButton);
            }
        }
    }

    #region MatchingMethod
    private void InvokeMatchingMethod(TitleButton kButton)
    {
        string methodName = kButton.ToString(); // Enum�̖��O�𕶎���

        // ���\�b�h�����擾
        MethodInfo method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (method != null)
        {
            method.Invoke(this, null);
        }
        else
        {
            Debug.LogWarning($"���\�b�h '{methodName}' ��������܂���B");
        }
    }
    private void InvokeMatchingMethod(LRButton kButton)
    {
        string methodName = kButton.ToString(); // Enum�̖��O�𕶎���

        // ���\�b�h�����擾
        MethodInfo method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (method != null)
        {
            method.Invoke(this, null);
        }
        else
        {
            Debug.LogWarning($"���\�b�h '{methodName}' ��������܂���B");
        }
    }
    private void InvokeMatchingMethod(MenuButton kButton)
    {
        string methodName = kButton.ToString(); // Enum�̖��O�𕶎���

        // ���\�b�h�����擾
        MethodInfo method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (method != null)
        {
            method.Invoke(this, null);
        }
        else
        {
            Debug.LogWarning($"���\�b�h '{methodName}' ��������܂���B");
        }
    }
    #endregion

    // GAS�Ƀf�[�^�𑗐M����R���[�`��
    private IEnumerator SendDataToGAS(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            // ���N�G�X�g�̌��ʂ��`�F�b�N
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("GAS�Ƀf�[�^�𑗐M���܂���: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("GAS�ւ̃f�[�^���M�Ɏ��s���܂���: " + request.error);
            }
        }
    }
}
