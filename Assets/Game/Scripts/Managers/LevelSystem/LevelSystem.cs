using UnityEngine.SceneManagement;
using Unity.Mathematics;
using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  public static LevelSystem Instance { get; private set; }
  LevelInformation _levelInformation;
  [SerializeField][Range(1, 20)] int levelSelected = 1;
  public bool IsSelectedLevel;
  [SerializeField] GridWorld passengerGrid;
  [SerializeField] GridWorld holeGrid;

  void Start()
  {
    if (IsSelectedLevel)
    {
      GameManager.Instance.CurrentLevelIndex = levelSelected - 1;
      LoadLevelFrom(levelSelected);
    }
    else LoadLevelFrom(GameManager.Instance.CurrentLevelIndex + 1);

    SetupCurrentLevel();
  }

  void Update()
  {

  }

  void OnDestroy()
  {
    passengerGrid.DisposePathFinding();
  }

  bool IsAtHoleTerritory(float3 passengerPos, LevelHoleData[] levelHoleDatas)
  {
    if (holeGrid.IsPosOutsideAt(passengerPos)) return false;
    var index = holeGrid.ConvertWorldPosToIndex(passengerPos);
    if (levelHoleDatas[index] == null) return false;
    return true;
  }

  int ConvertPercentToIdx(float percentInt, int gridSize)
  {
    return (int)math.floor(percentInt / 100.0f * gridSize);
  }

  void SetupCurrentLevel()
  {
    passengerGrid.transform.position = _levelInformation.GridPosition;
    holeGrid.transform.position = _levelInformation.GridPosition + new float3(0, .1f, 0);

    passengerGrid.GridScale = _levelInformation.GridScale;
    passengerGrid.GridSize = _levelInformation.GridSize;
    passengerGrid.InitValue();
    passengerGrid.BakingPathFinding();

    var holeSize = _levelInformation.HoleSize;
    var sizeUnitX = (int)math.floor(_levelInformation.GridSize.x / holeSize.x);
    var sizeUnitY = (int)math.floor(_levelInformation.GridSize.y / holeSize.y);
    var scaleUnitX = (int)math.floor(_levelInformation.GridScale.x * holeSize.x);
    var scaleUnitY = (int)math.floor(_levelInformation.GridScale.y * holeSize.y);
    holeGrid.GridSize = new int2(sizeUnitX, sizeUnitY);
    holeGrid.GridScale = new int2(scaleUnitX, scaleUnitY);
    holeGrid.InitValue();

    InitPassengers();
    InitHoles();

    var groupPassengerDatas = _levelInformation.GroupPassengerDatas;
    for (int i = 0; i < groupPassengerDatas.Length; ++i)
    {
      var index = groupPassengerDatas[i].Index;
      var obj = SpawnPassengerAt(index, spawnedParent);

      _passengers[index] = obj;
    }

    var levelHoleDatas = _levelInformation.LevelHoleDatas;
    for (int i = 0; i < levelHoleDatas.Length; ++i)
    {
      var index = levelHoleDatas[i].Index;
      var obj = SpawnHoleAt(index, spawnedParent);

      _holes[index] = obj;
    }
  }

  public void RestartLevel()
  {
    SceneManager.LoadScene(KeyString.NAME_SCENE_GAMEPLAY);
  }

  public void LoadLevelFrom(int level)
  {
    var _rawLevelInfo = Resources.Load<TextAsset>("Levels/" + KeyString.NAME_LEVEL_FILE + level).text;
    var levelInfo = JsonUtility.FromJson<LevelInformation>(_rawLevelInfo);

    if (levelInfo == null) { print("This level is not existed!"); return; }
    _levelInformation = levelInfo;
    print("Load level " + level + " successfully ");
  }
}
