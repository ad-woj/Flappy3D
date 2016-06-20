using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartGameController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Button butonObj = GameObject.Find("Start").GetComponent<Button>();

        butonObj.onClick.AddListener(delegate { startButtonClicked(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void startButtonClicked()
    {
        startGame();
    }

    public void startGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
