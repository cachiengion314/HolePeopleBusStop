using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  [Header("Passengers System")]
  Transform[] _passengerTransforms;

  void InitPassengerTransforms()
  {
    _passengerTransforms
      = new Transform[passengerGrid.GridSize.x * passengerGrid.GridSize.y];
  }
}