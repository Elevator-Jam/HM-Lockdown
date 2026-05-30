using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace HM.Lockdown.GameFeel.Tests {
    public class TestGameFeelEvent {
        public class SampleGameFeelAction : GameFeelAction {
            public override IEnumerator Invoke(GameFeelArgs args) {
                throw new System.NotImplementedException();
            }
        }

        // A Test behaves as an ordinary method
        [Test]
        public void TestGameFeelEventSimplePasses() {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestGameFeelEventWithEnumeratorPasses() {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
