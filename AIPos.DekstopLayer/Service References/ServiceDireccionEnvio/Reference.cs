﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34014
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AIPos.DekstopLayer.ServiceDireccionEnvio {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceDireccionEnvio.ISDireccionEnvio")]
    public interface ISDireccionEnvio {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISDireccionEnvio/Insert", ReplyAction="http://tempuri.org/ISDireccionEnvio/InsertResponse")]
        AIPos.Domain.DireccionEnvio Insert(AIPos.Domain.DireccionEnvio direccionEnvio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISDireccionEnvio/Insert", ReplyAction="http://tempuri.org/ISDireccionEnvio/InsertResponse")]
        System.Threading.Tasks.Task<AIPos.Domain.DireccionEnvio> InsertAsync(AIPos.Domain.DireccionEnvio direccionEnvio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISDireccionEnvio/Delete", ReplyAction="http://tempuri.org/ISDireccionEnvio/DeleteResponse")]
        void Delete(int DireccionId, int ClienteId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISDireccionEnvio/Delete", ReplyAction="http://tempuri.org/ISDireccionEnvio/DeleteResponse")]
        System.Threading.Tasks.Task DeleteAsync(int DireccionId, int ClienteId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISDireccionEnvioChannel : AIPos.DekstopLayer.ServiceDireccionEnvio.ISDireccionEnvio, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SDireccionEnvioClient : System.ServiceModel.ClientBase<AIPos.DekstopLayer.ServiceDireccionEnvio.ISDireccionEnvio>, AIPos.DekstopLayer.ServiceDireccionEnvio.ISDireccionEnvio {
        
        public SDireccionEnvioClient() {
        }
        
        public SDireccionEnvioClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SDireccionEnvioClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SDireccionEnvioClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SDireccionEnvioClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public AIPos.Domain.DireccionEnvio Insert(AIPos.Domain.DireccionEnvio direccionEnvio) {
            return base.Channel.Insert(direccionEnvio);
        }
        
        public System.Threading.Tasks.Task<AIPos.Domain.DireccionEnvio> InsertAsync(AIPos.Domain.DireccionEnvio direccionEnvio) {
            return base.Channel.InsertAsync(direccionEnvio);
        }
        
        public void Delete(int DireccionId, int ClienteId) {
            base.Channel.Delete(DireccionId, ClienteId);
        }
        
        public System.Threading.Tasks.Task DeleteAsync(int DireccionId, int ClienteId) {
            return base.Channel.DeleteAsync(DireccionId, ClienteId);
        }
    }
}
