using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMatcherManager : Singleton<TileMatcherManager>
{
    public Transform effectsParent;

    // 재사용 컬렉션 (필드로 유지하여 할당 최소화)
    private readonly Dictionary<Vector3Int, CubeTile> neighbors = new Dictionary<Vector3Int, CubeTile>(4);
    private readonly Dictionary<CubeColor, List<Vector3Int>> groups = new Dictionary<CubeColor, List<Vector3Int>>(6);
    private readonly HashSet<Vector3Int> toDestroy = new HashSet<Vector3Int>();
    private readonly Queue<Vector3Int> bombQueue = new Queue<Vector3Int>();

    // 재사용 임시 컬렉션
    private readonly List<Vector3Int> metaPositions = new List<Vector3Int>(4);
    private readonly Dictionary<Vector3Int, BombCubeTile> initialBombInfo = new Dictionary<Vector3Int, BombCubeTile>(4);

    // 체인 처리 재사용 컬렉션
    private readonly Queue<Vector3Int> chainQueue = new Queue<Vector3Int>();
    private readonly HashSet<Vector3Int> queuedBombs = new HashSet<Vector3Int>();
    private readonly HashSet<Vector3Int> pending = new HashSet<Vector3Int>();
    private readonly HashSet<Vector3Int> processed = new HashSet<Vector3Int>();

    public float destroyInterval = 0.12f; // 체인 폭탄 지연(초)
    bool isProcessing = false;

    public void TryMatchAroundPlayer(Vector3Int playerCell)
    {
        if (isProcessing) return;

        var tilemap = InGameManager.Instance?.stageTilemap;
        if (tilemap == null) return;

        neighbors.Clear();
        toDestroy.Clear();
        bombQueue.Clear();

        
        foreach (var kv in groups)
            kv.Value.Clear();

        metaPositions.Clear();
        initialBombInfo.Clear();

        var bounds = tilemap.cellBounds;

        for (int i = 0; i < StaticSet.neighborOffsets.Length; i++)
        {
            var dir = StaticSet.neighborOffsets[i];
            var pos = playerCell + dir;

            while (bounds.Contains(pos))
            {
                var tb = tilemap.GetTile(pos);
                if (tb == null)
                {
                    pos += dir;
                    continue;
                }

                if (tb is RuleTile) break;

                if (tb is CubeTile ct)
                {
                    neighbors[pos] = ct;
                }
                break;
            }
        }

        if (neighbors.Count == 0) return;

        var neighEnum = neighbors.GetEnumerator();
        try
        {
            while (neighEnum.MoveNext())
            {
                var kv = neighEnum.Current;
                var pos = kv.Key;
                var tile = kv.Value;
                if (tile.color == CubeColor.Meta)
                {
                    metaPositions.Add(pos);
                    continue;
                }

                if (!groups.TryGetValue(tile.color, out var list))
                {
                    list = new List<Vector3Int>(4);
                    groups[tile.color] = list;
                }
                list.Add(pos);
            }
        }
        finally
        {
            neighEnum.Dispose();
        }

        var gEnum = groups.GetEnumerator();
        try
        {
            while (gEnum.MoveNext())
            {
                var gkv = gEnum.Current;
                var list = gkv.Value;
                int metaCnt = metaPositions.Count;
                if (list.Count + metaCnt >= 2)
                {
                    for (int i = 0; i < list.Count; i++) toDestroy.Add(list[i]);
                    if (metaCnt > 0)
                    {
                        for (int i = 0; i < metaPositions.Count; i++) toDestroy.Add(metaPositions[i]);
                    }
                }
            }
        }
        finally
        {
            gEnum.Dispose();
        }

        if (toDestroy.Count == 0) return;

        var tdEnum = toDestroy.GetEnumerator();
        try
        {
            while (tdEnum.MoveNext())
            {
                var cell = tdEnum.Current;
                var tb = tilemap.GetTile(cell) as CubeTile;
                if (tb == null) continue;

                if (tb is BombCubeTile btb)
                {
                    initialBombInfo[cell] = btb;
                    bombQueue.Enqueue(cell);
                }
            }
        }
        finally
        {
            tdEnum.Dispose();
        }

        InGameManager.Instance.player.StartCoroutineShootRazer(toDestroy);
        var destroyEnum = toDestroy.GetEnumerator();
        try
        {
            while (destroyEnum.MoveNext())
            {
                var cell = destroyEnum.Current;
                var tile = tilemap.GetTile(cell) as CubeTile;
                if (tile == null) continue;

                ObjectPoolManager.Instance.Spawn(PoolItemType.DestoryObj, tilemap.CellToWorld(cell) + tilemap.tileAnchor, effectsParent, 2f);
                ObjectPoolManager.Instance.Spawn(PoolItemType.OxygenObj, tilemap.CellToWorld(cell) + tilemap.tileAnchor, effectsParent, 0f);
                
                tilemap.SetTile(cell, null);
            }
        }
        finally
        {
            destroyEnum.Dispose();
        }

        if (bombQueue.Count > 0)
        {
            StartCoroutine(ProcessBombChainCoroutine(tilemap));
        }
    }

    IEnumerator ProcessBombChainCoroutine(Tilemap tilemap)
    {
        if (isProcessing) yield break;
        isProcessing = true;

        pending.Clear();
        processed.Clear();
        chainQueue.Clear();
        queuedBombs.Clear();

        var tdEnum = toDestroy.GetEnumerator();
        try
        {
            while (tdEnum.MoveNext()) pending.Add(tdEnum.Current);
        }
        finally { tdEnum.Dispose(); }


        while (bombQueue.Count > 0)
        {
            var bpos = bombQueue.Dequeue();
            if (queuedBombs.Add(bpos))
                chainQueue.Enqueue(bpos);
        }

        while (chainQueue.Count > 0)
        {
            var bombPos = chainQueue.Dequeue();
            queuedBombs.Remove(bombPos);

            yield return new WaitForSeconds(destroyInterval);

            if (processed.Contains(bombPos)) continue;

            BombCubeTile bombAsset = null;
            if (!initialBombInfo.TryGetValue(bombPos, out bombAsset))
            {
                bombAsset = tilemap.GetTile(bombPos) as BombCubeTile;
            }

            for (int i = 0; i < StaticSet.neighborOffsets.Length; i++)
            {
                var p = bombPos + StaticSet.neighborOffsets[i];

                var adjTb = tilemap.GetTile(p);
                if (adjTb is RuleTile) continue;
                InGameManager.Instance.ShowEfxAtCell(p);
                if (adjTb == null) continue;

                
                if (pending.Contains(p) || processed.Contains(p)) continue;


                pending.Add(p);

                if (adjTb is BombCubeTile)
                {
                    
                    if (queuedBombs.Add(p))
                    {
                        chainQueue.Enqueue(p);
                    }
                }
                else if (adjTb is CubeTile adjCube)
                {
                    
                    ObjectPoolManager.Instance.Spawn(PoolItemType.DestoryObj, tilemap.CellToWorld(p) + tilemap.tileAnchor, effectsParent, 2f);
                    ObjectPoolManager.Instance.Spawn(PoolItemType.OxygenObj, tilemap.CellToWorld(p) + tilemap.tileAnchor, effectsParent, 0);
                    tilemap.SetTile(p, null);
                    processed.Add(p);

                    if (bombAsset != null && bombAsset.oxygenDamage > 0 && InGameManager.Instance?.player != null && InGameManager.Instance.player.nowPos == p)
                    {
                        GameUI.Instance?.oxygen?.ReduceOxygen(bombAsset.oxygenDamage);
                    }
                }
            }

            var nowTb = tilemap.GetTile(bombPos);
            if (nowTb != null && !(nowTb is RuleTile))
            {
                tilemap.SetTile(bombPos, null);
            }
            processed.Add(bombPos);
        }

        isProcessing = false;
    }
}