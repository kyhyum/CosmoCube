using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombAction : MonoBehaviour
{

    private bool activation = false;
    private bool toggle = false;
    private Vector2Int myPosition;
    public void SetActive()
    {
        activation = true;
    }
    public void SetBombLocation(Vector2Int pos)
    {
        myPosition = pos;
    }

    Dictionary<Vector2Int, MapObject> OriginalMapData;

    Vector2Int upPos ;
    Vector2Int downPos ;
    Vector2Int leftPos;
    Vector2Int rightPos ;

    MapObject upObj;
    MapObject downObj;
    MapObject leftObj;
    MapObject rightObj;


    private float selfDistructionTimer = 1.2f;
    private float curTime = 0;
    public bool selfDistructionTrigger = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activation && !toggle) 
        {
            toggle = true;
            FindNearObj();
            StartCoroutine(DelayChainExplosion());
        }

        if (selfDistructionTrigger)
        {
            curTime += Time.deltaTime;


            if(curTime >= selfDistructionTimer)
            {
                GameObject prefab = Resources.Load<GameObject>("Prefabs/BombArea");
                Vector3 loc = new Vector3(myPosition.x, myPosition.y, 0);
                GameObject bombExplosionArea = Instantiate(prefab, loc, Quaternion.identity);

                SoundManager.instance.GetSFX_BombExplosion().Play();
                Destroy(this.gameObject);
            }

        }

    }

    private void FindNearObj()
    {
        OriginalMapData = GameManager.Instance.GetOriginalMapData();

        upPos = new Vector2Int(myPosition.x + 0, myPosition.y + 1);
        downPos = new Vector2Int(myPosition.x + 0, myPosition.y - 1);
        leftPos = new Vector2Int(myPosition.x - 1, myPosition.y + 0);
        rightPos = new Vector2Int(myPosition.x + 1, myPosition.y + 0);

        upObj = OriginalMapData[upPos];
        downObj = OriginalMapData[downPos];
        leftObj = OriginalMapData[leftPos];
        rightObj = OriginalMapData[rightPos];

        Debug.Log($"up:{upPos}/{upObj.Get_OBJ_TYPE()},down:{downPos}/{downObj.Get_OBJ_TYPE()},left:{leftPos}/{leftObj.Get_OBJ_TYPE()},right:{rightPos}/{rightObj.Get_OBJ_TYPE()}");

    }


    IEnumerator DelayChainExplosion()
    {
        yield return new WaitForSeconds(0.1f);
        Bomb_Active();
    }

    private void GenO2(Vector2Int pos)
    {
        GameObject O2prefab = Resources.Load<GameObject>("Prefabs/O2prefab");
        Vector2 loc = new Vector2(pos.x, pos.y);
        GameObject O2obj = Instantiate(O2prefab, loc, Quaternion.identity);
    }
    public void Bomb_Active()
    {
        if (upObj.Get_OBJ_TYPE().ToString().Contains("BOMB"))
        {
            BombAction BombScript = upObj.Get_OBJ().GetComponent<BombAction>();
            Vector2Int ObjV2Int = new Vector2Int((int)upObj.Get_OBJ().transform.position.x, (int)upObj.Get_OBJ().transform.position.y);
            BombScript.SetBombLocation(ObjV2Int);
            BombScript.SetActive();
        }
        else if (upObj.Get_OBJ_TYPE().ToString().Contains("CUBE"))
        {
            GenO2(upPos);
            Destroy(upObj.Get_OBJ());
            MapObject NoneRf = new MapObject(null, ObjectType.NONE);
            GameManager.Instance.ReplaceMapData(upPos, NoneRf);
        }

        if (downObj.Get_OBJ_TYPE().ToString().Contains("BOMB"))
        {
            BombAction BombScript = downObj.Get_OBJ().GetComponent<BombAction>();
            Vector2Int ObjV2Int = new Vector2Int((int)downObj.Get_OBJ().transform.position.x, (int)downObj.Get_OBJ().transform.position.y);
            BombScript.SetBombLocation(ObjV2Int);
            BombScript.SetActive();
        }
        else if (downObj.Get_OBJ_TYPE().ToString().Contains("CUBE"))
        {
            GenO2(downPos);
            Destroy(downObj.Get_OBJ());
            MapObject NoneRf = new MapObject(null, ObjectType.NONE);
            GameManager.Instance.ReplaceMapData(downPos, NoneRf);
        }

        if (leftObj.Get_OBJ_TYPE().ToString().Contains("BOMB"))
        {
            BombAction BombScript = leftObj.Get_OBJ().GetComponent<BombAction>();
            Vector2Int ObjV2Int = new Vector2Int((int)leftObj.Get_OBJ().transform.position.x, (int)leftObj.Get_OBJ().transform.position.y);
            BombScript.SetBombLocation(ObjV2Int);
            BombScript.SetActive();
        }
        else if (leftObj.Get_OBJ_TYPE().ToString().Contains("CUBE"))
        {
            GenO2(leftPos);
            Destroy(leftObj.Get_OBJ());
            MapObject NoneRf = new MapObject(null, ObjectType.NONE);
            GameManager.Instance.ReplaceMapData(leftPos, NoneRf);
        }

        if (rightObj.Get_OBJ_TYPE().ToString().Contains("BOMB"))
        {
            BombAction BombScript = rightObj.Get_OBJ().GetComponent<BombAction>();
            Vector2Int ObjV2Int = new Vector2Int((int)rightObj.Get_OBJ().transform.position.x, (int)rightObj.Get_OBJ().transform.position.y);
            BombScript.SetBombLocation(ObjV2Int);
            BombScript.SetActive();
        }
        else if (rightObj.Get_OBJ_TYPE().ToString().Contains("CUBE"))
        {
            GenO2(rightPos);
            Destroy(rightObj.Get_OBJ());
            MapObject NoneRf = new MapObject(null, ObjectType.NONE);
            GameManager.Instance.ReplaceMapData(rightPos, NoneRf);
        }


        MapObject noneReplacer = new MapObject(null);
        GameManager.Instance.ReplaceMapData(myPosition, noneReplacer);

        GameObject prefab = Resources.Load<GameObject>("Prefabs/BombArea");
        Vector3 loc = new Vector3(myPosition.x, myPosition.y, 0);
        GameObject bombExplosionArea = Instantiate(prefab, loc, Quaternion.identity);

        SoundManager.instance.GetSFX_BombExplosion().Play();
        Destroy(this.gameObject);
    }

}
