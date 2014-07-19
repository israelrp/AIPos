using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DAO;
using AIPos.DAO.Implementation;
using AIPos.Domain;

namespace AIPos.BusinessLayer
{
    public class BOUsuario
    {
        UsuarioDaoImpl usuarioDaoImpl = new UsuarioDaoImpl();

        public Usuario Insert(Usuario usuario)
        {
            usuario.Password=BOEncrypt.Encrypt(usuario.Password.Trim().ToLower());
            usuario = usuarioDaoImpl.Insert(usuario);
            return usuario;
        }

        public void Delete(int id)
        {
            if (!usuarioDaoImpl.Delete(id))
            {
                throw new Exception("No fue posible eliminar la unidad");
            }
        }

        public Usuario Update(Usuario usuario)
        {
            usuario.Password = BOEncrypt.Encrypt(usuario.Password.Trim().ToLower());
            usuario = usuarioDaoImpl.Update(usuario);
            return usuario;
        }

        public List<Usuario> SelectAll()
        {            
            return usuarioDaoImpl.SelectAll();
        }

        public Usuario SelectById(int id)
        {
            return usuarioDaoImpl.SelectByKey(id);
        }

        public List<Usuario> SelectAllWithPasswordDecrypt()
        {
            List<Usuario> usuarios = usuarioDaoImpl.SelectAll();
            List<Usuario> usuariosDecrypt = new List<Usuario>();
            foreach (var usuario in usuarios)
            {
                usuario.Password = BOEncrypt.Decrypt(usuario.Password);
                usuariosDecrypt.Add(usuario);
            }
            return usuariosDecrypt;
        }

        public Usuario SelectByIdWithPasswordDecrypt(int id)
        {
            Usuario usuario = usuarioDaoImpl.SelectByKey(id);
            usuario.Password = BOEncrypt.Decrypt(usuario.Password);
            return usuario;
        }

        public Usuario Login(string UserName, string Password)
        {
            string passwordEncrypt = BOEncrypt.Encrypt(Password.Trim().ToLower());
            Usuario usuario = new BOUsuario().SelectAll().Where(x => x.UserName.ToLower() == UserName.ToLower() && x.Password == passwordEncrypt).FirstOrDefault();
            return usuario;
        }

        public Usuario Login(string UserName, string Password, int SucursalId)
        {
            string passwordEncrypt=BOEncrypt.Encrypt(Password.Trim().ToLower());
            Usuario usuario = new BOUsuario().SelectAll().Where(x => x.UserName.ToLower() == UserName.ToLower() && x.Password == passwordEncrypt).FirstOrDefault();
            if (usuario != null)
            {
                BOUsuarioSucursal boUsuarioSucursal = new BOUsuarioSucursal();
                UsuarioSucursal usuarioSucursal = boUsuarioSucursal.SelectBySucursalUsuarioId(SucursalId, usuario.Id);
                if (usuarioSucursal != null)
                {
                    return usuario;
                }
                else
                {
                    return null;
                }
            }
            return usuario;
        }
    }
}
