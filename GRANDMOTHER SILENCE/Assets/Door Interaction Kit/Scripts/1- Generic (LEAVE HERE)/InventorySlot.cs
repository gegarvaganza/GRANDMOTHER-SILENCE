using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DoorInteractionKit
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image KeyImage;
        [SerializeField] private TMP_Text KeyNameText;

        public Sprite SlotSprite
        {
            set { KeyImage.sprite = value; }
        }

        public string SlotText
        {
            set { KeyNameText.text = value; }
        }

        public void SetSlot(Sprite sprite, string name)
        {
            SlotSprite = sprite;
            SlotText = name;
        }
    }
}

