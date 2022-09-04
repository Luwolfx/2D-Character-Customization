using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canMove;
    public float moveSpeed = 2f;
    public Vector2 movePosition => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update() 
    {
        AnimationController();
    }

    void AnimationController()
    {
        if(movePosition.x > 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if(movePosition.x < 0)
            transform.localScale = new Vector3(1f, 1f, 1f);

        if(rb.velocity != Vector2.zero)
            anim.SetBool("Moving", true);
        else
            anim.SetBool("Moving", false);
    }


    void FixedUpdate() 
    {
        if(canMove)
            MovementController();
    }

    void MovementController()
    {
        rb.velocity = movePosition.normalized * moveSpeed;
    }
}
