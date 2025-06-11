using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegionManager : MonoBehaviour
{
    public RegionController startRegion;
    private RegionController currentRegion;

    void Start()
    {
        // ���� �� �� �̸� ���
        GameState.Instance.mapSceneName = SceneManager.GetActiveScene().name;

        // ù ������ Ȱ��ȭ
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
        // BattleScene�� Single ���� �ε�
        AsyncOperation op = SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Single);
        yield return op;

        // ���� ���� �� ���� Ŭ����
        region.isCleared = true;
        currentRegion = region;

        // ���� ���� ����
        foreach (var nb in region.neighbors)
            nb.gameObject.SetActive(true);
    }
}
