using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaxationLib;

namespace TaxationUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _url = "https://localhost:7290/api";

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        [Required(ErrorMessage = "Postal Code is required.")]
        public string postalCode { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Annual Income is required.")]
        public decimal annualIncome { get; set; }
        public decimal? calculatedTax { get; set; }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                calculatedTax = await GetTaxCalculationAsync();
            }

        }

        private async Task<decimal?> GetTaxCalculationAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = $"{_url}/TaxCalculation";

            var request = new TaxCalculationRequest
            {
                PostalCode = postalCode,
                AnnualIncome = annualIncome
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<decimal>();
                return result;
            }
            else
            { 
                _logger.LogError($"Error calling API: {response.StatusCode}");
                return null;
            }
        }
    }
}
