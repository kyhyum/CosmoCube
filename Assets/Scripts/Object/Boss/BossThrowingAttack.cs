using DG.Tweening;
using UnityEngine;

public class BossThrowingAttack : MonoBehaviour
{
    private float curTime = 0;
    private float deathTime = 3.0f;

    private float rotateAmount = 5.0f;
    private float moveSpeed;
    private Vector2 moveDir;

    private bool isOnOff = false;
    private float onOffTimer = 1.2f;
    private float curOnOffTimer = 0;
    private bool isOn = true;
    private Tween RotateTween;

    private Sprite stopSprite;
    private Sprite rotateSprite;
    private SpriteRenderer SR;
    public void SetIsOnOFF(bool onOff)
    {
        isOnOff = onOff;
        deathTime = 12f;
    }
    public void SetSpeedAndDir(float sp, Vector2 dir)
    {
        moveSpeed = sp;
        moveDir = dir;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stopSprite = Resources.Load<Sprite>("Art/IMG/FlowerBlade_B");
        rotateSprite = Resources.Load<Sprite>("Art/IMG/FlowerBlade_A");
        SR = this.gameObject.GetComponent<SpriteRenderer>();
        SR.sprite = rotateSprite;

        RotateTween = this.gameObject.transform.DORotate(new Vector3(0, 0, 180), 0.25f).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {

        curTime += Time.deltaTime;

        if (curTime >= deathTime)
        {
            Destroy(gameObject);
        }

        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);

        if (isOnOff)
        {
            Debug.Log("Call 2");
            curOnOffTimer += Time.deltaTime;

            if(curOnOffTimer >= onOffTimer)
            {
                Debug.Log("Call 3");
                curOnOffTimer = 0;

                if (!isOn)
                {
                    SR.sprite = rotateSprite;
                }
                else
                {
                    SR.sprite = stopSprite;
                }

                isOn = !isOn;


            }
        }
        
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isOn)
            {
                SoundManager.instance.GetSFX_PlayerHitSFX().Play();
                GameManager.Instance.HitByEnemyAttack(20);

            }

        }
        else if (collision.CompareTag("Meta"))
        {
            Destroy(gameObject);
        }
    }
}
