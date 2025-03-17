using UnityEngine;
using System.Collections;

public class TitleCameraController : MonoBehaviour
{
    public Transform[] cameraPositions;  // �J�������ړ�����ʒu���i�[����z��
    public float transitionSpeed = 2f;   // �J�����̈ړ����x�i�J�ڑ��x�j
    private int currentPositionIndex = 0; // ���݂̃J�����ʒu�C���f�b�N�X
    private bool isTransitioning = false; // �J�ڒ����ǂ���

    void Update()
    {
        // �J�ڒ��łȂ��ꍇ�ɁA���̈ʒu�ɃJ�������ړ�
        if (!isTransitioning)
        {
            if (currentPositionIndex < cameraPositions.Length - 1)
            {
                StartCoroutine(SmoothTransition(cameraPositions[currentPositionIndex + 1]));
                currentPositionIndex++;
            }
            else
            {
                // �Ō�̈ʒu�܂ŗ�����ŏ��ɖ߂�
                StartCoroutine(SmoothTransition(cameraPositions[0]));
                currentPositionIndex = 0;
            }
        }
    }

    // �J�����̃X���[�Y�Ȉړ�
    IEnumerator SmoothTransition(Transform targetPosition)
    {
        isTransitioning = true;

        // ���݂̈ʒu�Ɖ�]����^�[�Q�b�g�ʒu�܂ňړ�
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        Vector3 targetPos = targetPosition.position;
        Quaternion targetRot = targetPosition.rotation;

        float timeElapsed = 0f;
        while (timeElapsed < 1f)
        {
            // Lerp�ŃX���[�Y�Ɉʒu�Ɖ�]����
            transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed);
            transform.rotation = Quaternion.Lerp(startRot, targetRot, timeElapsed);

            timeElapsed += Time.deltaTime * transitionSpeed;
            yield return null;
        }

        // �ŏI�I�ɖڕW�ʒu�ɓ��B
        transform.position = targetPos;
        transform.rotation = targetRot;

        isTransitioning = false;
    }
}
