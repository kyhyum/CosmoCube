using UnityEngine;
using DG.Tweening;
public class O2Prefab : MonoBehaviour
{
    Vector2 playerLoc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerLoc = GameManager.Instance.Get_Player_Current_Pos();

        this.gameObject.transform.DOMove(playerLoc, 0.5f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.Increase_Player_O2(2f);
            Destroy(gameObject);
        }
    }
}
