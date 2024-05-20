using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports.PaymentType;

namespace CashFlow.Domain.Extensions;

public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ResourcePaymentTypeConvert.CASH,
            PaymentType.CreditCard => ResourcePaymentTypeConvert.CREDIT_CARD,
            PaymentType.DebitCard => ResourcePaymentTypeConvert.DEBIT_CARD,
            PaymentType.EletronicTransfer => ResourcePaymentTypeConvert.ELETRONIC_TRANSFER,
            _ => string.Empty
        };
    }
}
