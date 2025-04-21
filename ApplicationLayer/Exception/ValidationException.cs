using System;

namespace ApplicationLayer.Exception
{
    public class ValidationException : System.Exception
    {
        public ValidationException() : base("Error de validación") { }

        public ValidationException(string error) : base(error) { }
    }
}
