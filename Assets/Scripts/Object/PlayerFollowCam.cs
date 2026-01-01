using UnityEngine;
using DG.Tweening;
using System;

public class PlayerFollowCam : MonoBehaviour
{
    GameObject camObj;
    GameObject player;

    Vector2 camPos;
    Vector2 playerPos;
    Vector3 followingDesti;

    private bool followTrigger = false;

    [SerializeField, Range(0, 2f)]
    private float follow_duration = 0.55f;

    [SerializeField, Range(0, 1f)]
    private float offset=0.9f;

    private float cur_distance;

    private void Update()
    {
        if(camObj != null && player != null)
        {
            ObserveDistance();

            if (followTrigger)
            {
                Lets_Follow();
            }
        }
    }

    public void Regist_PlayerAndCamOBJ(GameObject C, GameObject P)
    {
        camObj = C;
        player = P;
    }
    private void ObserveDistance()
    {
        camPos = camObj.transform.position;
        playerPos = player.transform.position;

        cur_distance = Vector2.Distance(camPos, playerPos);

        if(cur_distance > offset)
        {
            followTrigger = true;
        }
        else
        {
            followTrigger = false;
        }
    }
    private void Lets_Follow()
    {
        followingDesti = new Vector3(playerPos.x, playerPos.y, -10f);
        camObj.transform.DOMove(followingDesti, follow_duration, false);
    }

}
