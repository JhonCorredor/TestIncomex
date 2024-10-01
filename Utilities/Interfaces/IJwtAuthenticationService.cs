namespace Utilities.Interfaces
{
    public interface IJwtAuthenticationService
    {
        /// <summary>
        /// Autentica a un usuario mediante el nombre de usuario y la contraseña.
        /// Si las credenciales son válidas, devuelve un token JWT.
        /// </summary>
        string Authenticate(string user, string password);

        /// <summary>
        /// Encripta una contraseña utilizando el algoritmo de hashing MD5.
        /// </summary>
        string EncryptMD5(string password);
    }
}
