using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

  GameObject currentAnimal;

  public GameObject SpawnAnimal(Animal an) {
    if (an == Animal.Rabbit)
      currentAnimal = (GameObject)Instantiate(rabbit, SpawnLocation.position, Quaternion.identity);
    if (an == Animal.Chick)
      currentAnimal = (GameObject)Instantiate(chick, SpawnLocation.position, Quaternion.identity);
    if (an == Animal.Deer)
      currentAnimal = (GameObject)Instantiate(deer, SpawnLocation.position, Quaternion.identity);
    if (an == Animal.Panda)
      currentAnimal = (GameObject)Instantiate(panda, SpawnLocation.position, Quaternion.identity);
    if (an == Animal.Orangutang)
      currentAnimal = (GameObject)Instantiate(orangutang, SpawnLocation.position, Quaternion.identity);
    if (an == Animal.Demon)
      currentAnimal = (GameObject)Instantiate(demon, SpawnLocation.position, Quaternion.identity);
    return currentAnimal;
  }

  public void DespawnCurrentAnimal() {
    Destroy(currentAnimal);
  }

  public void Update() {
    if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.ANIMAL_SPAWNING) {
      string nextKill = sacman.GetNextAnimal();

      GameObject spawnedAnimal = null;
      if (nextKill.Equals("Rabbit"))
        spawnedAnimal = SpawnAnimal(Animal.Rabbit);
      if (nextKill.Equals("Chick"))
        spawnedAnimal = SpawnAnimal(Animal.Chick);
      if (nextKill.Equals("Deer"))
        spawnedAnimal = SpawnAnimal(Animal.Deer);
      if (nextKill.Equals("Panda"))
        spawnedAnimal = SpawnAnimal(Animal.Panda);
      if (nextKill.Equals("Orangutang"))
        spawnedAnimal = SpawnAnimal(Animal.Orangutang);
      if (nextKill.Equals("Demon"))
        spawnedAnimal = SpawnAnimal(Animal.Demon);

      spawnedAnimal.GetComponent<AnimalMovementController>().sacrificeTargetLocation = WalkLoction;
      GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.ANIMAL_ENTERING);
    }

    if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.CLEANING_UP) {
      DespawnCurrentAnimal();
      GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.SCORING_KILL);
    }
  }
}
