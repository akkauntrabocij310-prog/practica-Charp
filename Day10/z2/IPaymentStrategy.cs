public interface IPaymentStrategy
{
    void Pay(decimal amount);
    string GetPaymentMethodName();
    bool ValidatePaymentDetails();
}