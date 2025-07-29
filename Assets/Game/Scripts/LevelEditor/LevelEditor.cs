using System;
using UnityEngine;

[Serializable]
public class LevelInformation
{
  [ViewOnly] public int Index;
}

public class LevelEditor : MonoBehaviour
{
  [Header("Level Editor")]
  [SerializeField] LevelInformation levelInformation;
  [SerializeField][Range(1, 20)] int levelSelected = 1;

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

    HoangNam.SaveSystem.Save(
      levelInformation,
      "Resources/Levels/" + KeyString.NAME_LEVEL_FILE + levelSelected
    );
    print("Save level successfully");
  }
}
