using UnityEngine;

public class O2RecoverItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //SoundManager.instance.GetSFX_itemGetSFX().Play();
            if(DataManager.Instance.currentGameStage != GameStage.Stage10)
            {
                InGameManager.Instance.player.RecoveryOxygen(70.0f);
            }
            else
            {
                InGameManager.Instance.player.RecoveryOxygen(35.0f);
            }
            
            ObjectPoolManager.Instance.ReturnToPool(PoolItemType.OxygenRecoveryItem, this.gameObject);

        }
    }
}
