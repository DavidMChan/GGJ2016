using UnityEngine;
using System.Collections;

public class AnimalSpawnController : MonoBehaviour {

  public Transform SpawnLocation;
  public Transform WalkLoction;
  public SacrificeManager sacman;

  public GameObject rabbit;
  public GameObject chick;
  public GameObject deer;
  public GameObject panda;
  public GameObject orangutang;
  public GameObject demon;

  public enum Animal {
    Rabbit,
    Chick,
    Deer,
    Panda,
    Orangutang,
    Demon
  }

  public GameObject SpawnAnimal(Animal an) {
    if (an == Animal.Rabbit)
      return (GameObject)Instantiate(rabbit, SpawnLocation.position, Quaternion.identity);
    return null;
  }

  public void Update() {
    if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.ANIMAL_SPAWNING) {
      string nextKill = sacman.GetNextAnimal();
      GameObject spawnedAnimal = null;
      if (nextKill.Equals("Rabbit"))
        spawnedAnimal = SpawnAnimal(Animal.Rabbit);
      if (nextKill.Equals("Chick"))
        spawnedAnimal = SpawnAnimal(Animal.Chick);

      spawnedAnimal.GetComponent<AnimalMovementController>().sacrificeTargetLocation = WalkLoction;
      GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.ANIMAL_ENTERING);
    }
  }
}
