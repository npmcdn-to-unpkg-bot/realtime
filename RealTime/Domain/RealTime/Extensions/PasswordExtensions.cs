// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordExtensions.cs" company="LngLng bvba">
//   Copyright 2016 LngLng bvba.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain.RealTime.Extensions
{
    using System;
    using System.Security.Cryptography;

    public static class PasswordExtensions
    {
        public static string HashPassword(this string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            
            byte[] salt;
            byte[] buffer;
            using (var bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer = bytes.GetBytes(0x20);
            }

            var dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}
