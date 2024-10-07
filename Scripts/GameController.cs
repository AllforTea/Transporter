using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    float lightchanges, pack_installtime;
    SwipeController swipecontroller;
    LightChanger lightchanger;
    CanvasController canvascontroller;
    PlayerController playerController;

    [SerializeField]
    GameObject backpack;
    // Start is called before the first frame update
    void Start()
    {
        swipecontroller = FindObjectOfType<SwipeController>();
        lightchanger = FindObjectOfType<LightChanger>();
        canvascontroller = FindObjectOfType<CanvasController>();
        playerController = FindObjectOfType<PlayerController>();
    }
    public IEnumerator StartRun()
    {
        Instantiate(backpack);
        StartCoroutine(lightchanger.ChangeLight(lightchanges));
        yield return new WaitForSeconds(lightchanges);
        GetOnStage();
        playerController.StartRun();
        
    }
    void GetOnStage()
    {
        //�antay� y�kle
        //I��klar� sar�,ye�ile d�nd�r
        //S�r��� ba�lat
        swipecontroller.runmode = 1;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
