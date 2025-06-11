using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegionManager : MonoBehaviour
{
    public RegionController startRegion;
    private RegionController currentRegion;

    void Start()
    {
        // 시작 맵 씬 이름 기록
        GameState.Instance.mapSceneName = SceneManager.GetActiveScene().name;

        // 첫 영역만 활성화
        foreach (var reg in FindObjectsOfType<RegionController>())
        {
            reg.gameObject.SetActive(reg == startRegion);
        }
        currentRegion = startRegion;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) HandleClick();
    }

    void HandleClick()
    {
        Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.Raycast(wp, Vector2.zero);
        if (hit.collider == null) return;

        var target = hit.collider.GetComponentInParent<RegionController>();
        if (target == null || !target.gameObject.activeSelf) return;

        StartCoroutine(BattleFlow(target));
    }

    IEnumerator BattleFlow(RegionController region)
    {
        // BattleScene을 Single 모드로 로드
        AsyncOperation op = SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Single);
        yield return op;

        // 전투 종료 후 지역 클리어
        region.isCleared = true;
        currentRegion = region;

        // 인접 영역 해제
        foreach (var nb in region.neighbors)
            nb.gameObject.SetActive(true);
    }
}
