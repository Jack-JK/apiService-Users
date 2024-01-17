using apiServicio.Models;
using apiServicio.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace apiServicio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController
    {
        private readonly IRolService _IRolService;
        public RolController(IRolService iTemp)
        {
            this._IRolService = iTemp;
        }
        [HttpGet]
        public async Task<List<Rol>> GetList()
        {
            return await _IRolService.GetList();
        }
        [HttpPost("AgregaActualiza")]
        public async Task<Rol> AgregaActualiza(int Id, string NombreRol, string t)
        {
            Rol l = new Rol();
            l.Id = Id;
            l.NombreRol = NombreRol;
            return await _IRolService.AgregaActualiza(l,t);
        }
    }
}
