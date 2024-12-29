public class DisappearBehaviour : BehaviourState
{
    public override BehaviourState NextState()
    {
        return null;
    }

    protected virtual void Update()
    {
        gameObject.SetActive(false);
    }
}
