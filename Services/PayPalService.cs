using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

namespace MUSEODESCALZOS.Services
{
    public class PayPalService
    {
        private readonly PayPalEnvironment _environment;
        private readonly PayPalHttpClient _paypalClient; // Cambiado a PayPalHttpClient

        public PayPalService(IConfiguration configuration)
        {
            var clientId = configuration["PayPal:ClientId"];
            var clientSecret = configuration["PayPal:ClientSecret"];

            _environment = new SandboxEnvironment(clientId, clientSecret); // Cambia a `LiveEnvironment` en producción
            _paypalClient = new PayPalHttpClient(_environment); // Cambiado a PayPalHttpClient
        }

        public async Task<Order> CreatePayment(decimal total, string currency, string returnUrl, string cancelUrl)
        {
            var orderRequest = new OrderRequest
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
                {
                    new PurchaseUnitRequest
                    {
                        AmountWithBreakdown = new AmountWithBreakdown
                        {
                            CurrencyCode = currency,
                            Value = total.ToString("F2")
                        }
                    }
                },
                ApplicationContext = new ApplicationContext
                {
                    ReturnUrl = returnUrl,
                    CancelUrl = cancelUrl
                }
            };

            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(orderRequest);

            var response = await _paypalClient.Execute(request); // Usar _paypalClient aquí
            var result = response.Result<Order>();

            return result;
        }
    }
}