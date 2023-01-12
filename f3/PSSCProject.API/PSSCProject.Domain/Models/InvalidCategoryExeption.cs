using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PSSCProject.Domain.Models
{
    [Serializable]
    internal class InvalidCategoryExeption : Exception
    {
        public InvalidCategoryExeption()
        {
        }

        public InvalidCategoryExeption(string? message) : base(message)
        {
        }

        public InvalidCategoryExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidCategoryExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
