using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    private int appleCount = 0;
    public AudioSource music;
    public AudioClip EatApple;

    [SerializeField] private Text appleText;

    private void Awake()
    {
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        EatApple = Resources.Load<AudioClip>("sound/eat apple");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            Destroy(collision.gameObject);
            appleCount++;
            appleText.text = "Apples: " + appleCount;
            music.clip = EatApple;
            music.Play();
        }

        if (collision.gameObject.CompareTag("Finish") && appleCount == 10)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
