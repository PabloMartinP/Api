using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class usuarioModel
    {
        public int usuario_sk { get; set; }
        public int cliente_sk { get; set; }
        public string usuario_nombre { get; set; }
        public string usuario_sexo { get; set; }
        public Nullable<int> usuario_edad { get; set; }
        public byte[] usuario_foto { get; set; }
        public string usuario_email { get; set; }


        public static usuarioModel ConvertTo(Data.usuario usuario)
        {
            usuarioModel usr = new usuarioModel();

            usr.cliente_sk = usuario.cliente_sk;
            usr.usuario_sk = usuario.usuario_sk;
            usr.usuario_nombre = usuario.usuario_nombre;
            usr.usuario_sexo = usuario.usuario_sexo;
            usr.usuario_edad = usuario.usuario_edad;
            usr.usuario_foto = usuario.usuario_foto;
            usr.usuario_email = usuario.usuario_email;
            
            return usr;
        }
    }
}