using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RestartGameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button butonObj = GameObject.Find("RestartGameButton").GetComponent<Button>();

        butonObj.onClick.AddListener( delegate { restartButtonClicked(); } );
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void restartButtonClicked()
    {
        restartGame();
    }

    public void restartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
