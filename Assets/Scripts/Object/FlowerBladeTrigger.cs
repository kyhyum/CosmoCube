using UnityEngine;

public class FlowerBladeTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.GetSFX_PlayerHitSFX().Play();
            GameManager.Instance.HitByEnemyAttack(20);
        }
    }
}
