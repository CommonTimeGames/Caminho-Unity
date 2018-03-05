using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caminho;

namespace CommonTimeGames.Caminho
{
    public class CaminhoDialogueRunner : MonoBehaviour
    {
        private CaminhoEngine _engine;

        // Use this for initialization
        void Start()
        {
            _engine = new CaminhoEngine();
            _engine.Initialize();

            _engine.Start("test");
            Debug.Log(_engine.Current.Text); //"Welcome to Caminho-Unity!"

            _engine.Continue();
            Debug.Log(_engine.Current.Text); //"If you're reading this then the dialogue loaded successfully."

            _engine.Continue();
            Debug.Log(_engine.Current.Text); //"Good bye!"
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void Start(string dialogue,
                          string package = default(string),
                          string startNode = default(string))
        {
            _engine.Start(dialogue, package, startNode);
        }

        public void Continue(int value = 1)
        {
            _engine.Continue(value);
        }

        public void End()
        {
            _engine.End();
        }

    }
}
