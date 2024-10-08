using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed = 5;
    [SerializeField] private  float _jumpPower = 5;

    private bool isFacingRight = true;
    private float horizontal;

    public float GroundRayDistance;
    [HideInInspector] public bool IsCanMove;

    public SoundPlayer sound;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        var collider = GetComponent<BoxCollider2D>();
        GroundRayDistance = collider.size.y / 2 + 0.01f;
        IsCanMove = true;
    }

    void Update()
    {
        if (!IsCanMove)
            return;
            
        
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpPower);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        Flip();

        if (rb.velocity.magnitude != 0)
        {
            sound.gameObject.GetComponent<AudioSource>().volume = 0.2F;
        }
        else
        {
            sound.gameObject.GetComponent<AudioSource>().volume = 0F;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * _speed, rb.velocity.y);
    }

    public void UpdateMovementValues(float speed,float jumpPower)
    {
        _speed = speed;
        _jumpPower = jumpPower;
        var collider = GetComponent<BoxCollider2D>();
        GroundRayDistance = collider.size.y / 2 + 0.01f;
        IsCanMove = true;
    }

    private bool IsGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, -groundCheck.up, GroundRayDistance, groundLayer);
        if (hit)
            return true;
        else 
            return false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0 || !isFacingRight && horizontal >0) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    public void TurnMovement(bool status)
    {
        rb.bodyType = status ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
        IsCanMove = status;
    }
}
