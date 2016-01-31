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

    public bool dragging;
    private SnapPoint currentLocation;

    public Transform Spawn;

    private int waitTick;

  void Awake () {
    Canvas canvas = GetComponentInParent <Canvas>();
    if (canvas != null) {
//      canvasRectTransform = canvas.transform as RectTransform;
      panelRectTransform = transform as RectTransform;
      containerRectTransform = transform.parent as RectTransform;
    }
    this.currentLocation = homeLocation;
  }

  public void OnPointerDown (PointerEventData data) {
//    panelRectTransform.SetAsLastSibling ();
      if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.SETTING_UP_SACRIFICE)
      {
          RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, data.position, data.pressEventCamera, out pointerOffset);
            dragging = true;

            AudioManager.GetInstance().PlaySound("click");
    
      }
  }

  public void OnDrag (PointerEventData data) {
      if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.SETTING_UP_SACRIFICE)
      {

          if (panelRectTransform == null)
              return;

          Vector2 pointerPostion = data.position;

          Vector2 localPointerPosition;
          if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            containerRectTransform, pointerPostion, data.pressEventCamera, out localPointerPosition
          ))
          {
              panelRectTransform.localPosition = localPointerPosition - pointerOffset;
          }
      }

   
  }

  public void GoHome()
  {
      this.destination = homeLocation;
      moving = true;
  }

  public void OnEndDrag (PointerEventData data)
  {
      if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.SETTING_UP_SACRIFICE)
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
          AudioManager.GetInstance().PlaySound("drop");
      }
  }

  public void GiveElement()
  {
      this.gameObject.SetActive(true);
      Debug.Log("Giving Element");
      this.transform.position = Spawn.position;
      this.destination = homeLocation;
      this.transform.localScale = new Vector2(5, 5);
      this.moving = true;
      this.waitTick = Time.frameCount + 60;
  }

  public void Update() {
      if (true || GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.SETTING_UP_SACRIFICE)
      {
          if (this.moving && Time.frameCount > waitTick)
          {
              this.transform.position = Vector2.MoveTowards(this.transform.position, this.destination.gameObject.transform.position, snapSpeed * Time.deltaTime);
              this.transform.localScale = Vector2.MoveTowards(this.transform.localScale, new Vector2(1,1), snapSpeed * Time.deltaTime);
              if (Vector2.Distance(this.transform.position, this.destination.gameObject.transform.position) < .01 && Vector2.Distance(this.transform.localScale, new Vector2(1,1)) < .01)
              {
                  this.moving = false;
                  this.dragging = false;
                  this.destination.occupied = true;
                  this.currentLocation.occupied = false;
                  this.currentLocation = this.destination;
                  this.destination = null;
                  this.waitTick = 0;
                  if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.GIVING_ITEMS)
                  {
                      SacrificeManager.GetInstance().toGive = null;
                      GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.SCORING_KILL);
                  }
              }
          }
      }
}

    public bool isBeingDragged()
    {
        return dragging;
    }

}