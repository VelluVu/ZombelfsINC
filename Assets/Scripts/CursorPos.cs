﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPos : MonoBehaviour {
	
	void Update ()
    {

        gameObject.transform.position = Input.mousePosition;

	}
}
