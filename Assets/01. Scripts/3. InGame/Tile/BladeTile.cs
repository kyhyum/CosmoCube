using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/BladeTile")]
public class BladeTile : Tile
{
    public GameObject bladePrefab;
    public BladeBehaviour bladeBehaviour;  
    public bool startOpen = false;
    public float curTime = 4f;
    public float openDuration = 2f;
    public float closeDuration = 4f;
    public float rotateSpeed = 300f;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        Tilemap tm = tilemap.GetComponent<Tilemap>();
        if (tm != null && bladePrefab != null)
        {
            // 월드 위치 계산 및 부모 지정(타일맵 트랜스폼에 붙이기)
            Vector3 worldPos = tm.CellToWorld(position) + tm.tileAnchor;
            GameObject inst;
            if (ObjectPoolManager.Instance != null)
            {
                inst = ObjectPoolManager.Instance.Spawn(PoolItemType.BladeObj, worldPos, tm.transform, 0f);

                bladeBehaviour = inst.GetComponent<BladeBehaviour>();
                if (bladeBehaviour != null)
                    bladeBehaviour.Initialize(startOpen, curTime, openDuration, closeDuration, rotateSpeed);

                InGameManager.Instance.SetTileEmpty(position);
            }

        }

        return base.StartUp(position, tilemap, go);
    }
}