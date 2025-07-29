using System;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class PartitionData
{
  public float2 PercentInX;
  public float2 PercentInY;
  public bool IsHidden;
  public int Value;
}

[Serializable]
public class LevelInformation
{
  [ViewOnly] public int Index;
  /// <summary>
  /// example
  /// item_0: partition of x: (0, 50), partition of y: (0, 50)
  /// item_1: partition of x: (50, 100), partition of y: (0, 50)
  /// item_2: partition of x: (0, 100), partition of y: (50, 100)
  /// item_3: partition of x: (50, 100), partition of y: (50, 100)
  /// </summary>
  public PartitionData[] PassengerPartitionDatas;
  public PartitionData[] HolePartitionDatas;
  public float3 GridPosition;
  public int2 GridSize;
  public float2 GridScale;
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
