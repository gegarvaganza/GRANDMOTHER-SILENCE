using UnityEngine;
using UnityEditor;

namespace DoorInteractionKit
{
    [CustomEditor(typeof(DoorInteractable))]
    public class DoorInteractableEditor : Editor
    {
        SerializedProperty interactableType;

        SerializedProperty doorTransform;

        SerializedProperty rotationAxis;
        SerializedProperty doorOpenRight;
        SerializedProperty openAngle;
        SerializedProperty openSpeed;
        SerializedProperty closeSpeed;

        SerializedProperty drawerOpenDirection;
        SerializedProperty drawerOpenSpeed;
        SerializedProperty drawerCloseSpeed;
        SerializedProperty slideDistance;

        SerializedProperty plankCount;
        SerializedProperty doorPlankedText;

        SerializedProperty isLocked;
        SerializedProperty lockedDoorText;
        SerializedProperty doorLockedSound;
        SerializedProperty keyScriptable;
        SerializedProperty removeKeyAfterUse;
        SerializedProperty removedPrompt;

        SerializedProperty doorUnlockSound;
        SerializedProperty unlockedDoorText;

        SerializedProperty doorOpenSound;
        SerializedProperty openDelay;

        SerializedProperty doorCloseSound;
        SerializedProperty closeDelay;

        SerializedProperty onDrawerOpen;

        private bool SpawnItems
        {
            get { return EditorPrefs.GetBool("SpawnItems" + target.GetInstanceID(), false); }
            set { EditorPrefs.SetBool("SpawnItems" + target.GetInstanceID(), value); }
        }

        private bool UsePlanks
        {
            get { return EditorPrefs.GetBool("UsePlanks" + target.GetInstanceID(), false); }
            set { EditorPrefs.SetBool("UsePlanks" + target.GetInstanceID(), value); }
        }

        private void OnEnable()
        {
            interactableType = serializedObject.FindProperty(nameof(interactableType));

            doorTransform = serializedObject.FindProperty(nameof(doorTransform));

            rotationAxis = serializedObject.FindProperty(nameof(rotationAxis));
            doorOpenRight = serializedObject.FindProperty(nameof(doorOpenRight));
            openAngle = serializedObject.FindProperty(nameof(openAngle));
            openSpeed = serializedObject.FindProperty(nameof(openSpeed));
            closeSpeed = serializedObject.FindProperty(nameof(closeSpeed));

            drawerOpenDirection = serializedObject.FindProperty(nameof(drawerOpenDirection));
            drawerOpenSpeed = serializedObject.FindProperty(nameof(drawerOpenSpeed));
            drawerCloseSpeed = serializedObject.FindProperty(nameof(drawerCloseSpeed));
            slideDistance = serializedObject.FindProperty(nameof(slideDistance));

            plankCount = serializedObject.FindProperty(nameof(plankCount));
            doorPlankedText = serializedObject.FindProperty(nameof(doorPlankedText));

            isLocked = serializedObject.FindProperty(nameof(isLocked));
            lockedDoorText = serializedObject.FindProperty(nameof(lockedDoorText));
            doorLockedSound = serializedObject.FindProperty(nameof(doorLockedSound));

            keyScriptable = serializedObject.FindProperty(nameof(keyScriptable));
            removeKeyAfterUse = serializedObject.FindProperty(nameof(removeKeyAfterUse));
            removedPrompt = serializedObject.FindProperty(nameof(removedPrompt));

            doorUnlockSound = serializedObject.FindProperty(nameof(doorUnlockSound));
            unlockedDoorText = serializedObject.FindProperty(nameof(unlockedDoorText));

            doorOpenSound = serializedObject.FindProperty(nameof(doorOpenSound));
            openDelay = serializedObject.FindProperty(nameof(openDelay));

            doorCloseSound = serializedObject.FindProperty(nameof(doorCloseSound));
            closeDelay = serializedObject.FindProperty(nameof(closeDelay));

            onDrawerOpen = serializedObject.FindProperty(nameof(onDrawerOpen));
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((DoorInteractable)target), typeof(DoorInteractable), false);
            GUI.enabled = true;

            EditorGUILayout.Space(5);

