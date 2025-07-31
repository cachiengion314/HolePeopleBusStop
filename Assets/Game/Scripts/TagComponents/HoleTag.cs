using UnityEngine;

public class HoleTag : MonoBehaviour
  , IColorValue
  , IMeshRend
{
  public MeshRenderer GetBodyRenderer()
  {
    return LevelSystem.MeshRendDatas[transform.GetInstanceID()].BodyRenderer;
  }

  public void SetBodyRenderer(MeshRenderer value)
  {
    var instanceID = transform.GetInstanceID();
    if (!LevelSystem.MeshRendDatas.TryGetValue(instanceID, out var data)) return;

    data.BodyRenderer = value;
    LevelSystem.MeshRendDatas[instanceID] = data;
  }

  public int GetColorValue()
  {
    return LevelSystem.ColorValueDatas[transform.GetInstanceID()].ColorValue;
  }

  public void SetColorValue(int value)
  {
    var instanceID = transform.GetInstanceID();
    if (!LevelSystem.ColorValueDatas.TryGetValue(instanceID, out var data)) return;

    data.ColorValue = value;
    LevelSystem.ColorValueDatas[instanceID] = data;

    var color = RendererSystem.Instance.GetColorBy(value);
    if (value == -1) color = Color.white;
    GetBodyRenderer().material.SetColor("_Color", color);
  }
}