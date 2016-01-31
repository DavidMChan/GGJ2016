using UnityEngine;
using System.Collections;

public class FeedbackManager : MonoBehaviour {

    public static FeedbackManager instance;
    public static FeedbackManager GetInstance()
    {
        return instance;
    }

    public GameObject HappyImage;
    public GameObject SadImage;

    public Transform spawnloc;
    public Transform target;

    public GameObject current;
    public Transform canvas;

    public bool moving;
    public float tics = 0;

    public void ShowFeedbackHappy()
    {
        current = (GameObject) Instantiate(HappyImage, spawnloc.position, Quaternion.identity);
        current.transform.SetParent(canvas, false);
        current.transform.position = spawnloc.position;
    }

    public void ShowFeedbackSad()
    {
        current = (GameObject)Instantiate(SadImage, spawnloc.position, Quaternion.identity);
        current.transform.SetParent(canvas, false);
        current.transform.position = spawnloc.position;
    }

    public void Start()
    {
        FeedbackManager.instance = this;
    }


    public void Update()
    {
        if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.SCORING_KILL)
        {

            if (!moving)
            {
                current.gameObject.transform.position = Vector2.MoveTowards(current.transform.position, target.position, 5.0f * Time.deltaTime);
            }
            else
            {
                current.gameObject.transform.position = Vector2.MoveTowards(current.transform.position, spawnloc.position, 5.0f * Time.deltaTime);
            }


            if (Vector2.Distance(current.transform.position, target.position) < 0.5)
            {
                    if (tics > 3)
                        moving = true;
            }

            if (tics > 5)
                {
                    GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.ANIMAL_SPAWNING);
                    Destroy(current);
                    current = null;
                    moving = false;
                    tics = 0;
                }
            tics += Time.deltaTime;
        }
    }
}
