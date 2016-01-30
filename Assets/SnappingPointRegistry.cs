using UnityEngine;
using System.Collections;

public class SnappingPointRegistry : MonoBehaviour {

    //Array of possible snap points for drag and drop
    public SnapPoint[] snaps;

    //Returns the snap point which is closest to the current object
    public SnapPoint returnClosestSnapPoint(Transform snapLocation)
    {
        SnapPoint closest = null;
        float distance = 0;
        foreach (SnapPoint point in snaps)
        {
            if (closest == null)
            {
                closest = point;
                distance = Vector2.Distance(point.gameObject.transform.position, snapLocation.gameObject.transform.position);
            }
            else if (Vector2.Distance(point.gameObject.transform.position, snapLocation.gameObject.transform.position) < distance)
            {
                closest = point;
                distance = Vector2.Distance(point.gameObject.transform.position, snapLocation.gameObject.transform.position);
            }
        }
        return closest;
    }
}
