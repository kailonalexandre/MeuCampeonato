using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
