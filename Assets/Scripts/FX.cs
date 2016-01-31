using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FX : MonoBehaviour {
  public ActiveElementManager AEM;
//  public Transform spawnPoint;

  List<ParticleSystem> fxQueue;
  bool showingFX = false;
  ParticleSystem currentFX;

  void Start() {
    foreach (Transform child in transform) {
      child.gameObject.SetActive(false);
    }
  }

  void Update() {
    if (!showingFX && GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.PRE_SACRIFICING_FX) {	    
      List<ActiveElement> aes = AEM.GetActiveElements();
      fxQueue = new List<ParticleSystem>();

      ParticleSystem particleSystem;
      GameObject gameObject;
      foreach (ActiveElement ae in aes) {
        Debug.Log(ae.ID);
        if (ae.fx != null) {
          gameObject = (GameObject) Instantiate(ae.fx, transform.position, Quaternion.identity);
          gameObject.transform.parent = transform;
//          gameObject.transform.position = new Vector3(0, 0, 0);
          particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
          fxQueue.Add(particleSystem);
        }
      }
      if (fxQueue.Count > 0) {
        showingFX = true;
        currentFX = fxQueue [0];
        currentFX.gameObject.SetActive(true);
        currentFX.Play();
        fxQueue.RemoveAt(0);
      }
      else {
        GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.SACRIFICING);
      }
    }

    if (showingFX && !currentFX.isPlaying) {
      if (fxQueue.Count == 0) {
        Destroy(currentFX.gameObject);
        showingFX = false;
        GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.SACRIFICING);
      } else {
        Destroy(currentFX.gameObject);
        currentFX = fxQueue [0];
        currentFX.gameObject.SetActive(true);
        currentFX.Play();
        fxQueue.RemoveAt(0);
      }
    }
  }
}