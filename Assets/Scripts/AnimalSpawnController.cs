using UnityEngine;
using System.Collections;

public class AnimalSpawnController : MonoBehaviour {

    public Transform SpawnLocation;
    public Transform WalkLoction;
    public SacrificeManager sacman;

    public GameObject COW;

    public enum Animal
    {
        COW
    }

    public GameObject SpawnAnimal(Animal an)
    {
        if (an == Animal.COW)
            return (GameObject) Instantiate(COW, SpawnLocation.position, Quaternion.identity);
        return null;
    }

    public void Update()
    {
        if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.ANIMAL_SPAWNING)
        {
            string nextKill = sacman.GetNextAnimal();
            GameObject spawnedAnimal = null;
            if (nextKill.Equals("cow"))
                spawnedAnimal = SpawnAnimal(Animal.COW);

            spawnedAnimal.GetComponent<AnimalMovementController>().sacrificeTargetLocation = WalkLoction;
            GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.ANIMAL_ENTERING);
        }
    }
}
