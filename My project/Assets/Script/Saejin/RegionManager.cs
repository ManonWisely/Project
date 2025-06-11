using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegionManager : MonoBehaviour
{
    public RegionController startRegion;      // ù ��� ����
    private RegionController currentRegion;

    void Start()
    {
        // ù ������ Ȱ��ȭ, ������ ��Ȱ��ȭ
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

        // 1) OverlapPoint ���
        Collider2D col = Physics2D.OverlapPoint(wp);
        if (col != null)
        {
            Debug.Log($"[Hit] OverlapPoint hit: {col.name}");
        }
        else
        {
            Debug.Log("[Hit] OverlapPoint hit nothing");
        }

        // 2) RaycastAll ���(������ �ְ�, ���� ª�� �Ÿ��� �˻�)
        var hits = Physics2D.RaycastAll(wp, Vector2.up, 0.01f);
        foreach (var h in hits)
        {
            Debug.Log($"[Hit] RaycastAll hit: {h.collider.name}");
        }
    }


    IEnumerator BattleFlow(RegionController region)
    {
        // ��: ������ ����Ƽ�� �ε�
        yield return SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive);

        // ���� �� �� Ŭ���� ó��
        region.isCleared = true;
        currentRegion = region;

        // ������ ��ε�
        yield return SceneManager.UnloadSceneAsync("BattleScene");

        // ���� ���� ����
        foreach (var nb in region.neighbors)
        {
            nb.gameObject.SetActive(true);
        }
    }
}
