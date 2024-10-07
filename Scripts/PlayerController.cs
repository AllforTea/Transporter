using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject backpack, explosion , targetAim, destroyedbridge;
    GameController gamecontroller;
    Animator playeranimator;
    SwipeController swipecontroller;
    MobSpawner mobspawner;
    CanvasController canvascontroller;

    float swipemaxleft  = -4f, swipemaxright = 4f, ROAD1MAXLEFT=-3.6f, ROAD1MAXRIGHT=-2.6f, ROAD2MAXLEFT=-0.7f, ROAD2MAXRIGHT=0.7f, ROAD3MAXLEFT=2.6f, ROAD3MAXRIGHT=3.6f;
    bool isFrictionOn=false;
    bool isRunOn=false;
    bool undying = false;
    bool aimlock = false;

    private void Start()
    {
        swipecontroller = GetComponent<SwipeController>();
        playeranimator = GetComponent<Animator>();
        gamecontroller = FindObjectOfType<GameController>();
        mobspawner = FindObjectOfType<MobSpawner>();
        canvascontroller = FindObjectOfType<CanvasController>();
        
    }

    public void StartRun()
    {
        isRunOn = true;
        StartCoroutine(CheckFriction());
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TruckObs" && undying == false)
        {
            StartCoroutine(TruckDeath());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BarbedObs")
        {
            StartCoroutine(BarbedDeath());
        }
        else if (other.gameObject.tag=="BackPack")
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "VanJumpTrig")
        {
            playeranimator.SetTrigger("VanJumpTrigger");
            swipecontroller.runmode = 2;
            aimlock = false;
            undying = true;
            targetAim.SetActive(false);
        }
        else if (other.gameObject.tag == "DestructableObs")
        {
            StartCoroutine(other.GetComponent<DestructableObs>().Yolaglatan());    
        }
        else if (other.gameObject.tag=="AimRange")
        {
            StartCoroutine(AimLock());
        }
        else if (other.gameObject.tag=="JumpExit")
        {
            swipecontroller.runmode = 1;
            undying = false;
        }
        else if (other.gameObject.tag == "ForceWay")
        {
            
            //ForceWay forceway = other.GetComponent<ForceWay>();
            //StartCoroutine(ForceALane(forceway.forceTime , forceway.forceType));
        }
        else if(other.gameObject.tag == "Final")
        {
            StartCoroutine(GameFinali());
        }
        else if(other.gameObject.tag == "BridgeObs")
        {
            aimlock = false;
            GameObject broke = Instantiate(destroyedbridge, other.transform.position, other.transform.rotation);
            other.gameObject.SetActive(false);
            StartCoroutine(DestroyLater(broke, 4f));
            
        }
    }
    IEnumerator DestroyLater(GameObject gameo, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameo);
    }

    IEnumerator AimLock()
    {
        aimlock = true;
        targetAim.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        if(aimlock == true)
        {
            StartCoroutine(ExplodeDeath());
        }
    }

    IEnumerator GameFinali()
    {
        StartCoroutine(swipecontroller.SlowlyLowerSpeed());
        
        yield return new WaitForSeconds(1f);
        swipecontroller.runmode = 3;
        GameObject bag = Instantiate(backpack, new Vector3(transform.position.x,transform.position.y+2,transform.position.z), transform.rotation);
        yield return new WaitForSeconds(1f);
        StartCoroutine(canvascontroller.GameWinSceen());
    }
    IEnumerator TruckDeath()
    {
        
        swipecontroller.runmode = 3;
        playeranimator.SetTrigger("HitTruckTrigger");
        yield return new WaitForSeconds(1f);
        mobspawner.SpawnVanMobs(1);
        yield return new WaitForSeconds(3.5f);
        canvascontroller.BustedScreen();
    }

    IEnumerator BarbedDeath()
    {
        swipecontroller.runmode = 3;
        playeranimator.SetTrigger("HitBarbedTrigger");
        yield return new WaitForSeconds(2f);
        mobspawner.SpawnVanMobs(2);
        yield return new WaitForSeconds(3.5f);
        canvascontroller.BustedScreen();
    }

    IEnumerator CheckFriction()
    {
        while (isRunOn == true)
        {
            yield return new WaitForSeconds(0.25f);
            float Xpos = transform.position.x;
            if (Xpos < swipemaxleft+0.1f && isFrictionOn == false)
            {
                Debug.Log("EnterFriction");
                playeranimator.SetTrigger("ForcedLeft");
                isFrictionOn = true;
            }
            else if (Xpos > swipemaxright-0.1f && isFrictionOn == false)
            {
                Debug.Log("EnterFriction");
                playeranimator.SetTrigger("ForcedRight");
                isFrictionOn = true;
            }
            else if (Xpos >= swipemaxleft+0.1f && Xpos <= swipemaxright - 0.1f && isFrictionOn==true)
            {
                Debug.Log("ReleaseFriction");
                playeranimator.SetTrigger("ReleaseForce");
                isFrictionOn = false;
            }
        }
        

    }

    void UpdateSwipeLimits(float newleft, float newright)
    {
        swipemaxleft = newleft;
        swipemaxright = newright;
        swipecontroller.maxSwipeLeft = newleft;
        swipecontroller.maxSwipeRight = newright;
    }
    void LimitLane(int roadway)
    {
        //Lane will be limited 1=left/2=mid/3=right/4=left+mid/5=mid+right/6=mid
        if (roadway == 1)
        {
            UpdateSwipeLimits(ROAD1MAXLEFT, ROAD1MAXRIGHT);
        }
        else if (roadway == 2)
        {
            UpdateSwipeLimits(ROAD2MAXLEFT, ROAD2MAXRIGHT);
        }
        else if (roadway ==3)
        {
            UpdateSwipeLimits(ROAD3MAXLEFT, ROAD3MAXRIGHT);
        }
        else if(roadway == 12)
        {
            UpdateSwipeLimits(ROAD1MAXLEFT, ROAD2MAXRIGHT);
        }
        else if(roadway == 23)
        {
            UpdateSwipeLimits(ROAD2MAXLEFT, ROAD3MAXRIGHT);
        }
    }
    IEnumerator ForceALane(float time , int roadway)
    {
        undying = true;
        LimitLane(roadway);
        yield return new WaitForSeconds(time);
        UpdateSwipeLimits(-4f, 4f);
        undying = false;

    }

    public IEnumerator ExplodeDeath()
    {
        swipecontroller.runmode = 3;
        Instantiate(explosion, transform.position, transform.rotation);
        yield return new WaitForSeconds(1f);
        canvascontroller.BustedScreen();
        yield return new WaitForSeconds(2f);
        gamecontroller.ReloadScene();
    }
}
