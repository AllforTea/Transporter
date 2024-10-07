using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObs : MonoBehaviour
{
    [SerializeField]
    GameObject destructedObj;

    public IEnumerator Yolaglatan()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject lamb = Instantiate(destructedObj, transform.position, transform.rotation);
        yield return new WaitForSeconds(2f);
        lamb.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
