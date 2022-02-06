using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IGS520b.starter.SampleGame
{    
    public class GameManager : MonoBehaviour
    {
        [Tooltip("Distance to move before game starts.")]
        [Range(0.0f, 5f)]
        public float startDistance;
        [Tooltip("Game time limit in seconds.")]
        public int timeLimit = 120;

        [Tooltip("Points text.")]
        public TextMeshProUGUI pointsText;
        [Tooltip("Time text.")]
        public TextMeshProUGUI timeText;

        private enum GameState
            {
                initiated,
                stopped,
                started
            };
        
        private Transform _characterTransform;
        private GameState _gameState;
        private Vector3 _startPosition;
        private float _gameStartTime;
        private float _timeRemaining;
        private float _points;
        private float _maxPoints;
        private bool startChasing;

        public GameObject heart1, heart2, heart3, gameOver;
        public static int health;

        public bool GetStartChasing(){
            return startChasing;
        }

        // Start is called before the first frame update
        void Start()
        {
            startChasing = false;

            // HealthInitialCheck();

            CharacterController[] characterControllers = FindObjectsOfType<CharacterController>();
            if (characterControllers.Length != 1)
            {
                Debug.LogError("Expecting only one `CharacterController`");
            }

            _characterTransform = characterControllers[0].transform;
            _startPosition = _characterTransform.position;
            timeText.text = "Move to start";
            pointsText.text = "";
        }

        void HealthInitialCheck() 
        {
            health = 3;
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
            gameOver.gameObject.SetActive(false);
        }

        void HealthCheck() 
        {
            if(health > 3)
                health = 3;
            
            switch(health)
            {
            case 3:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;

            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break; 

            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;

            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                // Time.timeScale = 0;
                _gameState = GameState.stopped;
                timeText.text += "\nGameOver"; // WHY \N?!?
                break;    
            }
        }

        void OnPointScored(GamePoint gamePoint)
        {
            gamePoint.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
            gamePoint.transform.Translate(Vector3.down, Space.World);
            _points += gamePoint.points;
            // Destroy(gamePoint.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            switch (_gameState)
            {
                // The timer/game-level is not started until the character moves by startDistance
                case GameState.initiated:
                    if ((_startPosition - _characterTransform.transform.position).magnitude > startDistance)
                    {
                        Debug.Log($"Game Started");
                        startChasing = true;
                        _gameState = GameState.started;
                        _gameStartTime = Time.fixedTime;
                        _timeRemaining = timeLimit;

                        foreach (GamePoint gamePoint in FindObjectsOfType<GamePoint>())
                        {
                            gamePoint.OnTriggerEnterAction += OnPointScored;
                            _maxPoints += gamePoint.points;
                        }
                    }
                    break;
                case GameState.started:
                    startChasing = true;
                    // HealthCheck();

                    _timeRemaining = timeLimit - (Time.fixedTime - _gameStartTime);

                    timeText.text = $"Time remaining: {Mathf.FloorToInt(_timeRemaining / 60)}:{_timeRemaining % 60.0f}";
                    pointsText.text = $"Points: {_points}/{_maxPoints}";
                    
                    if (_timeRemaining <= 0 || _points == _maxPoints)
                    {
                        startChasing = false;
                        _gameState = GameState.stopped;
                        if (_timeRemaining <= 0)
                        {
                            timeText.text += "\nTime out";
                        }
                        else
                        {
                            timeText.text += "\nMax Score Reached";
                        }
                        
                        foreach (GamePoint gamePoint in FindObjectsOfType<GamePoint>())
                        {
                            gamePoint.OnTriggerEnterAction -= OnPointScored;
                        }
                    }

                    break;
                case GameState.stopped:
                    startChasing = false;
                    break;
            }
        }
    }
}
