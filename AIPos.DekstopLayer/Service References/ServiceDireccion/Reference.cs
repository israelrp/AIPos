﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34014
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AIPos.DekstopLayer.ServiceDireccion {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceDireccion.ISDireccion")]
    public interface ISDireccion {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISDireccion/Insert", ReplyAction="http://tempuri.org/ISDireccion/InsertResponse")]
        AIPos.Domain.Direccion Insert(AIPos.Domain.Direccion direccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISDireccion/Insert", ReplyAction="http://tempuri.org/ISDireccion/InsertResponse")]
        System.Threading.Tasks.Task<AIPos.Domain.Direccion> InsertAsync(AIPos.Domain.Direccion direccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISDireccion/Update", ReplyAction="http://tempuri.org/ISDireccion/UpdateResponse")]
        AIPos.Domain.Direccion Update(AIPos.Domain.Direccion direccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISDireccion/Update", ReplyAction="http://tempuri.org/ISDireccion/UpdateResponse")]
        System.Threading.Tasks.Task<AIPos.Domain.Direccion> UpdateAsync(AIPos.Domain.Direccion direccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISDireccion/SelectByCliente", ReplyAction="http://tempuri.org/ISDireccion/SelectByClienteResponse")]
        AIPos.Domain.Direccion[] SelectByCliente(int ClienteId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISDireccion/SelectByCliente", ReplyAction="http://tempuri.org/ISDireccion/SelectByClienteResponse")]
        System.Threading.Tasks.Task<AIPos.Domain.Direccion[]> SelectByClienteAsync(int ClienteId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISDireccion/SelectById", ReplyAction="http://tempuri.org/ISDireccion/SelectByIdResponse")]
        AIPos.Domain.Direccion SelectById(int Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISDireccion/SelectById", ReplyAction="http://tempuri.org/ISDireccion/SelectByIdResponse")]
        System.Threading.Tasks.Task<AIPos.Domain.Direccion> SelectByIdAsync(int Id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISDireccionChannel : AIPos.DekstopLayer.ServiceDireccion.ISDireccion, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SDireccionClient : System.ServiceModel.ClientBase<AIPos.DekstopLayer.ServiceDireccion.ISDireccion>, AIPos.DekstopLayer.ServiceDireccion.ISDireccion {
        
        public SDireccionClient() {
        }
        
        public SDireccionClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SDireccionClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SDireccionClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SDireccionClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public AIPos.Domain.Direccion Insert(AIPos.Domain.Direccion direccion) {
            return base.Channel.Insert(direccion);
        }
        
        public System.Threading.Tasks.Task<AIPos.Domain.Direccion> InsertAsync(AIPos.Domain.Direccion direccion) {
            return base.Channel.InsertAsync(direccion);
        }
        
        public AIPos.Domain.Direccion Update(AIPos.Domain.Direccion direccion) {
            return base.Channel.Update(direccion);
        }
        
        public System.Threading.Tasks.Task<AIPos.Domain.Direccion> UpdateAsync(AIPos.Domain.Direccion direccion) {
            return base.Channel.UpdateAsync(direccion);
        }
        
        public AIPos.Domain.Direccion[] SelectByCliente(int ClienteId) {
            return base.Channel.SelectByCliente(ClienteId);
        }
        
        public System.Threading.Tasks.Task<AIPos.Domain.Direccion[]> SelectByClienteAsync(int ClienteId) {
            return base.Channel.SelectByClienteAsync(ClienteId);
        }
        
        public AIPos.Domain.Direccion SelectById(int Id) {
            return base.Channel.SelectById(Id);
        }
        
        public System.Threading.Tasks.Task<AIPos.Domain.Direccion> SelectByIdAsync(int Id) {
            return base.Channel.SelectByIdAsync(Id);
        }
    }
}
