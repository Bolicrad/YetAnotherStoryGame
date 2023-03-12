using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using DialogueSystemTrigger = PixelCrushers.DialogueSystem.Wrappers.DialogueSystemTrigger;

namespace Triggers
{
    public class DialogueTrigger : InteractTrigger
    {
        [SerializeField] private DialogueSystemTrigger systemTrigger;

        protected override void InteractOnPerformed(InputAction.CallbackContext obj)
        {
            base.InteractOnPerformed(obj);
            if (PlayerController.isInteracting)
            {
                DialogueManager.instance.conversationEnded += OnConversationEnd;
                systemTrigger.TryStart(null);
            }
            else
            {
                Debug.Log($"Conversation with {name} was Stopped by user Input");
                DialogueManager.StopConversation();
            }
        }

        private void OnConversationEnd(Transform player)
        {
            Debug.Log($"Conversation with {name} Ended");
            PlayerController.isInteracting = false;
            SwitchHintUI(false);
            DialogueManager.instance.conversationEnded -= OnConversationEnd;
        }
    }
}
