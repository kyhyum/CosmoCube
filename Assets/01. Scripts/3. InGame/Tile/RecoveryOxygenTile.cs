using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/RecoveryOxygenTile")]
public class RecoveryOxygenTile : Tile
{
    public GameObject recoveryOxygenPrefab;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        Tilemap tm = tilemap.GetComponent<Tilemap>();
        if (tm != null && recoveryOxygenPrefab != null)
        {
            // 월드 위치 계산 및 부모 지정(타일맵 트랜스폼에 붙이기)
            Vector3 worldPos = tm.CellToWorld(position) + tm.tileAnchor;
            GameObject inst;
            if (ObjectPoolManager.Instance != null)
            {
                inst = ObjectPoolManager.Instance.Spawn(PoolItemType.OxygenRecoveryItem, worldPos, tm.transform, 0f);
                
                InGameManager.Instance.SetTileEmpty(position);
            }
        }

        return base.StartUp(position, tilemap, go);
    }
}
