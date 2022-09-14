using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoEscola_API.Data;
using ProjetoEscola_API.Models;

namespace ProjetoEscola_API.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : Controller{
        private EscolaContext _context;
        public AlunoController(EscolaContext context){
            _context = context;
        }
        [HttpGet]
        public ActionResult<List<Aluno>> GetAll(){
            return _context.Aluno.ToList();
        }

        [HttpGet("{AlunoId}")]
        public ActionResult<List<Aluno>> Get(int AlunoId){
            try{
                var result = _context.Aluno.Find(AlunoId);
                if(result == null)
                    return NotFound();

                    return Ok(result);
            }
            catch{
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                                        "Falha ao acesso ao banco de dados.");
            }
        }
        [HttpPost]
        public async Task<ActionResult> post(Aluno model){
            try{
                _context.Aluno.Add(model);
                if(await _context.SaveChangesAsync() ==  1){
                    return Created($"/api/aluno/{model.ra}", model);
                }
            }
            catch{
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                        "Falha no acesso do banco de dados.");
            }
            return BadRequest();
        }

        [HttpPut("{AlunoRa}")]
        public ActionResult put(string AlunoRA){
            return Ok();
        }

        [HttpDelete("{AlunoRA}")]
        public ActionResult delete(string AlunoRA){
            return Ok();
        }
    }
}