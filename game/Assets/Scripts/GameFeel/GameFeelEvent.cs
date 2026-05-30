using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace HM.Lockdown.GameFeel {
    /// <summary>
    /// List of <see cref="GameFeelAction"/>s
    /// </summary>
    [CreateAssetMenu(fileName = "GameFeelEvent", menuName = "Scriptable Objects/Game-Feel Event", order = 1)]
    public class GameFeelEvent : ScriptableObject {
#if UNITY_EDITOR // Strings used in custom editor
        public const string NAME_ACTIONS = nameof(actions);
#endif

        [SerializeField]
        [FormerlySerializedAs("test")]
        private GameFeelAction[] actions;

        private RequiredArgsList requiredArguments = null;

        /// <summary>
        /// Dictionary of arguments required by this event.
        /// </summary>
        public RequiredArgsList RequiredArguments {
            get {
                // Check if the member variable is defined
                if (requiredArguments == null) {
                    // If not, construct and populate the arguments
                    requiredArguments = new(actions.Length);
                    foreach (GameFeelAction action in actions) {
                        action.AddArgs(requiredArguments);
                    }
                }

                // Return the cached arguments
                return requiredArguments;
            }
        }

        /// <summary>
        /// Make sure that <paramref name="args"/> contains all the required arguments and the appropriate type.
        /// </summary>
        public bool VerifyArgs(GameFeelArgs args) => RequiredArguments.VerifyArgs(args);

        /// <summary>
        /// Run the game feel actions.
        /// </summary>
        /// <param name="args">
        /// Arguments to feed each <see cref="GameFeelAction"/>s
        /// </param>
        /// <returns>The coroutine of actions</returns>
        public IEnumerator Invoke(GameFeelArgs args) {
            // Run each action synchronously
            foreach (GameFeelAction action in actions) {
                yield return args.Invoker.StartCoroutine(action.Invoke(args));
            }
        }
    }
}
