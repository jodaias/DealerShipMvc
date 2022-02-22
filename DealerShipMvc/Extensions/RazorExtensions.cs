using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerShipMvc.Extensions
{
    public static class RazorExtensions
    {
        //Validar algo na view
        public static bool IfClaim(this RazorPage page, string claimName, string claimValue)
        {
            return CustomAuthorization.ValidateClaimsUser(page.Context, claimName, claimValue);
        }

        //Desabilitar algum botão através de uma validação de claims
        public static string IfClaimShow(this RazorPage page, string claimName, string claimValue)
        {
            return CustomAuthorization.ValidateClaimsUser(page.Context, claimName, claimValue) ? "" : "disable";
        }

        //Mostrar ou não algum link
        public static IHtmlContent IfClaimShow(this IHtmlContent page, HttpContext context, string claimName, string claimValue)
        {
            return CustomAuthorization.ValidateClaimsUser(context, claimName, claimValue) ? page : null;
        }
    }
}
