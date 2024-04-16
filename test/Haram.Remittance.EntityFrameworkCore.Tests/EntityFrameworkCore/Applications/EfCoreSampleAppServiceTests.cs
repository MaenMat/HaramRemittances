using Haram.Remittance.Samples;
using Xunit;

namespace Haram.Remittance.EntityFrameworkCore.Applications;

[Collection(RemittanceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<RemittanceEntityFrameworkCoreTestModule>
{

}
