using System.Collections;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    private Vector2 myPos;
    private float offset = 0.5f;

    private GameObject playerImgObj;

    private SpriteRenderer gunImg;
    

    private Sprite gunImgObj_1;
    private Sprite gunImgObj_2;


    private GameObject bullet;

    private void Start()
    {
        bullet = Resources.Load<GameObject>("Prefabs/Boss/PlayerBullet");

        playerImgObj = GameObject.Find("Player IMG Obj");

        gunImgObj_1 = Resources.Load<Sprite>("Prefabs/Boss/player Gun_0");
        gunImgObj_2 = Resources.Load<Sprite>("Prefabs/Boss/player Gun_1");



        gunImg = this.gameObject.GetComponent<SpriteRenderer>();

        gunImg.sortingOrder = 70;
        gunImg.sprite = gunImgObj_1;

        StartCoroutine(GunImgChanger());
    }
    private void CalcMyPos()
    {
        myPos = new Vector2(playerImgObj.transform.position.x -0.15f, playerImgObj.transform.position.y + 0.4f);
    }

    private void Update()
    {
        CalcMyPos();
        transform.position = myPos;
    }

    IEnumerator GunImgChanger()
    {
        while (true)
        {
            gunImg.sprite = gunImgObj_1;

            yield return new WaitForSeconds(0.5f);

            gunImg.sprite = gunImgObj_2;
            StartCoroutine(FireBullet());
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FireBullet()
    {
        Vector2 firePos = new Vector2(transform.position.x + 0, transform.position.y + 0.1f);

        GameObject bulletPrefab_1 = Instantiate(bullet, firePos, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);

        GameObject bulletPrefab_2 = Instantiate(bullet, firePos, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);

        GameObject bulletPrefab_3 = Instantiate(bullet, firePos, Quaternion.identity);
        
    }

}
