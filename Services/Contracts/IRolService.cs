using apiServicio.Models;

namespace apiServicio.Services.Contracts
{
    public interface IRolService
    {
        Task<List<Rol>> GetList();
        Task<Rol> AgregaActualiza(Rol l, string t);
    }
}
