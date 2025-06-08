using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Ŭ�� ������ Tilemap")]
    public Tilemap targetTilemap;

    [Header("Ground Tilemap (Ŭ�����)")]
    public Tilemap groundTilemap;

    [Header("�̵���ų �÷��̾� Transform")]
    public Transform playerTransform;

    [Header("Ŭ�� �� ��ü�� Finish Tile")]
    public TileBase finishTile;

    [Header("������ �̵� �� ��� Replacement Tile")]
    public TileBase clearReplacementTile;

    [Header("���� ��� ���� (�� �� 1)")]
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
        // 1) ���콺 �� ���� �� ��
        Vector3 mW = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mW.z = 0;
        Vector3Int clickCell = targetTilemap.WorldToCell(mW);

        // 2) InteractionTile üũ
        var interaction = targetTilemap.GetTile<InteractionTile>(clickCell);
        if (interaction == null ||
            interaction.interactionType == InteractionTile.InteractionType.None)
            return;

        // 3) ������ �� ��ǥ (yOffset ����)
        Vector3 fC = playerTransform.position - new Vector3(0, yOffset, 0);
        Vector3Int playerCell = targetTilemap.WorldToCell(fC);

        // 4) �� �˻�
        int dx = clickCell.x - playerCell.x;
        int dy = clickCell.y - playerCell.y;
        if (Mathf.Abs(dx) > maxDelta || Mathf.Abs(dy) > maxDelta)
            return;

        // 5) InteractionType�� ȣ��
        switch (interaction.interactionType)
        {
            case InteractionTile.InteractionType.Battle: StartBattle(clickCell); break;
            case InteractionTile.InteractionType.Town: EnterTown(clickCell); break;
            case InteractionTile.InteractionType.Boss: StartBoss(clickCell); break;
            case InteractionTile.InteractionType.Finish:
                Debug.Log($"[Finish] �̹� �Ϸ�� ĭ @ {clickCell}");
                return;
        }

        // ??????????????????????????????????????????
        // 6) **�ٷ�** Ÿ�� ��ü & ��������
        //    Ŭ���� ĭ �� Finish
        targetTilemap.SetTile(clickCell, finishTile);
        targetTilemap.RefreshTile(clickCell);

        //    ������ �迭(��, ��, ��)�̸� ���� �迭 Ŭ����
        bool odd = (Mathf.Abs(playerCell.y) % 2 == 1);
        bool E = (dx == 1 && dy == 0);
        bool NE = odd ? (dx == 1 && dy == 1) : (dx == 0 && dy == 1);
        bool SE = odd ? (dx == 1 && dy == -1) : (dx == 0 && dy == -1);

        if (E || NE || SE)
        {
            // W, NW, SW ������
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
            // ��, ��, ���̸� Ŭ��+�̿� Finish ó�� (�̵� ����)
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

        // 7) **�� ü Ÿ �� ��** ��� ����
        targetTilemap.RefreshAllTiles();
        groundTilemap.RefreshAllTiles();

        // 8) **�� ������** �÷��̾� �̵�
        if (E || NE || SE)
        {
            Vector3 cen = targetTilemap.GetCellCenterWorld(clickCell);
            playerTransform.position = new Vector3(cen.x, cen.y + yOffset, playerTransform.position.z);
        }
    }

    void StartBattle(Vector3Int c)
    {
        SceneManager.LoadScene("BattleScene");
        Debug.Log($"�� ���� ���� @ {c}");
    }
    void EnterTown(Vector3Int c) => Debug.Log($"�� ���� ���� @ {c}");
    void StartBoss(Vector3Int c) => Debug.Log($"�� �������� ���� @ {c}");
}