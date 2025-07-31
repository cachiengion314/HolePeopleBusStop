using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public struct PassengerListData
{
  public List<Transform> Passengers;
  public float3[] SlotPositions;
}

public interface IPassengerList
{
  public List<Transform> GetPassengers();
  public void SetSlotPositions(float3[] slotPositions);
  public void SetPassengers(List<Transform> passengers);
  public void AddOnePassenger(Transform passenger);
  public void RemoveOnePassenger(Transform passenger);
  public float3 GetSlotPositionAt(int slotIndex);
}