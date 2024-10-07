using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{

    Touch touch;

    //Runmode 0=No Movement/1=Controlled Movement/2=Uncontrolled Movement/3=ForcedMovement
    public int runmode = 3;
    public float speed;
    float basespeed;
    [SerializeField] float swipeFactor, rotateFactor;
    public float maxSwipeLeft, maxSwipeRight;
    float mainSwipefactor;

    float fS, fT, fR;
    private void Start()
    {
        mainSwipefactor = swipeFactor;
        basespeed = speed;
    }
    private void FixedUpdate()
    {
        if (runmode==1)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    fS = (touch.position.x - Screen.width / 2) * 2 / Screen.width;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    fT = (touch.position.x - Screen.width / 2) * 2 / Screen.width;
                    //Debug.Log("fR=" + fR.ToString());
                }
                fR = transform.position.x / maxSwipeRight * 0.7f;
                transform.Translate((fT - fS) * swipeFactor * Time.fixedDeltaTime, 0, speed * Time.fixedDeltaTime);
            }
            else
            {
                transform.Translate(0, 0, speed * Time.fixedDeltaTime);
            }
            ValidateLocation();
        }else if (runmode == 2)
        {
            transform.Translate(0, 0, speed * Time.fixedDeltaTime);
        }
    }
    private void ValidateLocation()
    {
        var currentLocation = transform.position;

        if (currentLocation.x >= maxSwipeRight)
        {
            currentLocation.x = maxSwipeRight;
        }

        else if (currentLocation.x <= maxSwipeLeft)
        {
            currentLocation.x = maxSwipeLeft;
        }

        transform.position = currentLocation;
    }
    public IEnumerator SlowlyLowerSpeed()
    {
        for (int a = 0; a < 10; a++)
        {
            speed = basespeed-(basespeed * (a+1) / 10);
            yield return new WaitForSeconds(0.1f);
        }


    }

    
}
