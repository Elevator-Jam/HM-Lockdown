using System;
using System.Collections;
using UnityEngine;

namespace HM.Lockdown.GameFeel {
    [System.Serializable]
    public abstract class ITest : /*UnityEngine.Object*/ ScriptableObject {
        [SerializeField]
        protected string source = "source";
        [SerializeField]
        protected string target = "target";
    }
}