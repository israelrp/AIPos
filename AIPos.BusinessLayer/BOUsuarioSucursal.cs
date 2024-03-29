﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DAO;
using AIPos.DAO.Implementation;
using AIPos.Domain;

namespace AIPos.BusinessLayer
{
    public class BOUsuarioSucursal
    {
        UsuarioSucursalDaoImpl usuarioSucursalDaoImpl = new UsuarioSucursalDaoImpl();

        public List<UsuarioSucursal> SelectAll()
        {
            return usuarioSucursalDaoImpl.SelectAll();
        }

        public List<UsuarioSucursal> SelectByUsuarioId(int UsuarioId)
        {
            return usuarioSucursalDaoImpl.SelectByKey(null, UsuarioId);
        }

        public List<UsuarioSucursal> SelectBySucursalId(int SucursalId)
        {
            return usuarioSucursalDaoImpl.SelectByKey(SucursalId, null);
        }

        public UsuarioSucursal SelectBySucursalUsuarioId(int SucursalId, int UsuarioId)
        {
            return usuarioSucursalDaoImpl.SelectByKey(SucursalId, UsuarioId);
        }

        public void Delete(UsuarioSucursal usuarioSucursal)
        {
            usuarioSucursalDaoImpl.Delete(usuarioSucursal.SucursalId, usuarioSucursal.UsuarioId);
        }

        public UsuarioSucursal Insert(UsuarioSucursal usuarioSucursal)
        {
            return usuarioSucursalDaoImpl.Insert(usuarioSucursal);
        }
    }
}
