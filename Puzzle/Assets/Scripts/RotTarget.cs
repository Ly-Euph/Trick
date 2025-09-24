using UnityEngine;

public class RotTarget : MonoBehaviour
{
    // 回転速度（度/秒）
    [SerializeField] private float rotateSpeed = 360f;

    void Update()
    {
        // Singletonから目標回転を取得
        Quaternion targetRot = Quaternion.Euler(SingletonData.Instance.ROT);

        // 現在の回転から目標回転へ徐々に補間
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            rotateSpeed * Time.deltaTime
        );
    }
}
