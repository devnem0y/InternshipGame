using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("GUI Text")]
    public Text score;
    public Text lastScore;
    public Text topScore;
    public Text coins;
    public Text drop; // bravo, coolFlip
    public Text valDrop; // +1, +2
    public Text currValFlip; // x1...

    [Header("GUI Button")]
    public Button pause;

    [Header ("GUI Panel")]
    public GameObject menuP;
    public GameObject storeP;
    public GameObject locationP;
    public GameObject pauseP;
    public GameObject gameOverP;
    public GameObject settingsP;
    public GameObject exitP;

    [Header("GUI Objects")]
    public GameObject logo;
    public GameObject frame1, frame2;
    public GameObject coinsObj;
    public GameObject infoLastScore;
    public GameObject infoTopScore;
    public GameObject infoDrop;
}
