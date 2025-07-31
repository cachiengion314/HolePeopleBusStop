using UnityEngine;

public struct PassengerArrayData
{
    public GameObject[] Passengers;
}
public interface IPassengerArray
{
    public GameObject[] GetPassengers();
    public void SetPassengers(GameObject[] passengers);
}