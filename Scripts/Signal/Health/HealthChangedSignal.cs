public class HealthChangedSignal
{
    public IHasHealthHandler Owner;
    public float HealthMax;
    public float Health;
    public float HealthLast;
    public float HealthChangedTo => Health - HealthLast;
}
