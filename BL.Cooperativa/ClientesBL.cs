using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Cooperativa
{
    public class ClientesBL
    {
        Contexto _contexto;
        public BindingList<Cliente> ListaClientes { get; set; }

        public ClientesBL()
        {
            _contexto = new Contexto();
            ListaClientes = new BindingList<Cliente>();

        }

        public BindingList<Cliente> ObtenerClientes()
        {

            ListaClientes = new BindingList<Cliente>(_contexto.Clientes.OrderBy(o => o.Nombre).ToList());

            return ListaClientes;
        }

        public BindingList<Cliente> ObtenerClientes(string buscar)
        {
            var query = _contexto.Clientes
                .Where(p => p.Nombre.ToLower()
                    .Contains(buscar.ToLower()) == true)
                        .ToList();

            var resultado = new BindingList<Cliente>(query);

            return resultado;
        }

        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;
                item.Reload();
            }
        }

        public Resultados GuardarCliente(Cliente cliente)
        {
            var resultados = Validar(cliente);
            if (resultados.Exitiso == false)
            {
                return resultados;
            }

            _contexto.SaveChanges();

            resultados.Exitiso = true;
            return resultados;
        }

        public void AgregarCliente()
        {
            var nuevoCliente = new Cliente();
            ListaClientes.Add(nuevoCliente);
        }

        public bool ELiminarCliente(int id)
        {
            foreach (var cliente in ListaClientes)
            {
                if (cliente.Id == id)
                {
                    ListaClientes.Remove(cliente);
                    _contexto.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        private Resultados Validar(Cliente cliente)
        {
            var resultados = new Resultados();
            resultados.Exitiso = true;

            if (cliente == null)
            {
                resultados.Mensaje = "Agregue un Cliente valido.";
                resultados.Exitiso = false;

                return resultados;
            }

            if (string.IsNullOrEmpty(cliente.Nombre) == true)
            {
                resultados.Mensaje = "Ingrese un Nombre";
                resultados.Exitiso = false;
            }

            if (cliente.Identidad <= 0)
            {
                resultados.Mensaje = "Ingrese el Numero de Identidad valido";
                resultados.Exitiso = false;
            }

            if (string.IsNullOrEmpty(cliente.Direccion) == true)
            {
                resultados.Mensaje = "Ingrese una dirección";
                resultados.Exitiso = false;
            }

            return resultados;
        }
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Identidad { get; set; }
        public string Direccion { get; set; }
        public byte[] Foto { get; set; }
        public bool Activo { get; set; }

    }

    public class Resultados
    {
        public bool Exitiso { get; set; }
        public string Mensaje { get; set; }
    }
}
