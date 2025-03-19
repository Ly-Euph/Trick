using UnityEngine;
using UnityEngine.UI;
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

    // FPS�̕\�������Ă���e�L�X�g
    [SerializeField] Text FpsText;     // �ݒ��ʂ̕���
    [SerializeField] Text FpsModeText; // �ݒ��ʂ̕���
    [SerializeField] Text NowFPSText;  // FPS�l��\������
    // FPS�̕��ς����߂ĕ\������
    private float deltaTime = 0.0f;

    private void OnEnable()
    {
        if (Index != null) { return; }
        Index = new IndexFunc();
    }
    public void FPS_NUM_L()
    {
        // �C���f�b�N�X�����炵�āA�͈͓��Ɏ��߂�
        currentIndex_FPS = Index.GetCyclicIndex(currentIndex_FPS, -1, FPS.Length);
        FpsText.text = UpdateFPSDisplay();
    }
    public void FPS_NUM_R()
    {
        // �C���f�b�N�X�𑝂₵�āA�͈͓��Ɏ��߂�
        currentIndex_FPS = Index.GetCyclicIndex(currentIndex_FPS, 1, FPS.Length);
        FpsText.text = UpdateFPSDisplay();
    }
    public void FPS_MDOE_L()
    {
        // �C���f�b�N�X�����炵�āA�͈͓��Ɏ��߂�
        currentIndex_MODE = Index.GetCyclicIndex(currentIndex_MODE, -1, FPSMODE.Length);
        FpsModeText.text = UpdateMODEDisplay();
    }
    public void FPS_MODE_R()
    {
        // �C���f�b�N�X�𑝂₵�āA�͈͓��Ɏ��߂�
        currentIndex_MODE = Index.GetCyclicIndex(currentIndex_MODE, 1,FPSMODE.Length);
        FpsModeText.text = UpdateMODEDisplay();
    }

    /// <summary>
    /// FPS�̐��l�ؑ�
    /// </summary>
    /// <returns>FPS�̐��l</returns>
    private string UpdateFPSDisplay()
    {
        // ���݂�FPS��Text�ɕ\��
        return FPS[currentIndex_FPS];
    }
    /// <summary>
    /// FPS�\���ؑ�
    /// </summary>
    /// <returns>�ؑւ̗L��</returns>
    private string UpdateMODEDisplay()
    {
        // ���݂�FPS��Text�ɕ\��
        return FPSMODE[currentIndex_MODE];
    }

    /// <summary>
    /// �f�[�^�̃��[�h
    /// </summary>
    public void LOAD()
    {
        // �ǂݍ���
        currentIndex_FPS = PlayerPrefs.GetInt("currentIndex_FPS", 1);
        currentIndex_MODE = PlayerPrefs.GetInt("currentIndex_MODE", 0);
        // ���f������
        FpsText.text = UpdateFPSDisplay();
        FpsModeText.text = UpdateMODEDisplay();

        // FPS�̐ݒ�
        Application.targetFrameRate = int.Parse(FPS[PlayerPrefs.GetInt("currentIndex_FPS", 0)]);
    }

    /// <summary>
    /// �ݒ芮���Ɠ����ɕ��@���Ă���
    /// </summary>
    public void SAVE()
    {
        PlayerPrefs.SetInt("currentIndex_FPS",currentIndex_FPS);
        PlayerPrefs.SetInt("currentIndex_MODE", currentIndex_MODE);
        PlayerPrefs.Save(); // �ۑ�

        // ��\���Ȃ�e�L�X�g�͋󔒂ɂ��Ă���
        if (PlayerPrefs.GetInt("currentIndex_MODE", 0) == 0)
        {
            // ��
            NowFPSText.text = "";
        }

        // FPS�̐ݒ�
        Application.targetFrameRate = int.Parse(FPS[PlayerPrefs.GetInt("currentIndex_FPS", 0)]);
    }

    /// <summary>
    /// FPS�l���v�Z
    /// </summary>
     public void FPSdelta()
    {
        // �\����Ԃ̎��̂݌v�Z������̂Ŕ�\����Ԃ�return
        if(PlayerPrefs.GetInt("currentIndex_MODE", 0) == 0)
        {
            return;
        }
        else
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f; // �ߋ��̒l�ƕ��ω�
            float fps = 1.0f / deltaTime;
            NowFPSText.text = "FPS:" + fps.ToString("F0");
        }
    }
}
