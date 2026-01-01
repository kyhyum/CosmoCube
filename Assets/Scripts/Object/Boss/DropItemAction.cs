using DG.Tweening;
using UnityEngine;

public class DropItemAction : MonoBehaviour
{
    private float curTime = 0;

    private float FirstTime = 4.5f;
    private float SecondTime= 9f;

    private float DeathTime = 13.5f;

    private float RandomXtensor = 0;
    private float RandomYtensor = 0;

    private Vector2 moveDir;
    private float itemMoveSpeed = 2.0f;

    private void ChangeMoveSpeed(float amount)
    {
        itemMoveSpeed = amount;
    }

    private void ChangeMoveDir(Vector2 dir)
    {
        moveDir = dir;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, 180), 3f).OnComplete(
            () =>
            {
                transform.DORotate(new Vector3(0, 0, 360), 3f).OnComplete(
                    () =>
                    {
                        transform.DORotate(new Vector3(0, 0, 0), 0.1f);
                        transform.DORotate(new Vector3(0, 0, 180), 3f);
                    });
            });
    }


    private void MakeDir_DownLeft_Random()
    {
        RandomXtensor = Random.Range(0.15f, 0.5f);
        RandomYtensor = Random.Range(0.3f, 0.7f);

        RandomXtensor = -RandomXtensor;
        RandomYtensor = -RandomYtensor;
        moveDir = new Vector2(RandomXtensor, RandomYtensor);
        moveDir = moveDir.normalized;
    }

    private void MakeDir_UpLeft_Random()
    {
        RandomXtensor = Random.Range(0.15f, 0.5f);
        RandomXtensor = -RandomXtensor;
        RandomYtensor = Random.Range(0.3f, 0.7f);
        moveDir = new Vector2(RandomXtensor, RandomYtensor);
        moveDir = moveDir.normalized;
    }



    private bool toggle_1 = true;
    private bool toggle_2 = true;
    private bool toggle_3 = true;

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;

        if(curTime > 0 && curTime < FirstTime && toggle_1)
        {
            toggle_1 = false;
            MakeDir_DownLeft_Random();
        }

        if(curTime > FirstTime && curTime < SecondTime && toggle_2)
        {
            toggle_2 = false;
            MakeDir_UpLeft_Random();
        }
        if (curTime > SecondTime && curTime < DeathTime && toggle_3)
        {
            toggle_3 = false;
            MakeDir_DownLeft_Random();
        }
        if (curTime >= DeathTime)
        {
            Destroy(gameObject);
        }


        transform.Translate(itemMoveSpeed * moveDir * Time.deltaTime, Space.World   );


    }


}
