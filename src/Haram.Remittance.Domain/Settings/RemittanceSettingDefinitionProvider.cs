using Volo.Abp.Settings;

namespace Haram.Remittance.Settings;

public class RemittanceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(RemittanceSettings.MySetting1));
    }
}
