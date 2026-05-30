using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HM.Lockdown.Managers {
	public class PoolManager : SingletonConstructor<PoolManager> {
        private void Awake() {
            ConstructSingleton(this); // ! DO NOT DELETE
        }
    }
}