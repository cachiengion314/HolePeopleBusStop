using UnityEngine;

public struct ISkinnedMeshRendData
{
  public SkinnedMeshRenderer BodyRenderer;
}

public interface ISkinnedMeshRend
{
  public SkinnedMeshRenderer GetBodyRenderer();
  public void SetBodyRenderer(SkinnedMeshRenderer value);
}