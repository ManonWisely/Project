using UnityEngine;
using UnityEngine.Tilemaps;

// ���� ���� �޴��� "Tile/InteractionTile" �׸��� �߰�
[CreateAssetMenu(fileName = "New Interaction Tile", menuName = "Tile/InteractionTile")]
public class InteractionTile : Tile
{
    // None: �ƹ� ��ȣ�ۿ� ����, Battle: ����, Town: ����
    public enum InteractionType { None, Start, Battle, Town, Finish, Boss }
    public InteractionType interactionType = InteractionType.None;
}
