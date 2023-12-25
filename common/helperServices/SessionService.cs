using FinalCoffee1.common.helperClass;

namespace FinalCoffee1.common.helperServices;
public class SessionService
{

    private bool adminRegistered;
    public event Action OnChange;
    public void NotifyStateChanged() => OnChange?.Invoke();
    private bool currentUser;

    public void SetCurrentUser(bool set)
    {
        this.currentUser = set;
        OnChange?.Invoke();
    }
    public bool IsUserLoggedIn()
    {
        return this.currentUser;
    }
    public bool setAdminRegistered()
    {
        OnChange?.Invoke();
        return true;
    }
    public bool isAdminRegistered()
    {
        var path = new FileManagement().DirectoryPath("database", "admin.json");
        if (File.Exists(path))
        {
            return true;
        }
        return false;
    }
}