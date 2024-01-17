using apiServicio.Business.Contracts;
using apiServicio.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace apiServicio.Business.Clases
{
    public class RolRepository : IRolRepository
    {
        private readonly string conect;

        public RolRepository(IConfiguration _IConfiguration)
        {
            conect = _IConfiguration.GetConnectionString("conBase");
        }
        public async Task<List<Rol>> GetList()
        {
            List<Rol> list = new List<Rol>();
            Rol l;
            using (SqlConnection conn = new SqlConnection(conect))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("select * from Rol;", conn);
                using(var reader = await cmd.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        l = new Rol();
                        l.Id = Convert.ToInt32(reader["id"]);
                        l.NombreRol = Convert.ToString(reader["NombreRol"]);
                        list.Add(l);
                    }
                }
            }
            return list;
        }
        public async Task<Rol> AgregaActualiza(Rol l, string t)
        {
            using (SqlConnection conn = new SqlConnection(conect))
            {
                string cadena = "";

                if (t == "c")
                {
                    // Si estás creando un nuevo registro
                    cadena = "INSERT INTO Rol(NombreRol) OUTPUT INSERTED.Id VALUES(@NombreRol)";
                }
                else if (t == "u")
                {
                    // Si estás actualizando un registro existente
                    cadena = "UPDATE Rol SET NombreRol=@NombreRol WHERE Id=@Id";
                }

                using (SqlCommand cmd = new SqlCommand(cadena, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", l.Id);
                    cmd.Parameters.AddWithValue("@NombreRol", l.NombreRol);

                    await conn.OpenAsync();

                    if (t == "c")
                    {
                        // Si estás creando, asigna el nuevo valor de Id
                        l.Id = (int)await cmd.ExecuteScalarAsync();
                    }
                    else
                    {
                        // Si estás actualizando, simplemente ejecuta la actualización
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return l;
            }
            
            //using (SqlConnection conn = new SqlConnection(conect))
            //{
            //    string cadena = "";
            //    if (t == "c")
            //    {
            //        cadena = "set @I=(select isnull(mac(Id),0)+1 from Rol)" + "insert into Rol(NombreRol) values(@NombreRol)";
            //    }
            //    if(t == "u")
            //    {
            //        cadena = "update Rol set NombreRol=@NombreRol where Id=@Id; ";
            //    }

            //    using (SqlCommand cmd = new SqlCommand(cadena, conn))
            //    {
            //        SqlParameter Result = new SqlParameter("@I", System.Data.SqlDbType.Int)
            //        {
            //            Direction = System.Data.ParameterDirection.Output
            //        };
            //        cmd.Parameters.Add(Result);
            //        cmd.Parameters.AddWithValue("@Id", l.Id);
            //        cmd.Parameters.AddWithValue("@NombreRol", l.NombreRol);
            //        await conn.OpenAsync();
            //        await cmd.ExecuteNonQueryAsync();
            //        if (t == "c")
            //        {
            //            l.Id = int.Parse(Result.Value.ToString());
            //        }
            //    }
            //    return l;
            //}
        }
    }
}
