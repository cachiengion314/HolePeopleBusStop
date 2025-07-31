using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  [SerializeField] Transform queueSlotsPosParent;

  Transform FindFirstSpareQueueSlot()
  {
    return _queueSlotTransforms[0].transform;
  }
}