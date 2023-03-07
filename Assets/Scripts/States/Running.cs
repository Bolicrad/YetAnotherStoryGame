namespace States
{
    public class Running : Walking
    {
        protected override void AddAnim()
        {
            Controller.spineComponent.AnimationState.AddAnimation(0, "run", true, 0f);
        }

        protected override void SetAnim()
        {
            Controller.spineComponent.AnimationState.SetAnimation(0, "run", true);
        }
        
        public Running(PlayerController controller) : base(controller) { }

        public override void Update()
        {
            HorizontalMove(Controller.speed * Controller.runRatio);
        }
        
    }
}
