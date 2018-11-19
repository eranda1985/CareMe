using System;
using Vehicle.Core.Exceptions;

namespace Vehicle.Core.Validators
{
    public static class Validator
    {
        public static void CheckNull<T>(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
        }

        public static void CheckStringEmpty(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException();
            }
        }

        public static void UnAuthorized()
        {
            throw new UnauthorizedAccessException();
        }

        public static void CheckArgsLength(object[] args, int length)
        {
            if (args.Length != length)
            {
                throw new ValidationException("Argmument length does not match.");
            }
        }

        public static void CheckType<T>(object obj)
        {
            if (typeof(T) != obj.GetType())
            {
                throw new ValidationException("Object type mismatch.");
            }
        }
    }
}
