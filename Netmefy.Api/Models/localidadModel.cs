using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class localidadModel
    {
        public int id { get; set; }
        public string localidad { get; set; }
        public string provincia { get; set; }
        public string zona { get; set; }


        public static List<Models.localidadModel> ListConvertTo(List<Data.lk_localidad> localidades)
        {
            List<Models.localidadModel> list = new List<Models.localidadModel>();

            foreach (Data.lk_localidad l in localidades)
            {
                Models.localidadModel loc = ConvertTo(l);
                list.Add(loc);
            }
            return list;
        }

        public static Models.localidadModel ConvertTo(Data.lk_localidad localidad)
        {
            Models.localidadModel loc = new Models.localidadModel();

            loc.id = localidad.localidad_sk;
            loc.localidad = localidad.localidad_desc;
            loc.provincia = localidad.localidad_provincia;
            loc.zona = localidad.localidad_zona;

            return loc;
        }
    }

}