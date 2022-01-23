using AutoMapper;
using StudentAPI.DataModels;
using StudentAPI.DomainModels;
using System;

namespace StudentAPI.Profiles.AfterMaps
{
    public class AddStudentRequestAftermap : IMappingAction<AddStudentRequest, DataModels.Student>
    {
        public void Process(AddStudentRequest source, DataModels.Student destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
            destination.Address = new DataModels.Address()
            {
                Id = Guid.NewGuid(),
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress,
            };
        }
    }
}
