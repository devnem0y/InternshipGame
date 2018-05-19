using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CameraController cameraController;
    public GameObject fading;
    public Canvas canvas;
    private UI GUI;

    private AudioManager audioManager;
    private ConfigManager cm;

    private string stateGame = null;
    private bool isSettings;
    private bool isPause;

    public GameObject controller;
    public GameObject car;

    public GameObject storeContent;

    public GameObject generationLevel;
    private Spawn spawn;

    public GameObject background;
    private Background bg;

    private void Awake()
    {
        Application.targetFrameRate = 600;

        GUI = canvas.GetComponent<UI>();
        audioManager = FindObjectOfType<AudioManager>();
        cm = GetComponent<ConfigManager>();
        spawn = generationLevel.GetComponent<Spawn>();
        bg = background.GetComponent<Background>();
    }

    public ConfigManager ConfigM()
    {
        return cm;
    }

    private void Start()
    {
        cm.Load();
        InitSettings();

        Menu();
    }

    private void UpdateGUI()
    {
        GUI.score.text = GameParams.GetScore().ToString();
        GUI.lastScore.text = GameParams.GetLastScore().ToString();
        GUI.topScore.text = GameParams.GetTopScore().ToString();
        GUI.coins.text = GameParams.GetCoins().ToString();
    }

    private void Update()
    {
        UpdateGUI();

        if (car.GetComponent<Car>().IsCrash())
        {
            StartCoroutine(cameraController.Shake(.4f, .05f));
            GUI.pause.gameObject.SetActive(false);
            GUI.infoDrop.SetActive(false);
            GUI.score.gameObject.SetActive(false);
            GUI.currValFlip.gameObject.SetActive(false);
            audioManager.Stop("2");
            StartCoroutine(DelayGameOver(3f));
        } else
        {
            bg.Move();
            if (car.GetComponent<Car>().IsBackFlip())
            {
                GUI.currValFlip.text = "x" + car.GetComponent<Car>().GetCurrValFlip().ToString();
                GUI.currValFlip.gameObject.SetActive(true);
            }
            else GUI.currValFlip.gameObject.SetActive(false);
        }

        BtnBackM();
    }

    private IEnumerator DelayGameOver(float time)
    {
        yield return new WaitForSeconds(time);
        GameOver();
    }

    public void Menu()
    {
        fading.GetComponent<Animation>().Play("Fading");
        Time.timeScale = 1;
        stateGame = "menu";
        GUI.menuP.SetActive(true);
        GUI.storeP.SetActive(true);
        GUI.storeP.transform.position = new Vector3(10f, GUI.storeP.transform.position.y, GUI.storeP.transform.position.z);
        if (Data.gameStart == 0)
        {
            if (Data.music.Equals("on"))
            {
                if (!audioManager.Playing("0")) audioManager.PlayMenuFull();
            }
            GUI.logo.SetActive(true);
            GUI.frame1.SetActive(true);
            GUI.frame2.SetActive(false);
            GUI.coinsObj.SetActive(false);
            GUI.infoLastScore.SetActive(false);
            GUI.infoTopScore.SetActive(false);
        }
        else
        {
            if (Data.music.Equals("on"))
            {
                if (!audioManager.Playing("1")) audioManager.PlayMenuLoop();
            }
            GUI.logo.SetActive(false);
            GUI.frame1.SetActive(false);
            GUI.frame2.SetActive(true);
            GUI.coinsObj.SetActive(true);
            GUI.infoLastScore.SetActive(true);
            GUI.infoTopScore.SetActive(true);
        }
        controller.SetActive(false);
        car.SetActive(false);
        UpdateGUI();
    }

    public void Game()
    {
        fading.GetComponent<Animation>().Play("Fading");
        Time.timeScale = 1;
        stateGame = "game";
        spawn.GenerationLevel();
        GameParams.SetScore(0);
        GameParams.SetLastScore(0);
        GUI.menuP.SetActive(false);
        GUI.storeP.SetActive(false);
        GUI.infoLastScore.SetActive(false);
        GUI.score.gameObject.SetActive(true);
        GUI.coinsObj.SetActive(true);
        GUI.pause.gameObject.SetActive(true);
        controller.SetActive(true);
        audioManager.PlayClick();
        isPause = false;
        audioManager.PlayGamePlay();

        car.SetActive(true);
        InitGame();
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        stateGame = "gameover";
        controller.SetActive(false);
        GUI.pause.gameObject.SetActive(false);
        GUI.score.gameObject.SetActive(false);
        GUI.infoDrop.SetActive(false);
        GUI.currValFlip.gameObject.SetActive(false);
        GUI.gameOverP.SetActive(true);
        GUI.infoLastScore.SetActive(true);
    }

    public void Pause()
    {
        isPause = true;
        if (isPause)
        {
            Time.timeScale = 0;
            stateGame = "pause";
            //if (Data.music.Equals("on")) audioManager.SetVolumeMusic("GamePlay", 0f); // pause on
            GUI.pause.gameObject.SetActive(false);
            GUI.score.gameObject.SetActive(false);
            GUI.pauseP.SetActive(true);
            GUI.infoLastScore.SetActive(true);
            audioManager.PlayClick();
            GetComponent<ConfigManager>().Save();
            controller.SetActive(false);
        }
    }

    public void Store()
    {
        fading.GetComponent<Animation>().Play("Fading");
        stateGame = "store";
        GUI.menuP.SetActive(false);
        GUI.infoLastScore.SetActive(false);
        GUI.storeP.SetActive(true);
        GUI.storeP.transform.position = new Vector3(0f, GUI.storeP.transform.position.y, GUI.storeP.transform.position.z);
        GUI.coinsObj.SetActive(true);
        audioManager.PlayClick();
        controller.SetActive(false);
    }

    public void Location()
    {
        fading.GetComponent<Animation>().Play("Fading");
        stateGame = "location";
        GUI.menuP.SetActive(false);
        GUI.infoLastScore.SetActive(false);
        GUI.locationP.SetActive(true);
        GUI.coinsObj.SetActive(true);
        audioManager.PlayClick();
        controller.SetActive(false);
    }

    public void Settings()
    {
        fading.GetComponent<Animation>().Play("Fading");
        Time.timeScale = 1;
        isSettings = true;
        if (stateGame.Equals("menu")) GUI.menuP.SetActive(false);
        else if (stateGame.Equals("pause")) GUI.pauseP.SetActive(false);
        GUI.infoLastScore.SetActive(false);
        GUI.settingsP.SetActive(true);
        audioManager.PlayClick();
        controller.SetActive(false);
    }

    public void ReturnMenu()
    {
        fading.GetComponent<Animation>().Play("Fading");
        if (GameParams.GetTopScore() <= GameParams.GetLastScore()) GameParams.SetTopScore(GameParams.GetLastScore());
        audioManager.PlayClick();
        Data.gameStart = 1;
        GetComponent<ConfigManager>().Save();
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        audioManager.PlayClick();
        GUI.pauseP.SetActive(false);
        Game();
    }

    public void Back()
    {
        audioManager.PlayClick();
        if (GUI.exitP.activeSelf) GUI.exitP.SetActive(false);

        if (stateGame.Equals("store"))
        {
            fading.GetComponent<Animation>().Play("Fading");
            GUI.storeP.SetActive(false);
            Menu();
        } else if (stateGame.Equals("location"))
        {
            fading.GetComponent<Animation>().Play("Fading");
            GUI.locationP.SetActive(false);
            Menu();
        } else if (isSettings)
        {
            GUI.settingsP.SetActive(false);
            isSettings = false;
            if (stateGame.Equals("menu"))
            {
                fading.GetComponent<Animation>().Play("Fading");
                Menu();
            }
            else Pause();
        }
    }

    public void Accept()
    {
        audioManager.PlayClick();
        Data.gameStart = 0;
        GetComponent<ConfigManager>().Save();
        Application.Quit();
    }

    public UI GetGUI()
    {
        return GUI;
    }

    private void InitGame()
    {
        bg.InitBG();
        car.transform.GetComponent<Car>().InitSkin();
    }

    private void InitSettings()
    {
        if (Data.music.Equals("on")) audioManager.GlobalMusicVolumeChanged(true);
        else if (Data.music.Equals("off")) audioManager.GlobalMusicVolumeChanged(false);
        if (Data.sound.Equals("on")) audioManager.GlobalSoundVolumeChanged(true);
        else if (Data.sound.Equals("off")) audioManager.GlobalSoundVolumeChanged(false);
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        isPause = pause;
    }

    private void OnApplicationFocus(bool focus)
    {
        isPause = !focus;
    }
#endif
    private void OnApplicationQuit()
    {
        Data.gameStart = 0;
        GetComponent<ConfigManager>().Save();
        Debug.Log("Quit");
    }

    private void BtnBackM()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (stateGame.Equals("menu") && !isSettings)
            {
                Time.timeScale = 0f;
                GUI.exitP.SetActive(true);
            }
            else if (stateGame.Equals("menu") && isSettings)
            {
                GUI.settingsP.SetActive(false);
                isSettings = false;
                Menu();
            }
            if (stateGame.Equals("pause") && !isSettings)
            {
                Continue();
            }
            else if (stateGame.Equals("pause") && isSettings)
            {
                GUI.settingsP.SetActive(false);
                isSettings = false;
                Pause();
            }
            else if (stateGame.Equals("game"))
            {
                if (!car.GetComponent<Car>().IsCrash()) Pause();
            }
            else if (stateGame.Equals("store"))
            {
                GUI.storeP.SetActive(false);
                Menu();
            }
            else if (stateGame.Equals("location"))
            {
                GUI.locationP.SetActive(false);
                Menu();
            }
            else if (stateGame.Equals("gameover")) ReturnMenu();
        }
    }
}