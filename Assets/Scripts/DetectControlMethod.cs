using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectControlMethod : MonoBehaviour {

    CharacterControl player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>();
    }

    private void Update()
    {
        //Check mouseinput
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            player.useController = false;
        }
        if (Input.GetAxisRaw("Mouse X") != 0.0f || Input.GetAxisRaw("Mouse Y") != 0.0f)
        {
            player.useController = false;
        }

        //Check if joystickinput
        
        if (Input.GetAxisRaw("RHorizontal") != 0.0f || Input.GetAxisRaw("RVertical") != 0.0f)
        {
            player.useController = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button0)
            || Input.GetKey(KeyCode.Joystick1Button1)
            || Input.GetKey(KeyCode.Joystick1Button2)
            || Input.GetKey(KeyCode.Joystick1Button3)
            || Input.GetKey(KeyCode.Joystick1Button4)
            || Input.GetKey(KeyCode.Joystick1Button5)
            || Input.GetKey(KeyCode.Joystick1Button6)
            || Input.GetKey(KeyCode.Joystick1Button7)
            || Input.GetKey(KeyCode.Joystick1Button8)
            || Input.GetKey(KeyCode.Joystick1Button9)
            || Input.GetKey(KeyCode.Joystick1Button10))
        {
            player.useController = true;
        }
    }
}
