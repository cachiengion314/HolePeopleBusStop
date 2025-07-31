using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class QueueSlotTag : MonoBehaviour
  , IColorValue
  , IMeshRend
  , IPassengerList
{
  public MeshRenderer GetBodyRenderer()
  {
    return LevelSystem.MeshRendDatas[transform.GetInstanceID()].BodyRenderer;
  }

  public void SetBodyRenderer(MeshRenderer value)
  {
    var instanceID = transform.GetInstanceID();
    if (!LevelSystem.MeshRendDatas.TryGetValue(instanceID, out var data)) return;

    data.BodyRenderer = value;
    LevelSystem.MeshRendDatas[instanceID] = data;
  }

  public int GetColorValue()
  {
    return LevelSystem.ColorValueDatas[transform.GetInstanceID()].ColorValue;
  }

  public void SetColorValue(int value)
  {
    var instanceID = transform.GetInstanceID();
    if (!LevelSystem.ColorValueDatas.TryGetValue(instanceID, out var data)) return;

    data.ColorValue = value;
    LevelSystem.ColorValueDatas[instanceID] = data;

    var color = RendererSystem.Instance.GetColorBy(value);
    if (value == -1) color = Color.white;
    GetBodyRenderer().material.SetColor("_Color", color);
  }

  public List<Transform> GetPassengers()
  {
    return LevelSystem.PassengerListDatas[transform.GetInstanceID()].Passengers;
  }

  public void SetPassengers(List<Transform> passengers)
  {
    var instanceID = transform.GetInstanceID();

    var data = LevelSystem.PassengerListDatas[instanceID];
    data.Passengers = passengers;
    LevelSystem.PassengerListDatas[instanceID] = data;
  }

  public void AddOnePassenger(Transform passenger)
  {
    var instanceID = transform.GetInstanceID();

    var data = LevelSystem.PassengerListDatas[instanceID];
    data.Passengers.Add(passenger);
    LevelSystem.PassengerListDatas[instanceID] = data;
  }

  public void RemoveOnePassenger(Transform passenger)
  {
    var instanceID = transform.GetInstanceID();
    var data = LevelSystem.PassengerListDatas[instanceID];
    data.Passengers.Remove(passenger);
    LevelSystem.PassengerListDatas[instanceID] = data;
  }

  public float3 GetSlotPositionAt(int slotIndex)
  {
    var slotPositions = LevelSystem.PassengerListDatas[transform.GetInstanceID()].SlotPositions;
    if (slotIndex > slotPositions.Length - 1 || slotIndex < 0) return float3.zero;
    return slotPositions[slotIndex];
  }

  public void SetSlotPositions(float3[] slotPositions)
  {
    var instanceID = transform.GetInstanceID();

    var data = LevelSystem.PassengerListDatas[instanceID];
    data.SlotPositions = slotPositions;
    LevelSystem.PassengerListDatas[instanceID] = data;
  }
}