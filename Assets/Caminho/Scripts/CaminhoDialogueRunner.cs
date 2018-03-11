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
