using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {

    public float maxint = 0;
    public float minint = 0;
    public float multiplier = 1;


	// Update is called once per frame
	void Update () {
        this.GetComponent<Light>().intensity = (minint + maxint) / 2 + Mathf.Sin(multiplier*Time.realtimeSinceStartup); 
	}
}
