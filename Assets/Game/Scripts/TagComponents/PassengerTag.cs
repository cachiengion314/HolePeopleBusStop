using UnityEngine;

public class PassengerTag : MonoBehaviour
  , IColorValue
  , ISkinnedMeshRend
{
  public SkinnedMeshRenderer GetBodyRenderer()
  {
    return LevelSystem.SkinnedMeshRendDatas[transform.GetInstanceID()].BodyRenderer;
  }

  public void SetBodyRenderer(SkinnedMeshRenderer value)
  {
    var instanceID = transform.GetInstanceID();
    if (!LevelSystem.SkinnedMeshRendDatas.TryGetValue(instanceID, out var data)) return;

    data.BodyRenderer = value;
    LevelSystem.SkinnedMeshRendDatas[instanceID] = data;
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
    GetBodyRenderer().material.SetColor("_BaseColor", color);
  }
}