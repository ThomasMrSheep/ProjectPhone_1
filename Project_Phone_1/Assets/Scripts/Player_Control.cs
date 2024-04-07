using System.Linq;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;


public class Player_Control : MonoBehaviour
{
    public FixedJoystick joystick; 

    private Rigidbody2D rb;
    [Header("Player Varibles")]
    public int playerMaxHealth = 100;
    private int playerHealth = 100;
    public int playerMovespeed = 1;
    public int playerJumpVelocity;
    private bool touchingGround;
    private float jumpcooldown;
    private bool facingLeft;

    [Header("Bullet variables")]
    public GameObject bulletAsset;
    public Transform bulletSpawn;
    public float attackCooldown = 1;
    private float cooldown;
    void Start() {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        if (rb == null){
        Debug.LogError("Forgot to set player tag");
    }}
    private void FixedUpdate()
    {
        //Switch Player direction
        if (facingLeft){
            gameObject.transform.eulerAngles = new float3(0,180,0);
        }else{
            gameObject.transform.eulerAngles = new float3(0,0,0);
        }
        //Jump on touch screen
        if (Input.touchCount > 0){
            try{
                Touch toch = Input.GetTouch(1);
                if (toch.phase == TouchPhase.Began){
                    Jump();
                }
            }
            catch{
                Touch toch = Input.GetTouch(0);
                if (joystick.Horizontal == 0 && toch.phase == TouchPhase.Began){
                    Jump();
                }
            }
        }
        //Jump on get axis vertical
        if (Input.GetAxis("Vertical")>0){
            Jump();
        }
        //Move player
        float movedir = Input.GetAxisRaw("Horizontal") + joystick.Horizontal;
        if (movedir > 0){
            gameObject.transform.position += new UnityEngine.Vector3(playerMovespeed*Time.fixedDeltaTime,0,0);
            facingLeft = false;
        }else if (movedir < 0){
            gameObject.transform.position += new UnityEngine.Vector3(-playerMovespeed*Time.fixedDeltaTime,0,0);
            facingLeft = true;
        }
        
        //resets weapon cooldown
        if (cooldown > 0){
            cooldown -= Time.fixedDeltaTime;
        }
        //fires gun
        if(Input.GetAxis("Fire1")>0){
            FireBullet();
        }
        //resets jump cooldown
        if(jumpcooldown > 0){
            jumpcooldown -= Time.fixedDeltaTime;
        }
    }
    public void FireBullet(){
        if(bulletAsset == null){Debug.LogWarning("Bullet Asset set to \"null\""); return;}
        if (cooldown > 0){return;}
        cooldown = attackCooldown;
        GameObject nBullet = Instantiate(bulletAsset, bulletSpawn.position, bulletSpawn.transform.rotation);
    }

    void Jump(){
        if (jumpcooldown > 0 || rb.velocity.y < 0){return;}
        //primeira raycast no lado esquerdo do objeto
        RaycastHit2D[] below = 
        Physics2D.RaycastAll(
            transform.position + new UnityEngine.Vector3(transform.localScale.x/2-+0.1f,0,0),
            -transform.up, 
            transform.localScale.y/2 + 0.1f);

        //segunda lista de raycasts no ladi esquerdo do obj
        RaycastHit2D[] below2 = 
        Physics2D.RaycastAll(
            transform.position + new UnityEngine.Vector3(-transform.localScale.x/2+0.1f,0,0),
            -transform.up, 
            transform.localScale.y/2 + 0.1f);
        //concatena ambas arrays    
        RaycastHit2D[] belowActual = below.Concat(below2).ToArray();
        foreach(RaycastHit2D obj in belowActual){
            if (obj.collider.gameObject != gameObject){
                gameObject.GetComponent<Rigidbody2D>().velocity = UnityEngine.Vector3.up * playerJumpVelocity;
                return;
            }}
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(new UnityEngine.Vector3(
            transform.position.x + transform.localScale.x/2,
            transform.position.y, 
            transform.position.z
        ), 
        
            new UnityEngine.Vector3
                (transform.position.x, 
                transform.position.y - transform.localScale.y/2- 0.3f, 
                transform.position.z));
        Gizmos.DrawLine(
            new UnityEngine.Vector3(
                transform.position.x - transform.localScale.x/2,
                transform.position.y, 
                transform.position.z
            ), 
        
            new UnityEngine.Vector3
                (transform.position.x, 
                transform.position.y - transform.localScale.y/2- 0.3f, 
                transform.position.z));
    }
}
