public interface IInteractable
{
    public void Interact();
}

public interface IHitablea
{
    public void Damage(float damage);
    public void Slow(float time, float slow);
}

public interface IUIHitablea
{
    public void Dead();
}