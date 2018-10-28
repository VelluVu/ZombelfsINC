using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour {

    
    public Transform mapChar;

    private void LateUpdate()
    {

        transform.position = mapChar.position - new Vector3(0,0,10);
        

        /*transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftXBounds, rightXBounds), 0,-10);
        transform.Translate(Input.GetAxis("Horizontal")*camSpeed*Time.deltaTime, 0, 0);*/
        
    }
}
