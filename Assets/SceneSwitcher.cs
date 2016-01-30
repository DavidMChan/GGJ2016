using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

    public void SwitchScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
