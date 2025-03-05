using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameCuciTanganManager : Minigame
{
    //[SerializeField] private GameObject Timer;
    //[SerializeField] private Fader Fader;
    [SerializeField] private int gameDoneCheck;
    [SerializeField] private TimerScript timerScript;

    [SerializeField] private Sprite winPanelImg;
    [SerializeField] private Sprite losePanelImg;
    [SerializeField] private Image resultPanel;
    [SerializeField] private CanvasBehavior resultCanvas;

    GameObject[] viruses;

    int check = 0;

    [SerializeField] private Spawner _spawner;

    // Start is called before the first frame update
    void Start()
    {
        gameDoneCheck = 0;
    }

    protected override void CheckResult()
    {
        if (timerText.text == "00" && check == 0)  // Game over due to time running out
        {
            layouts.baseInGameCanvas.hideCanvas();
            layouts.coinLayout.showCanvas();
            layouts.staticLayout.showCanvas();
            currentScore = _spawner.VirusDestroyed;
            targetScore = _spawner.SpawnCount;
            setTargetScoreTxt();
            setCurrentScoreTxt();
            SoundManager.Instance.PlayMusicInList("House");

            if (_spawner.VirusDestroyed == _spawner.SpawnCount)  // If the time is up and enough viruses are destroyed, it's a win
            {
                //Debug.Log("You Win!");
                //StartCoroutine(Fader.FadeOutGameObject(Timer, (float)0.5));

                setWin();
                check++; // Prevent further checks after a condition is met
                gameDoneCheck++;
                SoundManager.Instance.PlaySFXInList("win");
            }
            else
            {
                //Debug.Log("You Lose!");
                //StartCoroutine(Fader.FadeOutGameObject(Timer, (float)0.5));

                GameObject[] viruses = GameObject.FindGameObjectsWithTag("Virus");
                //Debug.Log(viruses.Length);

                foreach (GameObject virus in viruses)
                {
                    VirusMove virusMove = virus.GetComponent<VirusMove>();

                    virusMove.speed = 0f;

                    if (!virusMove)
                    {
                        Debug.LogError("VirusMove script not found on a virus object!");
                    }
                }

                setLose();
                check++; // Prevent further checks after a condition is met
                gameDoneCheck++;
                SoundManager.Instance.PlaySFXInList("Lose");
            }
            resultCanvas.showCanvas();
        }
        else if (_spawner.VirusDestroyed == _spawner.SpawnCount && check == 0)  // If enough viruses are destroyed before time is up
        {
            layouts.baseInGameCanvas.hideCanvas();
            layouts.coinLayout.showCanvas();
            layouts.staticLayout.showCanvas();
            currentScore = _spawner.VirusDestroyed;
            targetScore = _spawner.SpawnCount;
            setTargetScoreTxt();
            setCurrentScoreTxt();
            SoundManager.Instance.PlayMusicInList("House");
            //Debug.Log("You Win!");
            //StartCoroutine(Fader.FadeOutGameObject(Timer, 1));

            setWin();
            check++; // Prevent further checks after a condition is met
            gameDoneCheck++;
            SoundManager.Instance.PlaySFXInList("win");
            resultCanvas.showCanvas();
        }
    }

    private void setWin()
    {
        int multiplier = 1;
        int initialCoin = 0;
        if (_spawner.SpawnCount == 20)
        {
            multiplier = 3;
            initialCoin = 150;
        }
        else if (_spawner.SpawnCount == 25)
        {
            multiplier = 5;
            initialCoin = 300;
        }
        coinGained = initialCoin + timerScript.timerRemains() * _spawner.SpawnCount * multiplier;
        CoinManager.Instance.addCoin(coinGained);

        resultPanel.sprite = winPanelImg;
        setCoinGainedTxt();
        targetScoreTXT.color = new Color32(85, 180, 1, 255);
        SoundManager.Instance.PlaySFXInList("win");
    }

    private void setLose()
    {
        Time.timeScale = 0;
        resultPanel.sprite = losePanelImg;
        coinGained = 0;
        setCoinGainedTxt();
        targetScoreTXT.color = new Color32(231, 26, 0, 255);
        SoundManager.Instance.PlaySFXInList("Lose");
    }
}
