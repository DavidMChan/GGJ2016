using UnityEngine;
using System.Collections;

public class SceneSwitcher : MonoBehaviour {

    public void SwitchScene(string Scene)
    {
        Application.LoadLevel(Scene);
    }
}
