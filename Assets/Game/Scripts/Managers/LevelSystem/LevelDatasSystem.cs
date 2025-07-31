using UnityEngine;
using System.Collections.Generic;

public partial class LevelSystem : MonoBehaviour
{
  /// <summary>
  /// Data static dictionaries
  /// </summary>
  public static Dictionary<int, ColorValueData> ColorValueDatas { get; private set; }
  public static Dictionary<int, IMeshRendData> MeshRendDatas { get; private set; }
  public static Dictionary<int, ISkinnedMeshRendData> SkinnedMeshRendDatas { get; private set; }
  public static Dictionary<int, DirectionValueData> DirectionValueDatas { get; private set; }
  public static Dictionary<int, PassengerArrayData> PassengerArrayDatas { get; private set; }

  void InitEntitiesDataBuffers()
  {
    SkinnedMeshRendDatas = new();
    ColorValueDatas = new();
    MeshRendDatas = new();
    DirectionValueDatas = new();
    PassengerArrayDatas = new();
  }
}