using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour {

    RestartGameController restartGameController = new RestartGameController();
    StartGameController startGameController = new StartGameController();

    void OnGUI()
    {
        if (Input.GetKey(KeyCode.R))
        {
            restartGameController.restartGame();
        }

        //if (Input.GetKey(KeyCode.S))
        //{
        //    startGameController.startGame();
        //}

    }
}
