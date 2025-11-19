using PROG8051_A3_Account;
namespace PROG8051_A3_IConnection
{
    public interface IConnection
    {
        // Attributes

        // Constructors

        // Properties
        bool ConnectUser();
        void DisconnectUser();
        Account AccountAccess();
        // Methods


    }
}
