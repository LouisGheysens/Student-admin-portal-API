using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.DomainModels;
using StudentAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.Controllers
{
    [ApiController]
    public class GenderController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GenderController(IStudentRepository studentRepository, IMapper mapper)
        {
            this._studentRepository = studentRepository;
            this._mapper = mapper;
        }


        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllGenders()
        {
            var genderList = await _studentRepository.GetGendersAsync();

            if(genderList == null || !genderList.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<Gender>>(genderList));
        }
    }
}
