using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour {

    public Transform mapChar;

    private void LateUpdate()
    {

        transform.LookAt(mapChar.position);
        transform.position = new Vector3(mapChar.position.x, mapChar.position.y + 15, mapChar.position.z - 15);

    }
}