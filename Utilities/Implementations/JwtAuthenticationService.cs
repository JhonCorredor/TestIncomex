﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Utilities.Interfaces;

namespace Utilities.Implementations
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        public readonly string _key;

        /// <summary>
        /// Inicializa una nueva instancia del servicio <see cref="JwtAuthenticationService"/>.
        /// </summary>
        public JwtAuthenticationService(string key)
        {
            this._key = key;
        }

        /// <summary>
        /// Autentica a un usuario generando un token JWT si el nombre de usuario y la contraseña son válidos.
        /// </summary>
        public string Authenticate(string user, string password)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(this._key);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Encripta una contraseña utilizando el algoritmo de hashing MD5.
        /// </summary>
        public string EncryptMD5(string password)
        {
            using (var md5Hash = MD5.Create())
            {
                // Byte array representation of source string
                var sourceBytes = Encoding.UTF8.GetBytes(password);

                // Generate hash value(Byte Array) for input data
                var hashBytes = md5Hash.ComputeHash(sourceBytes);

                // Convert hash byte array to string
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
                return hash;
            }
        }
    }
}
