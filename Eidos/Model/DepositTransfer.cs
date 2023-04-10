
namespace TheBank2.Model
{
    internal static class DepositTransfer
    {
        public static bool CheckDepositForMoneyAmount(double MoneyOnSourceDeposit, double RequiredMoney)
        {
            return RequiredMoney <= MoneyOnSourceDeposit;
        }
    }
}
