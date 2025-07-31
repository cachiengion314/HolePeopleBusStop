using UnityEngine;
using System.Collections.Generic;

public partial class LevelSystem : MonoBehaviour
{
  /// <summary>
  /// non-static datas for a better debugging
  /// </summary>
  Transform[] _holeTransforms;
  Transform[] _passengerTransforms;
  /// <summary>
  /// static dictionaries for a better structure of datas
  /// </summary>
  public static Dictionary<int, ColorValueData> ColorValueDatas { get; private set; }
  public static Dictionary<int, IMeshRendData> MeshRendDatas { get; private set; }
  public static Dictionary<int, ISkinnedMeshRendData> SkinnedMeshRendDatas
  {
    get; private set;
  }

  void InitEntitiesDataBuffers(LevelInformation levelInformation)
  {
    _holeTransforms = new Transform[
      holeGrid.GridSize.x * holeGrid.GridSize.y
    ];
    _passengerTransforms
      = new Transform[levelInformation.GridSize.x * levelInformation.GridSize.y];
    SkinnedMeshRendDatas = new();
    ColorValueDatas = new();
    MeshRendDatas = new();
  }
}