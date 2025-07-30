using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  [Header("Hole System")]
  Transform[] _holeTransforms;

  void InitHoleTransform()
  {
    _holeTransforms = new Transform[holeGrid.GridSize.x * holeGrid.GridSize.y];
  }
}