using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnType;
    public float rate;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1, rate);
    }

    void Spawn()
    {
        Instantiate(spawnType, transform.position, transform.rotation);
    }
}
