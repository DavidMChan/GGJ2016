using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActiveElementDetector : MonoBehaviour {

    public GameObject indicator;
    public ActiveElement current;
    public string baseString;

    public void Start()
    {
        baseString = indicator.GetComponent<Text>().text;
    }

    public void Update()
    {
        if (current != null)
        {
            indicator.SetActive(true);
            indicator.GetComponent<Text>().text = baseString + " with "+ current.ID;
        }
        else
        {
            indicator.GetComponent<Text>().text = baseString;
            indicator.SetActive(false);
        }
    }
}
