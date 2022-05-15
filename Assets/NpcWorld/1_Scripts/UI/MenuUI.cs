using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

namespace npcWorld
{
    public class MenuUI : MonoBehaviour //UI and GameManager
    {
        public static MenuUI InstanceMenu;

        PlayerController _player;       
        [SerializeField] private GameObject[] _racers;
        [SerializeField] private float _playerDistance;
        [SerializeField] private float[] racerDistances = new float[10];
        [SerializeField] private Transform _finishLine;
        [SerializeField] private bool _isGamePaused = false;
        [SerializeField] private GameObject _menuWindow;
        [SerializeField] private GameObject _startIns;
        [SerializeField] private TextMeshProUGUI _positionText;
        public bool raceStarted;      

        private void Awake()
        {          
            raceStarted = false;

            if (_startIns!=null)
            {
                _startIns.SetActive(true);
            }
            else
            {
                Debug.LogError("StartIns Missing");
            }

            InstanceMenu = this;

            _racers = GameObject.FindGameObjectsWithTag("Opponent");
        }

        private void Start()
        {
            _player = PlayerController.InstancePlayer;
            _menuWindow.SetActive(false);
            _isGamePaused = false;

            if (_player == null)
            {
                Debug.LogError("Player Missing");
            }

            if(_racers.Length>0)
            {
                racerDistances = new float[_racers.Length];
                PositionTracker();
            }
        }

        private void Update()
        {
            if (_racers.Length > 0)
            {
                racerDistances = new float[_racers.Length];
                PositionTracker();
            }
        }

        private void PositionTracker()
        {
            _playerDistance = Vector3.Distance(_finishLine.position, _player.transform.position);

            for (int i = 0; i < _racers.Length; i++)
            {
                racerDistances[i]= Vector3.Distance(_finishLine.position, _racers[i].transform.position);
            }

            Array.Sort(racerDistances);

            int x = 0;
            foreach(float f in racerDistances) //sanki daha iyi yol bulunabilir
            {                          
                if (_playerDistance < f)
                {
                    _positionText.text = x+1 + "/" + (_racers.Length + 1);

                    x = 0;
                    break;
                }
                else //sonuncu
                {
                    _positionText.text = 11 + "/" + (_racers.Length + 1);
                }
                x++;
            }
        }

        public void ShowFinWindow()
        {
            _menuWindow.SetActive(true);
            _isGamePaused = true;
        }

        public void StartRace()
        {
            if(!raceStarted)
            {
                _startIns.SetActive(false);
                raceStarted = true;
            }
        }

        public void FinRace()
        {
            raceStarted = false;
        }

    

        #region UI Buttons

        public void MenuButton()
        {
            _isGamePaused = !_isGamePaused;
            
            //if(_isGamePaused) //begenmedim ama kullanilabilir
            //{
            //    Time.timeScale = 0;               
            //}
            //else
            //{
            //    Time.timeScale = 1;
            //}

            _menuWindow.SetActive(_isGamePaused);
        }

        public void PlayAgaintsOppenents()
        {
            SceneManager.LoadScene("OpponentsPlay");

        }

        public void PlaySolo()
        {
            SceneManager.LoadScene("SoloPlay");
        }

        public void AutoRunner()
        {
            if(_player!=null)
            {
                _player.runnerType = PlayerController.RunnerTypes.AutoRun;
            }
        }

        public void FullControl()
        {
            if (_player != null)
            {
                _player.runnerType = PlayerController.RunnerTypes.FullControl;
            }
        }

        public void RestartGame()
        {
            Time.timeScale = 1;
            Application.LoadLevel(Application.loadedLevel);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ChangeResolution1() //harika bir yontem kekwait
        {
            Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        }

        public void ChangeResolution2()
        {
            Screen.SetResolution(1080, 1920, FullScreenMode.FullScreenWindow);
        }

        #endregion

    }
}