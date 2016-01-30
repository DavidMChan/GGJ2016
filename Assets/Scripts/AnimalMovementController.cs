using UnityEngine;
using System.Collections;

public class AnimalMovementController : MonoBehaviour {

    public Transform sacrificeTargetLocation;
    public float walkSpeed = 5;
	
	// Update is called once per frame
	void Update () {
        if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.ANIMAL_ENTERING)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, this.sacrificeTargetLocation.position, walkSpeed * Time.deltaTime);
            if (Vector2.Distance(this.transform.position, this.sacrificeTargetLocation.position) < 0.5)
                GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.SETTING_UP_SACRIFICE);

        }
       
	}
}
