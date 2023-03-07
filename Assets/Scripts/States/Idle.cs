using UnityEngine;

namespace States
{
    public class Idle : IState
    {
        private protected readonly PlayerController Controller;
        private protected IState Previous;

        public Idle(PlayerController controller)
        {
            Controller = controller;
        }

        public virtual void Walk()
        {
            //Change State to Walking
            Controller.State = Controller.Walking;
        }

        public virtual void Run()
        {
            //Change State to Running
            Controller.State = Controller.Running;
        }

        public virtual void Stop()
        {
            //Keep State as Idle (Do nothing)
            Controller.State = Controller.Idle;
        }

        public virtual void OnEnter(IState previous)
        {
            //On Enter, Play Animation
            Previous = previous;
            var trackEntry = Controller.spineComponent.AnimationState.GetCurrent(0);
            if (trackEntry is { Loop: true }) SetAnim();
            else AddAnim();
        }
        
        protected virtual void AddAnim()
        {
            Controller.spineComponent.AnimationState.AddAnimation(0, "Idle_1", true,0f);
        }

        protected virtual void SetAnim()
        {
            Controller.spineComponent.AnimationState.SetAnimation(0, "Idle_1", true);
        }

        public virtual void OnExit()
        {
            //On Exit, Stop Play Animation (if needed)
        }

        public virtual void Update()
        {
            //Update
            if (Controller.rigidBody.velocity.z != 0)
            {
                Controller.rigidBody.velocity = Vector3.zero;
            }
        }
    }
}
