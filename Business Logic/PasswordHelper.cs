using DTO;
using DataAccess.CRUD;
using System.Security.Cryptography;
using System.Collections;

namespace BL
{
    public class PasswordHelper
    {
        // 1) Creacion de usuario.
        // Se crea un usuario con una contraseña válida.
        // La contraseña no se guarda en la base de datos.
        // Se genera una sal aleatoria para el usuario.
        // La contrasena se encrypta junto con una sal.
        // Se guarda el resultado de la encryptacion.
        // Se guarda, por separado, la sal.

        // 2) Inicio de sesion.
        // El usuario ingresa usuario y contrasena.
        // Encrypto la contrasena recibida junto a la sal y comparo con la encryptacion guardada. 

        // Genera una sal aleatoria
        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[16]; // 16 bytes = 128 bits
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        // Hash password with salt using PBKDF2
        public byte[] HashPassword(string password, byte[] salt)
        {
            int iterations = 10000; // Number of iterations
            int hashByteSize = 32; // 256 bits
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return pbkdf2.GetBytes(hashByteSize);
            }
        }

        // Verify if entered password matches stored hashed password
        public bool VerifyPassword(string enteredPassword, byte[] storedHash, byte[] storedSalt)
        {
            byte[] enteredHash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, storedSalt, 10000))
            {
                enteredHash = pbkdf2.GetBytes(32);
            }

            // Compare storedHash with enteredHash
            return StructuralComparisons.StructuralEqualityComparer.Equals(enteredHash, storedHash);
        }
    }
}



