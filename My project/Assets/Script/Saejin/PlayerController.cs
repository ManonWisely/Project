using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("클릭 감지할 Tilemap")]
    public Tilemap targetTilemap;

    [Header("Ground Tilemap (클리어용)")]
    public Tilemap groundTilemap;

    [Header("이동시킬 플레이어 Transform")]
    public Transform playerTransform;

    [Header("클릭 후 교체할 Finish Tile")]
    public TileBase finishTile;

    [Header("오른쪽 이동 시 덮어쓸 Replacement Tile")]
    public TileBase clearReplacementTile;

    [Header("인접 허용 범위 (Δ ≤ 1)")]
    public int maxDelta = 1;

    float yOffset;

    void Start()
    {
        yOffset = targetTilemap.cellSize.y * 0.5f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryClickAction();
    }

    void TryClickAction()
    {
        // 1) 마우스 → 월드 → 셀
        Vector3 mW = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mW.z = 0;
        Vector3Int clickCell = targetTilemap.WorldToCell(mW);

        // 2) InteractionTile 체크
        var interaction = targetTilemap.GetTile<InteractionTile>(clickCell);
        if (interaction == null ||
            interaction.interactionType == InteractionTile.InteractionType.None)
            return;

        // 3) 판정용 셀 좌표 (yOffset 보정)
        Vector3 fC = playerTransform.position - new Vector3(0, yOffset, 0);
        Vector3Int playerCell = targetTilemap.WorldToCell(fC);

        // 4) Δ 검사
        int dx = clickCell.x - playerCell.x;
        int dy = clickCell.y - playerCell.y;
        if (Mathf.Abs(dx) > maxDelta || Mathf.Abs(dy) > maxDelta)
            return;

        // 5) InteractionType별 호출
        switch (interaction.interactionType)
        {
            case InteractionTile.InteractionType.Battle: StartBattle(clickCell); break;
            case InteractionTile.InteractionType.Town: EnterTown(clickCell); break;
            case InteractionTile.InteractionType.Boss: StartBoss(clickCell); break;
            case InteractionTile.InteractionType.Finish:
                Debug.Log($"[Finish] 이미 완료된 칸 @ {clickCell}");
                return;
        }

        // ??????????????????????????????????????????
        // 6) **바로** 타일 교체 & 리프레시
        //    클릭된 칸 → Finish
        targetTilemap.SetTile(clickCell, finishTile);
        targetTilemap.RefreshTile(clickCell);

        //    오른쪽 계열(→, ↗, ↘)이면 왼쪽 계열 클리어
        bool odd = (Mathf.Abs(playerCell.y) % 2 == 1);
        bool E = (dx == 1 && dy == 0);
        bool NE = odd ? (dx == 1 && dy == 1) : (dx == 0 && dy == 1);
        bool SE = odd ? (dx == 1 && dy == -1) : (dx == 0 && dy == -1);

        if (E || NE || SE)
        {
            // W, NW, SW 오프셋
            Vector3Int[] offs = new[]
            {
                new Vector3Int(-1,  0,0),
                new Vector3Int( odd? 0: -1, +1,0),
                new Vector3Int( odd? 0: -1, -1,0)
            };
            foreach (var o in offs)
            {
                var c = playerCell + o;
                if (groundTilemap.HasTile(c))
                {
                    groundTilemap.SetTile(c, clearReplacementTile);
                    groundTilemap.RefreshTile(c);
                }
            }
        }
        else
        {
            // ←, ↖, ↙이면 클릭+이웃 Finish 처리 (이동 없이)
            Vector3Int[] offs = new[]
            {
                new Vector3Int(-1,  0,0),
                new Vector3Int( odd? 0: -1, +1,0),
                new Vector3Int( odd? 0: -1, -1,0)
            };
            foreach (var o in offs)
            {
                var c = playerCell + o;
                if (targetTilemap.HasTile(c))
                {
                    targetTilemap.SetTile(c, finishTile);
                    targetTilemap.RefreshTile(c);
                }
            }
        }

        // 7) **전 체 타 일 맵** 즉시 갱신
        targetTilemap.RefreshAllTiles();
        groundTilemap.RefreshAllTiles();

        // 8) **그 다음에** 플레이어 이동
        if (E || NE || SE)
        {
            Vector3 cen = targetTilemap.GetCellCenterWorld(clickCell);
            playerTransform.position = new Vector3(cen.x, cen.y + yOffset, playerTransform.position.z);
        }
    }

    void StartBattle(Vector3Int c)
    {
        SceneManager.LoadScene("BattleScene");
        Debug.Log($"▶ 전투 시작 @ {c}");
    }
    void EnterTown(Vector3Int c) => Debug.Log($"▶ 마을 입장 @ {c}");
    void StartBoss(Vector3Int c) => Debug.Log($"▶ 보스전투 시작 @ {c}");
}