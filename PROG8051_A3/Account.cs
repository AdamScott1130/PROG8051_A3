using PROG8051_A3_ITradable;
using PROG8051_A3_User;
namespace PROG8051_A3_Account
{
    public abstract class Account : ITradable
    {
        // Attributes
        private List<User> owners;
        bool isShared;
        private uint id;
        // Constructors
        public Account(List<User> ownersProvided, uint idProvided)
        {
            this.owners = ownersProvided;
            this.isShared = owners.Count > 1;
            this.id = idProvided;
        }
        // Properties
        public List<User> Owners { get { return this.owners; } }
        public string IsShared { get { return isShared ? "This is a shared account." : "This is not a shared account"; } }
        public uint Id { get { return this.id; } }

        // Methods

        public abstract void Buy();
        public abstract void Sell();
        public override abstract string ToString();
    }
}
