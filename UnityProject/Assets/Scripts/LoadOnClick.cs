using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public void LoadScene(int level)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }
}
