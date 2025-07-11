namespace ZackV2.Services;

public class TabService
{
    public string CurrentTab { get; private set; } = "about";
    
    public event Action<string>? OnTabChanged;

    public void SetTab(string tabId)
    {
        if (tabId == CurrentTab) return;
        CurrentTab = tabId;
        OnTabChanged?.Invoke(tabId);
    }
}