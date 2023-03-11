using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Triggers
{
    public class PosterTrigger : InteractTrigger
    {
        [SerializeField] private Image container;
        [SerializeField] private Sprite image;

        protected override void InteractOnPerformed(InputAction.CallbackContext obj)
        {
            base.InteractOnPerformed(obj);
            container.sprite = container.sprite == image ? null : image;
        }

        protected override void OnPlayerExit()
        {
            base.OnPlayerExit();
            container.sprite = null;
        }
    }
}
