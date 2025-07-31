using UnityEngine;

public class HoleEditorControl : MonoBehaviour
{
    [SerializeField] SpriteRenderer bodyRenderer;
    public InitHoleData initHoleData;
    public HoleEditorControlType type;
    public void OnValidate()
    {
        if (bodyRenderer == null) return;
        if (type == HoleEditorControlType.None)
        {
            bodyRenderer.sortingOrder = 0;
            bodyRenderer.color = Color.gray;
        }
        else if (type == HoleEditorControlType.Hole)
        {
            bodyRenderer.sortingOrder = 2;
            bodyRenderer.color = Color.red;
        }
    }
}
public enum HoleEditorControlType
{
    None,
    Hole,
}