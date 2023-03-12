public interface IState
{
    void Walk();
    void Run();
    void Stop();
    
    void OnEnter(IState previous);
    void OnExit();
    void Update();
}
