using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.DomainModels;
using StudentAPI.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper, IImageRepository imageRepository)
        {
            this._studentRepository = studentRepository;
            this._mapper = mapper;
            this._imageRepository = imageRepository;
        }


        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetStudentsAsync();

            return Ok(_mapper.Map<List<Student>>(students));
        }

        [HttpGet]
        [Route("[controller]{studentId:guid}"), ActionName("GetStudentByIdAsync")]
        public async Task<IActionResult> GetStudentByIdAsync([FromRoute] Guid studentId)
        {
            var student = await _studentRepository.GetStudentIdAsync(studentId);

            if(student == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Student>(student));
        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
        {
            if(await _studentRepository.Exists(studentId))
            {

               var updatedStudent =  await _studentRepository.UpdateStudent(studentId, _mapper.Map<DataModels.Student>(request));

                if(updatedStudent != null)
                {
                    return Ok(_mapper.Map<DomainModels.Student>(updatedStudent));
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (await _studentRepository.Exists(studentId))
            {
                var student = await _studentRepository.DeleteStudent(studentId);
                return Ok(_mapper.Map<Student>(student));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            var newStudent = await _studentRepository.AddStudent(_mapper.Map<DataModels.Student>(request));
            return CreatedAtAction(nameof(GetStudentByIdAsync), new { studentId = newStudent.Id },
                _mapper.Map<Student>(newStudent));
        }
        
        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid studentId, IFormFile profileImage)
        {
            if(await _studentRepository.Exists(studentId))
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                var fileAwait = await _imageRepository.Upload(profileImage, fileName);
                if(await _studentRepository.UpdateProfileImage(studentId, fileAwait))
                {
                    return Ok(fileAwait);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
            }
            return NotFound();
        }
    }
}
