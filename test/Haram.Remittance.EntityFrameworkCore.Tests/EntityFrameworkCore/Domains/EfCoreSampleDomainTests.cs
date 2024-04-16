using Haram.Remittance.Samples;
using Xunit;

namespace Haram.Remittance.EntityFrameworkCore.Domains;

[Collection(RemittanceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<RemittanceEntityFrameworkCoreTestModule>
{

}
