using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public bool isPaused;
    public KeyCode pause = KeyCode.Escape;

    void Update () {
        if (Input.GetKeyDown(pause)) {
            isPaused = !isPaused;
        }
	}
}
