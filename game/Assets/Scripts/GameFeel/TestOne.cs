using System;
using System.Collections;
using UnityEngine;

namespace HM.Lockdown.GameFeel {
    [Serializable]
    public class TestOne : GameFeelAction {
        [SerializeField]
        private int one;

        public override IEnumerator Invoke(GameFeelArgs args) {
            throw new NotImplementedException();
        }
    }
}
