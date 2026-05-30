using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HM.Lockdown.GameFeel {
    /// <summary>
    /// Helper class indicating what argument names and types are expected
    /// </summary>
    public class RequiredArgsList : IReadOnlyDictionary<string, Type> {
        private const int DEFAULT_STARTING_SIZE = 4;
        private readonly Dictionary<string, Type> requiredArguments;

        public RequiredArgsList(int startingSize = DEFAULT_STARTING_SIZE) {
            requiredArguments = new(startingSize);
        }

        /// <summary>
        /// Make sure that <paramref name="args"/> contains all the required arguments and the appropriate type.
        /// </summary>
        public bool VerifyArgs(GameFeelArgs args) {
            bool toReturn = true;

            // Go through all required arguments
            foreach (var (argName, argType) in requiredArguments) {
                // Check if the args parameter has key
                if (!args.TryGetValue(argName, out object testValue)) {
                    Debug.LogWarningFormat("{0}: {1} doesn't contain required argument, \"{2}\"",
                        nameof(RequiredArgsList), nameof(GameFeelArgs), argName);
                    toReturn = false;
                    continue;
                }

                // Check if the arg type is correct
                if (!argType.IsInstanceOfType(testValue)) {
                    Debug.LogWarningFormat("{0}: {1} has the wrong value type for argument \"{2}\"; expected type \"{3}\"",
                        nameof(RequiredArgsList), nameof(GameFeelArgs), argName, argType.Name);
                    toReturn = false;
                    continue;
                }
            }

            return toReturn;
        }

        #region Implementing IReadOnlyDictionary<string, Type>
        /// <inheritdoc/>
        public Type this[string key] => requiredArguments[key];

        /// <inheritdoc/>
        public IEnumerable<string> Keys => requiredArguments.Keys;

        /// <inheritdoc/>
        public IEnumerable<Type> Values => requiredArguments.Values;

        /// <inheritdoc/>
        public int Count => requiredArguments.Count;

        /// <inheritdoc/>
        public bool ContainsKey(string key) => requiredArguments.ContainsKey(key);

        /// <inheritdoc/>
        public bool TryGetValue(string key, out Type value) => requiredArguments.TryGetValue(key, out value);

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, Type>> GetEnumerator() {
            return ((IEnumerable<KeyValuePair<string, Type>>)requiredArguments).GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable)requiredArguments).GetEnumerator();
        }
        #endregion

        /// <summary>
        /// Adds an argument and type to this dictionary
        /// </summary>
        internal void AddRequiredArg<ArgType>(string argName, string scriptName = null) {
            // Make sure the arguments are valid
            if (string.IsNullOrWhiteSpace(argName)) {
                throw new ArgumentNullException(nameof(argName));
            }

            // Add the argument to the required arguments dictionary
            Type type = typeof(ArgType);
            if (requiredArguments.TryAdd(argName, type)) {
                // If no conflicts, add key and type, and halt
                return;
            } else if (requiredArguments[argName] == type) {
                // If there is a conflict, check to see if the arg type is the same
                // halt if they are
                return;
            }

            // Indicate the conflict
            if (string.IsNullOrEmpty(scriptName)) {
                Debug.LogErrorFormat("{0}: Conflicting argument name \"{1}\" detected", nameof(RequiredArgsList), argName);
            } else {
                Debug.LogErrorFormat("{0}: Script \"{1}\" attempted to add a conflicting argument name, \"{2}\"", nameof(RequiredArgsList), scriptName, argName);
            }
        }
    }
}
