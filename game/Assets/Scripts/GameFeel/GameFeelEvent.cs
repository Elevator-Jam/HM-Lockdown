using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HM.Lockdown.GameFeel {
    [CreateAssetMenu(fileName = "GameFeelEvent", menuName = "Scriptable Objects/Game-Feel Event", order = 1)]
    public class GameFeelEvent : ScriptableObject {
        [SerializeField]
        private ITest[] test;

        private Dictionary<string, System.Type> requiredArguments = new();

        public void TestAdd() {
            if (test != null) {
                test = new ITest[test.Length + 1];
            } else {
                test = new ITest[1];
            }
            test[test.Length - 1] = new TestOne();
        }

        public IEnumerator Invoke(GameFeelArgs args) {
            throw new System.NotImplementedException();
        }

        internal void AddRequiredArg<ArgType>(string argName) {
            // Add the argument to the required arguments dictionary
            System.Type type = typeof(ArgType);
            if (!requiredArguments.TryAdd(argName, type)) {
                // If it failed, check to see if the arg type is the same
                if (requiredArguments[argName] != type) {

                }
            }
        }
    }
}