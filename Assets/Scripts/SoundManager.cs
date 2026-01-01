using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
   


    private AudioClip BGMobj;
    private AudioClip CofirmBtnSFXobj;
    private AudioClip CollectBtnClickSFXobj;
    private AudioClip CubeDestroySFXobj;
    private AudioClip LazerFireSFXobj;
    private AudioClip DenyingBtnSFXobj;
    private AudioClip MetaCubeGenSFXobj;
    private AudioClip PlayerMovementSFXobj;
    private AudioClip PlayerMove_sustainObj;
    private AudioClip TickSFXobj;
    private AudioClip BombExplosionSFXobj;
    private AudioClip BladeFlowerSFXobj;
    private AudioClip CoreModuleGet;
    private AudioClip PlayerHit;
    private AudioClip itemGet;



    private AudioSource BGM;
    private AudioSource ConfirmBtnSFX;
    private AudioSource CollectBtnClickSFX;
    private AudioSource CubeDestroySFX;
    private AudioSource LazerFireSFX;
    private AudioSource DenyingBtnSFX;
    private AudioSource MetaCubeGenSFX;
    private AudioSource PlayerMovementSFX;
    private AudioSource PlayerMove_sustainSFX;
    private AudioSource TickSFX;
    private AudioSource BombExplosionSFX;
    private AudioSource BladeFlowerSFX;
    private AudioSource CoreModuleGetSFX;
    private AudioSource PlayerHitSFX;
    private AudioSource itemGetSFX;



    private static string FileName_BGM = "BGM_01";
    private static string FileName_ConfirmBtnSFX = "Cofirm Btn SFX";
    private static string FileName_CollectBtnClickSFX = "Collect Btn Click SFX";
    private static string FileName_CubeDestroySFX = "SBsfi1_Arcade Single 005";
    private static string FileName_LazerFireSFX = "Laser Fire SFX";
    private static string FileName_DenyingBtnSFX = "Denying Btn SFX";
    private static string FileName_MetaCubeGenSFX = "MetaCubeGen VFX";
    private static string FileName_PlayerMovementSFX = "PlayerMovementSFX";
    private static string FileName_PlayerMove_sustainSFX = "PlayerMovementSFX_sustain2";
    private static string FileName_TickSFX = "Tick SFX";
    private static string FileName_BombExplosionSFX = "Future Weapons 3 - Grenade Launcher 2 - Hit 2";
    private static string FileName_BladeFlowerSFX = "270901 - Rotary Hammer Drill in Wall 01";
    private static string FileName_CoreModuleGetSFX = "power up_level up-sound 2";
    private static string FIleName_PlayerHitSFX = "BR_Glitch 047";
    private static string FileName_itemGetSFX = "collecting orb-sound 2";


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
        Register_AudioSources();
    }
    private void Register_AudioSources()
    {

        BGM = gameObject.AddComponent<AudioSource>();
        ConfirmBtnSFX = gameObject.AddComponent<AudioSource>();
        CollectBtnClickSFX = gameObject.AddComponent<AudioSource>();
        CubeDestroySFX = gameObject.AddComponent<AudioSource>();
        LazerFireSFX = gameObject.AddComponent<AudioSource>();
        DenyingBtnSFX = gameObject.AddComponent<AudioSource>();
        MetaCubeGenSFX = gameObject.AddComponent<AudioSource>();
        PlayerMovementSFX = gameObject.AddComponent<AudioSource>();
        PlayerMove_sustainSFX = gameObject.AddComponent<AudioSource>();
        TickSFX = gameObject.AddComponent<AudioSource>();
        BombExplosionSFX = gameObject.AddComponent<AudioSource>();
        BladeFlowerSFX = gameObject.AddComponent<AudioSource>();
        CoreModuleGetSFX = gameObject.AddComponent<AudioSource>();
        PlayerHitSFX = gameObject.AddComponent<AudioSource>();
        itemGetSFX = gameObject.AddComponent<AudioSource>();



        BGMobj = Resources.Load<AudioClip>($"Sound/{FileName_BGM}");
        CofirmBtnSFXobj = Resources.Load<AudioClip>($"Sound/{FileName_ConfirmBtnSFX}");
        CollectBtnClickSFXobj = Resources.Load<AudioClip>($"Sound/{FileName_CollectBtnClickSFX}");
        CubeDestroySFXobj = Resources.Load<AudioClip>($"Sound/{FileName_CubeDestroySFX}");
        LazerFireSFXobj = Resources.Load<AudioClip>($"Sound/{FileName_LazerFireSFX}");
        DenyingBtnSFXobj = Resources.Load<AudioClip>($"Sound/{FileName_DenyingBtnSFX}");
        MetaCubeGenSFXobj = Resources.Load<AudioClip>($"Sound/{FileName_MetaCubeGenSFX}");
        PlayerMovementSFXobj = Resources.Load<AudioClip>($"Sound/{FileName_PlayerMovementSFX}");
        PlayerMove_sustainObj = Resources.Load<AudioClip>($"Sound/{FileName_PlayerMove_sustainSFX}");
        TickSFXobj = Resources.Load<AudioClip>($"Sound/{FileName_TickSFX}");
        BombExplosionSFXobj = Resources.Load<AudioClip>($"Sound/{FileName_BombExplosionSFX}");
        BladeFlowerSFXobj = Resources.Load<AudioClip>($"Sound/{FileName_BladeFlowerSFX}");
        CoreModuleGet = Resources.Load<AudioClip>($"Sound/{FileName_CoreModuleGetSFX}");
        PlayerHit = Resources.Load<AudioClip>($"Sound/{FIleName_PlayerHitSFX}");
        itemGet = Resources.Load<AudioClip>($"Sound/{FileName_itemGetSFX}");



        BGM.clip = BGMobj;
        ConfirmBtnSFX.clip = CofirmBtnSFXobj;
        CollectBtnClickSFX.clip = CollectBtnClickSFXobj;
        CubeDestroySFX.clip = CubeDestroySFXobj;
        LazerFireSFX.clip = LazerFireSFXobj;
        DenyingBtnSFX.clip = DenyingBtnSFXobj;
        MetaCubeGenSFX.clip = MetaCubeGenSFXobj;
        PlayerMovementSFX.clip = PlayerMovementSFXobj;
        PlayerMove_sustainSFX.clip = PlayerMove_sustainObj;
        TickSFX.clip = TickSFXobj;
        BombExplosionSFX.clip = BombExplosionSFXobj;
        BladeFlowerSFX.clip = BladeFlowerSFXobj;
        CoreModuleGetSFX.clip = CoreModuleGet;
        PlayerHitSFX.clip = PlayerHit;
        itemGetSFX.clip = itemGet;



        BGM.playOnAwake = false;
        ConfirmBtnSFX.playOnAwake = false;
        CollectBtnClickSFX.playOnAwake = false;
        CubeDestroySFX.playOnAwake = false;
        LazerFireSFX.playOnAwake = false;
        DenyingBtnSFX.playOnAwake = false;
        MetaCubeGenSFX.playOnAwake = false;
        PlayerMovementSFX.playOnAwake = false;
        PlayerMove_sustainSFX.playOnAwake = false;
        TickSFX.playOnAwake = false;
        BombExplosionSFX.playOnAwake = false;
        BladeFlowerSFX.playOnAwake = false;
        PlayerHitSFX.playOnAwake = false;
        itemGetSFX.playOnAwake = false;


        BGM.loop = true;
        PlayerMove_sustainSFX.loop = true;
        BGM.volume = 0.2f;
        CoreModuleGetSFX.volume = 0.5f;
        BombExplosionSFX.volume = 0.5f;
        PlayerHitSFX.volume = 0.2f;
        PlayerHitSFX.pitch = 1.2f;
        itemGetSFX.volume = 0.6f;

        Debug.Log("SOUND REGISTER COMPLETE.");
    }

    public void AllMute(bool onOff)
    {
        BGM.mute = onOff;
        ConfirmBtnSFX.mute = onOff;
        CollectBtnClickSFX.mute = onOff;
        CubeDestroySFX.mute = onOff;
        LazerFireSFX.mute = onOff;
        DenyingBtnSFX.mute = onOff;
        MetaCubeGenSFX.mute = onOff;
        PlayerMovementSFX.mute = onOff;
        PlayerMove_sustainSFX.mute = onOff;
        TickSFX.mute = onOff;
        BombExplosionSFX.mute = onOff;
        BladeFlowerSFX.mute = onOff;
    }

    public AudioSource GetSFX_BGM()
    {
        return BGM;
    }
    public AudioSource GetSFX_ConfirmBtn()
    {
        return ConfirmBtnSFX;
    }
    public AudioSource GetSFX_CollectBtnClick()
    {
        return CollectBtnClickSFX;
    }
    public AudioSource GetSFX_CubeDestroy()
    {
        return CubeDestroySFX;
    }
    public AudioSource GetSFX_LazerFire()
    {
        return LazerFireSFX;
    }
    public AudioSource GetSFX_DenyingBtn()
    {
        return DenyingBtnSFX;
    }
    public AudioSource GetSFX_MetaCubeGen()
    {
        return MetaCubeGenSFX;
    }
    public AudioSource GetSFX_PlayerMovement()
    {
        return PlayerMovementSFX;
    }
    public AudioSource GetSFX_PlayerMoveSustain()
    {
        return PlayerMove_sustainSFX;
    }
    public AudioSource GetSFX_Tick()
    {
        return TickSFX;
    }
    public AudioSource GetSFX_BombExplosion()
    {
        return BombExplosionSFX;
    }
    public AudioSource GetSFX_BladeFlower()
    {
        return BladeFlowerSFX;
    }
    public AudioSource GetSFX_CoreModureGet()
    {
        return CoreModuleGetSFX;
    }
    public AudioSource GetSFX_PlayerHitSFX()
    {
        return PlayerHitSFX;
    }

    public AudioSource GetSFX_itemGetSFX()
    {
        return itemGetSFX;
    }
    
}
