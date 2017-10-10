using UnityEngine;
using System.Collections;

public class GameManagerActivate : MonoBehaviour {

    private GameObject gameManagerObject;
    private GameManager gameManager;

    public Camera cameraMain;
    public Transform uiCanvas;
    public UIFade p1;
    public UIFade p2;
    public UIFade p3;
    public UIFade p4;

    public GameObject[] spawnPositions;

    public GameObject map;

    public GameObject aiRotator;
    private AITurner aiTurner;

    private WaitForSeconds wait = new WaitForSeconds(2f);

    private AudioSource countdownTrack;

	void Awake()
    {
        aiTurner = aiRotator.GetComponent<AITurner>();
        countdownTrack = GetComponent<AudioSource>();
        
        //Debug.Log("Called");
        gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        gameManager.gameManagerActivate = this;
        gameManager.mainCamera = cameraMain;
        gameManager.UICanvas = uiCanvas;
        gameManager.classSelectScreen = false;
        gameManager.mapSpinScript = map.GetComponent<SlowSpin>();
        gameManager.speakerControlScript = map.GetComponent<SpeakerControl>();
        gameManager.countdownTrack = countdownTrack;

        gameManager.GameStart();

        Debug.Log(gameManager.classSelectScreen);

        aiTurner.gameManager = gameManager;

        //Give UIFade Boxes reference to GameManager script.
        p1.gameManager = gameManager;
        p2.gameManager = gameManager;
        p3.gameManager = gameManager;
        p4.gameManager = gameManager;
    }
}
