﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SystemScripts
{
    public class GameStatusController : MonoBehaviour
    {
        public TextMeshProUGUI playerScoreText;
        public TextMeshProUGUI collectedCoinText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI secondsText;
        public TextMeshProUGUI livesText;
        public GameObject pausePopup;
        public GameObject instructionPopup;
        public GameObject creditPopup;

        private bool _pauseTrigger;
        public static int CollectedCoin;
        public static int Score;
        public static int Live;
        public static int CurrentLevel;
        public static bool IsDead;
        public static bool IsGameOver;
        public static bool IsBigPlayer;
        public static string PlayerTag;
        private float _second;

        private void Awake()
        {
            _pauseTrigger = false;
        }

        private void Update()
        {
            SetCoin();
            SetLevel();
            SetScore();
            SetLive();
            Pause();
        }

        private void SetScore()
        {
            switch (Score.ToString().Length)
            {
                case 0:
                    playerScoreText.SetText("000000");
                    break;
                case 3:
                    playerScoreText.SetText($"000{Score}");
                    break;
                case 4:
                    playerScoreText.SetText($"00{Score}");
                    break;
                case 5:
                    playerScoreText.SetText($"0{Score}");
                    break;
                case 6:
                    playerScoreText.SetText($"{Score}");
                    break;
            }
        }

        private void SetCoin()
        {
            if (CollectedCoin > 0)
            {
                collectedCoinText.SetText($"x0{CollectedCoin}");
                if (CollectedCoin <= 9) return;
                collectedCoinText.SetText($"x{CollectedCoin}");
                if (CollectedCoin > 99)
                {
                    collectedCoinText.SetText("x00");
                }
            }
            else
            {
                collectedCoinText.SetText("x00");
            }
        }

        public void SetTime(float second)
        {
            _second = second;
            if (_second > 0)
            {
                if (_second > 99.5f)
                {
                    secondsText.SetText(Mathf.RoundToInt(_second).ToString());
                }
                else if (_second > 9.5f)
                {
                    secondsText.SetText($"0{Mathf.RoundToInt(_second).ToString()}");
                }
                else
                {
                    secondsText.SetText($"00{Mathf.RoundToInt(_second).ToString()}");
                }
            }
            else
            {
                secondsText.SetText("000");
            }
        }

        private void SetLevel()
        {
            levelText.SetText(SceneManager.GetActiveScene().name);
        }

        private void SetLive()
        {
            livesText.SetText($"x {Live.ToString()}");
        }

        private void Pause()
        {
            if (SceneManager.GetActiveScene().buildIndex > 1)
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    _pauseTrigger = !_pauseTrigger;
                    pausePopup.SetActive(_pauseTrigger);
                    Time.timeScale = _pauseTrigger ? 0 : 1;
                }
            }
        }

        public void StartGame()
        {
            SceneManager.LoadScene(1);
            CurrentLevel = 2;
            Live = 3;
            Score = 0;
            CollectedCoin = 0;
        }

        public void OpenInstructionPopup()
        {
            instructionPopup.SetActive(true);
        }

        public void OpenCreditPopup()
        {
            creditPopup.SetActive(true);
        }

        public void CloseInstructionPopup()
        {
            instructionPopup.SetActive(false);
        }

        public void CloseCreditPopup()
        {
            creditPopup.SetActive(false);
        }

        public void ExitGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}