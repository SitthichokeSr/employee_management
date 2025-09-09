using AutoMapper;
using EmployeeManagement.Application.DTO;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Services
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<Employee, EmployeeDto>(); 
        }
    }
}
