using UnityEngine;

namespace DoorInteractionKit
{
    [CreateAssetMenu(menuName = "Key")]
    public class Key : ScriptableObject
    {
        [SerializeField] private string keyName = "Key";
        [SerializeField] private Sprite keySprite = null;

        public Sprite _KeySprite
        {
            get { return keySprite; }
        }

        // Property to expose the key name
        public string _KeyName
        {
            get { return keyName; }
        }
    }
}

