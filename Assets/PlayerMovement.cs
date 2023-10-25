using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spr;
    GameObject[] platforms;

    float moveForce = 0.75f;
    float jumpForce = 4.5f;
    float cloudSpeed = 1.0f;
    float hitForce = 10.0f;

    bool isJumping = false;
    bool isFalling = false;
    GameObject? cloudId = null;    

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();

        platforms = GameObject.FindGameObjectsWithTag("Platform");
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && !(isJumping || isFalling)) {
            anim.SetBool("OnCloud", false);
            cloudId = null;
            anim.SetTrigger("Jump");
            isJumping = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            foreach (var item in platforms) {
                item.GetComponent<BoxCollider2D>().enabled = false;
            }
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        if (rb.velocity.y <= 0) {
            isJumping = false;
            isFalling = true;
            foreach (var item in platforms) {
                item.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        if (isFalling && (rb.velocity.y == 0)) {
            isFalling = false;
        }

        rb.AddForce(transform.right * moveForce * Input.GetAxis("Horizontal"), ForceMode2D.Force);

        if (rb.velocity.x * rb.velocity.x > 0.25) {
            anim.SetBool("IsWalking", true);
        } else {
            anim.SetBool("IsWalking", false);
        }

        if (rb.velocity.x < -0.1) {
            spr.flipX = true;
        } else if (rb.velocity.x > 0.1) {
            spr.flipX = false;
        }

        if (cloudId != null && transform.position.x > 30.4f && transform.position.x < 56.2f) {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            var pos = transform.position;
            pos.x += cloudId.GetComponent<CloudMovement>().speed * cloudId.GetComponent<CloudMovement>().movement * Time.deltaTime;
            transform.position = pos;
            rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Platform") {
            rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
            anim.SetBool("OnCloud", true);
            cloudId = col.gameObject;
        }
        if (col.gameObject.tag == "Fireball") {
            cloudId = null;
            anim.SetBool("OnCloud", false);
            var pos = transform.position;
            pos.x = 30.8f;
            pos.y = -6.5f;
            transform.position = pos;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
