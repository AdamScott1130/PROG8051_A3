using PROG8051_A3_Account;
namespace PROG8051_A3_IConnection
{
    public interface IConnection
    {
        // Methods
        bool ConnectUser(string passwordProvided);
        void DisconnectUser();
        Account AccountAccess(uint idProvided);
    }
}
