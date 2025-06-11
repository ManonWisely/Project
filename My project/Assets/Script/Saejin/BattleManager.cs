using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    // 맵 씬에서 전달받은 region 식별자
    public string regionId;

    // 전투 종료 시 호출
    public void OnBattleEnd()
    {
        // 클리어 상태 저장
        GameState.Instance.clearedRegions[regionId] = true;

        // 맵 씬으로 재로드
        SceneManager.LoadScene(GameState.Instance.mapSceneName, LoadSceneMode.Single);
    }
}