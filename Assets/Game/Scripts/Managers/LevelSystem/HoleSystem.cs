using DG.Tweening;
using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  [Header("Hole System")]
  Transform[] _holeTransforms;

  void InitHoleTransform()
  {
    _holeTransforms = new Transform[holeGrid.GridSize.x * holeGrid.GridSize.y];
  }

  void TouchControlling(Collider holeCollider)
  {
    if (passengerGrid.IsPosOutsideAt(holeCollider.transform.position)) return;
    if (!holeCollider.TryGetComponent<IMeshRend>(out var meshRendComp)) return;
    if (DOTween.IsTweening(meshRendComp.GetBodyRenderer().transform)) return;

    meshRendComp.GetBodyRenderer().transform
      .DOScale(1.3f, .15f).SetLoops(2, LoopType.Yoyo);
  }
}