using UnityEngine;
using System.Collections;

public class SacrificeManager : MonoBehaviour {

    [System.Serializable]
    public class Sacrifice
    {
        public string Animal;
        public string[] IDRequirements;
    }

    public Sacrifice[] sacrifices;



    private int sacrificeNumber;

    //For the sacrifice button
    public void SacrificeAnAnimal()
    {
        GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.SACRIFICING);
    }

    public void Start()
    {
        sacrificeNumber = 0;
    }

    public void Update()
    {
        if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.SACRIFICING)
        {
            //TODO: Implement Sacrificing
        }
    }

}
