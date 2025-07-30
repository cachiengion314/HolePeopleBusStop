using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  [Header("Touch Control System")]
  bool _isUserScreenTouching;
  public bool IsUserScreenTouching { get { return _isUserScreenTouching; } }

  void SubscribeTouchEvent()
  {
    LeanTouch.OnFingerDown += OnFingerDown;
    LeanTouch.OnGesture += OnGesture;
    LeanTouch.OnFingerInactive += OnFingerInactive;
  }

  void UnsubscribeTouchEvent()
  {
    LeanTouch.OnFingerDown -= OnFingerDown;
    LeanTouch.OnGesture -= OnGesture;
    LeanTouch.OnFingerInactive -= OnFingerInactive;
  }

  private void OnFingerDown(LeanFinger finger)
  {
    _isUserScreenTouching = true;

    if (GameManager.Instance.GetGameState() != GameState.Gameplay) return;
    Vector2 startTouchPos = Camera.main.ScreenToWorldPoint(finger.ScreenPosition);
    // Collider2D[] colliders = Physics.OverlapSphereNonAlloc(startTouchPos);

    print("finger " + finger);
  }

  void OnGesture(List<LeanFinger> list)
  {
    _isUserScreenTouching = true;
  }

  private void OnFingerInactive(LeanFinger finger)
  {
    _isUserScreenTouching = false;
  }
}