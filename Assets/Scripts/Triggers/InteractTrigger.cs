using UnityEngine;
using UnityEngine.InputSystem;

namespace Triggers
{
    public class InteractTrigger : TriggerBase
    {
        // Start is called before the first frame update
        [SerializeField]private RectTransform hintUI;
        [SerializeField]private RectTransform hintUI_Cancel;
        protected PlayerController PlayerController;
        protected void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            PlayerController = other.GetComponentInParent<PlayerController>();
            hintUI.gameObject.SetActive(true);
            PlayerController.Interact.performed += InteractOnPerformed;
        }

        protected void OnTriggerExit(Collider other)
        {
            StopAllCoroutines();
            hintUI.gameObject.SetActive(false);
            if(hintUI_Cancel)hintUI_Cancel.gameObject.SetActive(false);
            PlayerController.Interact.performed -= InteractOnPerformed;
            OnPlayerExit();
        }

        protected virtual void InteractOnPerformed(InputAction.CallbackContext obj)
        {
            Debug.Log($"Player triggered interaction inside {GetType()} {name}");
            PlayerController.isInteracting = !PlayerController.isInteracting;
            SwitchHintUI(PlayerController.isInteracting);
        }

        protected virtual void OnPlayerExit()
        {
            PlayerController.isInteracting = false;
            PlayerController = null;
            Debug.Log($"Player exited the region of {GetType()} {name}");
        }

        protected void SwitchHintUI(bool isCancel)
        {
            hintUI.gameObject.SetActive(!isCancel);
            if(hintUI_Cancel)hintUI_Cancel.gameObject.SetActive(isCancel);
        }
    }
}
