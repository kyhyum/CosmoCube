using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/PortalTile")]
public class PortalTile : Tile
{
    public GameStage targetStage;      // 목적 스테이지 (직접 지정)
}