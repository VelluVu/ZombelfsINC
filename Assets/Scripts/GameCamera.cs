using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        transform.LookAt(player.position);
        
        transform.position = new Vector3(player.position.x,player.position.y + 30 ,player.position.z -15);
    }
}
