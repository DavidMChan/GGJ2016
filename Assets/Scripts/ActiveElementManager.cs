using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveElementManager : MonoBehaviour {

    public ActiveElementDetector[] locations;
    public float detectRadius = 0.05f;

    // Get the current active elements
    public List<ActiveElement> GetActiveElements()
    {
        List<ActiveElement> ActiveElements = new List<ActiveElement>();
        foreach (ActiveElementDetector aed in locations){
            if (aed.current != null)
            {
                ActiveElements.Add(aed.current);
            }
        }
        return ActiveElements;
    }

    public void Clean()
    {
        foreach (ActiveElementDetector aed in locations)
        {
            aed.current = null;
      aed.gameObject.GetComponent<SnapPoint>().occupied = false;
        }
    }

    public void Reset()
    {
        foreach (ActiveElementDetector aed in locations)
        {
            if (aed.current != null)
                aed.current.GetComponent<DragAndDrop>().GoHome();
        }
    }

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
