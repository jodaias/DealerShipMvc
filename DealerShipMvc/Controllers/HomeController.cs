using DealerShipMvc.Extensions;
using DealerShipMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DealerShipMvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [ClaimsAuthorize("Home", "CanReady")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var errorModel = new ErrorViewModel();

            if (id == 500)
            {
                errorModel.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                errorModel.Title = "Ocorreu um erro!";
                errorModel.ErrorCode = id;
            }
            else if (id == 404)
            {
                errorModel.Message = "A página que está procurando não existe! <br /> Em Caso de dúvidas entre em contato com o nosso suporte.";
                errorModel.Title = "Ops! Página não encontrada.";
                errorModel.ErrorCode = id;
            }
            else if (id == 403) 
            {
                errorModel.Message = "Você não tem permissão para fazer isso";
                errorModel.Title = "Acesso Negado";
                errorModel.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", errorModel);
        }
    }
}
