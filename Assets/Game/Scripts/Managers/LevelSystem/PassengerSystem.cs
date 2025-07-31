using System.Collections.Generic;
using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  List<Transform> FindPassengersMatching(int colorValue)
  {
    ///TODO: need more work at here in the future
    var list = new List<Transform>();
    for (int i = 0; i < _passengerTransforms.Length; ++i)
    {
      var obj = _passengerTransforms[i];
      if (obj == null) continue;
      if (!obj.TryGetComponent<IColorValue>(out var colorComp)) continue;
      if (colorComp.GetColorValue() != colorValue) continue;
      if (list.Count == holeConsumeCapacity) break;
      list.Add(obj);
    }
    return list;
  }
}