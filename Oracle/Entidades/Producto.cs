using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oracle.Entidades
{
    /*
        codigo_pro NUMBER(3),
        descripcion varchar2(50),
        marca varchar2(30),
        medida varchar2(5),
        stock decimal(10,2),
        activo char(1),
     */
    public class Producto
    {
        public int Codigo_pro { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public string Medida { get; set; }
        public decimal Stock { get; set; }
        public int Categoria_id { get; set; }
    }
}
