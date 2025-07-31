using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class InitHoleData
{
  [ViewOnly] public int Index;
  public int Value;
}

[Serializable]
public class GroupPassengerData
{
  [ViewOnly] public int2 GridRangeX;
  [ViewOnly] public int2 GridRangeY;
  public bool IsHidden;
  public int Value;
}

[Serializable]
public class ConcreteBarrierData
{
  [ViewOnly] public int Index;
}
[Serializable]
public class TunnelData
{
  [ViewOnly] public int Index;
  public int2 direction;
  public int Value;
}

[Serializable]
public class LevelInformation
{
  [ViewOnly] public int Index;
  public InitHoleData[] InitHoleDatas;
  public GroupPassengerData[] GroupPassengerDatas;
  public ConcreteBarrierData[] ConcreteBarrierDatas;
  public TunnelData[] TunnelDatas;
  public float3 GridPosition;
  public int2 GridSize;
  public float2 GridScale;
  public int2 HoleScale;
}

public class LevelEditor : MonoBehaviour
{
  [Header("Level Editor")]
  [SerializeField] PassengerEditorControl passengerEditorPref;
  [SerializeField] HoleEditorControl holeEditorPref;
  [SerializeField] PassengerEditorControl[] passengerEditorInstance;
  [SerializeField] HoleEditorControl[] holeEditorInstance;
  [SerializeField] LevelInformation levelInformation;
  [SerializeField][Range(1, 20)] int levelSelected = 1;
  [SerializeField] GridWorld passengerGrid;
  [SerializeField] GridWorld holeGrid;

  [NaughtyAttributes.Button]
  void CreateGrid()
  {
    var gridSize = levelInformation.GridSize;
    if (gridSize.x % 2 != 0 || gridSize.y % 2 != 0)
    {
      Debug.LogError("Grid size must be even numbers");
      return;
    }
    ClearPassenger();
    ClearHole();

    var passengerGridSize = gridSize / 2;
    passengerGrid.GridSize = passengerGridSize;
    passengerGrid.GridScale = levelInformation.GridScale * 2;
    passengerGrid.InitValue();

    var holeScale = levelInformation.HoleScale;
    var sizeUnitX = (int)math.floor(gridSize.x / holeScale.x);
    var sizeUnitY = (int)math.floor(gridSize.y / holeScale.y);
    var scaleUnitX = levelInformation.GridScale.x * holeScale.x / levelInformation.GridScale.x;
    var scaleUnitY = levelInformation.GridScale.y * holeScale.y / levelInformation.GridScale.y;
    holeGrid.GridSize = new int2(sizeUnitX, sizeUnitY);
    holeGrid.GridScale = new float2(scaleUnitX, scaleUnitY);
    holeGrid.InitValue();

    passengerEditorInstance = new PassengerEditorControl[passengerGridSize.x * passengerGridSize.y];
    for (int i = 0; i < passengerEditorInstance.Length; ++i)
    {
      var instance = Instantiate(passengerEditorPref, passengerGrid.transform);
      var pos = passengerGrid.ConvertIndexToWorldPos(i);
      var scale = passengerGrid.GridScale * 0.9f;
      instance.transform.position = pos;
      instance.transform.localScale = new Vector3(scale.x, scale.y, 1);
      passengerEditorInstance[i] = instance;
    }

    holeEditorInstance = new HoleEditorControl[holeGrid.GridSize.x * holeGrid.GridSize.y];
    for (int i = 0; i < holeEditorInstance.Length; ++i)
    {
      var instance = Instantiate(holeEditorPref, holeGrid.transform);
      var pos = holeGrid.ConvertIndexToWorldPos(i);
      var scale = holeGrid.GridScale * 0.9f;
      instance.transform.position = pos;
      instance.transform.localScale = new Vector3(scale.x, scale.y, 1);
      holeEditorInstance[i] = instance;
    }
  }

  [NaughtyAttributes.Button]
  void Clear()
  {
    levelInformation = new LevelInformation();
    ClearPassenger();
    ClearHole();
  }

  void ClearPassenger()
  {
    for (int i = passengerGrid.transform.childCount - 1; i >= 0; i--)
    {
      var child = passengerGrid.transform.GetChild(i);
      DestroyImmediate(child.gameObject);
    }
  }

  void ClearHole()
  {
    for (int i = holeGrid.transform.childCount - 1; i >= 0; i--)
    {
      var child = holeGrid.transform.GetChild(i);
      DestroyImmediate(child.gameObject);
    }
  }

  [NaughtyAttributes.Button]
  void LoadLevel()
  {
    LoadLevelFrom(levelSelected);
  }

  public void LoadLevelFrom(int level)
  {
    var _rawLevelInfo = Resources.Load<TextAsset>(
      "Levels/" + KeyString.NAME_LEVEL_FILE + level
    ).text;
    var levelInfo = JsonUtility.FromJson<LevelInformation>(_rawLevelInfo);

    if (levelInfo == null) { print("This level is not existed!"); return; }
    levelInformation = levelInfo;
    CreateGrid();
    LoadHoleData();
    LoadPassengerData();
    LoadConcreteBarrierData();
    LoadTunnelData();
    print("Load level successfully");
  }

