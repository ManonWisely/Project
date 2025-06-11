using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegionManager : MonoBehaviour
{
    public RegionController startRegion;      // 첫 출발 영역
    private RegionController currentRegion;

    void Start()
    {
        // 첫 영역만 활성화, 나머지 비활성화
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
        Debug.Log($"[Click] WorldPos: {wp}");

        // 1) OverlapPoint 방식
        Collider2D col = Physics2D.OverlapPoint(wp);
        if (col != null)
        {
            Debug.Log($"[Hit] OverlapPoint hit: {col.name}");
        }
        else
        {
            Debug.Log("[Hit] OverlapPoint hit nothing");
        }

        // 2) RaycastAll 방식(방향을 주고, 아주 짧은 거리만 검사)
        var hits = Physics2D.RaycastAll(wp, Vector2.up, 0.01f);
        foreach (var h in hits)
        {
            Debug.Log($"[Hit] RaycastAll hit: {h.collider.name}");
        }
    }


    IEnumerator BattleFlow(RegionController region)
    {
        // 예: 전투씬 어드디티브 로드
        yield return SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive);

        // 전투 끝 → 클리어 처리
        region.isCleared = true;
        currentRegion = region;

        // 전투씬 언로드
        yield return SceneManager.UnloadSceneAsync("BattleScene");

        // 인접 영역 해제
        foreach (var nb in region.neighbors)
        {
            nb.gameObject.SetActive(true);
        }
    }
}
