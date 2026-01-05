using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Vector3Int nowPos = Vector3Int.zero;
    Vector3Int varPos = Vector3Int.zero;

    [HideInInspector]
    public int hp = 4;
    [HideInInspector]
    public float oxygen = 100;
    [HideInInspector]
    public int metaCount = 0;

    [SerializeField]
    Vector3 controlPos = new Vector3(0, 0);
    public PlayerController controller;
    public Animator playerAnimator;

    public LineRenderer[] lineRenderers;
    public Transform[] efxTrs;

    void Awake()
    {
        GameUI.Instance.lifeUI.InitLife(hp);
    }

    public void Move(MoveDirection moveDirection)
    {
        switch (moveDirection)
        {
            case MoveDirection.Up:
                controller.sm.SetState(controller.dicState[PlayerState.Up]);
                varPos.y++;
                break;
            case MoveDirection.Down:
                controller.sm.SetState(controller.dicState[PlayerState.Down]);
                varPos.y--;
                break;
            case MoveDirection.Left:
                controller.sm.SetState(controller.dicState[PlayerState.Left]);
                varPos.x--;
                break;
            case MoveDirection.Right:
                controller.sm.SetState(controller.dicState[PlayerState.Right]);
                varPos.x++;
                break;
        }
    }

    public void SetPlayerPosition()
    {
        varPos = nowPos;
        this.transform.DOMove(InGameManager.Instance.stageTilemap.CellToLocal(nowPos) + controlPos, 0.2f);

    }

    public void RecoveryHp()
    {
        if (hp < 4)
        {
            GameUI.Instance.lifeUI.RecoveryLife(hp);
            hp++;
        }
    }

    public void ReduceHp()
    {
        if(hp > 0)
            hp--;
        GameUI.Instance.lifeUI.ReduceLife(hp);
    }

    public void RecoveryOxygen(float value)
    {
        GameUI.Instance.oxygen.PlusOxygen(value);
    }

    public void ReduceOxygen(float value)
    {
        GameUI.Instance.oxygen.ReduceOxygen(value);
    }

    public void GetMetaCube()
    {
        metaCount++;
        GameUI.Instance.UpdateMetaBtn();
    }
    
    public void StartCoroutineShootRazer(HashSet<Vector3Int> pos)
    {
        StartCoroutine(ShootRazer(pos));
    } 
    public IEnumerator ShootRazer(HashSet<Vector3Int> pos)
    {
        int posCount = pos.Count;
        var enumerator = pos.GetEnumerator();
        int idx = 0;
        while (enumerator.MoveNext())
        {
            Vector3Int realPos = enumerator.Current - nowPos;
            efxTrs[idx].position = InGameManager.Instance.stageTilemap.CellToWorld(enumerator.Current) + InGameManager.Instance.stageTilemap.tileAnchor;
            efxTrs[idx].gameObject.SetActive(true);

            lineRenderers[idx].SetPosition(1, realPos);
            lineRenderers[idx].gameObject.SetActive(true);

            idx++;
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < posCount; i++)
        {
            efxTrs[i].gameObject.SetActive(false);
            lineRenderers[i].gameObject.SetActive(false);
        }
    }
}