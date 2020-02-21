using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWOF.Application.Models;

namespace WWOF.Application.Data.Repositories
{
    public class UserRepository
    {
        public int Insert(User user)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("usuario", user.UserName),
                new SqlParameter("contrasena", user.Password),
                new SqlParameter("nombre", user.Name),
                new SqlParameter("apellido", user.Surname),
                new SqlParameter("mail", user.Email)
            };
            
            var result = DbConnection.ExecuteNonQuery<int>("SP_GENERAR_USUARIO", sqlParameters);

            return result;
        }
    }
}
