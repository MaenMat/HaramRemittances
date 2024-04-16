using Haram.Remittance.Localization;
using Haram.Remittance.MultiTenancy;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace Haram.Remittance.Blazor.Menus;

public class RemittanceMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public RemittanceMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<RemittanceResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                RemittanceMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home"
            )
        );
     
        var administration = context.Menu.GetAdministration();

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        context.Menu.AddItem(
        new ApplicationMenuItem(
        "Remittances",
        l["Menu:Remittances"],
        icon: "fa fa-credit-card"
        ).AddItem(
        new ApplicationMenuItem(
            "Draft Remittances",
            l["Menu:Remittances.Draft"],
            url: "/remittances/drafts")
        ).AddItem(
        new ApplicationMenuItem(
            "Ready Remittances",
            l["Menu:Remittances.Ready"],
            url: "/remittances/ready")
        ).AddItem(
        new ApplicationMenuItem(
            "Approved Remittances",
            l["Menu:Remittances.Approved/Released"],
            url: "/remittances/approved")
        ));
        

        context.Menu.AddItem(
        new ApplicationMenuItem(
        "Customers",
        l["Menu:Customers"],
        icon: "fa fa-users",
        url: "/customers"
        ));

       context.Menu.AddItem(
       new ApplicationMenuItem(
       "Currencies",
       l["Menu:Currencies"],
       icon: "fa fa-usd",
       url: "/currencies"
       ));

        context.Menu.AddItem(
        new ApplicationMenuItem(
        "Report",
        l["Menu:Report"],
        icon: "fa fa-clipboard",
        url: "/remittances/report"
        ));

        context.Menu.AddItem(
        new ApplicationMenuItem(
        "Report",
        l["Menu:Search"],
        icon: "fa fa-search",
        url: "/remittances/search"
        ));

        return Task.CompletedTask;
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{authServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null).RequireAuthenticated());

        return Task.CompletedTask;
    }
}
