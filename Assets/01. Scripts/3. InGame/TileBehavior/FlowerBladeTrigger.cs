using UnityEngine;

public class FlowerBladeTrigger : MonoBehaviour
{
    public bool isTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isTrigger)
        {
            isTrigger = false;
            // SoundManager.instance.GetSFX_PlayerHitSFX().Play();
            InGameManager.Instance.player.ReduceHp();
        }
    }

    void OnEnable()
    {
        isTrigger = true;
    }
}
