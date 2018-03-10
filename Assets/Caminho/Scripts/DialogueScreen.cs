using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Caminho;

namespace CommonTimeGames.UI
{
    public class DialogueScreen : MonoBehaviour
    {
        public static DialogueScreen Instance { get; set; }

        public bool IsShowing = false;

        public float LetterTime = 0.05f;
        public float SpeedMultiplier = 0.5f;

        public GameObject Panel;

        public GameObject DialoguePanel;
        public Text DialogueText;
        public Image ContinueIcon;

        public GameObject ChoicePanel;
        public GameObject ChoiceButtonPanel;
        public Text ChoicePanelText;
        public Image ChoiceContinueIcon;

        public GameObject[] ChoiceButtons;
        public Text[] ChoiceButtonText;

        private Text _currentDialogueText;
        private Image _currentContinueIcon;

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
                    _letterTimer = accept ?
                        LetterTime * SpeedMultiplier : LetterTime;
                }

                _currentDialogueText.text = _dialogueString.ToString();
                _currentContinueIcon.color = Color.clear;
            }
            else
            {
                _currentContinueIcon.color = Color.white;

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

        public void ChoicePressed(int value = 0)
        {
            Continue(value);
        }

        private void RefreshDialogueScreen()
        {
            if (_engine.Status == CaminhoStatus.Active)
            {
                IsShowing = true;

                if (_engine.Current.Type == CaminhoNodeType.Text)
                {
                    _currentDialogueText = DialogueText;
                    _currentContinueIcon = ContinueIcon;

                    DialoguePanel.SetActive(true);
                    ChoicePanel.SetActive(false);
                    ChoiceButtonPanel.SetActive(false);

                    ShowText(_engine.Current.Text);
                }
                else if (_engine.Current.Type == CaminhoNodeType.Choice)
                {
                    _currentDialogueText = ChoicePanelText;
                    _currentContinueIcon = ChoiceContinueIcon;

                    DialoguePanel.SetActive(false);
                    ChoicePanel.SetActive(true);
                    ChoiceButtonPanel.SetActive(true);

                    for (int i = 0; i < ChoiceButtonText.Length; i++)
                    {
                        if (i < _engine.Current.Choices.Length)
                        {
                            ChoiceButtons[i].SetActive(true);

                            ChoiceButtonText[i].text =
                                _engine.Current.Choices[i].Text;
                        }

                        else
                        {
                            ChoiceButtons[i].SetActive(false);
                        }
                    }
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

