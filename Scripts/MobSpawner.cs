using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject threevanmobs, sixvanmobs;
    public void SpawnVanMobs(int mode)
    {
        if (mode == 1)
        {
            GameObject instamobs = Instantiate(threevanmobs, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            instamobs.transform.parent = gameObject.transform;
        }
        else
        {
            GameObject instamobs = Instantiate(sixvanmobs, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            instamobs.transform.parent = gameObject.transform;
        }
        
    }
}
