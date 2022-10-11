public interface ObjectState
{
    public ObjectState GetNextState();
    public void RestoreDefaults();
    public void StateUpdate();
    public void StateLateUpdate();
}
