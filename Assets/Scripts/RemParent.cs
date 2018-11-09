using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemParent : MonoBehaviour {

    public List<GameObject> WPlist = new List<GameObject>();

	void Start () {

        transform.parent= null;
		
	}

}