  void LoadHoleData()
  {
    if (levelInformation.InitHoleDatas == null) return;
    for (int i = 0; i < levelInformation.InitHoleDatas.Length; ++i)
    {
      var holeData = levelInformation.InitHoleDatas[i];
      if (holeData == null) continue;
      if (holeEditorInstance[holeData.Index] == null) continue;
      holeEditorInstance[holeData.Index].initHoleData = holeData;
      holeEditorInstance[holeData.Index].type = HoleEditorControlType.Hole;
      holeEditorInstance[holeData.Index].OnValidate();
    }
  }

  void LoadPassengerData()
  {
    if (levelInformation.GroupPassengerDatas == null) return;
    for (int i = 0; i < levelInformation.GroupPassengerDatas.Length; ++i)
    {
      var passengerData = levelInformation.GroupPassengerDatas[i];
      if (passengerData == null) continue;
      var index = passengerGrid.ConvertGridPosToIndex(
        new int2(passengerData.GridRangeX.x / 2,
        passengerData.GridRangeY.x / 2)
      );
      if (passengerEditorInstance[index] == null) continue;
      passengerEditorInstance[index].groupPassengerData = passengerData;
      passengerEditorInstance[index].type = PassengerEditorControlType.Passenger;
      passengerEditorInstance[index].OnValidate();
    }
  }

  void LoadConcreteBarrierData()
  {
    if (levelInformation.ConcreteBarrierDatas == null) return;
    for (int i = 0; i < levelInformation.ConcreteBarrierDatas.Length; ++i)
    {
      var concreteBarrierData = levelInformation.ConcreteBarrierDatas[i];
      if (concreteBarrierData == null) continue;
      if (passengerEditorInstance[concreteBarrierData.Index] == null) continue;
      passengerEditorInstance[concreteBarrierData.Index].concreteBarrierData = concreteBarrierData;
      passengerEditorInstance[concreteBarrierData.Index].type = PassengerEditorControlType.ConcreteBarrier;
      passengerEditorInstance[concreteBarrierData.Index].OnValidate();
    }
  }

  void LoadTunnelData()
  {
    if (levelInformation.TunnelDatas == null) return;
    for (int i = 0; i < levelInformation.TunnelDatas.Length; ++i)
    {
      var tunnelData = levelInformation.TunnelDatas[i];
      if (tunnelData == null) continue;
      if (passengerEditorInstance[tunnelData.Index] == null) continue;
      passengerEditorInstance[tunnelData.Index].tunnelData = tunnelData;
      passengerEditorInstance[tunnelData.Index].type = PassengerEditorControlType.Tunnel;
      passengerEditorInstance[tunnelData.Index].OnValidate();
    }
  }

  [NaughtyAttributes.Button]
  void SaveLevel()
  {
    levelInformation.Index = levelSelected - 1;
    SaveHoldData();
    SavePassengerData();
    SaveConcreteBarrierData();
    SaveTunnelData();

    HoangNam.SaveSystem.Save(
      levelInformation,
      "Resources/Levels/" + KeyString.NAME_LEVEL_FILE + levelSelected
    );
    print("Save level successfully");
  }

  void SaveHoldData()
  {
    List<InitHoleData> holeDatas = new();
    for (int i = 0; i < holeEditorInstance.Length; ++i)
    {
      var holeEditor = holeEditorInstance[i];
      if (holeEditor == null) continue;
      if (holeEditor.type != HoleEditorControlType.Hole) continue;
      holeEditor.initHoleData.Index = i;
      holeDatas.Add(holeEditor.initHoleData);
    }
    levelInformation.InitHoleDatas = holeDatas.ToArray();
  }

  void SavePassengerData()
  {
    List<GroupPassengerData> groupPassengerDatas = new();
    for (int i = 0; i < passengerEditorInstance.Length; ++i)
    {
      var passengerEditor = passengerEditorInstance[i];
      if (passengerEditor == null) continue;
      if (passengerEditor.type != PassengerEditorControlType.Passenger) continue;
      var gridPos = passengerGrid.ConvertIndexToGridPos(i);
      passengerEditor.groupPassengerData.GridRangeX = new int2(gridPos.x * 2, gridPos.x * 2 + 1);
      passengerEditor.groupPassengerData.GridRangeY = new int2(gridPos.y * 2, gridPos.y * 2 + 1);
      groupPassengerDatas.Add(passengerEditor.groupPassengerData);
    }
    levelInformation.GroupPassengerDatas = groupPassengerDatas.ToArray();
  }

  void SaveConcreteBarrierData()
  {
    List<ConcreteBarrierData> concreteBarrierDatas = new();
    for (int i = 0; i < passengerEditorInstance.Length; ++i)
    {
      var passengerEditor = passengerEditorInstance[i];
      if (passengerEditor == null) continue;
      if (passengerEditor.type != PassengerEditorControlType.ConcreteBarrier) continue;
      passengerEditor.concreteBarrierData.Index = i;
      concreteBarrierDatas.Add(passengerEditor.concreteBarrierData);
    }
    levelInformation.ConcreteBarrierDatas = concreteBarrierDatas.ToArray();
  }

  void SaveTunnelData()
  {
    List<TunnelData> tunnelDatas = new();
    for (int i = 0; i < passengerEditorInstance.Length; ++i)
    {
      var passengerEditor = passengerEditorInstance[i];
      if (passengerEditor == null) continue;
      if (passengerEditor.type != PassengerEditorControlType.Tunnel) continue;
      passengerEditor.tunnelData.Index = i;
      tunnelDatas.Add(passengerEditor.tunnelData);
    }
    levelInformation.TunnelDatas = tunnelDatas.ToArray();
  }
}
