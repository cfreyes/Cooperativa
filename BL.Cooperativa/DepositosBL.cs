using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Cooperativa
{
    public class DepositosBL
    {
        Contexto _contexto;
        public BindingList<Deposito> ListaDepositos { get; set; }

        public DepositosBL()
        {
            _contexto = new Contexto();
            ListaDepositos = new BindingList<Deposito>();
        }

        public BindingList<Deposito> ObtenerDeposito()
        {
            _contexto.Depositos.Load();
            ListaDepositos = _contexto.Depositos.Local.ToBindingList();
                
            return ListaDepositos;
        }

        public BindingList<Deposito> ObtenerDepositos(string buscar)
        {
            var query = _contexto.Depositos
                .Where(p => p.Nombre.ToLower()
                    .Contains(buscar.ToLower()) == true)
                        .ToList();

            var resultado = new BindingList<Deposito>(query);

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

        public Resultado GuardarDeposito(Deposito deposito)
        {
            var Resultado = Validar(deposito);
            if (Resultado.Exitiso == false)
            {
                return Resultado;
            }

            _contexto.SaveChanges();

            Resultado.Exitiso = true;
            return Resultado;
        }

        public void AgregarDeposito()
        {
            var nuevoDeposito = new Deposito();
            ListaDepositos.Add(nuevoDeposito);
        }

        public bool EliminarDeposito(int id)
        {
            foreach (var deposito in ListaDepositos)
            {
                if (deposito.Id == id)
                {
                    ListaDepositos.Remove(deposito);
                    _contexto.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        private Resultado Validar(Deposito deposito)
        {
            var resultado = new Resultado();
            resultado.Exitiso = true;

            if (deposito == null)
            {
                resultado.Mensaje = "Agregue un Deposito valido.";
                resultado.Exitiso = false;

                return resultado;
            }

            if (string.IsNullOrEmpty(deposito.Nombre) == true)
            {
                resultado.Mensaje = "Ingrese un Nombre";
                resultado.Exitiso = false;
            }

            if (deposito.Cantidad < 0)
            {
                resultado.Mensaje = "La cantidad debe de ser mayor a 0!";
                resultado.Exitiso = false;
            }

            if (deposito.NCuenta < 0)
            {
                resultado.Mensaje = "Ingreso un Numero de cuenta valido";
                resultado.Exitiso = false;
            }

            if (deposito.TipoId == 0)
            {
                resultado.Mensaje = "Seleccione un tipo";
                resultado.Exitiso = false;
            }

            return resultado;
        }
    }

    public class Deposito
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Cantidad { get; set; }
        public int NCuenta { get; set; }
        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }
        public byte[] Foto { get; set; }
        public bool Activo { get; set; }
    }

    public class Resultado
    {
        public bool Exitiso { get; set; }
        public string Mensaje { get; set; }
    }
}



