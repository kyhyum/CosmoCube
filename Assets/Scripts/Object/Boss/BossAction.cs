using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BossAction : MonoBehaviour
{

    public static BossAction instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private float BossHP_max = 50000.0f;
    private float BossHP_cur;
    private float BossHP_fillAmount;

    private Image BossHP_Bar;
    private GameObject BossHP_Bar_obj;
    private float curTime = 0;
    private float FirstActionTime = 5.0f;

    private bool isDead = false;



    private Vector2 attack_spawn_point;

    private Vector2[] horizontal_attackPoint = new Vector2[7];
    private Vector2[] vertical_attackPoint = new Vector2[12];


    private Vector2[] attack_point = new Vector2[7];
    private Vector2[] atk_dir = new Vector2[7];


    private GameObject Throwing_blade_fast;
    private float throwingBladeSpeed = 4.5f;

    private GameObject O2item;
    private GameObject Hpitem;


    private float O2spawnTimer = 12.0f;
    private float O2curTimer = 0;

    private float HPspawnTimer = 30.0f;
    private float HPcurTimer = 0;

    private GameObject playerGun;


    public void MakePlayerGun()
    {
        GameObject PlayerGun = Instantiate(playerGun, GameManager.Instance.Get_Player_Current_Pos(), Quaternion.identity);
    }
    public void DecreaseBossHP(float amount)
    {
        BossHP_cur -= amount;
        if (BossHP_cur <= 0)
        {
            isDead = true;
        }
    }
    private void Start()
    {
        playerGun = Resources.Load<GameObject>("Prefabs/Boss/PlayerGun");

        

        BossHP_cur = BossHP_max;
        BossHP_fillAmount = BossHP_cur / BossHP_max;
        BossHP_Bar_obj = GameObject.Find("Boss HP Bar IMG");
        BossHP_Bar = BossHP_Bar_obj.GetComponent<Image>();
        BossHP_Bar.fillAmount = BossHP_fillAmount;

        O2item = Resources.Load<GameObject>("Prefabs/Boss/O2 recover item");
        Hpitem = Resources.Load<GameObject>("Prefabs/Boss/Hp recover item");


        attack_spawn_point = new Vector2(14, 0);

        for (int i = 0; i < 7; i++)
        {
            attack_point[i] = new Vector2(-2, 3 - i);
            horizontal_attackPoint[i] = new Vector2(10, 3 - i);


            atk_dir[i] = attack_point[i] - attack_spawn_point;
            atk_dir[i] = atk_dir[i].normalized;

            Debug.Log("Vec : " + atk_dir[i]);
        }

        for (int i = 0; i < 12; i++)
        {
            vertical_attackPoint[i] = new Vector2(-2 + i, 5);
        }

        Throwing_blade_fast = Resources.Load<GameObject>("Prefabs/Boss/Throwing_blade_fast");
    }
    private void Attack_1()
    {
        GameObject atk1 = Instantiate(Throwing_blade_fast, attack_spawn_point, Quaternion.identity);
        GameObject atk2 = Instantiate(Throwing_blade_fast, attack_spawn_point, Quaternion.identity);
        GameObject atk3 = Instantiate(Throwing_blade_fast, attack_spawn_point, Quaternion.identity);
        GameObject atk4 = Instantiate(Throwing_blade_fast, attack_spawn_point, Quaternion.identity);

        BossThrowingAttack script = atk1.GetComponent<BossThrowingAttack>();
        script.SetSpeedAndDir(throwingBladeSpeed, atk_dir[0]);

        script = atk2.GetComponent<BossThrowingAttack>();
        script.SetSpeedAndDir(throwingBladeSpeed, atk_dir[2]);

        script = atk3.GetComponent<BossThrowingAttack>();
        script.SetSpeedAndDir(throwingBladeSpeed, atk_dir[4]);

        script = atk4.GetComponent<BossThrowingAttack>();
        script.SetSpeedAndDir(throwingBladeSpeed, atk_dir[6]);

    }
    private void Attack_2()
    {
        GameObject atk1 = Instantiate(Throwing_blade_fast, attack_spawn_point, Quaternion.identity);
        GameObject atk2 = Instantiate(Throwing_blade_fast, attack_spawn_point, Quaternion.identity);
        GameObject atk3 = Instantiate(Throwing_blade_fast, attack_spawn_point, Quaternion.identity);

        BossThrowingAttack script = atk1.GetComponent<BossThrowingAttack>();
        script.SetSpeedAndDir(throwingBladeSpeed, atk_dir[1]);

        script = atk2.GetComponent<BossThrowingAttack>();
        script.SetSpeedAndDir(throwingBladeSpeed, atk_dir[3]);

        script = atk3.GetComponent<BossThrowingAttack>();
        script.SetSpeedAndDir(throwingBladeSpeed, atk_dir[5]);
    }
    IEnumerator Pattern_1()
    {

        Attack_1();

        yield return new WaitForSeconds(0.8f);

        Attack_2();

        yield return new WaitForSeconds(0.8f);

        Attack_1();

        yield return new WaitForSeconds(0.8f);

        Attack_2();

        yield return new WaitForSeconds(0.8f);
    }  // 4 3 4 3 ¹æ»çÇü
    IEnumerator Pattern_2() // Horizontal Throwing Blade Down  7time
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject atk1 = Instantiate(Throwing_blade_fast, horizontal_attackPoint[i], Quaternion.identity);
            BossThrowingAttack script = atk1.GetComponent<BossThrowingAttack>();
            script.SetSpeedAndDir(throwingBladeSpeed + 3.0f, Vector2.left);

            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator Pattern_3() // Horizontal Throwing Blade Up  7time
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject atk1 = Instantiate(Throwing_blade_fast, horizontal_attackPoint[6 - i], Quaternion.identity);
            BossThrowingAttack script = atk1.GetComponent<BossThrowingAttack>();
            script.SetSpeedAndDir(throwingBladeSpeed + 3.0f, Vector2.left);

            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator Pattern_4()  //Random Horizontal Throwing Blade  15time
    {
        for (int i = 0; i < 15; i++)
        {
            int randomInt = Random.Range(0, 7);

            GameObject atk1 = Instantiate(Throwing_blade_fast, horizontal_attackPoint[randomInt], Quaternion.identity);
            BossThrowingAttack script = atk1.GetComponent<BossThrowingAttack>();
            script.SetSpeedAndDir(throwingBladeSpeed + 3.0f, Vector2.left);

            yield return new WaitForSeconds(0.3f);
        }


    }
    IEnumerator Pattern_5() // 3point to player attack, throwing blade 9time
    {

        for (int i = 0; i < 3; i++)
        {
            GameObject atk1 = Instantiate(Throwing_blade_fast, horizontal_attackPoint[0], Quaternion.identity);
            GameObject atk2 = Instantiate(Throwing_blade_fast, horizontal_attackPoint[3], Quaternion.identity);
            GameObject atk3 = Instantiate(Throwing_blade_fast, horizontal_attackPoint[6], Quaternion.identity);

            BossThrowingAttack script = atk1.GetComponent<BossThrowingAttack>();
            Vector2 dir = GameManager.Instance.Get_Player_Current_Pos() - horizontal_attackPoint[0];
            dir = dir.normalized;
            script.SetSpeedAndDir(throwingBladeSpeed, dir);

            script = atk2.GetComponent<BossThrowingAttack>();
            dir = GameManager.Instance.Get_Player_Current_Pos() - horizontal_attackPoint[3];
            dir = dir.normalized;
            script.SetSpeedAndDir(throwingBladeSpeed, dir);

            script = atk3.GetComponent<BossThrowingAttack>();
            dir = GameManager.Instance.Get_Player_Current_Pos() - horizontal_attackPoint[6];
            dir = dir.normalized;
            script.SetSpeedAndDir(throwingBladeSpeed, dir);

            yield return new WaitForSeconds(0.2f);
        }

    }
    IEnumerator Pattern_6()
    {
        for (int i = 0; i < 12; i++)
        {
            GameObject prefab = Instantiate(Throwing_blade_fast, vertical_attackPoint[11 - i], Quaternion.identity);
            BossThrowingAttack script = prefab.GetComponent<BossThrowingAttack>();
            script.SetSpeedAndDir(throwingBladeSpeed, Vector2.down);
            yield return new WaitForSeconds(0.5f);
        }

    }

    private void OnOFFBladeAttack()
    {

        for(int i = -3; i < 4; i++)
        {
            Debug.Log("Call 1");
            Vector2 makePos = new Vector2(11, i);
            GameObject prefab = Instantiate(Throwing_blade_fast, makePos, Quaternion.identity);
            BossThrowingAttack script = prefab.GetComponent<BossThrowingAttack>();
            script.SetSpeedAndDir(6f, Vector2.left);
            script.SetIsOnOFF(true);
        }

    }

    private void OnOFFBladeAttack(int a, int b, int c)
    {
        Vector2 makePos;
        GameObject prefab;
        BossThrowingAttack script;
        for (int i = -3; i < 4; i++)
        {
            if(i == a || i == b || i==c)
            {
                 makePos = new Vector2(11, i);
                 prefab = Instantiate(Throwing_blade_fast, makePos, Quaternion.identity);
                 script = prefab.GetComponent<BossThrowingAttack>();
                script.SetSpeedAndDir(5f, Vector2.left);
                script.SetIsOnOFF(true);
            }
            else
            {
                makePos = new Vector2(11, i);
                prefab = Instantiate(Throwing_blade_fast, makePos, Quaternion.identity);
                script = prefab.GetComponent<BossThrowingAttack>();
                script.SetSpeedAndDir(5f, Vector2.left);
                script.SetIsOnOFF(false);
            }
             
        }

    }
    IEnumerator OnOffBladeAttack_A()
    {

        OnOFFBladeAttack();

        yield return new WaitForSeconds(3.0f);
        OnOFFBladeAttack();

        yield return new WaitForSeconds(3.0f);
        OnOFFBladeAttack();

    }
    IEnumerator OnOffBladeAttack_B()
    {
        OnOFFBladeAttack(1,2,3);

        yield return new WaitForSeconds(2.5f);

        OnOFFBladeAttack(-1, 0 , 1);

        yield return new WaitForSeconds(2.5f);

        OnOFFBladeAttack(-3, -2, -1);
    }
    IEnumerator OnOffBladeAttack_C()
    {
        for(int i =0; i<5; i++)
        {
            int RI = UnityEngine.Random.Range(-2, 3);
            int a = RI - 1;
            int b = RI;
            int c = RI + 1;

            OnOFFBladeAttack(a, b, c);

            yield return new WaitForSeconds(2.5f);
        }
    }




    private int Phaze1counter = 0;
    private int Phaze2counter = 0;
    private int Phaze3counter = 0;
    IEnumerator Phase_1()
    {
        StartCoroutine(Pattern_2());
        yield return new WaitForSeconds(2f);
        StartCoroutine(Pattern_2());

        yield return new WaitForSeconds(4f);

        StartCoroutine(Pattern_3());
        yield return new WaitForSeconds(2f);
        StartCoroutine(Pattern_3());

        yield return new WaitForSeconds(4f);

        StartCoroutine(Pattern_4());
        yield return new WaitForSeconds(2f);
        StartCoroutine(Pattern_4());

        yield return new WaitForSeconds(6f);

        StartCoroutine(GenBombWallAndThrowing());


        yield return new WaitForSeconds(15f);
        Phaze1counter++;
    }
    IEnumerator Phase_2()
    {
        StartCoroutine(OnOffBladeAttack_A());
        yield return new WaitForSeconds(15.0f);
        StartCoroutine(OnOffBladeAttack_B());
        yield return new WaitForSeconds(15.0f);
        StartCoroutine(OnOffBladeAttack_C());
        yield return new WaitForSeconds(15.0f);
        StartCoroutine(GenBombWallAndThrowing());
        yield return new WaitForSeconds(6.0f);
        Phaze2counter++;
    }
    IEnumerator Phase_3()
    {
        StartCoroutine(Pattern_5());

        yield return new WaitForSeconds(15.0f);

        StartCoroutine(Pattern_2());
        StartCoroutine(Pattern_6());

        yield return new WaitForSeconds(15.0f);
        StartCoroutine(Pattern_5());

        StartCoroutine(GenBombWallAndThrowing());

        Phaze3counter++;
    }

    IEnumerator GenBombWallAndThrowing()
    {
        GameManager.Instance.ClearMap();
        GameManager.Instance.MakeBombWall();


        GameManager.Instance.MakeCubeUpDown();
        yield return new WaitForSeconds(3.3f);
        GameManager.Instance.UndoCubeUpDown();
        yield return new WaitForSeconds(1f);
        GameManager.Instance.MakeCubeUpDown();
        yield return new WaitForSeconds(3.3f);
        GameManager.Instance.UndoCubeUpDown();
        yield return new WaitForSeconds(1f);
        GameManager.Instance.MakeCubeUpDown();
        yield return new WaitForSeconds(3.3f);
        GameManager.Instance.UndoCubeUpDown();
        yield return new WaitForSeconds(1f);

        GameManager.Instance.ClearCubeWall();

        for (int i =9; i <11; i++)
        {
            for(int j = -3; j<4; j++)
            {
                Vector2Int startPos = new Vector2Int(i, j);
                GameManager.Instance.BombWallThrow(startPos, GameManager.Instance.Get_Player_Current_Pos());
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    private void O2Spawn()
    {
        
        GameObject O2prefab = Instantiate(O2item, vertical_attackPoint[11], Quaternion.identity);

    }

    private void HpSpawn()
    {
        GameObject HpPrefab = Instantiate(Hpitem, vertical_attackPoint[11], Quaternion.identity);
    }

    private bool toggle_0 = true;
    private bool toggle_1 = true;
    private bool toggle_2 = true;
    private bool toggle_3 = true;
    private bool toggle_4 = true;
    private bool toggle_5 = true;
    private bool toggle_6 = true;

    private void Update()
    {
        if(BossHP_cur <= 0) { isDead = true; }
        if (isDead)
        {
            Destroy(gameObject);

        }

        //BossHP_cur -= 100.0f * Time.deltaTime;
        BossHP_fillAmount = BossHP_cur / BossHP_max;

        BossHP_Bar.fillAmount = BossHP_fillAmount;

        curTime += Time.deltaTime;


        if(curTime >= FirstActionTime && toggle_0)
        {
            toggle_0 = false;
            if (Phaze1counter == 0)
            {
                StartCoroutine(Phase_1());
            }
        }

        if(Phaze1counter == 1 && toggle_1)
        {
            toggle_1 = false;
            StartCoroutine(Phase_1());
        }

        if(Phaze1counter == 2 && toggle_2)
        {
            toggle_2 = false;

            StartCoroutine(Phase_2());
        }

        if (Phaze2counter == 1 && toggle_3)
        {
            toggle_3 = false;
            StartCoroutine(Phase_3());
        }

        if(Phaze3counter == 1 && toggle_4)
        {
            toggle_4 = false;
            StartCoroutine(Phase_2());
        }

        if(Phaze2counter == 2 && toggle_5)
        {
            toggle_5 = false;
            StartCoroutine(Phase_3());
        }
        if (Phaze3counter == 2 && toggle_6)
        {
            toggle_6 = false;
            StartCoroutine(Phase_1());


            toggle_0 = true;
            toggle_1 = true;
            toggle_2 = true;
            toggle_3 = true;
            toggle_4 = true;
            toggle_5 = true;
            toggle_6 = true;
            Phaze1counter = 0;
            Phaze2counter = 0;
            Phaze3counter = 0;
        }

        //-----------------------------------------
        O2curTimer += Time.deltaTime;

        if(O2curTimer >= O2spawnTimer)
        {
            O2curTimer = 0;
            O2Spawn();
        }

        HPcurTimer += Time.deltaTime;

        if(HPcurTimer >= HPspawnTimer)
        {
            HPcurTimer = 0;
            HpSpawn();
        }

    }
}
