using UnityEngine;
using System.Collections;

public class AnimalMovementController : MonoBehaviour {

    public Transform sacrificeTargetLocation;
    public float walkSpeed = 5;

    public bool Laughing;

    public void setLaugh(bool iput){
        Laughing = iput;
    }

    public void Kill()
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("Die");
    }
	
	// Update is called once per frame
	void Update () {
        if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.ANIMAL_ENTERING)
        {
            this.gameObject.GetComponent<Animator>().SetBool("Walking", true);
            this.transform.position = Vector2.MoveTowards(this.transform.position, this.sacrificeTargetLocation.position, walkSpeed * Time.deltaTime);
            if (Vector2.Distance(this.transform.position, this.sacrificeTargetLocation.position) < 0.5)
            {
                this.gameObject.GetComponent<Animator>().SetBool("Walking", false);
                GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.SETTING_UP_SACRIFICE);
            }
        }
        //this.gameObject.GetComponent<Animator>().SetBool("Laughing", Laughing);
	}
}
