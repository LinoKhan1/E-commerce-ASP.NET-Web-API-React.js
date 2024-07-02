using e_commerce.Server.Models;

namespace e_commerce.Server.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePayment(Order order);
    }

}
