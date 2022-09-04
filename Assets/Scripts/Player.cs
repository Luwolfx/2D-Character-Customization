using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player Movement")]
    public bool canMove;
    public float moveSpeed = 2f;
    public Vector2 movePosition => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    [Header("Player Interaction")]
    public bool canInteract;
    public Interaction nearInteractionObject;
    public GameObject interactionIcon;
    public bool interactionCooldown => ((Time.time-lastInteraction) > 1);
    float lastInteraction = 0f;


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

        if(canInteract && interactionCooldown)
            InteractionController();
    }

    void AnimationController()
    {
        if(movePosition.x > 0)
            transform.GetChild(0).localScale = new Vector3(-1f, 1f, 1f);
        else if(movePosition.x < 0)
            transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);

        if(rb.velocity != Vector2.zero)
            anim.SetBool("Moving", true);
        else
            anim.SetBool("Moving", false);
    }

    void InteractionController()
    {
        if(nearInteractionObject != null)
        {
            switch(nearInteractionObject.trigger)
            {
                case Interaction.ActionTrigger.NONE:

                    break;
                case Interaction.ActionTrigger.BUTTON_PRESS:

                    interactionIcon.SetActive(true);
                    if(Input.GetKey(KeyCode.F))
                    {
                        nearInteractionObject.Interact();
                        lastInteraction = Time.time;
                        interactionIcon.SetActive(false);
                    }
                    break;
                case Interaction.ActionTrigger.ENTER_AREA:

                    nearInteractionObject.Interact();
                    lastInteraction = Time.time;
                    interactionIcon.SetActive(false);
                    break;
            }
        }
        else
        {
            if(interactionIcon.activeInHierarchy) 
                interactionIcon.SetActive(false);
        }
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

    void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.GetComponent<Interaction>())
            nearInteractionObject = col.GetComponent<Interaction>();
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        nearInteractionObject = null;
    }

    public void TeleportPlayer(Transform targetPos)
    {
        transform.position = targetPos.position;
        print("Teleported!");
    }

    public void ToggleMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void ToogleInteractive(bool canInteract)
    {
        this.canInteract = canInteract;
    }

}
