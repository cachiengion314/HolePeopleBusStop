using UnityEngine;

public class HoleEditorControl : MonoBehaviour
{
    [SerializeField] SpriteRenderer bodyRenderer;
    [SerializeField] InitHoleData initHoleData;
    [SerializeField] HoleEditorControlType type;
    void OnValidate()
    {
        if (bodyRenderer == null) return;
        if (type == HoleEditorControlType.None)
            bodyRenderer.color = Color.gray;
        else if (type == HoleEditorControlType.Hole)
            bodyRenderer.color = Color.red;
    }
}
public enum HoleEditorControlType
{
    None,
    Hole,
}