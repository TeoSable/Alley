using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMovement : MonoBehaviour
{
    public float speed = 5.0f;
    float timer;
    bool fire = false;
    public static System.Random rand = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        timer = rand.Next(10, 15);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0) {
            fire = !fire;
            if (fire) {
                timer = 6.0f;
            } else {
                timer = rand.Next(10, 15);
            }
        } else {
            timer -= Time.deltaTime;
        }
        var pos = transform.position;
        if (fire) {
            pos.x -= speed * Time.deltaTime;
        } else {
            pos.x = 58;
        }
        transform.position = pos;
    }
}
