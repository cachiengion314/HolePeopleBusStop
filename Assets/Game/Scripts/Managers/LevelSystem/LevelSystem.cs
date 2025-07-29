using UnityEngine.SceneManagement;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;

public partial class LevelSystem : MonoBehaviour
{
  public static LevelSystem Instance { get; private set; }
  LevelInformation _levelInformation;
  [SerializeField][Range(1, 20)] int levelSelected = 1;
  public bool IsSelectedLevel;
  [SerializeField] GridWorld gridWorld;
  [SerializeField] Transform startPos;
  [SerializeField] Transform endPos;

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
    using var excludes = new NativeArray<int2>(0, Allocator.TempJob);
    using var path = gridWorld.PathFindingTo(
      startPos.position, endPos.position, excludes
    );
    for (int i = 0; i < path.Length; ++i)
    {
      var pos = gridWorld.ConvertGridPosToWorldPos(path[i]);
      HoangNam.Utility.DrawQuad(pos, .8f, -90, HoangNam.ColorIndex.Green);
    }
  }

  void OnDestroy()
  {
    gridWorld.DisposePathFinding();
  }

  int ConvertPercentToIdx(float percentInt, int gridSize)
  {
    return (int)math.floor(percentInt / 100.0f * gridSize);
  }

  void SetupCurrentLevel()
  {
    gridWorld.transform.position = _levelInformation.GridPosition;
    gridWorld.GridScale = _levelInformation.GridScale;
    gridWorld.GridSize = _levelInformation.GridSize;
    gridWorld.InitValue();
    gridWorld.BakingPathFinding();

    InitPassengers();
    var passengerPartitionDatas = _levelInformation.PassengerPartitionDatas;

    for (int i = 0; i < passengerPartitionDatas.Length; ++i)
    {
      var partition = passengerPartitionDatas[i];
      var percentInX = partition.PercentInX;
      var percentInY = partition.PercentInY;

      var startX = ConvertPercentToIdx(percentInX.x, _levelInformation.GridSize.x);
      var endX = ConvertPercentToIdx(percentInX.y, _levelInformation.GridSize.x);
      var startY = ConvertPercentToIdx(percentInY.x, _levelInformation.GridSize.y);
      var endY = ConvertPercentToIdx(percentInY.y, _levelInformation.GridSize.y);

      for (int y = startY; y < endY; ++y)
      {
        for (int x = startX; x < endX; ++x)
        {
          var gridPos = new int2(x, y);
          var index = gridWorld.ConvertGridPosToIndex(gridPos);

          var obj = SpawnPassengerAt(index, spawnedParent);

          _passengers[index] = obj;
        }
      }
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
