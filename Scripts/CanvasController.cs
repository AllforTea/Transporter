using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    GameObject movescreen, bustedscreen ,gamewinscreen;
    GameController gamecontroller;

    private void Start()
    {
        gamecontroller = FindObjectOfType<GameController>();
    }
    public void MoveButton()
    {
        movescreen.SetActive(false);
        StartCoroutine(gamecontroller.StartRun());
    }

    public void BustedButton()
    {
        gamecontroller.ReloadScene();
    }

    public void BustedScreen()
    {
        if (bustedscreen.activeSelf == true)
        {
            bustedscreen.SetActive(false);
        }
        else
        {
            bustedscreen.SetActive(true);
        }
    }

    public IEnumerator GameWinSceen()
    {
        gamewinscreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
