using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnType;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1, 2);
    }

    void Spawn()
    {
        Instantiate(spawnType, transform.position, transform.rotation);
    }
}
