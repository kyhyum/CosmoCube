using UnityEngine;

public class PlayerBulletAction : MonoBehaviour
{
    private float speed = 15.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Vector2.right * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            BossAction.instance.DecreaseBossHP(110.0f);
        }else if (collision.CompareTag("Block"))
        {

        }


        Destroy(gameObject);
    }

}
