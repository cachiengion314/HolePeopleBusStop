using Unity.Mathematics;
using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  [SerializeField] Transform spawnedParent;
  [SerializeField] GameObject passengerPref;
  [SerializeField] GameObject holePref;
  [SerializeField] GameObject queueSlotPref;

  Transform SpawnPassengerAt(int index, Transform parent)
  {
    var obj = Instantiate(passengerPref, parent);
    var pos = passengerGrid.ConvertIndexToWorldPos(index);
    obj.transform.position = pos;

    return obj.transform;
  }

  Transform SpawnHoleAt(int index, Transform parent)
  {
    var obj = Instantiate(holePref, parent);
    var pos = holeGrid.ConvertIndexToWorldPos(index);
    obj.transform.position = pos;

    return obj.transform;
  }

  Transform SpawnQueueSlotAt(float3 pos, Transform parent)
  {
    var obj = Instantiate(queueSlotPref, parent);
    obj.transform.position = pos;

    return obj.transform;
  }
}