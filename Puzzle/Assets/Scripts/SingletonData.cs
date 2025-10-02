using UnityEngine;

public class SingletonData : MonoBehaviour
{
    public static SingletonData Instance { get; private set; }

    private float rotX = 0;
    private float rotY = 0;
    private float rotZ = 0;

    public Vector3 ROT => new Vector3(rotX, rotY, rotZ);

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddRotX(float value) => rotX = (rotX + value) % 360f;
    public void AddRotY(float value) => rotY = (rotY + value) % 360f;
    public void AddRotZ(float value) => rotZ = (rotZ + value) % 360f;
}
