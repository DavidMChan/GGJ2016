using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SacrificeManager : MonoBehaviour {

    [System.Serializable]
    public class Kill
    {
        public string[] Items;
        public string ReturnItem;
    }

    [System.Serializable]
    public class Sacrifice
    {
        public string Animal;
        public Kill[] Kills;
    }

    public ActiveElementManager AEM;
    public GameObject sacrificeButton;

    public Sacrifice[] sacrifices;



    private int sacrificeNumber;

    //For the sacrifice button
    public void SacrificeAnAnimal()
    {
        GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.SACRIFICING);
    }

    public string GetNextAnimal()
    {
        return sacrifices[sacrificeNumber].Animal;
    }

    public void Start()
    {
        sacrificeNumber = 0;
    }

    public void Update()
    {
        if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.SETTING_UP_SACRIFICE)
        {
            sacrificeButton.SetActive(true);
        } else {
            sacrificeButton.SetActive(false);
        }


        if (GameStateManager.GetInstance().GetCurrentState() == GameStateManager.GameState.SACRIFICING)
        {
            List<string> usedItems = new List<string>();
            List<ActiveElement> aes = AEM.GetActiveElements();

            foreach (ActiveElement ae in aes){
                usedItems.Add(ae.ID);
                Debug.Log(ae.ID);
            }

            Sacrifice currentSacrifice = sacrifices[sacrificeNumber];

            bool itemDrop = false;
            bool cleanKill = false;
            bool failed = false;

            List<Kill> matchedKills = new List<Kill>();

            foreach (Kill k in currentSacrifice.Kills)
            {
                bool matched = true;
                foreach (string kv in k.Items)
                {
                    if (!usedItems.Contains(kv))
                        matched = false;
                }
                if (matched)
                {
                    matchedKills.Add(k);
                }
            }

            foreach (Kill k in matchedKills)
            {
                if (k.ReturnItem != "" && k.Items.Length == usedItems.Count)
                    itemDrop = true;
            }

            foreach (Kill k in matchedKills)
            {
                if (k.Items.Length == usedItems.Count)
                    cleanKill = true;
            }
            if (matchedKills.Count == 0)
                failed = true;


            if (itemDrop)
            {
                Debug.Log("Perfect Item drop kill!");
                // LOGIC FOR ITEM-DROP PERFECT KILL
            }
            else if (failed)
            {
                Debug.Log("Failed kill!");
                // LOGIC FOR FAILED KILL
            }
            else if (cleanKill)
            {

                Debug.Log("Clean kill!");
                // LOGIC FOR CLEAN KILL
            }
            else
            {

                Debug.Log("Imperfect kill!");
                // LOGIC FOR IMPERFECT KILL
            }

            // Clean up the objects (remove them from the game)
            foreach (ActiveElement ae in aes)
            {
                Destroy(ae.gameObject);
            }
            AEM.Clean();

            //Increment sacrifice number
            sacrificeNumber += 1;

            GameStateManager.GetInstance().RequestGameStateChange(GameStateManager.GameState.CLEANING_UP);
        }

        
    }

}
