using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  [SerializeField] Transform spawnedParent;
  [SerializeField] GameObject passengerPref;

  GameObject SpawnPassengerAt(int index, Transform parent)
  {
    var obj = Instantiate(passengerPref, parent);
    var pos = gridWorld.ConvertIndexToWorldPos(index);
    obj.transform.position = pos;

    return obj;
  }
}