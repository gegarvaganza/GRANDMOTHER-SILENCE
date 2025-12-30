using System.Collections.Generic;
using UnityEngine;

namespace DoorInteractionKit
{
    public class DoorInventory : MonoBehaviour
    {
        [Header("Inventory Keycode")]
        [SerializeField] private KeyCode inventoryKey;

        [Header("Inventory Keylist")]
        public List<Key> _keyList = new List<Key>();

        private string collectPrompt = " Collected";

        public static DoorInventory instance;

        void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }

        private void Update()
        {
            if (Input.GetKeyDown(inventoryKey))
            {
                DoorUIManager.instance.OpenInventory();
            }
        }

        public void AddKey(Key key)
        {
            if (!_keyList.Contains(key))
            {
                _keyList.Add(key);
                DoorUIManager.instance.AddInventorySlot(key);
                DoorUIManager.instance.ShowNotification(key._KeyName + collectPrompt);
            }
        }

        public void RemoveKey(Key key)
        {
            if (_keyList.Contains(key))
            {
                _keyList.Remove(key);
                DoorUIManager.instance.RemoveInventorySlot(key);
            }
        }
    }
}