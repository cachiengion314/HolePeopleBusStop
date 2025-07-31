using Unity.Mathematics;
using UnityEngine;

public class PassengerEditorControl : MonoBehaviour
{
    [SerializeField] ThemeObj themeObj;
    [SerializeField] SpriteRenderer bodyRenderer;
    public GroupPassengerData groupPassengerData;
    public ConcreteBarrierData concreteBarrierData;
    public TunnelData tunnelData;
    public PassengerEditorControlType type;
    public void OnValidate()
    {
        if (bodyRenderer == null) return;
        if (type == PassengerEditorControlType.None)
            bodyRenderer.color = Color.white;
        else if (type == PassengerEditorControlType.Passenger)
            bodyRenderer.color = themeObj.colorValues[groupPassengerData.Value];
        else if (type == PassengerEditorControlType.ConcreteBarrier)
            bodyRenderer.color = Color.black;
        else if (type == PassengerEditorControlType.Tunnel)
            bodyRenderer.color = Color.gray;
    }
}

public enum PassengerEditorControlType
{
    None,
    Passenger,
    ConcreteBarrier,
    Tunnel
}
