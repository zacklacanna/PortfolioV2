using ZackV2.Classes;
namespace ZackV2.Services;

public class ExperienceModalService
{
    public event Action OnChange;
    public ExperienceClass.ExpModel Current { get; private set; }

    public void Show(ExperienceClass.ExpModel e)
    {
        Current = e;
        Notify();
    }

    public void Close()
    {
        Current = null;
        Notify();
    }

    private void Notify() => OnChange?.Invoke();
}