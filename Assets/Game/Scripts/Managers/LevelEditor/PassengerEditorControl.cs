using Unity.Mathematics;
using UnityEngine;

public class PassengerEditorControl : MonoBehaviour
{
    [SerializeField] SpriteRenderer bodyRenderer;
    [SerializeField] GroupPassengerData groupPassengerData;
    [SerializeField] PassengerEditorControlType type;
    void OnValidate()
    {
        if (bodyRenderer == null) return;
        if (type == PassengerEditorControlType.None)
            bodyRenderer.color = Color.white;
        else if (type == PassengerEditorControlType.Passenger)
            bodyRenderer.color = Color.green;
    }
}

public enum PassengerEditorControlType
{
    None,
    Passenger,
}
