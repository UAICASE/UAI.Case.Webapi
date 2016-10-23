using Newtonsoft.Json;
using System;
using UAI.Case.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UAI.Case.Domain.Common;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace UAI.Case.Security
{
    public static class TokenHandler

    {
        public const string JWT_TOKEN_ISSUER = "http://case.uai.edu.ar";
        public const string JWT_TOKEN_AUDIENCE = "http://case.uai.edu.ar";


        // TODO:
        //pasar todo lo que se necesite storear en el token, claims
        public static string GenerateToken(Usuario usuario)
        {
            //var tokenDescriptor = new SecurityTokenDescriptor();
            //tokenDescriptor.Issuer = JWT_TOKEN_ISSUER;
            //tokenDescriptor.Audience = JWT_TOKEN_AUDIENCE;
            //tokenDescriptor.SigningCredentials = new SigningCredentials(Keys.RSAKey, SecurityAlgorithms.RsaSha256Signature);
            //tokenDescriptor.Expires = DateTime.UtcNow.AddHours(1);
            //tokenDescriptor.IssuedAt = DateTime.UtcNow;
            //tokenDescriptor.Claims = new List<Claim>() {
            //    new Claim(ClaimTypes.Name, usuario.Apellido),
            //    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            //    new Claim(ClaimTypes.Email, usuario.Mail),
            //    new Claim(ClaimTypes.Role, usuario.Rol.ToString()),

            //};

            var claims = new Claim[]
            {
                new Claim("name", usuario.Nombre),
                new Claim("surname", usuario.Apellido),
                new Claim("rol", usuario.Rol.ToString()),
                new Claim("nameidentifier", usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Mail),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
            };


            var jwt = new JwtSecurityToken(
                issuer: JWT_TOKEN_ISSUER,
                audience: JWT_TOKEN_AUDIENCE,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(Keys.RSAKey, SecurityAlgorithms.RsaSha256Signature));

            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);


            return tokenString;
        }

    }




}
