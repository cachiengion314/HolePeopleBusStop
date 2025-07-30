using UnityEngine;

public struct IMeshRendData
{
  public MeshRenderer BodyRenderer;
}

public interface IMeshRend
{
  public MeshRenderer GetBodyRenderer();
  public void SetBodyRenderer(MeshRenderer value);
}