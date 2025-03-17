using UnityEngine;

public class CrosshairControl : MonoBehaviour
{
    public Texture2D loupeCursor;
    void Start()
    {
        Cursor.SetCursor(loupeCursor, Vector2.zero, CursorMode.Auto);
    }
    private void Update()
    {
        Cursor.SetCursor(loupeCursor, Vector2.zero, CursorMode.Auto);

    }
}