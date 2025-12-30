using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace DoorInteractionKit
{
    public class DoorUIManager : MonoBehaviour
    {
        [Header("Object Interaction Prompt UI")]
        [SerializeField] private GameObject interactTextBG;
        [SerializeField] private GameObject interactionUI;
        [SerializeField] private GameObject iconImage;
        [SerializeField] private GameObject keyPromptUI;

        [Header("Notification UI")]
        [SerializeField] private TMP_Text notificationTextUI;
        [SerializeField] private GameObject notificationUIBG;

        [Header("Notification UI - Text Customisation")]
        [SerializeField] private int TextSize = 36;
        [SerializeField] private TMP_FontAsset FontType = null;
        [SerializeField] private FontStyles FontStyle = FontStyles.Bold;
        [SerializeField] private Color FontColor = Color.white;

        [Header("Inventory Fields")]
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private CanvasGroup inventoryPanel;
        [SerializeField] private float inventoryFadeDuration = 1.0f;

        [Header("Timer")]
        [SerializeField] private float onScreenTimer = 2f;
        [SerializeField] private float fadeInDuration = 1f;
        [SerializeField] private float fadeOutDuration = 1f;

        [Header("Crosshair")]
        [SerializeField] private Image crosshairUI = null;

        private Dictionary<Key, GameObject> inventorySlots = new Dictionary<Key, GameObject>();
        private CanvasGroup notificationUICanvasGroup;
        private CanvasGroup interactNameUICanvasGroup;
        private TMP_Text interactTextUI;
        private TMP_Text keyPromptTextUI;
        private bool isFading = false;
        private bool isInventoryOpen = false;
        private List<GameObject> slotPool = new List<GameObject>();

        private bool isDisplayingNotification = false;
        private Queue<string> notificationQueue = new Queue<string>();

        public static DoorUIManager instance;

        void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }

            notificationUICanvasGroup = notificationUIBG.GetComponent<CanvasGroup>();
            interactNameUICanvasGroup = interactTextBG.GetComponent<CanvasGroup>();
            interactTextUI = interactionUI.GetComponent<TMP_Text>();
            keyPromptTextUI = keyPromptUI.GetComponent<TMP_Text>();
            notificationUICanvasGroup.alpha = 0;
            interactNameUICanvasGroup.alpha = 0;

            SetTextSettings();
        }

        void SetTextSettings()
        {
            notificationTextUI.fontSize = TextSize;
            notificationTextUI.font = FontType;
            notificationTextUI.fontStyle = FontStyle;
            notificationTextUI.color = FontColor;
        }

        public void ShowNotification(string notificationString)
        {
            notificationQueue.Enqueue(notificationString);
            if (!isDisplayingNotification)
            {
                StartCoroutine(DisplayNotification());
            }
        }

        private IEnumerator DisplayNotification()
        {
            isDisplayingNotification = true;
            while (notificationQueue.Count > 0)
            {
                string message = notificationQueue.Dequeue();
                notificationTextUI.text = message;

                yield return StartCoroutine(FadeCanvasGroup(notificationUICanvasGroup, true, fadeInDuration));
                yield return new WaitForSeconds(onScreenTimer);
                yield return StartCoroutine(FadeCanvasGroup(notificationUICanvasGroup, false, fadeOutDuration));
            }
            isDisplayingNotification = false;
        }

        public void ShowName(bool show, bool showIcon, bool showKeyPrompt, bool showInteractionName, string keyPromptText, string objectName = "")
        {
            interactNameUICanvasGroup.alpha = show ? 1 : 0;
            interactionUI.SetActive(showInteractionName ? true : false); 
            interactTextUI.text = show ? objectName : "";
            keyPromptUI.SetActive(showKeyPrompt ? true : false);
            keyPromptTextUI.text = keyPromptText;
            iconImage.SetActive(showIcon ? true : false);
        }

        public IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, bool fadeIn, float duration)
        {
            float targetAlpha = fadeIn ? 1f : 0f;
            float initialAlpha = canvasGroup.alpha;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(initialAlpha, targetAlpha, elapsedTime / duration);
                yield return null;
            }

            canvasGroup.alpha = targetAlpha;
            canvasGroup.interactable = fadeIn;
            canvasGroup.blocksRaycasts = fadeIn;
        }

        private GameObject GetSlotFromPool()
        {
            foreach (var slot in slotPool)
            {
                if (!slot.activeInHierarchy)
                {
                    slot.SetActive(true);
                    return slot;
                }
            }

            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel.transform);
            slotPool.Add(newSlot);
            return newSlot;
        }

        public void AddInventorySlot(Key key)
        {
            // Create new slot
            GameObject slot = GetSlotFromPool();
            InventorySlot slotScript = slot.GetComponent<InventorySlot>();

            // Set key sprite and name on the new slot
            slotScript.SetSlot(key._KeySprite, key._KeyName);

            // Store the new slot in the dictionary
            inventorySlots[key] = slot;
        }

        public void RemoveInventorySlot(Key key)
        {
            if (inventorySlots.TryGetValue(key, out GameObject slot))
            {
                slot.SetActive(false);
                inventorySlots.Remove(key);
            }
        }

        public void OpenInventory()
        {
            if (inventoryPanel != null && !isFading)
            {
                bool shouldFadeIn = !isInventoryOpen; // Determine whether to fade in or out
                StartCoroutine(FadeCanvasGroup(inventoryPanel, shouldFadeIn, inventoryFadeDuration));
                isInventoryOpen = !isInventoryOpen; // Toggle the state after initiating the fade
            }
        }

        public void HighlightCrosshair(bool on)
        {
            crosshairUI.color = on ? Color.red : Color.white;
        }
    }
}