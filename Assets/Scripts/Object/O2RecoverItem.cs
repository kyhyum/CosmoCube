using UnityEngine;

public class O2RecoverItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {

            SoundManager.instance.GetSFX_itemGetSFX().Play();
            if(GameManager.Instance.Get_Stage_Number() != Stage_Number.Stage10)
            {
                GameManager.Instance.Increase_Player_O2(70.0f);
            }
            else
            {
                GameManager.Instance.Increase_Player_O2(35.0f);
            }
            

            Destroy(gameObject);

        }
    }
}
