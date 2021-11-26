using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAPI.Models
{
    public class ToDoItemDTO
    {
        public ToDoItemDTO(ToDoItem toDoItem)
        {
            Id = toDoItem.Id;
            Name = toDoItem.Name;
            IsComplete = toDoItem.IsComplete;
        }
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
