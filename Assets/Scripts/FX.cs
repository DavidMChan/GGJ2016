using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FX : MonoBehaviour {
  public ActiveElementManager AEM;
//  public Transform spawnPoint;

  List<KeyValuePair<ParticleSystem,int>> fxQueue;
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
      fxQueue = new List<KeyValuePair<ParticleSystem,int>>();

      ParticleSystem particleSystem;
      GameObject gameObject;
      foreach (ActiveElement ae in aes) {
        Debug.Log(ae.ID);
        if (ae.fx != null) {
          gameObject = (GameObject) Instantiate(ae.fx, transform.position, Quaternion.identity);
          gameObject.SetActive(false);
          gameObject.transform.parent = transform;
//          gameObject.transform.position = new Vector3(0, 0, 0);
          particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
          fxQueue.Add(new KeyValuePair<ParticleSystem,int>(particleSystem, ae.sound));
        }
      }
      if (fxQueue.Count > 0) {
        showingFX = true;
        currentFX = fxQueue[0].Key;
        currentFX.gameObject.SetActive(true);
        currentFX.Play();
        AudioManager.GetInstance().PlaySound(fxQueue[0].Value);
        Debug.Log("Playing sound" + fxQueue[0].Value);

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
        currentFX = fxQueue[0].Key;
        currentFX.gameObject.SetActive(true);
        currentFX.Play();
        AudioManager.GetInstance().PlaySound(fxQueue[0].Value);
        fxQueue.RemoveAt(0);
      }
    }
  }
}