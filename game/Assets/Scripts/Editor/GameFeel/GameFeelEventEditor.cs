using System.Collections;
using HM.Lockdown.GameFeel;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace HM.Lockdown.Editor.GameFeel {
    [CustomEditor(typeof(GameFeelEvent))]
    [CanEditMultipleObjects]
    public class GameFeelEventEditor : UnityEditor.Editor {
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
                onAddDropdownCallback = (Rect buttonRect, ReorderableList list) => {
                    var addMenu = new GenericMenu();
                    addMenu.AddItem(new GUIContent("TestOne"), false, OnAdd, "TestOne");
                    addMenu.AddItem(new GUIContent("TestTwo"), false, OnAdd, "TestTwo");
                    addMenu.DropDown(buttonRect);
                },
                drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                    // Retrieve the array element
                    SerializedProperty newProp = testProp.GetArrayElementAtIndex(index);

                    // neato
                    rect.height = EditorGUIUtility.singleLineHeight;
                    EditorGUI.PropertyField(rect, newProp, true);

                    // Neato
                    if (newProp.objectReferenceValue == null) {
                        return;
                    }

                    // Neato
                    UnityEditor.Editor editor = CreateEditor(newProp.objectReferenceValue);
                    SerializedObject toEdit = editor.serializedObject;
                    toEdit.Update();

                    SerializedProperty iterator = toEdit.GetIterator();
                    iterator.NextVisible(true);
                    while (iterator.NextVisible(true)) {
                        rect.y += EditorGUIUtility.singleLineHeight;
                        EditorGUI.PropertyField(rect, iterator);
                    }

                    if (GUI.changed) {
                        toEdit.ApplyModifiedProperties();
                    }
                }
            };
        }

        public override void OnInspectorGUI() {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();

            testGui.DoLayoutList();

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }

        private void OnAdd(object add) {
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