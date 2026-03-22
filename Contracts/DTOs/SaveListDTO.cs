using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanning.Contracts.DTOs
{
    public class SaveListDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "未命名清單";
        public string IconKey { get; set; } = "Luggage28";
    }
}
