namespace Haram.Remittance;

public static class RemittanceDomainErrorCodes
{
        public const string CustomerAlreadyExists = "Remittance:00001";
        public const string CurrencyAlreadyExists = "Remittance:00002";
        public const string CustomerUnderAge = "Remittance:00003";
        public const string InternalRemittanceError = "Remittance:00004";
        public const string ExternalRemittanceError = "Remittance:00005";
        public const string OnRemittanceUpdateException = "Remittance:00006";
        public const string OnCurrencyDeletionException = "Remittance:00007";
        public const string OnCustomerDeletionException = "Remittance:00008";
        public const string SetAsReadyException = "Remittance:00009";
        public const string OnRemittanceDeletionException = "Remittance:00010";
}
