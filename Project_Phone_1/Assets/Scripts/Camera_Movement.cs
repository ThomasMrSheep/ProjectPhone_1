using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public float cameraSpeed = 1;
    private GameObject player;

    private Vector3 vector2Playerpos;
    private Vector3 vector2campos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vector2campos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        vector2Playerpos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        gameObject.transform.position += (vector2Playerpos - vector2campos) * cameraSpeed * Time.fixedDeltaTime;
    }
}
