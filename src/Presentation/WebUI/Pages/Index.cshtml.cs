using Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService productService1Service;
        private readonly IUserService userService;

        public IndexModel(IProductService productService1Service, IUserService userService)
        {
            this.productService1Service = productService1Service;
            this.userService = userService;
        }

        public void OnGet()
        {

        }
    }
}