﻿// 28/07/2018 -- geethamali
using System;
namespace Vehicle.Api.ExtensionMethods
{
    public static class IsAExtension
    {
        public static bool IsA<T>(this object obj)
        {
            return (obj.GetType() == typeof(T));
        }
    }
}
