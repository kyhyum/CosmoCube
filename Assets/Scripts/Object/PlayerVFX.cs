using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField, Range(0, 2.0f)]
    private float floatingSpeed = 0.25f;

    private Vector2 floatingDirection = Vector2.up;
    public bool isUp = true;

    [SerializeField, Range(0, 2.0f)]
    private float dirChangeTimer = 1.0f;
    private float curTimer = 0;
    // Update is called once per frame
    void Update()
    {
        curTimer += Time.deltaTime;

        if(curTimer >= dirChangeTimer)
        {
            isUp = !isUp;
            curTimer = 0;
        }

        if (isUp)
        {
            floatingDirection = Vector2.up;
        }
        else
        {
            floatingDirection = Vector2.down;
        }

        this.gameObject.transform.Translate(floatingDirection * floatingSpeed * Time.deltaTime);
    }
}
