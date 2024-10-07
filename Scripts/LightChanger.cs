using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanger : MonoBehaviour
{
    [SerializeField]
    MeshRenderer[] lights;
    [SerializeField]
    Material lightgreen, lightyellow;
    GameController GameController;
    public IEnumerator ChangeLight(float time)
    {
        yield return new WaitForSeconds(time / 2);
        lights[0].material = lightyellow;
        lights[1].material = lightyellow;

        yield return new WaitForSeconds(time / 2);
        lights[0].material = lightgreen;
        lights[1].material = lightgreen;
    }
}