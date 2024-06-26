﻿using MediatR;

namespace TodoList.DAL.Commands.EditTask
{
    public class EditTaskCommand : IRequest<EditTaskCommandResult>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Deadline { get; set; }
    }
}
