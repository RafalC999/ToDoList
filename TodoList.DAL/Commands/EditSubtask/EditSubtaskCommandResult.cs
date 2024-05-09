namespace TodoList.DAL.Commands.EditSubtask
{
    public class EditSubtaskCommandResult
    {
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public int Order { get; set; }
    }
}
