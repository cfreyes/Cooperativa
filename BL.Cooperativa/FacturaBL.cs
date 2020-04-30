using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Cooperativa
{
    public class FacturaBL
    {
        Contexto _contexto;

        public BindingList<Factura> ListaFacturas { get; set; }

        public FacturaBL()
        {
            _contexto = new Contexto();
        }

        public BindingList<Factura> ObtenerFacturas()
        {
            _contexto.Facturas.Include("FacturaDetalle").Load();
            ListaFacturas = _contexto.Facturas.Local.ToBindingList();

            return ListaFacturas;
        }

        public void AgregarFactura()
        {
            var nuevaFactura = new Factura();
            _contexto.Facturas.Add(nuevaFactura);
        }

        public void AgregarFacturaDetalle(Factura factura)
        {
            if (factura != null)
            {
                var nuevoDetalle = new FacturaDatalle();
                factura.FacturaDetalle.Add(nuevoDetalle);
            }
        }

        public void RemoverFacturaDetalle(Factura factura, FacturaDatalle facturaDatalle)
        {
            if (factura != null && facturaDatalle != null)
            {
                factura.FacturaDetalle.Remove(facturaDatalle);
            }
        }

        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;
                item.Reload();
            }
        }

        public Resultado GuardarFactura(Factura factura)
        {
            var resultado = Validar(factura);
            if (resultado.Exitiso == false)
            {
                return resultado;
            }

            _contexto.SaveChanges();
            resultado.Exitiso = true;
            return resultado;
        }

        private Resultado Validar(Factura factura)
        {
            var resultado = new Resultado();
            resultado.Exitiso = true;

            if (factura == null)
            {
                resultado.Mensaje = "Agregue una factura para poderla Guardar.";
                resultado.Exitiso = false;

                return resultado;
            }

            if (factura.Id != 0 && factura.Activo == true)
            {
                resultado.Mensaje = "La factura ya fue emitida y no se pueden realizar cambios en ella.";
                resultado.Exitiso = false;
            }

            if (factura.Activo == false)
            {
                resultado.Mensaje = "La factura esta anulada y no se pueden realizar cambios en ella.";
                resultado.Exitiso = false;
            }

            if (factura.ClienteId == 0)
            {
                resultado.Mensaje = "Seleccione un Cliente.";
                resultado.Exitiso = false;
            }

            if (factura.FacturaDetalle.Count == 0)
            {
                resultado.Mensaje = "Agregue depositos a la factura";
                resultado.Exitiso = false;
            }

            foreach (var detalle in factura.FacturaDetalle)
            {
                if (detalle.DepositoId == 0)
                {
                    resultado.Mensaje = "Seleccione Depositos validos.";
                    resultado.Exitiso = false;
                }
            }

            return resultado;
        }

        public void CalcularFactura(Factura factura)
        {
            if (factura != null)
            {
                double subtotal = 0;

                foreach (var detalle in factura.FacturaDetalle)
                {
                    var deposito = _contexto.Depositos.Find(detalle.DepositoId);
                    if (deposito != null)
                    {
                        detalle.Precio = deposito.Cantidad;
                        detalle.Total = detalle.Cantidad * deposito.Cantidad;

                        subtotal += detalle.Total;
                    }
                }

                factura.Subtotal = subtotal;
                factura.Impuesto = subtotal * 0.15;

                factura.Total = subtotal + factura.Impuesto;
            }

        }

        public bool AnularFactura(int id)
        {
            foreach (var factura in ListaFacturas)
            {
                if (factura.Id == id)
                {
                    factura.Activo = false;
                    _contexto.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }

    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public BindingList<FacturaDatalle> FacturaDetalle { get; set; }
        public double Subtotal { get; set; }
        public double Impuesto { get; set; }
        public double Total { get; set; }
        public bool Activo { get; set; }

        public Factura()
        {
            Fecha = DateTime.Now;
            FacturaDetalle = new BindingList<FacturaDatalle>();
            Activo = true;
        }
    }

    public class FacturaDatalle
    {
        public int Id { get; set; }
        public int DepositoId { get; set; }
        public Deposito Deposito { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double Total { get; set; }

        public FacturaDatalle()
        {
            Cantidad = 1;
        }
    }
}
