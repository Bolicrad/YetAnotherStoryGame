using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Triggers
{
    public class InteractTrigger : TriggerBase
    {
        // Start is called before the first frame update
        [SerializeField]private RectTransform hintUI;
        private PlayerController _playerController;
        
        protected void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _playerController = other.GetComponentInParent<PlayerController>();
            hintUI.gameObject.SetActive(true);
            _playerController.Interact.performed += InteractOnPerformed;
        }

        protected void OnTriggerExit(Collider other)
        {
            StopAllCoroutines();
            hintUI.gameObject.SetActive(false);
            _playerController.Interact.performed -= InteractOnPerformed;
            OnPlayerExit();
        }

        protected virtual void InteractOnPerformed(InputAction.CallbackContext obj)
        {
            _playerController.isInteracting = !_playerController.isInteracting;
            Debug.Log($"Player triggered interaction inside {GetType()} {name}");
        }

        protected virtual void OnPlayerExit()
        {
            _playerController.isInteracting = false;
            _playerController = null;
            Debug.Log($"Player exited the region of {GetType()} {name}");
        }
    }
}
