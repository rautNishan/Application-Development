using System.Diagnostics;
using FinalCoffee1.common.helperClass;

namespace FinalCoffee1.common.helperServices;
public class SessionService
{

    private bool adminRegistered;

    private bool? needAuthorized;
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
    public bool setNeedAuthorized(bool set)
    {
        this.needAuthorized = set;
        OnChange?.Invoke();
        return (bool)this.needAuthorized;
    }
    public bool isNeedAuthorized()
    {
        return (bool)this.needAuthorized;
    }
    public bool defaultNeedAuthorized()
    {
        if (this.needAuthorized.HasValue)
        {
            return (bool)this.needAuthorized;
        }
        else
        {
            this.needAuthorized = true;
            return (bool)this.needAuthorized;
        }
    }
}