using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Caminho;

namespace CommonTimeGames.NekoMaze.UI
{
    public class DialogueScreen : MonoBehaviour
    {
        public static DialogueScreen Instance { get; set; }

        public bool IsShowing = false;

        public float LetterTime = 0.05f;
        public float SpeedMultiplier = 0.5f;

        public GameObject Panel;
        public Text DialogueText;
        public Image ContinueIcon;

        private string _dialogueText;

        private float _letterTimer;
        private int _currentLetterIndex;
        private StringBuilder _dialogueString;

        private CaminhoEngine _engine;

        void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("DialogueScreen already exists; destroying this");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        // Use this for initialization
        void Start()
        {
            _dialogueString = new StringBuilder();
            _dialogueText = "";
            _currentLetterIndex = 0;
            _engine = new CaminhoEngine();
            _engine.Initialize();
            IsShowing = false;
            StartDialogue("test");
        }

        // Update is called once per frame
        void Update()
        {
            Panel.SetActive(IsShowing);

            if (!IsShowing)
            {
                return;
            }

            var accept = Input.GetKey(KeyCode.Return);
            var acceptPressed = Input.GetKeyDown(KeyCode.Return);

            if (_dialogueString.Length < _dialogueText.Length)
            {
                _letterTimer -= Time.unscaledDeltaTime;

                if (_letterTimer <= 0)
                {
                    _dialogueString.Append(_dialogueText[_currentLetterIndex]);
                    _currentLetterIndex++;
                    _letterTimer = accept ? LetterTime * SpeedMultiplier : LetterTime;
                }

                DialogueText.text = _dialogueString.ToString();
                ContinueIcon.color = Color.clear;
            }
            else
            {
                ContinueIcon.color = Color.white;

                if (acceptPressed)
                {
                    Continue();
                }
            }
        }

        private void _DialogueMessage(string message, string metadata)
        {
            Debug.Log("Send message: " + message + " to object: " + metadata);

            var target = GameObject.Find(metadata);

            if (target != null)
            {
                target.SendMessage(message, null, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                Debug.LogWarning("Object " + metadata + " not found!");
            }
        }

        private void _DialogueWaitStart()
        {
            IsShowing = false;
        }

        void _ShowText(string text)
        {
            IsShowing = true;
            _dialogueText = text;
            _dialogueString.Remove(0, _dialogueString.Length);
            _currentLetterIndex = 0;
        }

        public static void ShowText(string text)
        {
            Instance._ShowText(text);
        }

        public static void StartDialogue(string name, string package = default(string))
        {
            Instance._engine.Start(name, package);
            Instance.RefreshDialogueScreen();
        }

        public static void Continue(int value = 0)
        {
            Instance._engine.Continue();
            Instance.RefreshDialogueScreen();
        }

        private void RefreshDialogueScreen()
        {
            if (_engine.Status == CaminhoStatus.Active)
            {
                IsShowing = true;

                if (_engine.Current.Type == CaminhoNodeType.Text)
                {
                    ShowText(_engine.Current.Text);
                }
                else if (_engine.Current.Type == CaminhoNodeType.Choice)
                {
                    ShowText(_engine.Current.Text);
                }
            }
            else if (_engine.Status == CaminhoStatus.Inactive)
            {
                IsShowing = false;
            }
            else
            {
                IsShowing = false;
                Debug.LogError(_engine.Current.ErrorMessage);
            }
        }
    }

}

