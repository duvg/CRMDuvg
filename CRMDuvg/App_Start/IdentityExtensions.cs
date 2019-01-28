using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace App.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetNombre( this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Nombre");
            return (claim != null) ? claim.Value : string.Empty;  
        }

        public static string GetApellidos( this IIdentity identity )
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Apellidos");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}