using UnityEngine;
using System.Collections;
using InControl;

public class GameManager : Singleton<GameManager> {

    public GameObject StartGameMenu;
    public GameObject PauseGameMenu;
    public GameObject Player1WinsScreen;
    public GameObject Player2WinsScreen;
	public SoundManager SoundManager;
	public CameraScript MainCameraScript;

    PlayerCharacterActions actions;

    float timeSinceLastSwitch = 0;

    void Start(){
        actions = PlayerCharacterActions.GetDefaultBindings(true,true);
        Time.timeScale = 0;
        StartGameMenu.gameObject.SetActive(true);
        PauseGameMenu.gameObject.SetActive(false);
        Player1WinsScreen.gameObject.SetActive(false);
        Player2WinsScreen.gameObject.SetActive(false);
        State = GameState.MainMenu;
    }

    void Update(){
        timeSinceLastSwitch += Time.unscaledDeltaTime;

        var inputDevice = InputManager.Devices.Count > 0 ? InputManager.Devices[0] : null;
        bool anyButton = false;
        if(inputDevice != null){
            anyButton = inputDevice.AnyButton.WasPressed;
        }
        anyButton = anyButton || Input.anyKeyDown;
        if(anyButton && timeSinceLastSwitch > 1)
        {
            timeSinceLastSwitch = 0;
            switch(State){
                case GameState.MainMenu:
                    StartGame();
                break;
                case GameState.Paused:
                break;
                case GameState.Running:
                break;
                case GameState.WinScreen:
                    GotToMainMenu();
                break;
            }
        }
    }

    public static void StartGame(){
        State = GameState.Running;
        Time.timeScale = 1;
        Instance.StartGameMenu.gameObject.SetActive(false);
        Instance.PauseGameMenu.gameObject.SetActive(false);
        Instance.Player1WinsScreen.gameObject.SetActive(false);
        Instance.Player2WinsScreen.gameObject.SetActive(false);
    }

    public enum GameState {
        MainMenu,
        Paused,
        Running,
        WinScreen
    }

    public static void Player1Won(){
        Time.timeScale = 0;
        Instance.Player1WinsScreen.gameObject.SetActive(true);
        State = GameState.WinScreen;
    }

    public static void Player2Won(){
        Time.timeScale = 0;
        Instance.Player2WinsScreen.gameObject.SetActive(true);
        State = GameState.WinScreen;
    }

    public static GameState State = GameState.Running;

    public static void GotToMainMenu() {
        State = GameState.MainMenu;
        Application.LoadLevel("Game");
    }

    public static void PauseGame() {
        State = GameState.Paused;
        Time.timeScale = 0;
    }

    public static void UnPauseGame() {
        State = GameState.Running;
        Time.timeScale = 1;
    }

    public static void PauseUnPauseGame() {
        if (State == GameState.Paused)
            UnPauseGame();
        else
            PauseGame();
    }
}
