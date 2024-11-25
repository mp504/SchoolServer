using AutoMapper;
using ServerOfSchool.Dto;
using ServerOfSchool.Models;
namespace ServerOfSchool.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<Course, CourseDto>();
            CreateMap<Teacher, TeacherDto>();
            CreateMap<StudentAddress, StudentAddressDto>();
            CreateMap<StudentDto, Student>();
            CreateMap<CourseDto, Course>();
            CreateMap<TeacherDto, Teacher>();
            CreateMap<StudentAddressDto, StudentAddress>();


        }
    }
}
