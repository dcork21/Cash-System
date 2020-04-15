using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.Business
{
    public interface IIdentityMgt
    {
        bool CreateIdentity();
        Identity GetIdentity();
        bool UpdateIdentity();
        bool DeleteIdentity();
    }
}