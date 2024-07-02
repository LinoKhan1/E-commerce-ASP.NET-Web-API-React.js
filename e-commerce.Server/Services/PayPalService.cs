using e_commerce.Server.Config;
using e_commerce.Server.Models;
using e_commerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using PayPal.Api;

namespace e_commerce.Server.Services
{
    public class PayPalService : IPaymentService
    {
        private readonly PayPalSettings _payPalSettings;

        public PayPalService(IOptions<PayPalSettings> payPalSettings)
        {
            _payPalSettings = payPalSettings.Value;
        }

        public async Task<string> CreatePayment(e_commerce.Server.Models.Order order)
        {
            try
            {
                var apiContext = new APIContext(new OAuthTokenCredential(_payPalSettings.ClientId, _payPalSettings.ClientSecret).GetAccessToken())
                {
                    Config = new Dictionary<string, string>
            {
                { "mode", _payPalSettings.Mode }
            }
                };

                var itemList = new ItemList()
                {
                    items = order.OrderItems.Select(oi => new Item()
                    {
                        name = oi.Product.Name,
                        currency = oi.Price.ToString("F2"),
                        price = oi.Quantity.ToString(),
                        sku = oi.ProductId.ToString()
                    }).ToList()
                };

                var payer = new Payer() { payment_method = "paypal" };

                var redirectUrls = new RedirectUrls()
                {
                    cancel_url = "https://localhost:7077/cancel",
                    return_url = "https://localhost:7077/success"
                };

                var details = new Details()
                {
                    tax = "0",
                    shipping = "0",
                    subtotal = order.TotalAmount.ToString("F2")
                };

                var amount = new Amount()
                {
                    currency = "USD",
                    total = order.TotalAmount.ToString("F2"),
                    details = details
                };

                var transactionList = new List<Transaction>
        {
            new Transaction
            {
                description = "Your order description",
                invoice_number = Guid.NewGuid().ToString(),
                amount = amount,
                item_list = itemList
            }
        };

                var payment = new Payment()
                {
                    intent = "sale",
                    payer = payer,
                    transactions = transactionList,
                    redirect_urls = redirectUrls
                };

                var createdPayment = await Task.Run(() => payment.Create(apiContext));

                var approvalUrl = createdPayment.links.FirstOrDefault(lnk => lnk.rel == "approval_url")?.href;

                if (approvalUrl == null)
                {
                    // Handle case where approval URL is not returned
                    throw new Exception("Approval URL not found in PayPal response.");
                }

                return approvalUrl;
            }
            catch (PayPal.HttpException ex)
            {
                // Handle PayPal API errors
                throw new Exception("PayPal API error: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("Payment creation failed: " + ex.Message, ex);
            }
        }



    }
}
