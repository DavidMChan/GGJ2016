using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SacrificeManager : MonoBehaviour {

    private static SacrificeManager instance;
    public static SacrificeManager GetInstance() {
        return instance;
    }

  [System.Serializable]
  public class Kill {
    public string[] Items;
    public string ReturnItem;
    public bool canContinue;
  }

  [System.Serializable]
  public class Sacrifice {
    public string Animal;
    public Kill[] Kills;
  }

  public ActiveElementManager AEM;
  public GameObject sacrificeButton;
  public GameObject resetButton;


  public Sacrifice[] sacrifices;

  public int sacrificeNumber;

  public GameObject RabitFoot;
  public GameObject PandaPelt;
  public GameObject DeerAshes;

  public GameObject toGive = null;

  private bool currentFeedback = false;
  private bool first = true;

  //For the sacrifice button
  public void SacrificeAnAnimal() {
//    GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.SACRIFICING);

      AudioManager.GetInstance().SwitchToDemonMusic();
    GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.PRE_SACRIFICING_FX);
    //GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("CastSpell");
  }

  public string GetNextAnimal() {
    return sacrifices [sacrificeNumber].Animal;
  }

  public void Start() {
      SacrificeManager.instance = this;
    sacrificeNumber = 0;
    RabitFoot.SetActive(false);
    PandaPelt.SetActive(false);
    DeerAshes.SetActive(false);
  }

  public void Update() {
    if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.SETTING_UP_SACRIFICE) {
      sacrificeButton.SetActive(true);
      resetButton.SetActive(true);
    } else {
      sacrificeButton.SetActive(false);
      resetButton.SetActive(false);
    }


    if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.SACRIFICING) {
      List<string> usedItems = new List<string>();
      List<ActiveElement> aes = AEM.GetActiveElements();

      foreach (ActiveElement ae in aes) {
        usedItems.Add(ae.ID);
        Debug.Log(ae.ID);
      }

      Sacrifice currentSacrifice = sacrifices [sacrificeNumber];

      bool itemDrop = false;
      bool cleanKill = false;
      bool failed = false;

      List<Kill> matchedKills = new List<Kill>();

      foreach (Kill k in currentSacrifice.Kills) {
        bool matched = true;
        foreach (string kv in k.Items) {
          if (!usedItems.Contains(kv))
            matched = false;
        }
        if (matched) {
          matchedKills.Add(k);
        }
      }

      foreach (Kill k in matchedKills) {
        if (k.ReturnItem != "" && k.Items.Length == usedItems.Count)
          itemDrop = true;
      }

      foreach (Kill k in matchedKills) {
        if (k.Items.Length == usedItems.Count && k.canContinue)
          cleanKill = true;
      }

      if (matchedKills.Count == 0)
        failed = true;


      if (itemDrop) {
        Debug.Log("Perfect Item drop kill!");
        foreach (Kill k in matchedKills)
        {
            if (k.ReturnItem != "" && k.ReturnItem == "RabbitFoot")
            {   toGive = RabitFoot;
            }
            if (k.ReturnItem != "" && k.ReturnItem == "DeerAshes")
            {
                toGive = DeerAshes;
            }
            if (k.ReturnItem != "" && k.ReturnItem == "PandaPelt")
            {
                toGive = PandaPelt;
            }
            FeedbackManager.GetInstance().ShowFeedbackHappy();
        }
        // LOGIC FOR ITEM-DROP PERFECT KILL
      } else if (failed) {
        Debug.Log("Failed kill!");
        // LOGIC FOR FAILED KILL
        GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.ANIMAL_PLAYING_KILL_ANIMATION);
        return;
      } else if (cleanKill) {
          FeedbackManager.GetInstance().ShowFeedbackHappy();
        Debug.Log("Clean kill!");
        // LOGIC FOR CLEAN KILL

      } else {

        Debug.Log("Imperfect kill!");
        // LOGIC FOR IMPERFECT KILL
        FeedbackManager.GetInstance().ShowFeedbackSad();
      }

      //GameObject.FindGameObjectWithTag("Animal").GetComponent<AnimalMovementController>().Kill();

      // Clean up the objects (remove them from the game)
      foreach (ActiveElement ae in aes) {
        Destroy(ae.gameObject);
      }
      AEM.Clean();

      //Increment sacrifice number
      sacrificeNumber += 1;
      first = true;

        GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.ANIMAL_PLAYING_DEATH_ANIMATION);
    }

    if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.GIVING_ITEMS)
    {
        if (toGive != null && first == true)
        {
            toGive.GetComponent<DragAndDrop>().GiveElement();
            first = false;
        }
        else if (toGive == null)
        {
            GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.SCORING_KILL);
        }
    }
  }
}
