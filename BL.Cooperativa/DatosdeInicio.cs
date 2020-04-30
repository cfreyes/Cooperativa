using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Cooperativa
{
    public class DatosdeInicio : CreateDatabaseIfNotExists<Contexto>
    {
        protected override void Seed(Contexto contexto)
        {
            var usuarioAdmin = new Usuario();
            usuarioAdmin.Nombre = "admin";
            usuarioAdmin.Contrasena = "123";
            usuarioAdmin.TipoUsuario = "Administradores";

            contexto.Usuarios.Add(usuarioAdmin);

            var tipo1 = new Tipo();
            tipo1.Descripcion = "Ahorro";
            contexto.Tipos.Add(tipo1);

            var tipo2 = new Tipo();
            tipo2.Descripcion = "Credito";
            contexto.Tipos.Add(tipo2);

            var archivo = "../../../clientes.csv";
            using (var reader = new StreamReader(archivo))
            {
                reader.ReadLine(); // Lee primera fila de encabezados

                while (!reader.EndOfStream)
                {
                    var linea = reader.ReadLine();
                    var valores = linea.Split(',');

                    var clienteNuevo = new Cliente();
                    clienteNuevo.Nombre = valores[0].ToString();
                    clienteNuevo.Activo = bool.Parse(valores[1].ToString());

                    contexto.Clientes.Add(clienteNuevo);
                }
            }

            base.Seed(contexto);
        }


    }
}
