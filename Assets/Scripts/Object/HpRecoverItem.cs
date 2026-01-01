using UnityEngine;

public class HpRecoverItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            SoundManager.instance.GetSFX_itemGetSFX().Play();

            if(GameManager.Instance.Get_Stage_Number() != Stage_Number.Stage10)
            {

                GameManager.Instance.Increase_Player_HP(70);
            }
            else
            {

                GameManager.Instance.Increase_Player_HP(15);
            }


            Destroy(gameObject);

        }
    }
}
