﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34014
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AIPos.DekstopLayer.ServiceTipoProducto {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceTipoProducto.ISTipoProducto")]
    public interface ISTipoProducto {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISTipoProducto/SelectAll", ReplyAction="http://tempuri.org/ISTipoProducto/SelectAllResponse")]
        AIPos.Domain.TipoProducto[] SelectAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISTipoProducto/SelectAll", ReplyAction="http://tempuri.org/ISTipoProducto/SelectAllResponse")]
        System.Threading.Tasks.Task<AIPos.Domain.TipoProducto[]> SelectAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISTipoProducto/SelectById", ReplyAction="http://tempuri.org/ISTipoProducto/SelectByIdResponse")]
        AIPos.Domain.TipoProducto SelectById(int Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISTipoProducto/SelectById", ReplyAction="http://tempuri.org/ISTipoProducto/SelectByIdResponse")]
        System.Threading.Tasks.Task<AIPos.Domain.TipoProducto> SelectByIdAsync(int Id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISTipoProductoChannel : AIPos.DekstopLayer.ServiceTipoProducto.ISTipoProducto, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class STipoProductoClient : System.ServiceModel.ClientBase<AIPos.DekstopLayer.ServiceTipoProducto.ISTipoProducto>, AIPos.DekstopLayer.ServiceTipoProducto.ISTipoProducto {
        
        public STipoProductoClient() {
        }
        
        public STipoProductoClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public STipoProductoClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public STipoProductoClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public STipoProductoClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public AIPos.Domain.TipoProducto[] SelectAll() {
            return base.Channel.SelectAll();
        }
        
        public System.Threading.Tasks.Task<AIPos.Domain.TipoProducto[]> SelectAllAsync() {
            return base.Channel.SelectAllAsync();
        }
        
        public AIPos.Domain.TipoProducto SelectById(int Id) {
            return base.Channel.SelectById(Id);
        }
        
        public System.Threading.Tasks.Task<AIPos.Domain.TipoProducto> SelectByIdAsync(int Id) {
            return base.Channel.SelectByIdAsync(Id);
        }
    }
}
