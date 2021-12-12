using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    public float speed = 1;
    public float distance = 2;

    private void Start()
    {
        Destroy(gameObject, 10);
    }
    void Update()
    {
        if(Vector3.Distance(transform.position, Player.instance.transform.position) < distance)
        {
            if (Player.isDasing) return;
            Destroy(gameObject);
            Player.instance.life -= 1;
            if (Player.instance.life <= 0) GameStartup.instance.EndGame(false);
        }

        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
