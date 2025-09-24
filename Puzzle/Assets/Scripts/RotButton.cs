using UnityEngine;
using UnityEngine.EventSystems;

public class RotButton : MonoBehaviour, IPointerClickHandler
{
    [Header("âÒì]Ç≥ÇπÇΩÇ¢é≤")]
    [SerializeField] private Axis axis = Axis.Y;

    [Header("âÒì]Ç≥ÇπÇÈäpìx (ó·: +90, -90)")]
    [SerializeField] private float rotationValue = 90f;

    public enum Axis { X, Y, Z }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (axis)
        {
            case Axis.X:
                SingletonData.Instance.AddRotX(rotationValue);
                break;
            case Axis.Y:
                SingletonData.Instance.AddRotY(rotationValue);
                break;
            case Axis.Z:
                SingletonData.Instance.AddRotZ(rotationValue);
                break;
        }

        Debug.Log($"{gameObject.name} ÇÉNÉäÉbÉN Å® åªç›ÇÃâÒì]íl: {SingletonData.Instance.ROT}");
    }
}
