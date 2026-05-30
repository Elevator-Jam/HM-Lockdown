using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HM.Lockdown.Managers {
    public class PauseManager : SingletonConstructor<PauseManager> {
        private void Awake() {
            ConstructSingleton(this); // ! DO NOT DELETE
        }
    }
}