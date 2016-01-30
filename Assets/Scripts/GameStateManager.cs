using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStateManager : MonoBehaviour {

  private static GameStateManager instance;

  public static GameStateManager GetInstance() {
    return instance;
  }

  public enum GameState {
    ANIMAL_SPAWNING,
    // The animal is spawning
    ANIMAL_ENTERING,
    // The animal is entering the game
    ANIMAL_PLAYING_READY_ANIMATION,
    // The sacrifical animal is playing some kind of "entry animation"
    SETTING_UP_SACRIFICE,
    // The player is setting up the sacrifice
    SACRIFICING,
    // The game is sacrificing the animal (determining the results)
    ANIMAL_PLAYING_DEATH_ANIMATION,
    // The animal is playing a death animation (player wins)
    ANIMAL_PLAYING_KILL_ANIMATION,
    // The animal is playing a kill animation (player loss)
    SCORING_KILL,
    // Scoring the kill (when the player wins)
    ADDING_ITEMS,
    // Adding items to the game (when the player wins)
    CLEANING_UP,
    //Cleaning up between sacrifies - checks the win conditions,
    LOSE,
    WIN
  }

  public GameState currentState;

  public GameState GetCurrentState() {
    return currentState;
  }

  public void RequestGameStateChange(GameState newState) {
    this.currentState = newState; 
    /*
        if (currentState == GameState.ANIMAL_ENTERING)
            currentState = GameState.SETTING_UP_SACRIFICE;
        else if (currentState == GameState.SETTING_UP_SACRIFICE)
            currentState = GameState.SACRIFICING;
         * */
  }

  public void Awake() {
    GameStateManager.instance = this;
    this.currentState = GameState.ANIMAL_SPAWNING;
  }

  public void Update() {
    if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.WIN) {
      SceneManager.LoadScene(3);
          
    }
    if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.LOSE) {
      SceneManager.LoadScene(4);
    }
  }
}
