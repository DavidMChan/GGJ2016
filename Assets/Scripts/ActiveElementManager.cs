using UnityEngine;
using System.Collections;

public class ActiveElementManager : MonoBehaviour {

    public ActiveElementDetector[] locations;
    public float detectRadius = 0.05f;



	// Update is called once per frame
	void Update () {
        ActiveElement[] currentActives = GameObject.FindObjectsOfType<ActiveElement>();
        foreach (ActiveElementDetector aed in locations)
        {
            bool active = false;
            foreach (ActiveElement ae in currentActives){
                if (Vector2.Distance(ae.transform.position, aed.transform.position) < detectRadius && ae.GetComponent<DragAndDrop>().isBeingDragged() == false)
                {
                    aed.current = ae;
                    active = true;
                }
            }
            if (active == false)
                aed.current = null;
        }
	}
}
