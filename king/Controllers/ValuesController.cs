using king.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Schema;

namespace king.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<student> Getstudent() 
        {
            return collegeRepository.students;
        }
        
        
        
        [HttpGet("{id}")]
        public ActionResult<student> Getstudentsbyid(int id)
        {
            if (id <= 0)
            {
                return BadRequest("must enter positive values");
            }
            else if (id == null)
                return NotFound("dont enter charcters");
            else
            {
                return collegeRepository.students.Where(s=>s.id == id).FirstOrDefault();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Deletestudentbyid(int id)
        {
            var stud = collegeRepository.students.Where(s => s.id == id).FirstOrDefault();
            collegeRepository.students.Remove(stud);
            if (stud == null)
            {
                return BadRequest("not found that value");
            }
           
                return Ok($"deleted {id} successfully");
          
        }

        [HttpGet("getage/{age}")]
        public ActionResult<IEnumerable<student>> Getstudentsbyage(int age)
        {
            //Check age is gratterthan Zero
            if(age <= 0)
            {
                return BadRequest("invalid AGE!");
            }
            var result  = collegeRepository.students.Where(s=> s.age == age).ToList();
            if(result.Count == 0)
            {
                return NotFound($"There are no students with this age {age}");
            }
            return Ok(result);
        }
        
        

        [HttpGet("getbyname/{name}")]
        public ActionResult<student> Getstudentsbyname( string name)
        {
            return collegeRepository.students.Where(s=>s.name == name).FirstOrDefault();
        }


        [HttpGet("getbynamestring/{namestring}")]
        public ActionResult<IEnumerable<student>> GetstudentsbynameString(string namestring)
            
        { 
            if (String.IsNullOrEmpty(namestring))
            {
                return BadRequest("this is invalied string");
            }
          var students= collegeRepository.students.Where(s => s.name.Contains(namestring) ).ToList();
            if(students.Count == 0)
            {
                return BadRequest("there are no students with this string");
            }
            return Ok(students);


            

        }

        [HttpGet("getalldata/")]
        public ActionResult<IEnumerable<student>> Getstudentsalldata(string namestring )
          
        {
            if (namestring == null)
            {
                return NotFound("enter any value");
            }
         var st= collegeRepository.students.Where(s => s.name.Contains(namestring) || s.id.ToString().Contains(namestring) ||

                     s.age.ToString().Contains(namestring) ||  s.designation.Contains(namestring)).ToList();
           if(st==null)
            {
                return NotFound("there are no data");
            }
           return st;
           
        }




        [HttpPost]
       public ActionResult <student> createstudent(student student)
        {
            var stude = collegeRepository.students.Where(s => s.id == student.id).FirstOrDefault();
            if(stude != null)
            {
                return BadRequest("Stunt with id is already Exists");

            }
            collegeRepository.students.Add(student);
            return Ok(student);

        }
        [HttpPut("{name}")]
        public ActionResult<student> updatrstudent(string name ,student student)
        {
            if ( name != student.name)
            {
                return BadRequest("student name is not match");
            }
            
            
            var stud = collegeRepository.students.Where(s => s.name == student.name).FirstOrDefault();
            //checking name is there 
            if (student == null)
            {
                return BadRequest("Stunt with name is not Exists");

            }
            collegeRepository.students.Where(s => s.name == student.name).Select(s =>
            {
                s.id=student.id;
                s.name=student.name;
                s.age=student.age;
                s.designation=student.designation;
                return s;
            }).FirstOrDefault();
               return Ok(student);

        }

    }
}
 