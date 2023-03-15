public interface State
{
    public void OnEnter();
    public void Tick();
    public void OnExit();
}