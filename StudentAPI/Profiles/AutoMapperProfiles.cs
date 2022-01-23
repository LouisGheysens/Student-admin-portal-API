using AutoMapper;
using StudentAPI.DomainModels;
using StudentAPI.Profiles.AfterMaps;
using DataModels = StudentAPI.DataModels;

namespace StudentAPI.Profiles
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DataModels.Student, Student>()
                .ReverseMap();

            CreateMap<DataModels.Gender, Gender>()
           .ReverseMap();

            CreateMap<DataModels.Address, Address>()
           .ReverseMap();

            CreateMap<UpdateStudentRequest, DataModels.Student>()
                .AfterMap<UpdateStudentRequestAfterMap>();

            CreateMap<AddStudentRequest, DataModels.Student>()
                .AfterMap<AddStudentRequestAftermap>();
        }
    }
}
