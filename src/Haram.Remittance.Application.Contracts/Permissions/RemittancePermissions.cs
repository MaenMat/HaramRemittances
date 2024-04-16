namespace Haram.Remittance.Permissions;

public static class RemittancePermissions
{
    public const string GroupName = "Remittance";

    public static class Currencies
    {
        public const string Default = GroupName + ".Currencies";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Customers
    {
        public const string Default = GroupName + ".Customers";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Remittances
    {
        public const string Default = GroupName + ".Remittances";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string SetAsReady = Default + ".SetAsReady";
        public const string Approve = Default + ".Approve";
        public const string Release = Default + ".Release";
    }
}
