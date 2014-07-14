using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DataContext;
using AIPos.DAO.IGeneric;

namespace AIPos.DAO.Implementation
{
    public class ProductoDaoImpl : ProductoDao
    {
        ConnectionManager cm = new ConnectionManager();

        public Producto Insert(Producto entity)
        {
            object[] parameters = new object[] { entity.UnidadId, entity.TipoId, entity.Nombre, entity.Precio, entity.SePesa,
                entity.Impuesto, entity.Codigo, entity.Fotografia, entity.Eliminado, entity.Descuento };
            return cm.Database.SqlQuery<Producto>("dbo.usp_ProductosInsert @UnidadId={0}, @TipoId={1}, @Nombre={2}, @Precio={3}, "+
                "@SePesa={4}, @Impuesto={5}, @Codigo={6}, @Fotografia={7}, @Eliminado={8}, @Descuento={9}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_ProductosDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public Producto Update(Producto entity)
        {
            object[] parameters = new object[] { entity.Id, entity.UnidadId, entity.TipoId, entity.Nombre, entity.Precio, entity.SePesa,
                entity.Impuesto, entity.Codigo, entity.Fotografia, entity.Eliminado, entity.Descuento };
            return cm.Database.SqlQuery<Producto>("dbo.usp_ProductosUpdate @Id={0}, @UnidadId={1}, @TipoId={2}, @Nombre={3}, @Precio={4}, " +
                "@SePesa={5}, @Impuesto={6}, @Codigo={7}, @Fotografia={8}, @Eliminado={9}, @Descuento={10}", parameters).FirstOrDefault();
        }

        public List<Producto> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<Producto>("dbo.usp_ProductosSelect @Id={0}", parameters).ToList();
        }

        public Producto SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<Producto>("dbo.usp_ProductosSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
