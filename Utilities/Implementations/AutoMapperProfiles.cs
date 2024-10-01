using AutoMapper;
using Utilities.Interfaces;

namespace Utilities.Implementations
{
    public class AutoMapperProfiles : Profile
    {
        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="AutoMapperProfiles"/> con el servicio de autenticación JWT.
        /// </summary>
        public AutoMapperProfiles(IJwtAuthenticationService jwtAuthenticationService) : base()
        {
            _jwtAuthenticationService = jwtAuthenticationService;

            //Parameter

        }
    }
}
