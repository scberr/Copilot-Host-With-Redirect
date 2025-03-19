using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;

namespace BotHostingWebsite.Pages
{
    [AuthorizeForScopes(Scopes = new string[] { "api://ac889341-279e-4cb6-a3b0-308332866925/SampleBot.Use" })]
    public class WebChatModel : PageModel
    {

        public string DisplayName { get; set; } = string.Empty;

        private readonly ITokenAcquisition tokenAcquisitionService;
        private readonly GraphServiceClient graphServiceClient;

        public WebChatModel(ILogger<IndexModel> logger, ITokenAcquisition tokenAcquisitionService, GraphServiceClient graphServiceClient)
        {
            this.tokenAcquisitionService = tokenAcquisitionService;
            this.graphServiceClient = graphServiceClient;
        }


        public void OnGet()
        {
            this.DisplayName = this.graphServiceClient.Me.Request().GetAsync().Result.DisplayName;
            //user.DisplayName;

        }
        public async Task<JsonResult> OnGetBotTokenAsync()
        {
            return new JsonResult(await tokenAcquisitionService.GetAccessTokenForUserAsync(new string[] { "api://ac889341-279e-4cb6-a3b0-308332866925/SampleBot.Use" }));
        }

    }
}