            SetInteractableType();

            EditorGUILayout.Space(5);

            SetDoorObject();

            EditorGUILayout.Space(5);

            DrawerSettings();

            EditorGUILayout.Space(5);

            OpeningSettings();

            EditorGUILayout.Space(5);

            SoundDelaySettings();

            EditorGUILayout.Space(5);

            PlankSettings();

            EditorGUILayout.Space(5);

            LockSettings();

            EditorGUILayout.Space(5);

            SpawningItems();

            EditorGUILayout.Space(5);

            OpenEditorScript();

            serializedObject.ApplyModifiedProperties();
        }

        void SetInteractableType()
        {
            EditorGUILayout.LabelField("Interactable Type", EditorStyles.toolbarTextField);
            EditorGUILayout.PropertyField(interactableType);
        }

        void SetDoorObject()
        {
            EditorGUILayout.LabelField("Door Object / Pivot Object", EditorStyles.toolbarTextField);
            EditorGUILayout.Space(5);
            EditorGUILayout.PropertyField(doorTransform);
        }

        void DrawerSettings()
        {
            if (interactableType.enumValueIndex == (int)DoorInteractable.InteractableType.Drawer)
            {
                EditorGUILayout.LabelField("Drawer Settings", EditorStyles.toolbarTextField);
                EditorGUILayout.PropertyField(drawerOpenDirection);
                EditorGUILayout.PropertyField(slideDistance);
                EditorGUILayout.PropertyField(drawerOpenSpeed);
                EditorGUILayout.PropertyField(drawerCloseSpeed);
            }
        }

        void OpeningSettings()
        {
            if (interactableType.enumValueIndex == (int)DoorInteractable.InteractableType.Door)
            {
                EditorGUILayout.LabelField("Door Opening Settings", EditorStyles.toolbarTextField);
                EditorGUILayout.PropertyField(rotationAxis);
                EditorGUILayout.PropertyField(openAngle);
                EditorGUILayout.PropertyField(openSpeed);
                EditorGUILayout.PropertyField(closeSpeed);

                EditorGUILayout.PropertyField(doorOpenRight);
            }
        }

        void SoundDelaySettings()
        {
            EditorGUILayout.LabelField("Sound Delay Settings", EditorStyles.toolbarTextField);
            EditorGUILayout.PropertyField(openDelay);
            EditorGUILayout.PropertyField(closeDelay);

            EditorGUILayout.Space(5);

            EditorGUILayout.PropertyField(doorOpenSound);
            EditorGUILayout.PropertyField(doorCloseSound);
        }

        void PlankSettings()
        {
            EditorGUILayout.LabelField("Plank Settings", EditorStyles.toolbarTextField);
            UsePlanks = EditorGUILayout.Toggle("Use Planks", UsePlanks);
            if (UsePlanks)
            {
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(plankCount);
                EditorGUILayout.PropertyField(doorPlankedText);
            }
        }

        void LockSettings()
        {
            EditorGUILayout.LabelField("Lock Settings", EditorStyles.toolbarTextField);
            EditorGUILayout.PropertyField(isLocked);
            if (isLocked.boolValue)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Locked Settings", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(lockedDoorText);
                EditorGUILayout.PropertyField(unlockedDoorText);
                EditorGUILayout.PropertyField(doorLockedSound);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Unlock Settings", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(removeKeyAfterUse);
                EditorGUILayout.PropertyField(keyScriptable);
                EditorGUILayout.PropertyField(removedPrompt);

                EditorGUILayout.Space(5);

                EditorGUILayout.PropertyField(doorUnlockSound);
            }
        }

        void SpawningItems()
        {
            EditorGUILayout.LabelField("Spawning Items", EditorStyles.toolbarTextField);
            SpawnItems = EditorGUILayout.Toggle("Spawn Items", SpawnItems);
            if (SpawnItems)
            {
                EditorGUILayout.PropertyField(onDrawerOpen);
            }
        }

        void OpenEditorScript()
        {
            if (GUILayout.Button("Open Editor Script"))
            {
                string scriptFilePath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
                AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<MonoScript>(scriptFilePath));
            }
        }
    }
}
