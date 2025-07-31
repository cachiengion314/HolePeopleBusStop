using UnityEngine;

public class HoleEditorControl : MonoBehaviour
{
    [SerializeField] ThemeObj themeObj;
    [SerializeField] SpriteRenderer bodyRenderer;
    public InitHoleData initHoleData;
    public HoleEditorControlType type;
    public void OnValidate()
    {
        if (bodyRenderer == null) return;
        if (type == HoleEditorControlType.None)
        {
            bodyRenderer.sortingOrder = 0;
            bodyRenderer.color = Color.beige;
        }
        else if (type == HoleEditorControlType.Hole)
        {
            bodyRenderer.sortingOrder = 2;
            bodyRenderer.color = themeObj.colorValues[initHoleData.Value];
        }
    }
}
public enum HoleEditorControlType
{
    None,
    Hole,
}