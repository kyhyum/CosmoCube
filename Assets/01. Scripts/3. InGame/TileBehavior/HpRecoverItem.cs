using UnityEngine;

public class HpRecoverItem : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            //SoundManager.instance.GetSFX_itemGetSFX().Play();

            InGameManager.Instance.player.RecoveryHp();

            ObjectPoolManager.Instance.ReturnToPool(PoolItemType.HpRecoverItem, this.gameObject);

        }
    }
}
