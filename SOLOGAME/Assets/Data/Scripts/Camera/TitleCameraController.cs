using UnityEngine;
using System.Collections;

public class TitleCameraController : MonoBehaviour
{
    public Transform[] cameraPositions;  // カメラが移動する位置を格納する配列
    public float transitionSpeed = 2f;   // カメラの移動速度（遷移速度）
    private int currentPositionIndex = 0; // 現在のカメラ位置インデックス
    private bool isTransitioning = false; // 遷移中かどうか

    void Update()
    {
        // 遷移中でない場合に、次の位置にカメラを移動
        if (!isTransitioning)
        {
            if (currentPositionIndex < cameraPositions.Length - 1)
            {
                StartCoroutine(SmoothTransition(cameraPositions[currentPositionIndex + 1]));
                currentPositionIndex++;
            }
            else
            {
                // 最後の位置まで来たら最初に戻る
                StartCoroutine(SmoothTransition(cameraPositions[0]));
                currentPositionIndex = 0;
            }
        }
    }

    // カメラのスムーズな移動
    IEnumerator SmoothTransition(Transform targetPosition)
    {
        isTransitioning = true;

        // 現在の位置と回転からターゲット位置まで移動
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        Vector3 targetPos = targetPosition.position;
        Quaternion targetRot = targetPosition.rotation;

        float timeElapsed = 0f;
        while (timeElapsed < 1f)
        {
            // Lerpでスムーズに位置と回転を補間
            transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed);
            transform.rotation = Quaternion.Lerp(startRot, targetRot, timeElapsed);

            timeElapsed += Time.deltaTime * transitionSpeed;
            yield return null;
        }

        // 最終的に目標位置に到達
        transform.position = targetPos;
        transform.rotation = targetRot;

        isTransitioning = false;
    }
}
