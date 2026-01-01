using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class StageMapManager : MonoBehaviour
{

    //-----Variables----------////-----Variables----------//
    //-----Variables----------////-----Variables----------//

    private Dictionary<Vector2Int, MapObject> OriginalMapData = new Dictionary<Vector2Int, MapObject>();
    private Tilemap stageTileMap;
    private GameObject stageTileMapOBJ;

    //-----Methods----------////-----Methods----------//
    //-----Methods----------////-----Methods----------//
    public void Set_Stage_Map()
    {

        OriginalMapData = new Dictionary<Vector2Int, MapObject>();
        Regist_StageTileMap();
        Replace_Tile_to_Prefab();
        GameManager.Instance.SetOriginalMapData(OriginalMapData);

        StartCoroutine(DeActivateTileMap());

    } //게임매니저가 씬 로드 이후 스테이지를 세팅할때 사용하는 메서드


    
    IEnumerator DeActivateTileMap()
    {
        yield return new WaitForSeconds(5.0f);
        if (stageTileMapOBJ.activeSelf)
        {
            //stageTileMapOBJ.SetActive(false);
        }
    }
    private void Replace_Tile_to_Prefab()
    {
        BoundsInt bounds = stageTileMap.cellBounds;
        int TotalCount =0;
        int CubeCount = 0;
        int WallCount = 0;
        int NoneCount = 0;
       

        for (int x = bounds.xMin; x< bounds.xMax+1; x++)
        {
            for(int y = bounds.yMin; y < bounds.yMax+1; y++)
            {

                Vector3Int pos = new Vector3Int(x,y,0);
                TileBase tile = stageTileMap.GetTile(pos);
                /*
                if (tile == null) { Debug.Log($"PosTile // Pos:({pos.x+1},{pos.y+1}) /Tile: null"); }
                else { Debug.Log($"PosTile // Pos:({pos.x + 1},{pos.y + 1}) /Tile: {tile.name}"); }
                */
                Vector2Int correctPos = new Vector2Int(pos.x + 1, pos.y + 1);
                Vector3Int correntPos_V3 = new Vector3Int(pos.x + 1, pos.y + 1, 0);
                if(tile != null)
                {
                    TotalCount++;
                    string prefabName = tile.name;
                    
                    GameObject prefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");

                    if (prefabName.Contains("Cosmo")) //벽인 경우
                    {
                        WallCount++;
                        MapObject mapOBJ = new MapObject(null, ObjectType.WALL);
                        if (!OriginalMapData.ContainsKey(correctPos))
                        {
                            OriginalMapData.Add(correctPos, mapOBJ);
                        }
                    }
                    else if (prefabName.Contains("Bomb"))
                    {
                        Vector3 worldPos = stageTileMap.GetCellCenterWorld(pos);
                        GameObject bombInstance = Instantiate(prefab, worldPos, Quaternion.identity);
                        stageTileMap.SetTile(pos, null);

                        Classify_Prefab_Type(correctPos, bombInstance);
                    }
                    else if (prefabName.Contains("Cube")) //큐브인 경우
                    {
                        CubeCount++;
                        Vector3 worldPos = stageTileMap.GetCellCenterWorld(pos);

                        if (prefabName == "RandomColorCube_0")
                        {
                            System.Random rd = new System.Random();
                            int rdNumber = rd.Next(6);
                            if(rdNumber == 0)
                            {
                                GameObject pre = Resources.Load<GameObject>("Prefabs/RedCube_0");
                                GameObject cubeInstance = Instantiate(pre, worldPos, Quaternion.identity);
                                MapObject mobj = new MapObject(cubeInstance, ObjectType.REDCUBE);
                                stageTileMap.SetTile(pos, null);
                                OriginalMapData.Add(correctPos, mobj);
                            }
                            if (rdNumber == 1)
                            {
                                GameObject pre = Resources.Load<GameObject>("Prefabs/GreenCube_0");
                                GameObject cubeInstance = Instantiate(pre, worldPos, Quaternion.identity);
                                MapObject mobj = new MapObject(cubeInstance, ObjectType.GREENCUBE);
                                stageTileMap.SetTile(pos, null);
                                OriginalMapData.Add(correctPos, mobj);
                            }
                            if (rdNumber == 2)
                            {
                                GameObject pre = Resources.Load<GameObject>("Prefabs/BlueCube_0");
                                GameObject cubeInstance = Instantiate(pre, worldPos, Quaternion.identity);
                                MapObject mobj = new MapObject(cubeInstance, ObjectType.BLUECUBE);
                                stageTileMap.SetTile(pos, null);
                                OriginalMapData.Add(correctPos, mobj);
                            }
                            if (rdNumber == 3)
                            {
                                GameObject pre = Resources.Load<GameObject>("Prefabs/CyanCube_0");
                                GameObject cubeInstance = Instantiate(pre, worldPos, Quaternion.identity);
                                MapObject mobj = new MapObject(cubeInstance, ObjectType.CYANCUBE);
                                stageTileMap.SetTile(pos, null);
                                OriginalMapData.Add(correctPos, mobj);
                            }
                            if (rdNumber == 4)
                            {
                                GameObject pre = Resources.Load<GameObject>("Prefabs/PuppleCube_0");
                                GameObject cubeInstance = Instantiate(pre, worldPos, Quaternion.identity);
                                MapObject mobj = new MapObject(cubeInstance, ObjectType.PUPPLECUBE);
                                stageTileMap.SetTile(pos, null);
                                OriginalMapData.Add(correctPos, mobj);
                            }
                            if (rdNumber == 5)
                            {
                                GameObject pre = Resources.Load<GameObject>("Prefabs/YellowCube_0");
                                GameObject cubeInstance = Instantiate(pre, worldPos, Quaternion.identity);
                                MapObject mobj = new MapObject(cubeInstance, ObjectType.YELLOWCUBE);
                                stageTileMap.SetTile(pos, null);
                                OriginalMapData.Add(correctPos, mobj);
                            }
                        }
                        else
                        {

                            GameObject cubeInstance = Instantiate(prefab, worldPos, Quaternion.identity);
                            stageTileMap.SetTile(pos, null);

                            Classify_Prefab_Type(correctPos, cubeInstance);
                        }
                    }else if (prefabName.Contains("Blade_A"))
                    {
                        Vector3 worldPos = stageTileMap.GetCellCenterWorld(pos);
                        GameObject bladeOpenPrefab = Resources.Load<GameObject>("Prefabs/BladeFlower_Open");
                        GameObject bladeInstance = Instantiate(bladeOpenPrefab, worldPos, Quaternion.identity);
                        stageTileMap.SetTile(pos, null);
                        MapObject mapOBJ = new MapObject(null, ObjectType.NONE);
                        OriginalMapData[correctPos] = mapOBJ;
                    }
                    else if (prefabName.Contains("Blade_B"))
                    {
                        Vector3 worldPos = stageTileMap.GetCellCenterWorld(pos);
                        GameObject bladeClosePrefab = Resources.Load<GameObject>("Prefabs/BladeFlower_Close");
                        GameObject bladeInstance = Instantiate(bladeClosePrefab, worldPos, Quaternion.identity);
                        stageTileMap.SetTile(pos, null);

                        MapObject mapOBJ = new MapObject(null, ObjectType.NONE);
                        OriginalMapData[correctPos] = mapOBJ;
                    }
                }
                else //어디에도 포함되지않는 스프라이트인 경우
                {
                    NoneCount++;
                    MapObject MOBJ = new MapObject(null, ObjectType.NONE);

                    if (!OriginalMapData.ContainsKey(correctPos)) 
                    {
                        OriginalMapData.Add(correctPos, MOBJ);
                    }


                }
            }
        }

        //Debug.Log($"StageMapManager : MapSize = {((bounds.xMax - bounds.xMin) * (bounds.yMax - bounds.yMin))} /None={NoneCount}/ Prefab = {TotalCount}/ Wall = {WallCount}/ Cube = {CubeCount}");
    }
    private string Get_SpriteName(Vector3Int position)
    {
        TileBase tileBase = stageTileMap.GetTile(position);

        if(tileBase is RuleTile)
        {
            TileData tileData = new TileData();
            ((RuleTile)tileBase).GetTileData(position, stageTileMap, ref tileData);
            if(tileData.sprite != null)
            {
                return tileData.sprite.name;
            }
        }
        else if(tileBase is Tile)
        {
            return ((Tile)tileBase).sprite.name;
        }

        return "Cant find prefab with sprite name.";
    }

    private void Classify_Prefab_Type(Vector2Int pos, GameObject OBJECT)
    {
        MapObject mapOBJ;
        switch (OBJECT.name)
        {
            default:
                mapOBJ = new MapObject(OBJECT);
                break;
            case "RedCube_0(Clone)":
                mapOBJ = new MapObject(OBJECT,ObjectType.REDCUBE);
                break;
            case "BlueCube_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.BLUECUBE);
                break;
            case "GreenCube_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.GREENCUBE);
                break;
            case "CyanCube_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.CYANCUBE);
                break;
            case "PuppleCube_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.PUPPLECUBE);
                break;
            case "YellowCube_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.YELLOWCUBE);
                break;
            case "Switch_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.SWITCH);
                break;
            case "MetaCube_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.METACUBE);
                break;
            case "RedSwitch_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.REDSWITCH);
                break;
            case "BlueSwitch_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.BLUESWITCH);
                break;
            case "GreenSwitch_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.GREENSWITCH);
                break;
            case "CyanSwitch_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.CYANSWITCH);
                break;
            case "PuppleSwitch_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.PUPPLESWITCH);
                break;
            case "YellowSwitch_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.YELLOWSWITCH);
                break;
            case "RedBomb_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.REDBOMB);
                break;
            case "BlueBomb_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.BLUEBOMB);
                break;
            case "GreenBomb_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.GREENBOMB);
                break;
            case "CyanBomb_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.CYANBOMB);
                break;
            case "PuppleBomb_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.PUPPLEBOMB);
                break;
            case "YellowBomb_0(Clone)":
                mapOBJ = new MapObject(OBJECT, ObjectType.YELLOWBOMB);
                break;


        } //새로운 타입의 오브젝트는 이곳에 등록하삼

        if (!OriginalMapData.ContainsKey(pos))
        {
            OriginalMapData.Add(pos, mapOBJ);
        }

    }
    private void Regist_StageTileMap()
    {

        stageTileMapOBJ = GameObject.Find("Stage TileMap");
        if(stageTileMapOBJ == null) { Debug.Log("GM:Regist Stage Tile Map OBJ Find null"); }
        stageTileMap = stageTileMapOBJ.GetComponent<Tilemap>();
    }

}//개별요소의 위치와 타입, 실제오브젝트를 모두 갖는 맵 정보 클래스


