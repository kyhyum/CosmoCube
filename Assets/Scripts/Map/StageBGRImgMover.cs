using UnityEngine;
using UnityEngine.UI;

public class StageBGRImgMover : MonoBehaviour
{
    GameObject[] bgrImg = new GameObject[9];

    SpriteRenderer img;

    float imgXsize;
    float imgYsize;
    Vector2 centerPos = new Vector2(0,0);

    void CalcImgSize(SpriteRenderer sr)
    {
        Vector2 spriteSize = sr.sprite.bounds.size;

        imgXsize = spriteSize.x;
        imgYsize = spriteSize.y;
    }

    void AsignImg(ref GameObject obj)
    {
        obj = new GameObject();
        obj.transform.SetParent(this.gameObject.transform);
        img = obj.AddComponent<SpriteRenderer>();
        img.sprite = Resources.Load<Sprite>("Art/UI/StageBGRimg");
        img.sortingOrder = -20;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        for(int i =0; i <9; i++)
        {
            AsignImg(ref bgrImg[i]);
        }

        CalcImgSize(img);

        SetAllCenter();

        Alignment_init();

    }
    void SetAllCenter()
    {
        for(int i =0; i<9; i++)
        {
            bgrImg[i].transform.position = centerPos;
        }
    }
    void Alignment_init()
    {
        //  0 1 2  <-Row 1
        //  3 4 5  
        //  6 7 8  <-Row 2
        //  ^   ^
        //  1   2  < Column
 

        move_Unit(bgrImg[0], 1, Vector2.left);
        move_Unit(bgrImg[0], 1, Vector2.up);

        move_Unit(bgrImg[1], 1, Vector2.up);

        move_Unit(bgrImg[2], 1, Vector2.right);
        move_Unit(bgrImg[2], 1, Vector2.up);

        move_Unit(bgrImg[3], 1, Vector2.left);

        move_Unit(bgrImg[5], 1, Vector2.right);

        move_Unit(bgrImg[6], 1, Vector2.down);
        move_Unit(bgrImg[6], 1, Vector2.left);

        move_Unit(bgrImg[7], 1, Vector2.down);

        move_Unit(bgrImg[8], 1, Vector2.down);
        move_Unit(bgrImg[8], 1, Vector2.right);

    }

    void MoveDown()
    {
        move_Unit(bgrImg[0], 3, Vector2.down);
        move_Unit(bgrImg[1], 3, Vector2.down);
        move_Unit(bgrImg[2], 3, Vector2.down);

        GameObject[] tmp = { bgrImg[0], bgrImg[1], bgrImg[2] };

        bgrImg[0] = bgrImg[3];
        bgrImg[1] = bgrImg[4];
        bgrImg[2] = bgrImg[5];
        bgrImg[3] = bgrImg[6];
        bgrImg[4] = bgrImg[7];
        bgrImg[5] = bgrImg[8];
        bgrImg[6] = tmp[0];
        bgrImg[7] = tmp[1];
        bgrImg[8] = tmp[2];

        centerPos = new Vector2(centerPos.x, centerPos.y + (imgYsize * -1));
    }
    void MoveUp()
    {
        move_Unit(bgrImg[6], 3, Vector2.up);
        move_Unit(bgrImg[7], 3, Vector2.up);
        move_Unit(bgrImg[8], 3, Vector2.up);

        GameObject[] tmp = { bgrImg[6], bgrImg[7], bgrImg[8] };

        bgrImg[6] = bgrImg[3];
        bgrImg[7] = bgrImg[4];
        bgrImg[8] = bgrImg[5];
        bgrImg[3] = bgrImg[0];
        bgrImg[4] = bgrImg[1];
        bgrImg[5] = bgrImg[2];
        bgrImg[0] = tmp[0];
        bgrImg[1] = tmp[1];
        bgrImg[2] = tmp[2];

        centerPos = new Vector2(centerPos.x, centerPos.y + (imgYsize * 1));
    }
    void MoveLeft()
    {
        move_Unit(bgrImg[2], 3, Vector2.left);
        move_Unit(bgrImg[5], 3, Vector2.left);
        move_Unit(bgrImg[8], 3, Vector2.left);

        GameObject[] tmp = { bgrImg[2], bgrImg[5], bgrImg[8] };

        bgrImg[2] = bgrImg[1];
        bgrImg[5] = bgrImg[4];
        bgrImg[8] = bgrImg[7];
        bgrImg[1] = bgrImg[0];
        bgrImg[4] = bgrImg[3];
        bgrImg[7] = bgrImg[6];
        bgrImg[0] = tmp[0];
        bgrImg[3] = tmp[1];
        bgrImg[6] = tmp[2];

        centerPos = new Vector2(centerPos.x + (imgXsize * -1), centerPos.y);
    }
    void MoveRight()
    {
        move_Unit(bgrImg[0], 3, Vector2.right);
        move_Unit(bgrImg[3], 3, Vector2.right);
        move_Unit(bgrImg[6], 3, Vector2.right);

        GameObject[] tmp = { bgrImg[0], bgrImg[3], bgrImg[6] };

        bgrImg[0] = bgrImg[1];
        bgrImg[3] = bgrImg[4];
        bgrImg[6] = bgrImg[7];
        bgrImg[1] = bgrImg[2];
        bgrImg[4] = bgrImg[5];
        bgrImg[7] = bgrImg[8];
        bgrImg[2] = tmp[0];
        bgrImg[5] = tmp[1];
        bgrImg[8] = tmp[2];


        centerPos = new Vector2(centerPos.x + (imgXsize * 1), centerPos.y);
    }


    void move_Unit(GameObject target, int multiplyier, Vector2 dir)
    {
        float moveDistanceX = multiplyier * dir.x * imgXsize;
        float moveDistanceY = multiplyier * dir.y * imgYsize;

        Vector2 moveDistance = new Vector2(moveDistanceX, moveDistanceY);
        Vector2 curTargetPos = target.transform.position;
        Vector2 moveDestination = new Vector2(curTargetPos.x + moveDistance.x, curTargetPos.y + moveDistance.y);

        target.transform.position = moveDestination;
    }


    Vector2 playerCurLoc;

    // Update is called once per frame
    void Update()
    {
        playerCurLoc = GameManager.Instance.Get_Player_Current_Pos();

        if(playerCurLoc.x > centerPos.x + (imgXsize/2)) //플레이어가 센터화면의 우측면을 넘어간경우
        {
            MoveRight();
        }
        else if(playerCurLoc.x < centerPos.x - (imgXsize / 2)) //플레이어가 센터화면의 좌측면을 넘어간경우
        {
            MoveLeft();
        }
        else if (playerCurLoc.y > centerPos.y + (imgYsize/2)) //플레이어가 센터화면의 상측면을 넘어간경우
        {
            MoveUp();
        }
        else if (playerCurLoc.y < centerPos.y - (imgYsize / 2))//플레이어가 센터화면의 하측면을 넘어간경우
        {
            MoveDown();
        }
    }
}
