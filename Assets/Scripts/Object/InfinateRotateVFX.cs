using DG.Tweening;
using UnityEngine;

public class InfinateRotateVFX : MonoBehaviour
{
    private float rotateAmount = 20.0f;
    private Vector3 curRot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.forward*rotateAmount * Time.deltaTime); 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("onTrigger");

        if (collision.CompareTag("Player"))
        {
            Debug.Log("onTrigger Player");
            GameManager.Instance.PortalCollide();
        }
        else
        {
            Debug.Log("onTrigger else");
        }
    }
}
