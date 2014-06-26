﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Services.LDAP {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Services.LDAP.ILDAPService")]
    public interface ILDAPService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILDAPService/IsAuthenticated", ReplyAction="http://tempuri.org/ILDAPService/IsAuthenticatedResponse")]
        bool IsAuthenticated(Arvato.TestProject.UsrMgmt.Entity.Model.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILDAPService/IsExistUser", ReplyAction="http://tempuri.org/ILDAPService/IsExistUserResponse")]
        bool IsExistUser(Arvato.TestProject.UsrMgmt.Entity.Model.User user);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILDAPServiceChannel : Arvato.TestProject.UsrMgmt.UI.Desktop.Services.LDAP.ILDAPService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LDAPServiceClient : System.ServiceModel.ClientBase<Arvato.TestProject.UsrMgmt.UI.Desktop.Services.LDAP.ILDAPService>, Arvato.TestProject.UsrMgmt.UI.Desktop.Services.LDAP.ILDAPService {
        
        public LDAPServiceClient() {
        }
        
        public LDAPServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LDAPServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LDAPServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LDAPServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool IsAuthenticated(Arvato.TestProject.UsrMgmt.Entity.Model.User user) {
            return base.Channel.IsAuthenticated(user);
        }
        
        public bool IsExistUser(Arvato.TestProject.UsrMgmt.Entity.Model.User user) {
            return base.Channel.IsExistUser(user);
        }
    }
}