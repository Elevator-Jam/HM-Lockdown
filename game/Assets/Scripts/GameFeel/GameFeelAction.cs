using System.Collections;
using UnityEngine;

namespace HM.Lockdown.GameFeel {
    /// <summary>
    /// Base class for an action invoked by <see cref="GameFeelEvent.Invoke(GameFeelArgs)"/>.
    /// </summary>
    public abstract class GameFeelAction : ScriptableObject {
        public const string DEFAULT_NAME_SOURCE = nameof(source);

        [SerializeField]
        protected string source = DEFAULT_NAME_SOURCE;

        /// <summary>
        /// Coroutine for the action to perform.
        /// </summary>
        public abstract IEnumerator Invoke(GameFeelArgs args);

        /// <summary>
        /// Adds arguments required by this action into <paramref name="parentEvent"/>.
        /// </summary>
        internal virtual void AddArgs(RequiredArgsList parentEvent) {
            // Add the source and target requirements
            parentEvent.AddRequiredArg<Transform>(source, nameof(GameFeelAction));
        }
    }
}
