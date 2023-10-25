using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 1.5f;
    float timer;
    public int movement = 0;
    bool flip = false;
    public static System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        timer = rnd.Next(10, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0) {
            if (movement != 0) {
                movement = 0;
                timer = rnd.Next(10, 20);
                flip = !flip;
            } else if (flip) {
                movement = -1;
                timer = 2.0f;
            } else {
                movement = 1;
                timer = 2.0f;
            }
        } else {
            timer -= Time.deltaTime;
            var pos = transform.position;
            pos.x += speed * movement * Time.deltaTime;
            transform.position = pos;
        }
    }
}
