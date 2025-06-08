using UnityEngine;
using UnityEngine.Tilemaps;

// 에셋 생성 메뉴에 "Tile/InteractionTile" 항목을 추가
[CreateAssetMenu(fileName = "New Interaction Tile", menuName = "Tile/InteractionTile")]
public class InteractionTile : Tile
{
    // None: 아무 상호작용 없음, Battle: 전투, Town: 마을
    public enum InteractionType { None, Start, Battle, Town, Finish, Boss }
    public InteractionType interactionType = InteractionType.None;
}
