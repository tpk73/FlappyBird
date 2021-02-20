using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notACopyofBirdScript : MonoBehaviour
{
    public float upForce;
    private bool isDead = false;

    public int oofs = 2;

    private Animator anim;
    private Rigidbody2D rb2d;

    private SpriteRenderer steveSprite;
    private PolygonCollider2D stevePoly;

    void Start() {

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        steveSprite = GetComponent<SpriteRenderer>();
        stevePoly = GetComponent<PolygonCollider2D>();
    }

    void Update() {

        if (isDead == false) {
            
            if (Input.GetButtonDown("Flap")) {
                
                anim.SetTrigger("Flap");
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        
        if (other.gameObject.tag == "column") {
            
            oofs--;

            if (oofs <= 0) {

                rb2d.velocity = Vector2.zero;
                isDead = true;
                anim.SetTrigger("Die");
                NotACopyOfGameController.instance.BirdDied();
            } else {

                stevePoly.enabled = false;
                steveSprite.color = new Color(1, 0, 0, .5f);
                StartCoroutine(EnableBox(1.0f));
            }
        } else {

            rb2d.velocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Die");
            NotACopyOfGameController.instance.BirdDied();
        }
    
    }
    IEnumerator EnableBox (float waitTime) {

        yield return new WaitForSeconds(waitTime);
        steveSprite.color = new Color(1, 1, 1, 1);
        stevePoly.enabled = true;
    }
}

