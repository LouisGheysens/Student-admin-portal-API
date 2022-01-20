using AutoMapper;
using StudentAPI.DomainModels;
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
        }
    }
}
