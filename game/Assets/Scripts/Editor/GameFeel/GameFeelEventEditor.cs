using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditorInternal;
using UnityEngine;

namespace HM.Lockdown.GameFeel.Editor {
    [CustomEditor(typeof(GameFeelEvent))]
    [CanEditMultipleObjects]
    public class GameFeelEventEditor : UnityEditor.Editor {
        private static readonly Vector2 margin = new(16, 2);
        SerializedProperty testProp;
        ReorderableList testGui;
        readonly List<AnimBool> foldoutOpen = new();

        void OnEnable() {
            // Setup the SerializedProperties.
            testProp = serializedObject.FindProperty(GameFeelEvent.NAME_ACTIONS);

            // Setup the test gui
            testGui = new(serializedObject, testProp) {
                drawHeaderCallback = (Rect rect) => {
                    EditorGUI.LabelField(rect, "Actions");
                },
                drawElementCallback = OnArrayDrawElement,
                elementHeightCallback = GetArrayElementHeight,
                onAddDropdownCallback = OnArrayDropdown,
                onRemoveCallback = OnArrayRemove
            };

            // Update fold-out status
            while (foldoutOpen.Count < testProp.arraySize) {
                foldoutOpen.Add(new AnimBool(false));
            }
            while (foldoutOpen.Count > testProp.arraySize) {
                foldoutOpen.RemoveAt(foldoutOpen.Count - 1);
            }
        }

        public override void OnInspectorGUI() {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();

            testGui.DoLayoutList();

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }

        #region ReorderableList delegates
        private void OnArrayDrawElement(Rect rect, int index, bool isActive, bool isFocused) {
            // Retrieve the array element
            SerializedProperty newProp = testProp.GetArrayElementAtIndex(index);

            // Setup property
            string headerName = newProp.objectReferenceValue != null ? newProp.objectReferenceValue.name : "null";
            headerName = $"{newProp.displayName} - {headerName}";

            //using (new EditorGUI.IndentLevelScope())
            using (var scope = new EditorGUI.PropertyScope(rect, new GUIContent(headerName), newProp)) {
                // Setup rect
                rect.x += margin.x;
                rect.width -= margin.x;
                rect.height = EditorGUIUtility.singleLineHeight;

                // Draw the foldable array
                foldoutOpen[index].target = EditorGUI.BeginFoldoutHeaderGroup(rect, foldoutOpen[index].target, scope.content);

                // Don't draw any elements if collapsed
                // Don't draw anything if object is null
                if (!foldoutOpen[index].target || (newProp.objectReferenceValue == null)) {
                    EditorGUI.EndFoldoutHeaderGroup();
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
                rect.y += EditorGUIUtility.singleLineHeight + margin.y;

                // Draw all fields
                while (iterator.NextVisible(true)) {
                    EditorGUI.PropertyField(rect, iterator);
                    rect.y += EditorGUIUtility.singleLineHeight + margin.y;
                }

                if (GUI.changed) {
                    toEdit.ApplyModifiedProperties();
                }

                // Close out the header group
                EditorGUI.EndFoldoutHeaderGroup();
            }
        }

        private float GetArrayElementHeight(int index) {
            // Retrieve the array element
            float toReturn = EditorGUIUtility.singleLineHeight;
            SerializedProperty newProp = testProp.GetArrayElementAtIndex(index);

            // Render only 1 line if drawing collapsed header or if object is null
            if (!foldoutOpen[index].target || (newProp.objectReferenceValue == null)) {
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
            // Create a new test instance
            GameFeelAction newAction = CreateInstance<TestOne>();
            newAction.name = nameof(TestOne);
            AssetDatabase.AddObjectToAsset(newAction, serializedObject.targetObject);
            AssetDatabase.SaveAssets();

            // Add an empty entry
            testProp.arraySize += 1;

            // Retrieve the array element
            SerializedProperty newProp = testProp.GetArrayElementAtIndex(testProp.arraySize - 1);

            // Change the null reference to an actual instance
            newProp.objectReferenceValue = newAction;

            // Apply changes to the serializedProperty
            serializedObject.ApplyModifiedProperties();

            // Create new undo group
            Undo.SetCurrentGroupName($"Add {newAction.name} to GameFeelEvent");
            int group = Undo.GetCurrentGroup();
            Undo.RegisterCreatedObjectUndo(newAction, $"Create a new {newAction.name}");
            Undo.RecordObject(serializedObject.targetObject, "Add new element to array");
            Undo.CollapseUndoOperations(group);

            // Update fold-out status
            while (foldoutOpen.Count < testProp.arraySize) {
                foldoutOpen.Add(new AnimBool(true));
            }
        }

        private void OnArrayRemove(ReorderableList list) {
            // Make sure there's something to remove
            if (list.selectedIndices.Count <= 0) {
                return;
            }

            // Setup list of objects to destroy
            var toDestroy = new List<Object>(list.selectedIndices.Count);

            // Remove the elements from the array in reverse chronological order
            var indicesSorted = new List<int>(list.selectedIndices);
            indicesSorted.Sort();
            for (int i = indicesSorted.Count - 1; i >= 0; --i) {
                // Collect objects to destroy
                int index = indicesSorted[i];
                SerializedProperty prop = list.serializedProperty.GetArrayElementAtIndex(index);
                toDestroy.Add(prop.objectReferenceValue);

                // Removing in reverse order to make sure elements indices don't get shifted too much
                list.serializedProperty.DeleteArrayElementAtIndex(index);
            }

            // Apply changes to the serializedProperty
            serializedObject.ApplyModifiedProperties();

            // Create new undo group
            Undo.SetCurrentGroupName("Remove elements from GameFeelEvent");
            int group = Undo.GetCurrentGroup();
            Undo.RecordObject(serializedObject.targetObject, "Remove elements from array");

            // Destroy removed objects
            foreach (var subObject in toDestroy) {
                Undo.DestroyObjectImmediate(subObject);
            }

            // Close the undo group
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Undo.CollapseUndoOperations(group);

            // Update foldout status
            while (foldoutOpen.Count > testProp.arraySize) {
                foldoutOpen.RemoveAt(foldoutOpen.Count - 1);
            }
        }
        #endregion
    }
}
