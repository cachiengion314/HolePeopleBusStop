using System;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class InitHoleData
{
  public int Index;
  public int Value;
}

[Serializable]
public class GroupPassengerData
{
  public int2 GridRangeX;
  public int2 GridRangeY;
  public bool IsHidden;
  public int Value;
}

[Serializable]
public class LevelInformation
{
  [ViewOnly] public int Index;
  public InitHoleData[] InitHoleDatas;
  public GroupPassengerData[] GroupPassengerDatas;
  public float3 GridPosition;
  public int2 GridSize;
  public float2 GridScale;
  public int2 HoleSize;
}

public class LevelEditor : MonoBehaviour
{
  [Header("Level Editor")]
  [SerializeField] LevelInformation levelInformation;
  [SerializeField][Range(1, 20)] int levelSelected = 1;
  [SerializeField] GridWorld grid;

  [NaughtyAttributes.Button]
  void Clear()
  {
    levelInformation = new LevelInformation();
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
    print("Load level successfully");
  }

  [NaughtyAttributes.Button]
  void SaveLevel()
  {
    levelInformation.Index = levelSelected - 1;
    levelInformation.GridPosition = grid.transform.position;
    levelInformation.GridScale = grid.GridScale;
    levelInformation.GridSize = grid.GridSize;

    HoangNam.SaveSystem.Save(
      levelInformation,
      "Resources/Levels/" + KeyString.NAME_LEVEL_FILE + levelSelected
    );
    print("Save level successfully");
  }
}
