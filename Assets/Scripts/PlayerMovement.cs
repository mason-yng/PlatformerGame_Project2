using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D pl;
    private Animator an;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    public AudioSource music;
    public AudioClip jump;
    public AudioClip land;

    [SerializeField] private LayerMask jumpGround;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpSpeed = 13f;


    private enum MovementState { idle, run, jump, fall }

    // Start is called before the first frame update
    private void Start()
    {
        pl = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        jump = Resources.Load<AudioClip>("sound/jump");
        land = Resources.Load<AudioClip>("sound/land");

    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        pl.velocity = new Vector2(dirX * moveSpeed, pl.velocity.y);

        if (Input.GetButtonDown("Jump") && playerLanded())
        {
            pl.velocity = new Vector2(pl.velocity.x, jumpSpeed);
            music.clip = jump;
            music.Play();
        }

        if(pl.velocity.y< 0f)
        {
            if (!playerLanded()) 
            {
                music.clip = land;
                music.Play();
            }
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.run;
            sprite.flipX = false; //face left
        }
        else if (dirX < 0f)
        {
            state = MovementState.run;
            sprite.flipX = true; //face right
        }
        else
        {
            state = MovementState.idle;
        }

        if (pl.velocity.y > .1f)
        {
            state = MovementState.jump;
        }
        else if (pl.velocity.y < -.1f)
        {
            state = MovementState.fall;
        }
        an.SetInteger("state", (int)state);
    }

    private bool playerLanded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpGround);
    }
}
