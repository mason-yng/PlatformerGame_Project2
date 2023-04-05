using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator an;
    private Rigidbody2D pl;
    public AudioSource music;
    public AudioClip fail;

    private void Start()
    {
        an = GetComponent<Animator>();
        pl = GetComponent<Rigidbody2D>();
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        fail = Resources.Load<AudioClip>("sound/fail");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
            music.clip = fail;
            music.Play();
        }
    }

    private void Die()
    {
        pl.bodyType = RigidbodyType2D.Static;
        an.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
