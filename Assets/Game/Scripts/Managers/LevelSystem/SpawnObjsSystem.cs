using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  [SerializeField] Transform spawnedParent;
  [SerializeField] GameObject passengerPref;
  [SerializeField] GameObject holePref;

  GameObject SpawnPassengerAt(int index, Transform parent)
  {
    var obj = Instantiate(passengerPref, parent);
    var pos = passengerGrid.ConvertIndexToWorldPos(index);
    obj.transform.position = pos;

    return obj;
  }

  GameObject SpawnHoleAt(int index, Transform parent)
  {
    var obj = Instantiate(holePref, parent);
    var pos = holeGrid.ConvertIndexToWorldPos(index);
    obj.transform.position = pos;

    return obj;
  }
}