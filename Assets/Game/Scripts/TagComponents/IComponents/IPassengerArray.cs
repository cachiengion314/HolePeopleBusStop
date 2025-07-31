using UnityEngine;

public struct PassengerArrayData
{
  public Transform[] Passengers;
}

public interface IPassengerArray
{
  public Transform[] GetPassengers();
  public void SetPassengers(Transform[] passengers);
}