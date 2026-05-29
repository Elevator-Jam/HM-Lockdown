using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace HM.Lockdown.GameFeel.Editor {
    [CustomEditor(typeof(GameFeelEvent))]
    [CanEditMultipleObjects]
    public class GameFeelEventEditor : UnityEditor.Editor {
        private static readonly Vector2 margin = new(16, 2);
        SerializedProperty testProp;
        ReorderableList testGui;

        void OnEnable() {
            // Setup the SerializedProperties.
            testProp = serializedObject.FindProperty("test");

            // Setup the test gui
            testGui = new(serializedObject, testProp) {
                drawHeaderCallback = (Rect rect) => {
                    EditorGUI.LabelField(rect, "Actions");
                },
                onAddDropdownCallback = OnArrayDropdown,
                drawElementCallback = OnArrayDrawElement,
                elementHeightCallback = GetArrayElementHeight
            };
        }

        public override void OnInspectorGUI() {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();

            testGui.DoLayoutList();

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }

        private void OnArrayDrawElement(Rect rect, int index, bool isActive, bool isFocused) {
            // Retrieve the array element
            SerializedProperty newProp = testProp.GetArrayElementAtIndex(index);
            GUIContent label = EditorGUI.BeginProperty(rect, new GUIContent(newProp.displayName), newProp);

            // Setup rect
            rect.x += margin.x;
            rect.width -= margin.x;
            rect.y += margin.y;
            rect.height = EditorGUIUtility.singleLineHeight;
            bool foldOut = EditorGUI.BeginFoldoutHeaderGroup(rect, true, label);

            // Neato
            if (newProp.objectReferenceValue == null) {
                return;
            }

            // Neato
            UnityEditor.Editor editor = CreateEditor(newProp.objectReferenceValue);
            SerializedObject toEdit = editor.serializedObject;
            toEdit.Update();

            // Skip the first scriptable object argument
            SerializedProperty iterator = toEdit.GetIterator();
            iterator.NextVisible(true);

            // Setup rect
            rect.y += EditorGUIUtility.singleLineHeight;

            // Draw all fields
            while (iterator.NextVisible(true)) {
                EditorGUI.PropertyField(rect, iterator);
                rect.y += EditorGUIUtility.singleLineHeight + margin.y;
            }

            if (GUI.changed) {
                toEdit.ApplyModifiedProperties();
            }
            EditorGUI.EndFoldoutHeaderGroup();
            EditorGUI.EndProperty();
        }

        private float GetArrayElementHeight(int index) {
            // Retrieve the array element
            float toReturn = EditorGUIUtility.singleLineHeight;
            SerializedProperty newProp = testProp.GetArrayElementAtIndex(index);

            // Neato
            if (newProp.objectReferenceValue == null) {
                return toReturn;
            }

            // Neato
            UnityEditor.Editor editor = CreateEditor(newProp.objectReferenceValue);
            SerializedObject toEdit = editor.serializedObject;
            toEdit.Update();

            SerializedProperty iterator = toEdit.GetIterator();
            iterator.NextVisible(true);
            while (iterator.NextVisible(true)) {
                toReturn += EditorGUIUtility.singleLineHeight + margin.y;
            }
            return toReturn;
        }

        private void OnArrayDropdown(Rect buttonRect, ReorderableList list) {
            var addMenu = new GenericMenu();
            // FIXME: read the items from a list or dictionary
            addMenu.AddItem(new GUIContent("TestOne"), false, OnArrayAdd, "TestOne");
            addMenu.AddItem(new GUIContent("TestTwo"), false, OnArrayAdd, "TestTwo");
            addMenu.DropDown(buttonRect);
        }

        private void OnArrayAdd(object add) {
            // Add an empty entry
            testProp.arraySize += 1;

            // Retrieve the array element
            SerializedProperty newProp = testProp.GetArrayElementAtIndex(testProp.arraySize - 1);

            // Create a new test instance
            ITest newTest = CreateInstance<TestOne>();
            newTest.name = nameof(TestOne);
            AssetDatabase.AddObjectToAsset(newTest, serializedObject.targetObject);

            // Change the null reference to an actual instance
            newProp.objectReferenceValue = newTest;

            // Apply changes to the serializedProperty
            serializedObject.ApplyModifiedProperties();
            AssetDatabase.SaveAssets();
        }
    }
}