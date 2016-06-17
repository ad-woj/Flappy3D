using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour {

    RestartGameController restartGameController = new RestartGameController();

    void OnGUI()
    {
        if (Input.GetKey(KeyCode.R))
        {
            restartGameController.restartGame();
        }
    }
}
