using UnityEngine;
namespace States
{
    public class Walking : Idle
    {
        public Walking(PlayerController controller) : base(controller) { }
        
        protected override void AddAnim()
        {
            Controller.spineComponent.AnimationState.AddAnimation(0, "walk", true, 0f);
        }

        protected override void SetAnim()
        {
            Controller.spineComponent.AnimationState.SetAnimation(0, "walk", true);
        }
        public override void Update()
        {
            HorizontalMove(Controller.speed);
        }
        
        protected void HorizontalMove(float speed)
        {
            var dir = Mathf.Sign(Controller.Move.ReadValue<float>());
            var velocityZ = dir * speed;
            Controller.rigidBody.velocity = Vector3.forward * velocityZ;

            var localScale = Controller.spineComponent.transform.localScale;
            if (localScale.x * dir > 0)
            {
                localScale = new Vector3(-Mathf.Abs(localScale.x) * dir, localScale.y, localScale.z);
                Controller.spineComponent.transform.localScale = localScale;
                SetAnim();
            }
        }
    }
}
