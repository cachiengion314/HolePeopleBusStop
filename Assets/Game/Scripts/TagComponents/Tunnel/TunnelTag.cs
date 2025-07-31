using Unity.Mathematics;
using UnityEngine;

public class TunnelControl : MonoBehaviour, IDirection, IPassengerArray
{
    public int2 GetDirectionValue()
    {
        return LevelSystem.DirectionValueDatas[transform.GetInstanceID()].DirectionValue;
    }

    public GameObject[] GetPassengers()
    {
        return LevelSystem.PassengerArrayDatas[transform.GetInstanceID()].Passengers;
    }
    public void SetDirectionValue(int2 value)
    {
        var instanceID = transform.GetInstanceID();
        if (!LevelSystem.DirectionValueDatas.ContainsKey(instanceID))
            LevelSystem.DirectionValueDatas.Add(instanceID, new DirectionValueData() { DirectionValue = value });
            
        var data = LevelSystem.DirectionValueDatas[instanceID];
        data.DirectionValue = value;
        LevelSystem.DirectionValueDatas[instanceID] = data;
        // Additional logic to handle direction change can be added here
    }

    public void SetPassengers(GameObject[] passengers)
    {
        var instanceID = transform.GetInstanceID();
        if (!LevelSystem.PassengerArrayDatas.ContainsKey(instanceID))
            LevelSystem.PassengerArrayDatas.Add(instanceID, new PassengerArrayData() { Passengers = passengers });

        var data = LevelSystem.PassengerArrayDatas[instanceID];
        data.Passengers = passengers;
        LevelSystem.PassengerArrayDatas[instanceID] = data;

        // Additional logic to handle passenger updates can be added here
    }
}