public class MapObject
{
    private ObjectType objType;
    private GameObject obj;
    public MapObject(GameObject OBJ)
    {
        objType = ObjectType.NONE;
        obj = OBJ;
    }
    public MapObject(GameObject OBJ, ObjectType type)
    {
        objType = type;
        obj = OBJ;
    }
    public ObjectType Get_OBJ_TYPE()
    {
        return objType;
    }

    public void Set_OBJ_Type(ObjectType type)
    {
        objType = type;
    }

    public GameObject Get_OBJ()
    {
        return obj;
    }

    public string GetTypeColor(ObjectType type)
    {
        if (type.ToString().Contains("RED"))
        {
            return "R";
        }else if (type.ToString().Contains("GREEN"))
        {
            return "G";
        }else if (type.ToString().Contains("BLUE"))
        {
            return "B";
        }else if (type.ToString().Contains("CYAN"))
        {
            return "C";
        }else if (type.ToString().Contains("PUPPLE"))
        {
            return "P";
        }else if (type.ToString().Contains("YELLOW"))
        {
            return "Y";
        }else
        {
            return "N";
        }
    }

    public bool isSwitch()
    {
        if (objType.ToString().Contains("SWITCH"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}//맵에 존재하는 개별요소 클래스 (GameObject , ObjectType)
public enum ObjectType
{
    NONE =0,
    REDCUBE,
    GREENCUBE,
    BLUECUBE,
    CYANCUBE,
    PUPPLECUBE,
    YELLOWCUBE,
    SWITCH,
    METACUBE,
    REDSWITCH,
    GREENSWITCH,
    BLUESWITCH,
    CYANSWITCH,
    PUPPLESWITCH,
    YELLOWSWITCH,
    REDBOMB,
    GREENBOMB,
    BLUEBOMB,
    CYANBOMB,
    PUPPLEBOMB,
    YELLOWBOMB,
    WALL,
}//개별요소의 타입 열거형, 새로운 타입은 꼭 등록하삼