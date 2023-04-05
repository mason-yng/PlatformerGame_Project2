using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskDude : MonoBehaviour
{
    public float speed = 0.5f;
    public LayerMask layer;

    public Transform headPoint;
    public Transform rightUp;
    public Transform rightDown;

    public AudioSource music;
    public AudioClip die;

    private bool _collided;

    private Rigidbody2D _rigidbody2D;
    private CapsuleCollider2D _capsuleCollider2D;
    private Animator _animator;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        die = Resources.Load<AudioClip>("sound/enemy die");
    }

    private void FixedUpdate()
    {
        Vector3 movment = new Vector3(speed, _rigidbody2D.velocity.y, 0);
        transform.position += movment * Time.deltaTime;

        _collided = Physics2D.Linecast(rightUp.position, rightDown.position, layer);

        if (_collided)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, 0);
            speed *= -1f;
        }
        else
        {
            Debug.DrawLine(rightUp.position, rightDown.position,Color.blue);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Untagged")
        {
            float height = other.contacts[0].point.y - headPoint.position.y;

            if (height > 0)
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8.0f, ForceMode2D.Impulse);
                speed = 0f;
                _animator.SetTrigger("die");
                _capsuleCollider2D.enabled = false;
                _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                Destroy(gameObject, 1f);
                music.clip = die;
                music.Play();
            }
        }
    }
}
