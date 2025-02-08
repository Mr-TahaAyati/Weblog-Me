using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Weblog.Areas.Admin
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class AdminControllerBase : Controller
    {
    }
}
