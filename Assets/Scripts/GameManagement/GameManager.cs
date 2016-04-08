using UnityEngine;
using System.Collections;

public class GameManager {

    public enum GameState {
        MainMenu, Paused, Running
    }

    public static GameState State = GameState.Running;

    public static void GotToMainMenu(){
        State = GameState.MainMenu;
        Application.LoadLevel("MainMenu");
    }

    public static void StartGame(){
        State = GameState.Running;
        Application.LoadLevel("Game");
    }

    public static void PauseGame(){
        State = GameState.Paused;
    }

    public static void UnPauseGame(){
        State = GameState.Running;
    }


}
