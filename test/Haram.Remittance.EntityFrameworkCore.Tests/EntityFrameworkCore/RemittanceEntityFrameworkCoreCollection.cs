using Xunit;

namespace Haram.Remittance.EntityFrameworkCore;

[CollectionDefinition(RemittanceTestConsts.CollectionDefinitionName)]
public class RemittanceEntityFrameworkCoreCollection : ICollectionFixture<RemittanceEntityFrameworkCoreFixture>
{

}
