using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("Main");
    }
}