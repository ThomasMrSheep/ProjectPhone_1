using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;
    public float speed;
    public int damage;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<BoxCollider2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 5f);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            /*
                Put code here to do damage
                🌩️🌩️🌩️
                🌩️🌩️🌩️
                🌩️🌩️🌩️
                You can use a Try{}Catch{} to try to do damage to "other" or use this method, CAREFULL WITH TOO MANY "IF" STATEMENTS

                Coloque código aqui para dar dano
                🌩️🌩️🌩️
                🌩️🌩️🌩️
                🌩️🌩️🌩️
                Você pode usar Try{}Catch{} para tentar dar dano ao "other", CUIDADO AO USAR MUITOS "IF"
            */
            Debug.Log($"Destroying bullet {gameObject.name}, hit {other.gameObject.name}");
            Destroy(gameObject);
        }
        if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Player_Bullet")){
            // Put code here to colide with anything
            Debug.Log($"Destroying bullet {gameObject.name}, hit {other.gameObject.name}");
            Destroy(gameObject);
        }

    }
}
