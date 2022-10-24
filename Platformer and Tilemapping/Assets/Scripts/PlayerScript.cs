using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public Text score;
    public GameObject winText;
    public GameObject loseText;
    private int scoreValue = 0;
    public float jumpForce;
    public int livesCount = 3;
    public Text lives;
    public bool playerWon = false;
    public Vector2 level2Position = new Vector2(73.25f, -1.45f);
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public Animator playerAnim;
    public Transform playerGraphics;


    private void Start() {
        musicSource.clip = musicClipOne;
        musicSource.Play();
        rb = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.SetActive(false);
        loseText.SetActive(false);
        livesCount = 3;
        lives.text = livesCount.ToString();
    }

    private void Update() {
        if(rb.velocity.y == 0 && rb.velocity.x != 0){
            playerAnim.SetInteger("onGround", 1);
            playerAnim.SetInteger("inAir", 0);
        }else if(rb.velocity.y == 0 && rb.velocity.x == 0){
            playerAnim.SetInteger("onGround", -1);
            playerAnim.SetInteger("inAir", 0);
        }else if(rb.velocity.y > 0){
            playerAnim.SetInteger("inAir", 1);
            playerAnim.SetInteger("onGround", 0);
        }else if(rb.velocity.y < 0){
            playerAnim.SetInteger("inAir", -1);
            playerAnim.SetInteger("onGround", 0);
        }

        if(rb.velocity.x < 0){
            playerGraphics.localScale = new Vector3(-1, 1, 1);
        }else if(rb.velocity.x > 0){
            playerGraphics.localScale = new Vector3(1, 1, 1);
        }
    }

    private void FixedUpdate() {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rb.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Coin"){
            scoreValue += 1;
            score.text = scoreValue.ToString();
            UpdateWinText();
            Destroy(other.collider.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.collider.tag == "Ground"){
            if(Input.GetKey(KeyCode.W)){
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    private void UpdateWinText(){
        if(scoreValue == 4){
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = level2Position;
        }

        if(scoreValue == 8){
            winText.SetActive(true);
            playerWon = true;
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
    }

    private void UpdateLivesText(){
        lives.text = livesCount.ToString();

        if(livesCount <= 0 && !playerWon){
            loseText.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Enemy"){
            livesCount--;
            UpdateLivesText();
            Destroy(other.gameObject);
        }
    }

}
