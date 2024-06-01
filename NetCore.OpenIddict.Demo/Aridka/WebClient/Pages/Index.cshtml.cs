using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenIddict.Client;

namespace WebClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly OpenIddictClientService _service;

        public IndexModel(ILogger<IndexModel> logger, OpenIddictClientService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task OnGet()
        {
            //retrieve an access token from the remote server
            //var result = await _service.AuthenticateWithClientCredentialsAsync(new());
            //var token = result.AccessToken;
        }
    }
}
