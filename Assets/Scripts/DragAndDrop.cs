using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler {

  private Vector2 pointerOffset;
//  private RectTransform canvasRectTransform;
  private RectTransform panelRectTransform;
  private RectTransform containerRectTransform;

  public SnapPoint homeLocation;
  public float snapRadius = 30;
    public float snapSpeed = 10;

  private SnapPoint destination = null;
  private bool moving = false;

  void Awake () {
    Canvas canvas = GetComponentInParent <Canvas>();
    if (canvas != null) {
//      canvasRectTransform = canvas.transform as RectTransform;
      panelRectTransform = transform as RectTransform;
      containerRectTransform = transform.parent as RectTransform;
    }
  }

  public void OnPointerDown (PointerEventData data) {
//    panelRectTransform.SetAsLastSibling ();
    RectTransformUtility.ScreenPointToLocalPointInRectangle (panelRectTransform, data.position, data.pressEventCamera, out pointerOffset);
  }

  public void OnDrag (PointerEventData data) {
    if (panelRectTransform == null)
      return;

    Vector2 pointerPostion = data.position;

    Vector2 localPointerPosition;
    if (RectTransformUtility.ScreenPointToLocalPointInRectangle (
      containerRectTransform, pointerPostion, data.pressEventCamera, out localPointerPosition
    )) {
      panelRectTransform.localPosition = localPointerPosition - pointerOffset;
    }
  }

  public void OnEndDrag (PointerEventData data)
  {
        SnapPoint where = GameObject.FindObjectOfType<SnappingPointRegistry>().returnClosestSnapPoint(this.gameObject.transform);
        if (where == null || Vector2.Distance(this.gameObject.transform.position, where.gameObject.transform.position) > snapRadius)
        {
            destination = homeLocation;
            moving = true;
        }
        else
        {
            destination = where;
            moving = true;
        }

  }

  public void Update() {
        if (this.moving)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, this.destination.gameObject.transform.position, snapSpeed * Time.deltaTime);
            if (Vector2.Distance(this.transform.position, this.destination.gameObject.transform.position) < .01)
            {
                this.destination = null;
                this.moving = false;
            }
        }
}

}