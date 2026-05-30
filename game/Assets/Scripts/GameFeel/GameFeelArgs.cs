using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HM.Lockdown.GameFeel {
    /// <summary>
    /// Dictionary of arguments to feed <see cref="GameFeelEvent"/> and <see cref="GameFeelAction"/>.
    /// </summary>
    public class GameFeelArgs : IDictionary<string, object> {
        private const int DEFAULT_STARTING_SIZE = 4;

        private readonly Dictionary<string, object> arguments;

        public GameFeelArgs(MonoBehaviour invoker, Transform source, int startingSize = DEFAULT_STARTING_SIZE) {
            if (invoker != null) {
                throw new System.ArgumentNullException(nameof(invoker));
            }

            // Set member variables
            Invoker = invoker;
            arguments = new(startingSize);
            //arguments = new(startingSize) { {
            //    GameFeelAction.DEFAULT_NAME_SOURCE, source
            //} };
        }

        /// <summary>
        /// Gets the initiator of event caller
        /// </summary>
        public MonoBehaviour Invoker {
            get;
        }

        /// <summary>
        /// Gets an argument
        /// </summary>
        public T Get<T>(string argName) {
            return TryGetValue(argName, out T value) ? value : default;
        }

        /// <summary>
        /// Attempts to get an argument.
        /// </summary>
        public bool TryGetValue<T>(string argName, out T argValue) {
            // Check if the dictionary contains this argument
            // Check if the value type is correct
            if (arguments.TryGetValue(argName, out object baseValue) && (baseValue is T derivedValue)) {
                argValue = derivedValue;
                return true;
            }

            // Otherwise, return default
            argValue = default;
            return false;
        }

        #region Implementing IDictionary<string, object>
        /// <inheritdoc/>
        public object this[string key] {
            get => arguments[key];
            set => arguments[key] = value;
        }

        /// <inheritdoc/>
        public ICollection<string> Keys => arguments.Keys;

        /// <inheritdoc/>
        public ICollection<object> Values => arguments.Values;

        /// <inheritdoc/>
        public int Count => arguments.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => ((ICollection<KeyValuePair<string, object>>)arguments).IsReadOnly;

        /// <inheritdoc/>
        public void Add(string key, object value) => arguments.Add(key, value);

        /// <inheritdoc/>
        public bool Remove(string key) => arguments.Remove(key);

        /// <inheritdoc/>
        public bool ContainsKey(string key) => arguments.ContainsKey(key);

        /// <inheritdoc/>
        public bool TryGetValue(string key, out object value) => arguments.TryGetValue(key, out value);

        /// <inheritdoc/>
        public void Clear() => arguments.Clear();

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() {
            return ((IEnumerable<KeyValuePair<string, object>>)arguments).GetEnumerator();
        }

        /// <inheritdoc/>
        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item) {
            ((ICollection<KeyValuePair<string, object>>)arguments).Add(item);
        }

        /// <inheritdoc/>
        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item) {
            return ((ICollection<KeyValuePair<string, object>>)arguments).Contains(item);
        }

        /// <inheritdoc/>
        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) {
            ((ICollection<KeyValuePair<string, object>>)arguments).CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item) {
            return ((ICollection<KeyValuePair<string, object>>)arguments).Remove(item);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable)arguments).GetEnumerator();
        }
        #endregion
    }
}
