﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34014
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AIPos.DekstopLayer.ServiceCorteCaja {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceCorteCaja.ISCorteCaja")]
    public interface ISCorteCaja {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISCorteCaja/Insert", ReplyAction="http://tempuri.org/ISCorteCaja/InsertResponse")]
        AIPos.Domain.CorteCaja Insert(AIPos.Domain.CorteCaja entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISCorteCaja/Insert", ReplyAction="http://tempuri.org/ISCorteCaja/InsertResponse")]
        System.Threading.Tasks.Task<AIPos.Domain.CorteCaja> InsertAsync(AIPos.Domain.CorteCaja entity);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISCorteCajaChannel : AIPos.DekstopLayer.ServiceCorteCaja.ISCorteCaja, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SCorteCajaClient : System.ServiceModel.ClientBase<AIPos.DekstopLayer.ServiceCorteCaja.ISCorteCaja>, AIPos.DekstopLayer.ServiceCorteCaja.ISCorteCaja {
        
        public SCorteCajaClient() {
        }
        
        public SCorteCajaClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SCorteCajaClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SCorteCajaClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SCorteCajaClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public AIPos.Domain.CorteCaja Insert(AIPos.Domain.CorteCaja entity) {
            return base.Channel.Insert(entity);
        }
        
        public System.Threading.Tasks.Task<AIPos.Domain.CorteCaja> InsertAsync(AIPos.Domain.CorteCaja entity) {
            return base.Channel.InsertAsync(entity);
        }
    }
}
