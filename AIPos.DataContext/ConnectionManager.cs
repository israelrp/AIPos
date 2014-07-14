using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using AIPos.Domain;


namespace AIPos.DataContext
{
    public class ConnectionManager : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<CorteCaja> CortesCaja { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<DireccionEnvio> DireccionesEnvio { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<EstatusServicioApartado> EstatusServicioApartado { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Nivel> Niveles { get; set; }
        public DbSet<PedidoSucursal> PedidosSucursal { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<RetiroDinero> RetirosDinero { get; set; }
        public DbSet<ServicioApartado> ServiciosApartados { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<TipoProducto> TiposProducto { get; set; }
        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioSucursal> UsuariosSucursal{ get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaDetalle> VentasDetalle { get; set; }
        public DbSet<DireccionFacturacion> DireccionesFacturacion { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
