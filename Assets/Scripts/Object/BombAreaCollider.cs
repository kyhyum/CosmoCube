using UnityEngine;

public class BombAreaCollider : MonoBehaviour
{
    private float curTime = 0;
    private float deathTime = 0.2f;


    private void Update()
    {
        curTime += Time.deltaTime;

        if(curTime >= deathTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.GetSFX_PlayerHitSFX().Play();
            GameManager.Instance.HitByEnemyAttack(9999);
        }else if (collision.CompareTag("Boss"))
        {
            BossAction.instance.DecreaseBossHP(500);
        }
    }
}
