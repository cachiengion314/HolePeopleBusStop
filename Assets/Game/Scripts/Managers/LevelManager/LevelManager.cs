using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class LevelManager : MonoBehaviour
{
  public static LevelManager Instance { get; private set; }
  [SerializeField] GridWorld gridWorld;

  void Start()
  {
    gridWorld.BakingGridWorld();
    gridWorld.BakingPathFinding();
  }

  void Update()
  {
    using var excludes = new NativeArray<int2>(0, Allocator.TempJob);
    using var path = gridWorld.PathFindingTo(new float3(1, 1, 0), new float3(5, 2, 0), excludes);
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

  void SetupCurrentLevel()
  {

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
    // levelInformation = levelInfo;
    print("Load level " + level + " successfully ");
  }
}
