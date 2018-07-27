using System;
namespace Identity.Core.Validators
{
    public static class Validator
    {
        public static void CheckNull<T>(T obj)
        {
            if(obj == null)
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

        public static void UnAuthorized(){
            throw new UnauthorizedAccessException();
        }
    }
}
