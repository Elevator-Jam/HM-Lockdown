using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HM.Lockdown.GameFeel {
    [CreateAssetMenu(fileName = "GameFeelEvent", menuName = "ScriptableObjects/GameFeelEvent", order = 1)]
    public class GameFeelEvent : ScriptableObject {
        [SerializeField]
        private ITest[] test;

        private Dictionary<string, System.Type> requiredArguments = new();

        public IEnumerator Invoke(GameFeelArgs args) {
            throw new System.NotImplementedException();
        }

        internal void AddRequiredArg<ArgType>(string argName) {
            if(!requiredArguments.TryAdd(argName, typeof(ArgType)))
            {

            }
        }
    }
}