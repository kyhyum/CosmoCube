using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class InGameManager : Singleton<InGameManager>
{
    public Transform mapTransform;
    public Tilemap efxTileMap;
    public TileBase efxTileBase;
    public Tilemap stageTilemap;
    public Player player;
    public TileBase metaTile;
    void Awake()
    {
        StartCoroutine(LoadMapObjectsToSingleParentCoroutine(DataManager.Instance.SELECTSTAGE));
    }

    public IEnumerator LoadMapObjectsToSingleParentCoroutine(GameStage gameStage)
    {
        string resourcesPath = $"MapObjects/{gameStage}";

        ResourceRequest request = Resources.LoadAsync<GameObject>(resourcesPath);

        GameObject prefab = request.asset as GameObject;

        GameObject go = Instantiate(prefab, mapTransform, false);
        go.name = prefab.name;
        go.transform.localPosition = Vector3.zero;

        stageTilemap = go.GetComponent<Stage>().stageTileMap;

        yield return request; // 로드 완료까지 대기
    }

    public bool IsTileOccupied(Vector3Int cellPos)
    {
        TileBase tb = stageTilemap.GetTile(cellPos);
        if (CheckPortalAtCell(tb))
        {
            return false;
        }
        else
        {
            return stageTilemap.HasTile(cellPos);
        }

    }

    public bool TryPerformMove(PlayerState dir)
    {
        if (player == null)
        {
            Debug.LogWarning("InGameManager: player is null.");
            return false;
        }

        Vector3Int delta = DirectionToDelta(dir);
        Vector3Int target = player.nowPos + delta;

        if (IsTileOccupied(target))
        {
            // 타일이 있으면 이동 불가
            return false;
        }

        // 이동 가능: 플레이어 위치 갱신 및 시각적 위치 설정
        player.nowPos = target;
        player.SetPlayerPosition();
        return true;
    }

    Vector3Int DirectionToDelta(PlayerState dir)
    {
        switch (dir)
        {
            case PlayerState.Up: return new Vector3Int(0, 1, 0);
            case PlayerState.Down: return new Vector3Int(0, -1, 0);
            case PlayerState.Left: return new Vector3Int(-1, 0, 0);
            case PlayerState.Right: return new Vector3Int(1, 0, 0);
            default: return Vector3Int.zero;
        }
    }

    public bool CheckPortalAtCell(TileBase tb)
    {
        if (stageTilemap == null) return false;

        if (tb == null) return false;
        if (tb is RuleTile) return false; // RuleTile은 무시

        var portal = tb as PortalTile;
        if (portal == null) return false;

        DataManager.Instance.SELECTSTAGE = portal.targetStage;

        UIManager.Instance.Push(UIName.ClearLayer);
        return true;
    }

    public void PlaceMetaAtCell(Vector3Int cell)
    {
        stageTilemap.SetTile(cell, metaTile);
        stageTilemap.RefreshTile(cell);
    }

    public void ShowEfxAtCell(Vector3Int cell)
    {
        if (efxTileMap == null || efxTileBase == null) return;
        StartCoroutine(SpawnEfxCoroutine(cell));
    }


    private IEnumerator SpawnEfxCoroutine(Vector3Int cell)
    {
        efxTileMap.SetTile(cell, efxTileBase);
        efxTileMap.RefreshTile(cell);

        // 유지 시간
        yield return new WaitForSeconds(0.35f);

        efxTileMap.SetTile(cell, null);
        efxTileMap.RefreshTile(cell);
    }

    public void SetTileEmpty(Vector3Int cell)
    {
        StartCoroutine(SetTileNull(cell));
    }

    private IEnumerator SetTileNull(Vector3Int cell)
    {
        yield return null;
        stageTilemap.SetTile(cell, null);
    }
}