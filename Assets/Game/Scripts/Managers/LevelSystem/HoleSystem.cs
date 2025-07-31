using DG.Tweening;
using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  [Header("Hole System")]
  [Range(2, 64)]
  [SerializeField] int holeConsumeCapacity = 32;

  void TouchControlling(Collider holeCollider)
  {
    if (passengerGrid.IsPosOutsideAt(holeCollider.transform.position)) return;
    if (!holeCollider.TryGetComponent<IMeshRend>(out var meshRendComp)) return;
    if (DOTween.IsTweening(meshRendComp.GetBodyRenderer().transform)) return;

    var originalScale = meshRendComp.GetBodyRenderer().transform.localScale;
    meshRendComp.GetBodyRenderer().transform
      .DOScale(1.15f * originalScale, .1f)
      .SetLoops(2, LoopType.Yoyo);

    if (!holeCollider.TryGetComponent<IColorValue>(out var colorComp)) return;

    // the hole should consume all of people that matched its color
    var matchedPassengers = FindPassengersMatching(colorComp.GetColorValue());
    var targetPos = holeCollider.transform.position;

    for (int i = 0; i < matchedPassengers.Count; ++i)
    {
      var passenger = matchedPassengers[i];
      var index = passengerGrid.ConvertWorldPosToIndex(passenger.position);
      _passengerTransforms[index] = null;

      passenger.transform
        .DOMove(targetPos, .5f)
        .OnComplete(() =>
        {
          // fill passengers to queue slots
          var queueSlot = FindFirstSpareQueueSlot();
          if (!queueSlot.TryGetComponent<IPassengerList>(out var passengerList)) return;
          passengerList.AddOnePassenger(passenger);
          var pos = passengerList.GetSlotPositionAt(passengerList.GetPassengers().Count - 1);
          if (pos.Equals(0)) return;
          passenger.transform.position = pos;
        });
    }
  }
}