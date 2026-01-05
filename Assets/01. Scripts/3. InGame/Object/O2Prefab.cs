using UnityEngine;
using DG.Tweening;
using System.Collections;
public class O2Prefab : MonoBehaviour
{
    Player player;
    Tweener tween;

    void OnEnable()
    {
        player = InGameManager.Instance.player;
    }
void Update()
{
    if (player != null)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.transform.position,
            Time.deltaTime * 5f);
    }
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InGameManager.Instance.player.RecoveryOxygen(15f);
            ObjectPoolManager.Instance.ReturnToPool(PoolItemType.OxygenObj, this.gameObject);
            tween?.Kill();
        }
    }
}
