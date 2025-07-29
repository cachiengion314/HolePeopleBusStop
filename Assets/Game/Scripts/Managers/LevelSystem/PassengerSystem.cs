using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  [Header("Passengers System")]
  GameObject[] _passengers;

  void InitPassengers()
  {
    _passengers = new GameObject[gridWorld.GridSize.x * gridWorld.GridSize.y];
  }
}