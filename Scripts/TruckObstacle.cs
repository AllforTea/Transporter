using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckObstacle : MonoBehaviour
{
    Animation anim;
    private void Start()
    {
        anim = GetComponent<Animation>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("TruckTrigger");
            anim.Play("TruckKun");
        }
    }
}
