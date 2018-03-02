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
            Debug.Log(_engine.Current.Text);

            _engine.Continue();
            Debug.Log(_engine.Current.Text);

            _engine.Continue();
            Debug.Log(_engine.Current.Text);
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void Start(string dialogue,
                          string package = default(string),
                          string startNode = default(string))
        {

        }

        public void Continue(int value = 1)
        {

        }

        public void End()
        {

        }

    }
}
