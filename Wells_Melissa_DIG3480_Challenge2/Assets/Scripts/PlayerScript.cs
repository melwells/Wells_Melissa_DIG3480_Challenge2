using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score; //game score
    public Text livesText; //lives text
    public Text winText; //win text

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    Animator anim;

    private int scoreValue = 0; //score deafault
    private int livesValue = 3; //lives default

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winText.text = "";
        livesText.text = "Lives: " + livesValue.ToString();

        musicSource.clip = musicClipOne;
        musicSource.Play();

        anim = GetComponent<Animator>();
      }

    public void Update()
    {
      if (Input.GetKeyDown(KeyCode.D))
           {
                anim.SetInteger("State", 1);
           }

      if (Input.GetKeyUp(KeyCode.D))
           {
                anim.SetInteger("State", 0);
           }

      if (Input.GetKeyDown(KeyCode.A))
          {
                anim.SetInteger("State", 1);
          }

      if (Input.GetKeyUp(KeyCode.A))
           {
                anim.SetInteger("State", 0);
           }

       if (Input.GetKeyDown(KeyCode.W))
            {
                 anim.SetInteger("State", 2);
            }

        if (Input.GetKeyUp(KeyCode.W))
            {
                 anim.SetInteger("State", 0);
            }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if (scoreValue == 4)
          {
            transform.position = new Vector2(46.03f, -0.19f);
            livesValue = 3;
            SetLivesText();
          }

        if (scoreValue >= 8)
            //... then set the text property of our winText object to "You win!"
            winText.text = "You win! Game created by Melissa Wells.";
            MusicControl();

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            //count = count - 1; subtract one point from countText
            livesValue = livesValue - 1;
            SetLivesText();
        }

        if (livesValue == 0)
        {
          winText.text = "You Lose. Game created by Melissa Wells.";
          Destroy(this);
        }

    void SetLivesText()
    {
        //setting lives text information
        livesText.text = "Lives: " + livesValue.ToString ();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void MusicControl()
    {
      if (scoreValue == 8)
      {
        musicSource.clip = musicClipTwo;
        musicSource.Play();
    }
  }
}
