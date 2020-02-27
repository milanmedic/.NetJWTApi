using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JwtApi.Domain.Models;
using JwtApi.Domain.Services;
using AutoMapper;

namespace JwtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        public ProjectsController(IUserService userService, IProjectService projectService, IMapper mapper){
            _userService = userService; 
            _projectService = projectService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("add_project")]
        public async Task<IActionResult> AddProject([FromBody] ProjectDto projectDto){
            if(projectDto.CreatorId == 0 || string.IsNullOrEmpty(projectDto.Description) == null || string.IsNullOrEmpty(projectDto.Name)){
                return BadRequest(ModelState);
            }
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var project = _mapper.Map<Project>(projectDto);
            try {
                var added = await _projectService.AddNewProjectAsync(project);
                
                if(!added){
                    return BadRequest("There was an error while adding the project");
                }
                try {
                    var response = await _projectService.GetProjectAsync(project.Id);
                    var returnProject = _mapper.Map<ProjectResponse>(response);
                    return Ok(returnProject);
                }catch(AppException ex){
                    return BadRequest(new { message = ex.Message});
                }

            } catch(AppException ex){
                return BadRequest(new {message = ex.Message});
            }
        }
        [Authorize]
        [HttpGet("get_projects")]
        public async Task<IActionResult> GetProjects([FromBody] string email) {
            if(string.IsNullOrEmpty(email)){
                return BadRequest("Email or id were not provided");
            }

            var user = await _userService.GetUserByEmailAsync(email);
            if(user == null) {
                return BadRequest("User doesn't exist");
            }

            try {
               var projects = await _projectService.GetProjectsWithCreatorIdAsync(user.Id);
               IList<ProjectDto> response = new List<ProjectDto>();
               //iterate over every list item and convert it to ProjectDto
               foreach(var item in projects){
                   response.Add(_mapper.Map<ProjectDto>(item));
               }
               //var response = _mapper.Map<ProjectDto>(projects);
               return Ok(response);
            }catch(AppException ex){
                Console.WriteLine(ex.Message);
                return BadRequest("There was an error while searching for projects");
            }
        }
    }
}
