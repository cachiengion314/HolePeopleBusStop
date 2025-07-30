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
    if (Instance == null)
      Instance = this;
    else Destroy(gameObject);

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

  void SetupCurrentLevel()
  {
    passengerGrid.transform.position = _levelInformation.GridPosition;
    holeGrid.transform.position = _levelInformation.GridPosition + new float3(0, .1f, 0);

    passengerGrid.GridScale = _levelInformation.GridScale;
    passengerGrid.GridSize = _levelInformation.GridSize;
    passengerGrid.InitValue();
    passengerGrid.BakingPathFinding();

    var holeScale = _levelInformation.HoleScale;
    var sizeUnitX = (int)math.floor(_levelInformation.GridSize.x / holeScale.x);
    var sizeUnitY = (int)math.floor(_levelInformation.GridSize.y / holeScale.y);
    var scaleUnitX = _levelInformation.GridScale.x * holeScale.x;
    var scaleUnitY = _levelInformation.GridScale.y * holeScale.y;
    holeGrid.GridSize = new int2(sizeUnitX, sizeUnitY);
    holeGrid.GridScale = new float2(scaleUnitX, scaleUnitY);
    holeGrid.InitValue();

    InitLevelDatas();
    InitHoleTransform();
    InitPassengerTransforms();

    var initHoleDatas = _levelInformation.InitHoleDatas;
    for (int i = 0; i < initHoleDatas.Length; ++i)
    {
      var index = initHoleDatas[i].Index;
      var colorValue = initHoleDatas[i].Value;
      var obj = SpawnHoleAt(index, spawnedParent);

      _holeTransforms[index] = obj.transform;
      ColorValueDatas.Add(
        obj.GetInstanceID(),
        new ColorValueData { ColorValue = colorValue }
      );
      var mesh = obj.GetComponentInChildren<MeshRenderer>();
      MeshRendDatas.Add(
        obj.GetInstanceID(),
        new IMeshRendData { BodyRenderer = mesh }
      );
      if (obj.TryGetComponent<IColorValue>(out var colorComp))
      {
        colorComp.SetColorValue(colorValue);
      }
    }

    var groupPassengerDatas = _levelInformation.GroupPassengerDatas;
    for (int i = 0; i < groupPassengerDatas.Length; ++i)
    {
      var colorValue = groupPassengerDatas[i].Value;
      var gridRangeX = groupPassengerDatas[i].GridRangeX;
      var gridRangeY = groupPassengerDatas[i].GridRangeY;

      var startX = gridRangeX.x;
      var endX = gridRangeX.y;
      var startY = gridRangeY.x;
      var endY = gridRangeY.y;

      for (int y = startY; y <= endY; ++y)
      {
        for (int x = startX; x <= endX; ++x)
        {
          var gridPos = new int2(x, y);
          var index = passengerGrid.ConvertGridPosToIndex(gridPos);
          var obj = SpawnPassengerAt(index, spawnedParent);

          _passengerTransforms[index] = obj;
          ColorValueDatas.Add(
            obj.GetInstanceID(),
            new ColorValueData { ColorValue = colorValue }
          );
          var mesh = obj.GetComponentInChildren<Animator>()
            .GetComponentInChildren<SkinnedMeshRenderer>();
          SkinnedMeshRendDatas.Add(
            obj.GetInstanceID(),
            new ISkinnedMeshRendData { BodyRenderer = mesh }
          );
          if (obj.TryGetComponent<IColorValue>(out var colorComp))
          {
            colorComp.SetColorValue(colorValue);
          }
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
