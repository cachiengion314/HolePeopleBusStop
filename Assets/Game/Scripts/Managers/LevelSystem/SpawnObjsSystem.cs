using Unity.Mathematics;
using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  [SerializeField] Transform spawnedParent;
  [SerializeField] GameObject passengerPref;
  [SerializeField] GameObject holePref;
  [SerializeField] GameObject concreteBarrierPref;
  [SerializeField] GameObject tunnelPref;
  [SerializeField] GameObject queueSlotPref;

  Transform SpawnPassengerAt(int index, Transform parent)
  {
    var obj = Instantiate(passengerPref, parent);
    var pos = passengerGrid.ConvertIndexToWorldPos(index);
    obj.transform.position = pos;

    return obj.transform;
  }

  Transform SpawnConcreteBarrierAt(int index, Transform parent)
  {
    var obj = Instantiate(concreteBarrierPref, parent);
    var pos = obstacleGrid.ConvertIndexToWorldPos(index);
    obj.transform.position = pos;

    return obj.transform;
  }

  Transform SpawnTunnelAt(int index, int2 direction, Transform parent)
  {
    var obj = Instantiate(tunnelPref, parent);
    var pos = obstacleGrid.ConvertIndexToWorldPos(index);
    obj.transform.position = pos;
    obj.transform.rotation = GetAngleFromDirection(direction);
    return obj.transform;
  }

  Quaternion GetAngleFromDirection(int2 direction)
  {
    if (direction.x == 0 && direction.y == 1) return Quaternion.Euler(0, 0, 0);
    if (direction.x == 1 && direction.y == 0) return Quaternion.Euler(0, 0, -90);
    if (direction.x == 0 && direction.y == -1) return Quaternion.Euler(0, 0, -180);
    if (direction.x == -1 && direction.y == 0) return Quaternion.Euler(0, 0, 90);

    return Quaternion.identity; // Default case
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