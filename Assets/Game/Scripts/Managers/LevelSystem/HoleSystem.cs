using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  [Header("Hole System")]
  GameObject[] _holes;

  void InitHoles()
  {
    _holes = new GameObject[holeGrid.GridSize.x * holeGrid.GridSize.y];
  }
}