using AutoMapper;
using TodoList.Api.Models.Requests;
using TodoList.Api.Models.Responses;
using TodoList.DAL.Commands.EditTask;
using TodoList.DAL.Entities;
using TodoList.DAL.Models;
using TodoList.DAL.Queries.GetSubtasksList;
using TodoList.DAL.Queries.GetUser;
using TodoList.DAL.Queries.GetUserTaskList;
using TodoList.DAL.Queries.GetUserTasks;
//using TodoList.DAL.Queries.LogInUser;

namespace TodoList.Api.Core
{
    public class AutomapperApiConfiguration : Profile
    {
        public AutomapperApiConfiguration()
        {
            CreateMap<GetAllTasksRequest, BasePage>();
            CreateMap<GetAllUserTasksQueryResult, GetAllTasksResponse>();
            CreateMap<UserTask, GetTaskResponse>();
            CreateMap<SubTask, GetSubTaskResponse>();
            //CreateMap<LogInUser, LogInUserQuery>();
            CreateMap<EditTaskRequest, EditTaskCommand>();
            CreateMap<GetUserTaskListQueryResult, GetUserTaskListResponse>();
            CreateMap<GetSubtaskListQueryResult, GetSubtaskListResponse>();
            CreateMap<GetUserQueryResult, GetUserResponse>();
        }
    }
}
