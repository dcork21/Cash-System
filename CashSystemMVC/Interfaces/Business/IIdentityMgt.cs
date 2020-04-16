using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.Business
{
    public interface IIdentityMgt
    {
        Identity CreateIdentity();
        Identity GetIdentity();
        Identity UpdateIdentity();
        bool DeleteIdentity();
    }
}