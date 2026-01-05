using UnityEngine;

public class MetaItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //SoundManager.instance.GetSFX_itemGetSFX().Play();
            InGameManager.Instance.player.GetMetaCube();
            
            ObjectPoolManager.Instance.ReturnToPool(PoolItemType.MetaItem, this.gameObject);

        }
    }
}
