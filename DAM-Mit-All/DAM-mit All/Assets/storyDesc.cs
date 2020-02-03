using UnityEngine;
using UnityEngine.SceneManagement;

public class storyDesc : MonoBehaviour
{
    // Detects if any key has been pressed.
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(2);
        }
    }
}