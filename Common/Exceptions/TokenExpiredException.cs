﻿namespace Common.Exceptions
{
    public class TokenExpiredException : Exception
    {
        public TokenExpiredException() : base("Token is expired.") {}
    }
}
