
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Reflection;
using TMPro;

public class GameManager : MonoBehaviour
{
    //--------- Awake()-----------////--------- Awake()-----------//
    //--------- Awake()-----------////--------- Awake()-----------//
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log($"SceneName : {scene.name}");
        if (scene.name == "Menu") 
        {
            Stage_Number = Stage_Number.Menu;
            LoadMenu();
        }
        else if(scene.name == "Stage1")
        {
            Stage_Number = Stage_Number.Stage1;
            OriginalMapData = new Dictionary<Vector2Int, MapObject>();
            LoadStage(Stage_Number);
        }else if(scene.name == "Stage2")
        {
            Stage_Number = Stage_Number.Stage2;
            OriginalMapData = new Dictionary<Vector2Int, MapObject>();
            LoadStage(Stage_Number);
        }
        else if(scene.name == "Stage3")
        {
            Stage_Number = Stage_Number.Stage3;
            OriginalMapData = new Dictionary<Vector2Int, MapObject>();
            LoadStage(Stage_Number);
        }else if(scene.name == "Stage4")
        {
            Stage_Number = Stage_Number.Stage4;
            OriginalMapData = new Dictionary<Vector2Int, MapObject>();
            LoadStage(Stage_Number);
        }else if(scene.name == "Stage5")
        {
            Stage_Number = Stage_Number.Stage5;
            OriginalMapData = new Dictionary<Vector2Int, MapObject>();
            LoadStage(Stage_Number);
        }else if(scene.name == "Stage6")
        {
            Stage_Number = Stage_Number.Stage6;
            OriginalMapData = new Dictionary<Vector2Int, MapObject>();
            LoadStage(Stage_Number);
        }else if(scene.name == "Stage7")
        {
            Stage_Number = Stage_Number.Stage7;
            OriginalMapData = new Dictionary<Vector2Int, MapObject>();
            LoadStage(Stage_Number);
        }else if(scene.name == "Stage8")
        {
            Stage_Number = Stage_Number.Stage8;
            OriginalMapData = new Dictionary<Vector2Int, MapObject>();
            LoadStage(Stage_Number);
        }else if(scene.name == "Stage9")
        {
            Stage_Number = Stage_Number.Stage9;
            OriginalMapData = new Dictionary<Vector2Int, MapObject>();
            LoadStage(Stage_Number);
        }else if(scene.name == "Stage10")
        {
            Stage_Number = Stage_Number.Stage10;
            OriginalMapData = new Dictionary<Vector2Int, MapObject>();
            LoadStage(Stage_Number);
        }
    }


    //---------All Variable------------////---------All Variable------------//
    //---------All Variable------------////---------All Variable------------//
    //---------All Variable------------////---------All Variable------------//

    //>>>> Player
    //     >> Sprite
    private GameObject player;
    private GameObject playerIMGObj;
    private Sprite playerSprite;
    private SpriteRenderer playerSpriteRenderer;
    //     >> Movement
    private Vector2 playerCurrentPosition = new Vector2(0,0);
    private Vector2 playerMoveDestination = new Vector2(0, 0);
    private bool isMoving = false;
    private bool isOtherMoveBtnUsing = false;
    private Move_Direction move_Direction = Move_Direction.None;
    private bool isHolding = false;
    private bool canMove = false;
    [SerializeField, Range(0,1f)]
    private float moveDuration = 0.15f;
    //     >> Cam & VFX
    private PlayerFollowCam PFC;
    private PlayerVFX player_VFX;
    //     >> HP , O2
    private float Player_Current_HP;
    private float Player_Max_HP;
    private float Player_Current_O2;
    private float Player_Max_O2;
    private bool isHPZero = false;
    private bool isO2Zero = false;
    private bool Invincible = false;
    //      >> Life
    private int player_current_life;
    private int player_max_life = 0;
    
    private GameObject life_1_back;
    private GameObject life_1_img;
    private GameObject life_2_back;
    private GameObject life_2_img;

    private void Regist_life()
    {
        player_current_life = player_max_life;

        life_1_back = GameObject.Find("Life_Back_1");
        life_2_back = GameObject.Find("Life_Back_2");
        life_1_img = GameObject.Find("Life_img_1");
        life_2_img = GameObject.Find("Life_img_2");

        Set_life_indicator(player_current_life);

    }
    public void Set_life_indicator(int i)
    {
        switch (i)
        {
            case 0:
                life_1_back.SetActive(false);
                life_2_back.SetActive(false);
                life_1_img.SetActive(false);
                life_2_img.SetActive(false);
                break;
            case 1:
                life_1_back.SetActive(true);
                life_1_img.SetActive(true);
                life_2_back.SetActive(false);
                life_2_img.SetActive(false);
                break;
            case 2:
                life_1_back.SetActive(true);
                life_1_img.SetActive(true);
                life_2_back.SetActive(true);
                life_2_img.SetActive(true);
                break;
        }
    }
    public void Set_life_max(int i)
    {
        player_max_life= i;
    }
    public int Get_life_current()
    {
        return player_current_life;
    }
    public int Get_life_max()
    {
        return player_max_life;
    }
    public void Plus_life()
    {
        if (Get_life_current() == Get_life_max())
        {
            return;
        }
        else
        {
            player_current_life++;

            if(player_current_life == 1)
            {
                life_1_img.SetActive(true);
                life_2_img.SetActive(false);
            }
            else if(player_current_life == 2)
            {
                life_1_img.SetActive(true);
                life_2_img.SetActive(true);
            }
        }
    }
    public void Minus_life()
    {
        player_current_life--;
        if(player_current_life <= 0)
        {
            player_current_life = 0;
        }
    }

    //>>>> Stage UI
    private Button LeftMoveBtn;
    private Button RightMoveBtn;
    private Button UpMoveBtn;
    private Button DownMoveBtn;
    private Button ShootBtn;
    private Button MetaCubeBtn;
    private Button SetupBtn;
    private Button Setup_ContinueBtn;
    private Button Setup_ToMenuBtn;
    private GameObject Setup_Panel;
    private Image HP_Bar_img;
    private Image O2_Bar_img;
    GameObject gameOverPanelTxt;
    CanvasGroup GOPcanvasGroup;
    GameObject gameOverPanelObj;
    GameObject clearPanel;
    CanvasGroup clearPanelCG;

    //  >>MetaCube UI
    GameObject metaBarObj;
    static string metaBarObj_name = "Meta Bar";
    GameObject metaTxtObj;
    static string metaTxt_name = "Meta Counter Txt";
    private int current_meta_count = 0;
    private int init_meta_count = 0;
    private int max_meta_count = 0;
    private float metaChargingSpeed = 2f;

    //>>>> Menu UI
    private GameObject descriptionTxtOBJ;

    //>>>> Original Map Data
    private Dictionary<Vector2Int, MapObject> OriginalMapData;

    //>>>> Color Match
    private List<MapObject> ColorMatchList = new List<MapObject>();

    //>>>> Stage Number
    private Stage_Number Stage_Number;

    //>>>> StageManager
    private StageMapManager SMM;
    private GameObject Stage_Tile_Map;



    //--------- Methods-----------////--------- Methods-----------//
    //--------- Methods-----------////--------- Methods-----------//
    //--------- Methods-----------////--------- Methods-----------//

    //>>>> Register
    private void Register_PlayerSprite()
    {
        player = GameObject.Find("Player");
        if(player == null) { Debug.Log("GameManager : player object cant find"); return; }

        playerIMGObj = new GameObject();
        playerIMGObj.name = "Player IMG Obj";
        playerIMGObj.transform.SetParent(player.transform);
        playerIMGObj.AddComponent<SpriteRenderer>();

        playerSprite = Resources.Load<Sprite>("Art/IMG/PlayerIMG");
        playerSpriteRenderer = playerIMGObj.GetComponent<SpriteRenderer>();

        if(playerSpriteRenderer == null) { Debug.Log("GameManager : playerSpriteRenderer cant find"); return; }
        if (playerSprite == null) { Debug.Log("GameManager : playerSprite cant find"); return; }
        else
        { 
            playerSpriteRenderer.sprite = playerSprite;
            playerSpriteRenderer.sortingOrder = 50;
        }
    }
    private void Register_PlayerPosition()
    {
        if(player != null)
        {
            playerCurrentPosition = player.transform.position;
            SetPlayerCurrentPosition(0, 0);
        }
        else
        {
            //Debug.Log("GameManager : player position error : player is null");
            return;
        }
    }
    private void Register_Btn(ref Button btn , string FindOBJname)
    {
        GameObject BtnOBJ = GameObject.Find(FindOBJname);
        if (BtnOBJ == null) 
        { 
            //Debug.Log("GM : Register Buttons fail :"+ FindOBJname+" is null");
            return;
        }
        else 
        {
            //Debug.Log(BtnOBJ.name+"is registered");
            btn = BtnOBJ.GetComponent<Button>(); 
        }
    }

    private void Register_Trigger_to_Move_Buttons(Button Btn, string methodName)
    {
        
        if(Btn == null) { Debug.Log("GM : Register UI Buttons fail : Btn is null"); return; }

        Register_UI_Btn_Trigger(Btn, EventTriggerType.PointerDown, methodName);
        Register_UI_Btn_Trigger(Btn, EventTriggerType.PointerDown, "Btn_down");
        Register_UI_Btn_Trigger(Btn, EventTriggerType.PointerUp, "Btn_up");
    }
    private void Register_Trigger_to_Other_Buttons(Button Btn, string methodName)
    {
        if (Btn == null) { Debug.Log("GM : Register UI Buttons fail : Btn is null"); return; }
        
        Register_UI_Btn_Trigger(Btn, EventTriggerType.PointerDown, methodName);        
    }
    private void Register_UI_Btn_Trigger(Button bt, EventTriggerType ETT, string methodName)
    {
        EventTrigger ET = bt.GetComponent<EventTrigger>();
        if (ET == null)
        {
            ET = bt.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = ETT;
        entry.callback.AddListener((data) =>
        {
            switch (methodName)
            {
                case "Left_Click":
                    Left_Click((PointerEventData)data);
                    break;
                case "Right_Click":
                    Right_Click((PointerEventData)data);
                    break;
                case "Up_Click":
                    Up_Click((PointerEventData)data);
                    break;
                case "Down_Click":
                    Down_Click((PointerEventData)data);
                    break;
                case "Btn_down":
                    Btn_down((PointerEventData)data);
                    break;
                case "Btn_up":
                    Btn_up((PointerEventData)data);
                    break;
                case "Setup_Btn":
                    Setup_Btn((PointerEventData)data);
                    break;
                case "Setup_To_Menu_Btn":
                    Setup_To_Menu_Btn((PointerEventData)data);
                    break;
                case "Setup_Continue_Btn":
                    Setup_Continue_Btn((PointerEventData)data);
                    break;
                case "Shoot_Btn_Click":
                    Shoot_Btn_Click((PointerEventData)data);
                    break;
                case "Meta_Cube_Btn_Click":
                    Meta_Cube_Btn_Click((PointerEventData)data);
                    break;
            }
            
        });
        ET.triggers.Add(entry);
    } //새로운 BTN 메소드 추가시 스위치에 등록 필요
    private void Register_UI_Other_Trigger(GameObject bt, EventTriggerType ETT, string methodName)
    {
        EventTrigger ET = bt.GetComponent<EventTrigger>();
        if (ET == null)
        {
            ET = bt.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = ETT;
        entry.callback.AddListener((data) =>
        {
            switch (methodName)
            {
                case "MainMenu_Panel_Click":
                    MainMenu_Panel_Click((PointerEventData)data);
                    break;
            }

        });
        ET.triggers.Add(entry);
    } //새로운 UI 메소드 추가시 스위치에 등록 필요

    //>>>> Player Movement
    private void SetPlayerCurrentPosition(int x, int y)
    {
        playerCurrentPosition = new Vector2(x, y);
        player.transform.position = playerCurrentPosition;
    }
    public void Left_Click(PointerEventData data)
    {
        SoundManager.instance.GetSFX_Tick().Play();

        if (!isOtherMoveBtnUsing) //이동버튼 사용중이면 다른 이동버튼 작동불가
        {
            if (Stage_Number != Stage_Number.Stage10)
            {
                playerSpriteRenderer.flipX = true;
            }
            
            isOtherMoveBtnUsing = true;

            move_Direction = Move_Direction.Left;
            Player_Move_Order(move_Direction);
        }        
    }
    public void Right_Click(PointerEventData data)
    {
        SoundManager.instance.GetSFX_Tick().Play();
        if (!isOtherMoveBtnUsing) //이동버튼 사용중이면 다른 이동버튼 작동불가
        {
            
            playerSpriteRenderer.flipX = false;

            isOtherMoveBtnUsing = true;

            move_Direction = Move_Direction.Right;
            Player_Move_Order(move_Direction);
        }
    }
    public void Up_Click(PointerEventData data)
    {
        SoundManager.instance.GetSFX_Tick().Play();
        if (!isOtherMoveBtnUsing) //이동버튼 사용중이면 다른 이동버튼 작동불가
        {
            isOtherMoveBtnUsing = true;

            move_Direction = Move_Direction.Up;
            Player_Move_Order(move_Direction);
        }
    }
    public void Down_Click(PointerEventData data)
    {
        SoundManager.instance.GetSFX_Tick().Play();
        if (!isOtherMoveBtnUsing) //이동버튼 사용중이면 다른 이동버튼 작동불가
        {
            isOtherMoveBtnUsing = true;

            move_Direction = Move_Direction.Down;
            Player_Move_Order(move_Direction);
        }
    }
    public void Btn_down(PointerEventData data)
    {
        isHolding = true;
    }
    public void Btn_up(PointerEventData data)
    {
        isHolding = false;
    }
    private float Float_To_Int_Round(float number)
    {
        float convert = (int)Math.Round(number);
        return convert;
    }
    public void Player_Current_Position_Int()
    {
        playerCurrentPosition.x = Float_To_Int_Round(player.transform.position.x);
        playerCurrentPosition.y = Float_To_Int_Round(player.transform.position.y);
    }
    private void Set_Player_Move_Destination(Move_Direction md)
    {
        if (md == Move_Direction.Left)
        {
            playerMoveDestination = playerCurrentPosition + Vector2.left;
        }
        else if(md == Move_Direction.Right)
        {
            playerMoveDestination = playerCurrentPosition + Vector2.right;
        }
        else if(md == Move_Direction.Up)
        {
            playerMoveDestination = playerCurrentPosition + Vector2.up;
        }
        else if(md == Move_Direction.Down)
        {
            playerMoveDestination = playerCurrentPosition + Vector2.down;
        }
        else
        {
            playerMoveDestination = playerCurrentPosition;
            Debug.Log("GameManager : Set_Player_Move_Destination Wrong.");
            return;
        }
    }
    private void Lets_Move(Move_Direction MD)
    {
        
        Player_Current_Position_Int(); //약간의 위치오차있으면 교정
        Set_Player_Move_Destination(MD); //현재위치에서 누른 버튼 방향으로 목적지 설정
        CanMoveValidate(MD);
        
        if (canMove)
        {
            SoundManager.instance.GetSFX_PlayerMovement().Play();
            player.transform.DOMove(playerMoveDestination, moveDuration).OnComplete(() =>
            {
                if (isHolding)
                {
                    SoundManager.instance.GetSFX_PlayerMoveSustain().Play();
                    Lets_Move(MD);
                }
                else
                {
                    
                    MD = Move_Direction.None;
                    isMoving = false;
                    isOtherMoveBtnUsing = false;
                }
            });
        }        
    }
    private void CanMoveValidate(Move_Direction MD)
    {
        int ValidateX = 0;
        int ValidateY = 0;

        switch (MD)
        {
            case Move_Direction.Left:
                
                ValidateX = -1;
                ValidateY = 0;
                break;

            case Move_Direction.Right:
                
                ValidateX = 1;
                ValidateY = 0;
                break;

            case Move_Direction.Up:
                
                ValidateX = 0;
                ValidateY = 1;
                break;

            case Move_Direction.Down:
                
                ValidateX = 0;
                ValidateY = -1;
                break;
        }
        
        Vector2Int playerNearPos = new Vector2Int((int)playerCurrentPosition.x+ValidateX, (int)playerCurrentPosition.y+ValidateY);
        
        if (OriginalMapData[playerNearPos].Get_OBJ_TYPE() == ObjectType.NONE)
        {
            canMove = true;
        }
        else
        {
            isOtherMoveBtnUsing = false;
            canMove = false;
        }
    }
    private void Player_Move_Order(Move_Direction MD)
    {
        
        if (!isMoving) // 움직이지 않을때 작동
        {
            Player_Current_Position_Int(); //약간의 위치오차있으면 교정
            Set_Player_Move_Destination(MD);
            CanMoveValidate(MD);
            
            if (canMove)
            {
                Lets_Move(MD); //버튼 홀드를 고려한 실제 이동 시작
            }
        }
        else
        {
            isOtherMoveBtnUsing = false;
        }
    }
    public Vector2 Get_Player_Current_Pos()
    {
        return player.transform.position;
    }

    //>>>> Map Data
    public void SetOriginalMapData(Dictionary<Vector2Int, MapObject> dic)
    {
        OriginalMapData = dic;
    }
    public Dictionary<Vector2Int, MapObject> GetOriginalMapData()
    {
        return OriginalMapData;
    }
    public void DeleteMapData(Vector2Int pos)
    {
        if (OriginalMapData.ContainsKey(pos) == true)
        {
            OriginalMapData.Remove(pos);
        }
    }
    public void ReplaceMapData(Vector2Int pos, MapObject targetObj)
    {
        if (OriginalMapData[pos].Get_OBJ() != null)
        {
            Destroy(OriginalMapData[pos].Get_OBJ());
        }
        //OriginalMapData.Remove(pos);
        OriginalMapData[pos] = targetObj;

        //Vector3Int v3Int = new Vector3Int(pos.x, pos.y, 0);
        //Instantiate(targetObj.Get_OBJ(), v3Int, Quaternion.identity);
    }
    public MapObject GetMapObject(Vector2Int position)
    {
        return OriginalMapData[position];
    }


    // >>>> Boss Map Pattern
    public void ClearMap()
    {
        Vector2Int key = new Vector2Int(0, 0);
        for(int i = -4; i<11; i++) //Map horizontal coordination -4~10
        {
            for(int j = -3; j<4; j++)
            {
                if (i == -4 && j == 3) { }
                else if (i == -4 && j == 2) { }
                else if (i == -4 && j == -2) { }
                else if (i == -4 && j == -3) { }
                else if (i == -3 && j == 3) { }
                else if (i == -3 && j == -3) { }
                else
                {
                    key = new Vector2Int(i, j);
                    MapObject mapobj = new MapObject(null, ObjectType.NONE);
                    if (OriginalMapData[key].Get_OBJ() != null) { Destroy(OriginalMapData[key].Get_OBJ()); }

                    OriginalMapData[key] = mapobj;
                }

            }
        }
    }  //Clear Map Data & object in the player moving area. (not wall)
    public void MakeBombWall() // make vertical Line of Bomb(not regular bomb, boss bomb prefab) in front of Boss's left side (1 distance)
    {
        for(int i = 9; i<11; i++) // X coord 9~ 10
        {
            for(int j =-3; j<4; j++) // Y coord -3~3
            {
                //Debug.Log("Make Bomb");
                GameObject bossBombPrefab = Resources.Load<GameObject>("Prefabs/Boss/BossBomb/RedBossBomb");
                


                Vector2 makingPos = new Vector2(i, j-7);
                GameObject prefab = Instantiate(bossBombPrefab, makingPos, Quaternion.identity);

                Vector2 movePos = new Vector2(i, j);
                prefab.transform.DOMove(movePos, 1.0f);

                MapObject bossBombMapObj = new MapObject(prefab, ObjectType.REDBOMB);

                Vector2Int key = new Vector2Int(i, j);
                OriginalMapData[key] = bossBombMapObj;
            }
        }
        GameObject findCube;
        ObjectType objType;

        for (int i = -2; i < 3; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, 6);

            switch (randomIndex)
            {
                case 0:
                    findCube = Resources.Load<GameObject>("Prefabs/RedCube_0");
                    objType = ObjectType.REDCUBE;
                    break;
                case 1:
                    findCube = Resources.Load<GameObject>("Prefabs/GreenCube_0");
                    objType = ObjectType.GREENCUBE;
                    break;
                case 2:
                    findCube = Resources.Load<GameObject>("Prefabs/BlueCube_0");
                    objType = ObjectType.BLUECUBE;
                    break;
                case 3:
                    findCube = Resources.Load<GameObject>("Prefabs/CyanCube_0");
                    objType = ObjectType.CYANCUBE;
                    break;
                case 4:
                    findCube = Resources.Load<GameObject>("Prefabs/PuppleCube_0");
                    objType = ObjectType.PUPPLECUBE;
                    break;
                case 5:
                    findCube = Resources.Load<GameObject>("Prefabs/YellowCube_0");
                    objType = ObjectType.YELLOWCUBE;
                    break;
                default:
                    findCube = Resources.Load<GameObject>("Prefabs/RedCube_0");
                    objType = ObjectType.REDCUBE;
                    break;
            }
            Vector2 makingPos = new Vector2(11, i);
            Vector2 movePos = new Vector2(8, i);

            GameObject cubePrefab = Instantiate(findCube, makingPos, Quaternion.identity);
            cubePrefab.transform.DOMove(movePos, 1.5f);

            MapObject MOBJ = new MapObject(cubePrefab, objType);
            Vector2Int key = new Vector2Int(8, i);
            OriginalMapData[key] = MOBJ;
        }
    }
    public void BombWallThrow(Vector2Int startPos, Vector2 endPos)
    {

        Vector2Int endPosV2 = new Vector2Int((int)endPos.x, (int)endPos.y);
        OriginalMapData[startPos].Get_OBJ().GetComponent<BombAction>().SetBombLocation(endPosV2);
        OriginalMapData[startPos].Get_OBJ().GetComponent<BombAction>().selfDistructionTrigger = true;

        OriginalMapData[startPos].Get_OBJ().transform.DOMove(endPos, 1.5f);
        MapObject mobj = new MapObject(null);
        OriginalMapData[startPos] = mobj;
    }
    public void ClearCubeWall()
    {
        for(int i = -2; i < 3; i++)
        {

            MapObject nullObj = new MapObject(null);
            Vector2Int key = new Vector2Int(8, i);

            if (OriginalMapData[key].Get_OBJ() != null)
            {
                Vector2 movePos = new Vector2(14, i);
                OriginalMapData[key].Get_OBJ().transform.DOMove(movePos, 1.0f).OnComplete(
                    () =>
                    {
                        Destroy(OriginalMapData[key].Get_OBJ());
                    });
                OriginalMapData[key] = nullObj;
            }
            

        }
    }
    
    public void MakeCubeUpDown()
    {
        GameObject findCube;
        ObjectType objType;
        for (int i =-2; i<9; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, 6);
            switch (randomIndex)
            {
                case 0:
                    findCube = Resources.Load<GameObject>("Prefabs/RedCube_0");
                    objType = ObjectType.REDCUBE;
                    break;
                case 1:
                    findCube = Resources.Load<GameObject>("Prefabs/GreenCube_0");
                    objType = ObjectType.GREENCUBE;
                    break;
                case 2:
                    findCube = Resources.Load<GameObject>("Prefabs/BlueCube_0");
                    objType = ObjectType.BLUECUBE;
                    break;
                case 3:
                    findCube = Resources.Load<GameObject>("Prefabs/CyanCube_0");
                    objType = ObjectType.CYANCUBE;
                    break;
                case 4:
                    findCube = Resources.Load<GameObject>("Prefabs/PuppleCube_0");
                    objType = ObjectType.PUPPLECUBE;
                    break;
                case 5:
                    findCube = Resources.Load<GameObject>("Prefabs/YellowCube_0");
                    objType = ObjectType.YELLOWCUBE;
                    break;
                default:
                    findCube = Resources.Load<GameObject>("Prefabs/RedCube_0");
                    objType = ObjectType.REDCUBE;
                    break;
            }
            Vector2 makingPos = new Vector2(i, 5);
            Vector2 movePos = new Vector2(i, 3);

            GameObject cubePrefab = Instantiate(findCube, makingPos, Quaternion.identity);
            cubePrefab.transform.DOMove(movePos, 1.5f);

            MapObject MOBJ = new MapObject(cubePrefab, objType);
            Vector2Int key = new Vector2Int(i, 3);
            OriginalMapData[key] = MOBJ;
        }

        for (int i = -2; i < 9; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, 6);
            switch (randomIndex)
            {
                case 0:
                    findCube = Resources.Load<GameObject>("Prefabs/RedCube_0");
                    objType = ObjectType.REDCUBE;
                    break;
                case 1:
                    findCube = Resources.Load<GameObject>("Prefabs/GreenCube_0");
                    objType = ObjectType.GREENCUBE;
                    break;
                case 2:
                    findCube = Resources.Load<GameObject>("Prefabs/BlueCube_0");
                    objType = ObjectType.BLUECUBE;
                    break;
                case 3:
                    findCube = Resources.Load<GameObject>("Prefabs/CyanCube_0");
                    objType = ObjectType.CYANCUBE;
                    break;
                case 4:
                    findCube = Resources.Load<GameObject>("Prefabs/PuppleCube_0");
                    objType = ObjectType.PUPPLECUBE;
                    break;
                case 5:
                    findCube = Resources.Load<GameObject>("Prefabs/YellowCube_0");
                    objType = ObjectType.YELLOWCUBE;
                    break;
                default:
                    findCube = Resources.Load<GameObject>("Prefabs/RedCube_0");
                    objType = ObjectType.REDCUBE;
                    break;
            }
            Vector2 makingPos = new Vector2(i, -5);
            Vector2 movePos = new Vector2(i, -3);

            GameObject cubePrefab = Instantiate(findCube, makingPos, Quaternion.identity);
            cubePrefab.transform.DOMove(movePos, 1.5f);

            MapObject MOBJ = new MapObject(cubePrefab, objType);
            Vector2Int key = new Vector2Int(i, -3);
            OriginalMapData[key] = MOBJ;
        }
    }
    public void UndoCubeUpDown()
    {

        for(int i=-2; i < 9; i++)
        {
            int j = 3;
            int k = -3;

            Vector2Int key1 = new Vector2Int(i, j);
            Vector2Int key2 = new Vector2Int(i, k);

            Vector2 movePos1 = new Vector2(i, 5);
            Vector2 movePos2 = new Vector2(i, -5);

            if (OriginalMapData[key1].Get_OBJ() != null)
            {
                MapObject nullObj = new MapObject(null);
                OriginalMapData[key1].Get_OBJ().transform.DOMove(movePos1, 1f).OnComplete(
                    () =>
                    {
                        Destroy(OriginalMapData[key1].Get_OBJ());
                    });
                OriginalMapData[key1] = nullObj;
            }
            if (OriginalMapData[key2].Get_OBJ() != null)
            {
                MapObject nullObj = new MapObject(null);
                OriginalMapData[key2].Get_OBJ().transform.DOMove(movePos2, 1f).OnComplete(
                    () =>
                    {
                        Destroy(OriginalMapData[key2].Get_OBJ());
                    });
                OriginalMapData[key1] = nullObj;
            }
        }
    }



    //>>>> Color Match
    private void Color_Match_System_Start()
    {
        if (!isMoving)
        {
            ColorMatchList.Clear();

            Player_Current_Position_Int();
            Vector2 check_center_pos = playerCurrentPosition;
            Vector2Int pos1 =  Check_Add_Tiles(check_center_pos,Vector2.up , ColorMatchList);
            Vector2Int pos2 = Check_Add_Tiles(check_center_pos, Vector2.down, ColorMatchList);
            Vector2Int pos3 = Check_Add_Tiles(check_center_pos, Vector2.left, ColorMatchList);
            Vector2Int pos4 = Check_Add_Tiles(check_center_pos, Vector2.right, ColorMatchList);
            //4방향 빈타일 제외 오브젝트를 리스트에 등록완료
            MakeLiner(pos1);
            MakeLiner(pos2);
            MakeLiner(pos3);
            MakeLiner(pos4);
            SoundManager.instance.GetSFX_LazerFire().Play();

            Debug.Log($"[0]:{ColorMatchList[0].Get_OBJ_TYPE().ToString()}/[1]:{ColorMatchList[1].Get_OBJ_TYPE().ToString()}/[2]:{ColorMatchList[2].Get_OBJ_TYPE().ToString()}/[3]:{ColorMatchList[3].Get_OBJ_TYPE().ToString()}");
            Color_Match_System(pos1, pos2, pos3, pos4);
        }
    }

    private void MakeLiner(Vector2Int pos)
    {
        GameObject linePrefab_1 = Resources.Load<GameObject>("Prefabs/Liner");
        LineRenderer line_1 = linePrefab_1.GetComponent<LineRenderer>();
        line_1.SetPosition(0, player.transform.position);
        Vector2 lineEndPos = new Vector2(pos.x, pos.y);
        line_1.SetPosition(1, lineEndPos);
        GameObject liner = Instantiate(linePrefab_1, Vector2.zero, Quaternion.identity);
    }
    private Vector2Int Check_Add_Tiles(Vector2 CCP, Vector2 dir, List<MapObject> list)
    {
        Vector2 tmpCCP = CCP;
        Vector2Int pos = new Vector2Int((int)tmpCCP.x, (int)tmpCCP.y); ;
        
        MapObject tempMapObj = OriginalMapData[pos];
        bool isAdded = false;

        while (!isAdded)
        {
            
            tmpCCP += dir;
            pos = new Vector2Int((int)tmpCCP.x, (int)tmpCCP.y);
            //Debug.Log($"check Pos{pos}={OriginalMapData[pos].Get_OBJ_TYPE().ToString()}");
            if (OriginalMapData.ContainsKey(pos))
            {
                tempMapObj = OriginalMapData[pos];
                
                if (tempMapObj.Get_OBJ_TYPE() == ObjectType.WALL)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }
                else if(tempMapObj.Get_OBJ_TYPE() == ObjectType.REDCUBE)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }
                else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.GREENCUBE)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }
                else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.BLUECUBE)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }
                else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.CYANCUBE)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }
                else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.PUPPLECUBE)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }
                else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.YELLOWCUBE)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }
                else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.REDSWITCH)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }
                else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.GREENSWITCH)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }
                else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.BLUESWITCH)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }
                else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.CYANSWITCH)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }
                else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.PUPPLESWITCH)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }
                else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.YELLOWSWITCH)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }

                else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.METACUBE)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }
                else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.REDBOMB)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.GREENBOMB)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.BLUEBOMB)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.CYANBOMB)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.PUPPLEBOMB)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }else if (tempMapObj.Get_OBJ_TYPE() == ObjectType.YELLOWBOMB)
                {
                    tempMapObj = GetMapObject(pos);
                    isAdded = true;
                }

                
            }
            
        }

        list.Add(tempMapObj);
        return pos;
    } //None 타입제외 발견시 리스트등록 위치반환
    private void Color_Match_System(Vector2Int pos1, Vector2Int pos2, Vector2Int pos3, Vector2Int pos4)
    {
        Vector2Int[] posArray = new Vector2Int[4];
        posArray[0] = pos1;
        posArray[1] = pos2;
        posArray[2] = pos3;
        posArray[3] = pos4;

        MapObject[] mapObjArray = new MapObject[4];

        int[] ColorCounterArray = { 0, 0, 0, 0, 0, 0 ,0 }; //R G B C P Y (Meta)

        for(int i =0; i<4; i++)
        {
            ColorCounter(ColorMatchList[i], ColorCounterArray);
        }

        
        bool toggle1 = false;
        bool toggle2 = false;
        bool toggle3 = false;
        bool toggle4 = false;
        int toggleCounter = 0;

        if(ColorMatchList.Count == 4)
        {
            //>>MetaCube Color Change

            int MetaCubeColorCode = MetaColorShifter(ColorCounterArray); //메타큐브가 변할 색깔 얻음
            bool[] MetaCubeBox = WhoIsMetaCube(ColorMatchList);
            MetaCubeChanger(MetaCubeBox, MetaCubeColorCode, posArray);

            for (int i = 1; i < 4; i++) 
            {
                if (ColorMatchList[0].GetTypeColor(ColorMatchList[0].Get_OBJ_TYPE()) == ColorMatchList[i].GetTypeColor(ColorMatchList[i].Get_OBJ_TYPE())) //이 조건으로는 레드큐브와 레드스위치가 안걸림
                {
                    if (!toggle1)
                    {
                        mapObjArray[0] = ColorMatchList[0];
                        toggle1 = true;
                    }

                    if (i == 1)
                    {
                        mapObjArray[1] = ColorMatchList[1];
                        toggle2 = true;
                    }

                    if (i == 2)
                    {
                        mapObjArray[2] = ColorMatchList[2];
                        toggle3 = true;
                    }

                    if (i == 3)
                    {
                        mapObjArray[3] = ColorMatchList[3];
                        toggle4 = true;
                    }

                }
            } // 0 -1 , 0-2 , 0-3 비교
            if (ColorMatchList[1].GetTypeColor(ColorMatchList[1].Get_OBJ_TYPE()) == ColorMatchList[2].GetTypeColor(ColorMatchList[2].Get_OBJ_TYPE()))
            {
                if (!toggle2)
                {
                    mapObjArray[1] = ColorMatchList[1];
                    toggle2 = true;
                    toggleCounter++;
                }
                if (!toggle3)
                {
                    mapObjArray[2] = ColorMatchList[2];
                    toggle3 = true;
                    toggleCounter++;
                }

            }
            if (ColorMatchList[1].GetTypeColor(ColorMatchList[1].Get_OBJ_TYPE()) == ColorMatchList[3].GetTypeColor(ColorMatchList[3].Get_OBJ_TYPE()))
            {
                if (!toggle2)
                {
                    mapObjArray[1] = ColorMatchList[1];
                    toggle2 = true;
                    toggleCounter++;
                }
                if (!toggle4)
                {
                    mapObjArray[3] = ColorMatchList[3];
                    toggle4 = true;
                    toggleCounter++;
                }
                
            }
            if (ColorMatchList[2].GetTypeColor(ColorMatchList[2].Get_OBJ_TYPE()) == ColorMatchList[3].GetTypeColor(ColorMatchList[3].Get_OBJ_TYPE()))
            {

                if (!toggle3)
                {
                    mapObjArray[2] = ColorMatchList[2];
                    toggle3 = true;
                    toggleCounter++;
                }
                if (!toggle4)
                {
                    mapObjArray[3] = ColorMatchList[3];
                    toggle4 = true;
                    toggleCounter++;
                }
            }
            //여기까지하면 같은 매칭된것은 토글됌

            bool[] toggleBox = { toggle1, toggle2, toggle3, toggle4 };

            
            for(int i =0; i<4; i++)
            {
                if (toggleBox[i])
                {
                    if (mapObjArray[i].isSwitch()) 
                    { 
                        SwitchOBJActivate(); 
                    }
                    else 
                    {
                        MatchedCubeDestroy(mapObjArray[i], posArray[i]); 
                    }
                }
            }
        }
        else
        {
            Debug.Log($"Color Match List isnt Full. Current Count : {ColorMatchList.Count}");
        }
    }


    // >>>> MetaCube Color Match
    private void ColorCounter(MapObject mobj , int[] CC)
    {
        string mobjTypeName = mobj.Get_OBJ_TYPE().ToString();

        if (mobjTypeName.Contains("RED")){
            CC[0]++;
        }
        if (mobjTypeName.Contains("GREEN"))
        {
            CC[1]++;
        }
        if (mobjTypeName.Contains("BLUE"))
        {
            CC[2]++;
        }
        if (mobjTypeName.Contains("CYAN"))
        {
            CC[3]++;
        }
        if (mobjTypeName.Contains("PUPPLE"))
        {
            CC[4]++;

        }
        if (mobjTypeName.Contains("YELLOW"))
        {
            CC[5]++;
        }
        if (mobjTypeName.Contains("META"))
        {
            CC[6]++;
        }
    }
    private int MetaColorShifter(int[] CC) 
    {
        // main case
        // {0,0,1,0,2,0,1} -> meta : 3
        // {0,0,1,1,1,0,1} -> meta : 3, 4, 5
        // {0,0,4,0,0,0,0} x
        // {0,0,0,0,3,0,1} -> meta : 5
        // (0,2,0,0,0,0,1} -> meta : 2

        for(int i =0; i<6; i++)
        {
            if (CC[i] == 3)  // {0,0,0,0,3,0,1} 
            {
                return i;
            }

            if (CC[i] == 2) //{0,0,1,0,2,0,1} & (0,2,0,0,0,0,1}
            {
                for(int j =0; j<6; j++ )
                {
                    if (CC[j] == 1) //{0,0,1,0,2,0,1}
                    {
                        return j;


                    }
                    if(j == 5)
                    {
                        if (CC[j] != 1)
                        {
                            return i;
                        }
                    }
                }
            }

            if (CC[i] == 1) //{0,0,1,1,0,1,1} 
            {
                int[] OneCounter = new int[10];
                System.Random RD = new System.Random();


                OneCounter[0] = i; //이 경우에는 [0] = 2 

                if (i != 5) //마지막번호가 아닐때 남은 번호에서 더 찾기
                {
                    for (int j = i + 1; j < 6; j++) //3 4 5
                    {
                        if (CC[j] == 1)
                        {
                            OneCounter[1] = j; //이 경우에는 [1] = 3 

                            if(j != 5) //j가 마지막 번호가 아닌경우에만
                            {
                                for (int k = j + 1; k<6; k++) //4 5
                                {
                                    if (CC[k] == 1)
                                    {
                                        OneCounter[2] = k; // 이 경우에는 [2] = 5
                                                           //최대개수인 3개까지 찾음.
                                                           // 3개중 하나를 랜덤 반환

                                        int[] numbers = { i, j, k };
                                        int MixNumber = RD.Next(numbers.Length);
                                        return numbers[MixNumber]; // 2,3,5 중 하나 반환
                                    }
                                    else // { 0,0,1,1,0,0,1} 의 경우
                                    {
                                        int[] numbers = { i, j };
                                        int MixNumber = RD.Next(numbers.Length);
                                        return numbers[MixNumber]; // 2,3 중 하나 반환
                                    }
                                }
                            }
                        }
                        else // , {0,0,1,0,0,0,1} 
                        {
                            return i;
                        }
                    }
                }
                else //CC[5] ==1 인경우  {0,0,0,0,0,1,1} 
                {
                    return i;
                }
                
            }
        }
        return 6; //컬러오브젝트가 없는경우
    }
    private bool[] WhoIsMetaCube(List<MapObject> list)
    {
        bool[] MetaCubeBox = { false, false, false, false };

        if (list[0].Get_OBJ_TYPE() == ObjectType.METACUBE)
        {
            MetaCubeBox[0] = true;
        }
        if (list[1].Get_OBJ_TYPE() == ObjectType.METACUBE)
        {
            MetaCubeBox[1] = true;
        }
        if (list[2].Get_OBJ_TYPE() == ObjectType.METACUBE)
        {
            MetaCubeBox[2] = true;
        }
        if (list[3].Get_OBJ_TYPE() == ObjectType.METACUBE)
        {
            MetaCubeBox[3] = true;
        }

        return MetaCubeBox;
        
    }
    private void MetaCubeChanger(bool[] MCB,int ColorCode, Vector2Int[] posArray)
    {
        bool[] MetaPosArray = { false, false, false, false };

        for(int i =0; i< 4; i++)
        {
            if (OriginalMapData[posArray[i]].Get_OBJ_TYPE() == ObjectType.METACUBE)
            {
                MetaPosArray[i] = true;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (MCB[i])
            {
                for(int j =0; j<4; j++)
                {
                    if (MetaPosArray[j])
                    {
                        MetaPosArray[j] = false;

                        switch (ColorCode)
                        {
                            case 0:
                                OriginalMapData[posArray[j]].Set_OBJ_Type(ObjectType.REDCUBE);
                                break;
                            case 1:
                                OriginalMapData[posArray[j]].Set_OBJ_Type(ObjectType.GREENCUBE);
                                break;
                            case 2:
                                OriginalMapData[posArray[j]].Set_OBJ_Type(ObjectType.BLUECUBE);
                                break;
                            case 3:
                                OriginalMapData[posArray[j]].Set_OBJ_Type(ObjectType.CYANCUBE);
                                break;
                            case 4:
                                OriginalMapData[posArray[j]].Set_OBJ_Type(ObjectType.PUPPLECUBE);
                                break;
                            case 5:
                                OriginalMapData[posArray[j]].Set_OBJ_Type(ObjectType.YELLOWCUBE);
                                break;
                        }
                    }
                }
            }
        }
    }
    private void SwitchOBJActivate()
    {

    }
    private void MatchedCubeDestroy(MapObject MOBJ, Vector2Int pos)
    {
        if (MOBJ.Get_OBJ_TYPE().ToString().Contains("CUBE"))
        {
            GameObject O2prefab = Resources.Load<GameObject>("Prefabs/O2prefab");
            Vector2 loc = new Vector2(pos.x, pos.y);
            GameObject O2obj = Instantiate(O2prefab, loc, Quaternion.identity);

            SoundManager.instance.GetSFX_CubeDestroy().Play();
            Destroy(MOBJ.Get_OBJ());
            MapObject noneReplacer = new MapObject(null);
            ReplaceMapData(pos, noneReplacer);
        }
        //Debug.Log($"MCD/pos:{pos}/type:{OriginalMapData[pos].Get_OBJ_TYPE()}");

        if (MOBJ.Get_OBJ_TYPE().ToString().Contains("BOMB"))
        {

            BombAction BA = MOBJ.Get_OBJ().GetComponent<BombAction>();
            if(BA == null)
            {
                Debug.Log("BA Null");
            }
            else
            {

                BA.SetBombLocation(pos);
                BA.SetActive();
            }
            
            //MapObject noneReplacer = new MapObject(null);
            //ReplaceMapData(pos, noneReplacer);
            

           
        }
    }

    //>>>> MetaCube
    private void MakeMetaCube()
    {
        if (Get_Meta_Current() > 0)
        {
            if (!isMoving)
            {
                Player_Current_Position_Int();
                Vector2 makingPos = new Vector2(playerCurrentPosition.x, playerCurrentPosition.y);
                Vector2Int makingPosVint2 = new Vector2Int((int)makingPos.x, (int)makingPos.y);
                if (OriginalMapData[makingPosVint2].Get_OBJ_TYPE() == ObjectType.NONE)
                {
                    GameObject metaCubeFinding = Resources.Load<GameObject>("Prefabs/MetaCube_0");
                    Debug.Log("S3");
                    GameObject MetaCubePrefab = Instantiate(metaCubeFinding, makingPos, Quaternion.identity);

                    MapObject NewMetaCubeAsign = new MapObject(MetaCubePrefab, ObjectType.METACUBE);

                    Vector2Int makingPosV2int = new Vector2Int((int)makingPos.x, (int)makingPos.y);

                    ReplaceMapData(makingPosV2int, NewMetaCubeAsign);

                    SoundManager.instance.GetSFX_MetaCubeGen().Play();
                    Changer_Meta_Current(-1);
                }
                    
            }
            else
            {
                Debug.Log("Player is moving. Cant Make MetaCube");
            }
        }
        
    }
    public int Get_Meta_Current()
    {
        return current_meta_count;
    }
    public int Get_Meta_Max()
    {
        return max_meta_count;
    }
    public int Get_Meta_Init()
    {
        return init_meta_count;
    }
    public void Changer_Meta_Current(int number)
    {
        current_meta_count += number;
        if(current_meta_count > max_meta_count)
        {
            current_meta_count = max_meta_count;
        }
        if(current_meta_count < 0)
        {
            current_meta_count = 0;
        }
    }
    public void Set_Meta_max(int number)
    {
        max_meta_count = number;
    }
    
    public float GetMetaChargingSpeed()
    {
        return metaChargingSpeed;
    }
    public void SetMetaChargingSpeed(float amount)
    {
        metaChargingSpeed = amount;
    }

    //>>>> Stage Number Control
    private void Set_Stage_Number(Stage_Number SN)
    {
        Stage_Number = SN;
    }
    public Stage_Number Get_Stage_Number()
    {
        return Stage_Number;
    }
    

    //>>>> UI
    public void Setup_Btn(PointerEventData data)
    {
        SoundManager.instance.GetSFX_Tick().Play();
        SoundManager.instance.GetSFX_BGM().Pause();
        Time.timeScale = 0f;
        if(Setup_Panel.activeSelf == false)
        {
            Setup_Panel.SetActive(true);
        }
    }
    public void Setup_To_Menu_Btn(PointerEventData data)
    {
        SoundManager.instance.GetSFX_Tick().Play();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }
    public void Setup_Continue_Btn(PointerEventData data)
    {
        SoundManager.instance.GetSFX_Tick().Play();
        SoundManager.instance.GetSFX_BGM().Play();
        Time.timeScale = 1.0f;
        if (Setup_Panel.activeSelf == true)
        {
            Setup_Panel.SetActive(false);
        }
    }
    public void Shoot_Btn_Click(PointerEventData data)
    {
        SoundManager.instance.GetSFX_Tick().Play();
        Color_Match_System_Start();
    }
    public void Meta_Cube_Btn_Click(PointerEventData data)
    {
        SoundManager.instance.GetSFX_Tick().Play();
        
        MakeMetaCube();
    }
    public void MainMenu_Panel_Click(PointerEventData data)
    {
        if(SoundManager.instance.GetSFX_Tick() != null)
        {
            SoundManager.instance.GetSFX_Tick().Play();
        }
        
        
        SceneManager.LoadScene("Stage1");
    }

    //>>>> Load
    private void  LoadMenu()
    {
        LoadingSceneToggle = true;
        if (LoadingSceneToggle)
        {
            StartCoroutine(FakeLoadingScreen());
        }

        descriptionTxtOBJ = GameObject.Find("Description");
        if(descriptionTxtOBJ == null) { Debug.Log("GM : LoadMenu() : txtOBJ NUll"); }
        descriptionTxtOBJ.AddComponent<TextfadeINOUT>();

        Setup_Panel = GameObject.Find("Menu BGR Panel");
        Register_UI_Other_Trigger(Setup_Panel, EventTriggerType.PointerUp, "MainMenu_Panel_Click");

        SoundManager.instance.GetSFX_BGM().Play();
    }
    private void LoadStage(Stage_Number SN)
    {
        Stage_Number = SN;

        LoadingSceneToggle = true;
        if (LoadingSceneToggle)
        {
            StartCoroutine(FakeLoadingScreen());
        }

        StartCoroutine(StageInfoActive());

        Register_PlayerSprite();
        Register_PlayerPosition(); //초기 포지션 0,0

        //이동 버튼 등록
        Register_Btn(ref LeftMoveBtn, "Left Btn");
        Register_Btn(ref RightMoveBtn, "Right Btn");
        Register_Btn(ref UpMoveBtn, "Up Btn");
        Register_Btn(ref DownMoveBtn, "Down Btn");
        //이동 버튼 트리거 등록
        Register_Trigger_to_Move_Buttons(LeftMoveBtn, "Left_Click");
        Register_Trigger_to_Move_Buttons(RightMoveBtn, "Right_Click");
        Register_Trigger_to_Move_Buttons(UpMoveBtn, "Up_Click");
        Register_Trigger_to_Move_Buttons(DownMoveBtn, "Down_Click");
                
        // 플레이어 팔로우 캠 등록
        this.gameObject.AddComponent<PlayerFollowCam>();
        PFC = this.gameObject.GetComponent<PlayerFollowCam>();

        GameObject cam = GameObject.Find("Main Camera");
        if (cam == null) { Debug.Log("GM: Cam Object null"); }
        PFC.Regist_PlayerAndCamOBJ(cam, player);

        // 플레이어 VFX register
        playerIMGObj.AddComponent<PlayerVFX>();
        player_VFX = playerIMGObj.GetComponent<PlayerVFX>();

        //슈팅 버튼, 메타 큐브 버튼, 설정 버튼 등록
        Register_Btn(ref ShootBtn, "Shoot Btn");
        Register_Btn(ref MetaCubeBtn, "Meta Btn");
        Register_Btn(ref SetupBtn, "Setup Btn");
        Register_Btn(ref Setup_ContinueBtn, "Setup : Continue");
        Register_Btn(ref Setup_ToMenuBtn, "Setup : ToMENU");

        Setup_Panel = GameObject.Find("Setup PopUp Panel A");
        if (Setup_Panel == null) { Debug.Log("GM : Setup Panel Null"); }
        else { if (Setup_Panel.activeSelf) { Setup_Panel.SetActive(false); } }

        Register_Trigger_to_Other_Buttons(SetupBtn, "Setup_Btn");
        Register_Trigger_to_Other_Buttons(Setup_ContinueBtn, "Setup_Continue_Btn");
        Register_Trigger_to_Other_Buttons(Setup_ToMenuBtn, "Setup_To_Menu_Btn");

        Register_Trigger_to_Other_Buttons(ShootBtn, "Shoot_Btn_Click");
        Register_Trigger_to_Other_Buttons(MetaCubeBtn, "Meta_Cube_Btn_Click");

        //게임오버패널 등록
        Regist_GameOverPanel();

        //게임매니저의 스테이지맵매니저 컴포넌트 변수 가져오기
        SMM = this.gameObject.GetComponent<StageMapManager>();
        SMM.Set_Stage_Map();

        //산소수치, 체력바
        Regist_HP_O2();

        //메타큐브 UI 인디케이터 등록

        metaBarObj = GameObject.Find(metaBarObj_name);
        metaTxtObj = GameObject.Find(metaTxt_name);

        current_meta_count = max_meta_count;

        MetaUI metaTxtScript = metaTxtObj.GetComponent<MetaUI>();
        if(metaTxtScript == null) { metaTxtScript = metaTxtObj.AddComponent<MetaUI>(); }
        
        metaTxtScript.RegisterMetaVariables(metaBarObj, metaTxtObj);

        //클리어 패널 미리 등록 후 안보이게
        clearPanel = GameObject.Find("Clear Panel");
        clearPanelCG = clearPanel.GetComponent<CanvasGroup>();
        if(clearPanelCG == null) { clearPanelCG = clearPanel.AddComponent<CanvasGroup>(); }
        clearPanelCG.alpha = 0;
        clearPanel.SetActive(false);
        Debug.Log("Clear Panel DOne.");


        SoundManager.instance.GetSFX_BGM().Play();

        //라이프 UI 등록
        Regist_life();
        player_current_life = player_max_life;
        Set_life_indicator(player_max_life);
    }

    public bool LoadingSceneToggle = false;
    public bool Get_LoadingSceneToggle()
    {
        return LoadingSceneToggle;
    }
    IEnumerator FakeLoadingScreen()
    {
        GameObject loadingPanel = GameObject.Find("Fake Loading BGR Panel");
        CanvasGroup CG = loadingPanel.GetComponent<CanvasGroup>();
        CG.DOFade(1.0f, 2.0f); //alpha , time

        yield return new WaitForSeconds(1.0f);

        GameObject bar = GameObject.Find("Bar");
        Image barImg = bar.GetComponent<Image>();

        barImg.DOFillAmount(1.0f, 1.5f);
        yield return new WaitForSeconds(1.0f);
        CG.DOFade(0, 1.0f); //alpha , time

        LoadingSceneToggle = false;
    }

    GameObject stageInfoPanel;
    CanvasGroup CG;
    GameObject stageInfo;
    TextMeshProUGUI stageInfoTxt;
    IEnumerator StageInfoActive()
    {
        

        stageInfoPanel = GameObject.Find("Stage Info A");
        CG= stageInfoPanel.AddComponent<CanvasGroup>();
        CG.alpha = 1;

         stageInfo = GameObject.Find("Stage Info Txt");
         stageInfoTxt = stageInfo.GetComponent<TextMeshProUGUI>();
        if(stageInfo == null || stageInfoTxt == null)
        {
            Debug.Log($"Point 1 / {Stage_Number}");
        }

        switch(Stage_Number){
            case Stage_Number.Stage1:
                stageInfoTxt.text = "Stage 1";
                break;
            case Stage_Number.Stage2:
                stageInfoTxt.text = "Stage 2";
                break;
            case Stage_Number.Stage3:
                stageInfoTxt.text = "Stage 3";
                break;
            case Stage_Number.Stage4:
                stageInfoTxt.text = "Stage 4";
                break;
            case Stage_Number.Stage5:
                stageInfoTxt.text = "Stage 5";
                break;
            case Stage_Number.Stage6:
                stageInfoTxt.text = "Stage 6";
                break;
            case Stage_Number.Stage7:
                stageInfoTxt.text = "Stage 7";
                break;
            case Stage_Number.Stage8:
                stageInfoTxt.text = "Stage 8";
                break;
            case Stage_Number.Stage9:
                stageInfoTxt.text = "Stage 9";
                break;
            case Stage_Number.Stage10:
                stageInfoTxt.text = "Stage 10";
                break;
            
        }

        yield return new WaitForSeconds(3f);

        

        CG.DOFade(0.0f, 1.5f);

        stageInfoPanel.SetActive(false);
    }
   
    public void PortalCollide()
    {
        if(clearPanel.activeSelf == false)
        {
            clearPanel.SetActive(true);
        }
        //클리어 UI 액티브
        clearPanelCG.DOFade(1.0f, 1.5f);

        StartCoroutine(NextStage());
    }

    IEnumerator NextStage()
    {
        yield return new WaitForSeconds(1.0f);
        switch (Stage_Number)
        {
            case Stage_Number.Stage1:
                SceneManager.LoadScene("Stage2");
                break;
            case Stage_Number.Stage2:
                SceneManager.LoadScene("Stage3");
                break;
            case Stage_Number.Stage3:
                SceneManager.LoadScene("Stage4");
                break;
            case Stage_Number.Stage4:
                SceneManager.LoadScene("Stage5");
                break;
            case Stage_Number.Stage5:
                SceneManager.LoadScene("Stage6");
                break;
            case Stage_Number.Stage6:
                SceneManager.LoadScene("Stage7");
                break;
            case Stage_Number.Stage7:
                SceneManager.LoadScene("Stage8");
                break;
            case Stage_Number.Stage8:
                SceneManager.LoadScene("Stage9");
                break;
            case Stage_Number.Stage9:
                SceneManager.LoadScene("Stage10");
                break;
            case Stage_Number.Stage10:
                SceneManager.LoadScene("Ending");
                break;
        }
    }

    //      >>GameOverPanel
    private void Regist_GameOverPanel()
    {
        gameOverPanelTxt = GameObject.Find("TapToRestartTxt");
        gameOverPanelTxt.AddComponent<TextfadeINOUT>();
        gameOverPanelTxt.SetActive(false);
        gameOverPanelObj = GameObject.Find("GameOver Panel");
        gameOverPanelObj.SetActive(false);
    }
    public IEnumerator Load_GameOverPanel()
    {
        SoundManager.instance.GetSFX_BGM().Stop();

        gameOverPanelObj.SetActive(true);
        Debug.Log("Load_GameOverPanel");
        gameOverPanelTxt.SetActive(true);

        GOPcanvasGroup = gameOverPanelObj.GetComponent<CanvasGroup>();
        GOPcanvasGroup.alpha = 0;
       

        GOPcanvasGroup.DOFade(1.0f, 1.0f);

        yield return new WaitForSeconds(1.0f);

        

        EventTrigger ET =  gameOverPanelObj.GetComponent<EventTrigger>();
        if(ET == null)
        {
            ET = gameOverPanelObj.AddComponent<EventTrigger>();
        }
        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) =>
        {
            GameOverPanelClick((PointerEventData)data);
        });
        ET.triggers.Add(entry);
    }
    public void GameOverPanelClick(PointerEventData data)
    {
        SoundManager.instance.GetSFX_Tick().Play();
        toggle_isHPzero = true;
        isHPZero = false;

        switch (Stage_Number)
        {
            case Stage_Number.Stage1:
                SceneManager.LoadScene("Stage1");
                break;
            case Stage_Number.Stage2:
                SceneManager.LoadScene("Stage2");
                break;
            case Stage_Number.Stage3:
                SceneManager.LoadScene("Stage3");
                break;
            case Stage_Number.Stage4:
                SceneManager.LoadScene("Stage4");
                break;
            case Stage_Number.Stage5:
                SceneManager.LoadScene("Stage5");
                break;
            case Stage_Number.Stage6:
                SceneManager.LoadScene("Stage6");
                break;
            case Stage_Number.Stage7:
                SceneManager.LoadScene("Stage7");
                break;
            case Stage_Number.Stage8:
                SceneManager.LoadScene("Stage8");
                break;
            case Stage_Number.Stage9:
                SceneManager.LoadScene("Stage9");
                break;
            case Stage_Number.Stage10:
                SceneManager.LoadScene("Stage10");
                break;
        }
    }


    //>>>> Player HP & O2
    public float GetPlayerCurrentHP()
    {
        return Player_Current_HP;
    }
    public float GetPlayerCurrentO2()
    {
        return Player_Current_O2;
    }
    public float GetPlayerMaxHP()
    {
        return Player_Max_HP;
    }
    public float GetPlayerMaxO2()
    {
        return Player_Max_O2;
    }
    public void Increase_Player_HP(float amount)
    {
        isHPZero = false;
        Player_Current_HP += amount;
        if(Player_Current_HP >= Player_Max_HP)
        {
            Player_Current_HP = Player_Max_HP;
        }
    }
    public void Decrease_Player_HP(float amount)
    {
        
        Player_Current_HP -= amount;
        if (Player_Current_HP <= 0)
        {
            if (player_current_life > 0)
            {
                player_current_life--;

                if(player_current_life == 1)
                {
                    Player_Current_HP = Player_Max_HP;
                    life_1_img.SetActive(true);
                    life_2_img.SetActive(false);
                }
                else if (player_current_life == 0)
                {
                    Player_Current_HP = Player_Max_HP;
                    life_1_img.SetActive(false);
                    life_2_img.SetActive(false);
                }
            }
            else if (player_current_life == 0)
            {
                
                life_1_img.SetActive(false);
                life_2_img.SetActive(false);
                gameoverTrigger = true;
            }

            isHPZero = true;
            //Player_Current_HP = 0;
        }
    }
    private bool gameoverTrigger = false;
    public void HitByEnemyAttack(float amount)
    {
        if (!Invincible)
        {
            StartCoroutine(InvincibleOn());
            Decrease_Player_HP(amount);
        }
    }
    public void Increase_Player_O2(float amount)
    {
        isO2Zero = false;
        Player_Current_O2 += amount;
        if(Player_Current_O2 >= Player_Max_O2)
        {
            Player_Current_O2 = Player_Max_O2;
        }
    }
    public void Decrease_Player_O2(float amount)
    {
        Player_Current_O2 -= amount;
        if(Player_Current_O2 <= 0)
        {
            isO2Zero = true;
            Player_Current_O2 = 0;
        }
    }   
    private void Regist_HP_O2()
    {
        Player_Max_HP = 100.0f;
        Player_Max_O2 = 100.0f;

        Player_Current_HP = Player_Max_HP;
        Player_Current_O2 = Player_Max_O2;

        GameObject find_HPbar = GameObject.Find("HP Bar");
        GameObject find_O2bar = GameObject.Find("O2 Bar");

        HP_Bar_img = find_HPbar.GetComponent<Image>();
        O2_Bar_img = find_O2bar.GetComponent<Image>();

        if (HP_Bar_img != null && O2_Bar_img != null)
        {
            HP_Bar_img.AddComponent<PlayerHP>();
            O2_Bar_img.AddComponent<PlayerO2>();
        }
        else { Debug.Log("GM:LoadStage : HP/O2 bar Img Null"); }
    }
    public void Set_isHPzero(bool YN)
    {
        isHPZero = YN;
    }
    public void Set_isO2zero(bool YN)
    {
        isO2Zero = YN;
    }
    public bool Get_isO2zero()
    {
        return isO2Zero;
    }
    public bool Get_isHPzero()
    {
        return isHPZero;
    }
    public bool Get_Invincible()
    {
        return Invincible;
    }
    IEnumerator InvincibleOn()
    {
        Invincible = true;

        playerSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        Invincible = false;
    }

    //--------- Start()-----------////--------- Start()-----------//
    //--------- Start()-----------////--------- Start()-----------//
    void Start()
    {
        
    }

    //--------- Update()-----------////--------- Update()-----------//
    //--------- Update()-----------////--------- Update()-----------//
    bool toggle_isHPzero = true;
    void Update()
    {
        if (isHPZero && toggle_isHPzero && player_current_life==0 && gameoverTrigger)
        {
            toggle_isHPzero = false;
            Player_Current_HP = Player_Max_HP;
            Player_Current_O2 = Player_Max_O2;
            StartCoroutine(Load_GameOverPanel());
        }


       
    }
}

public enum Stage_Number
{
    Menu = 0,
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Stage5,
    Stage6,
    Stage7,
    Stage8,
    Stage9,
    Stage10,
    Ending
}

public enum Move_Direction
{
    None=0,
    Left,
    Right,
    Up,
    Down
}