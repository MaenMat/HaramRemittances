using Haram.Remittance.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Haram.Remittance.Permissions;

public class RemittancePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(RemittancePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(RemittancePermissions.MyPermission1, L("Permission:MyPermission1"));

        var CustomersPermission = myGroup.AddPermission(
            RemittancePermissions.Customers.Default, L("Permission:Customer"));
        CustomersPermission.AddChild(
            RemittancePermissions.Customers.Create, L("Permission:Customer.Create"));
        CustomersPermission.AddChild(
            RemittancePermissions.Customers.Edit, L("Permission:Customer.Edit"));
        CustomersPermission.AddChild(
            RemittancePermissions.Customers.Delete, L("Permission:Customer.Delete"));

        var CurrenciesPermission = myGroup.AddPermission(
           RemittancePermissions.Currencies.Default, L("Permission:Currency"));
        CurrenciesPermission.AddChild(
            RemittancePermissions.Currencies.Create, L("Permission:Currency.Create"));
        CurrenciesPermission.AddChild(
            RemittancePermissions.Currencies.Edit, L("Permission:Currency.Edit"));
        CurrenciesPermission.AddChild(
            RemittancePermissions.Currencies.Delete, L("Permission:Currency.Delete"));

        var RemittancesPermission = myGroup.AddPermission(
            RemittancePermissions.Remittances.Default, L("Permission:Remittance"));
        RemittancesPermission.AddChild(
            RemittancePermissions.Remittances.Create, L("Permission:Remittance.Create"));
        RemittancesPermission.AddChild(
            RemittancePermissions.Remittances.Edit, L("Permission:Remittance.Edit"));
        RemittancesPermission.AddChild(
            RemittancePermissions.Remittances.Delete, L("Permission:Remittance.Delete"));
        RemittancesPermission.AddChild(
           RemittancePermissions.Remittances.SetAsReady, L("Permission:Remittance.SetAsReady"));
        RemittancesPermission.AddChild(
           RemittancePermissions.Remittances.Approve, L("Permission:Remittance.Approve"));
        RemittancesPermission.AddChild(
            RemittancePermissions.Remittances.Release, L("Permission:Remittance.Release"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<RemittanceResource>(name);
    }
}
