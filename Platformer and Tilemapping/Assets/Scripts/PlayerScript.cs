using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    public float jumpForce;


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
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

}
