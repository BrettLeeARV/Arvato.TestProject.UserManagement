﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Services.Asset {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Services.Asset.IAssetService")]
    public interface IAssetService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAssetService/GetList", ReplyAction="http://tempuri.org/IAssetService/GetListResponse")]
        Arvato.TestProject.UsrMgmt.Entity.Model.Asset[] GetList(bool Enabled);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAssetService/Save", ReplyAction="http://tempuri.org/IAssetService/SaveResponse")]
        bool Save(Arvato.TestProject.UsrMgmt.Entity.Model.Asset asset);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAssetService/Delete", ReplyAction="http://tempuri.org/IAssetService/DeleteResponse")]
        bool Delete(Arvato.TestProject.UsrMgmt.Entity.Model.Asset entity);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAssetServiceChannel : Arvato.TestProject.UsrMgmt.UI.Desktop.Services.Asset.IAssetService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AssetServiceClient : System.ServiceModel.ClientBase<Arvato.TestProject.UsrMgmt.UI.Desktop.Services.Asset.IAssetService>, Arvato.TestProject.UsrMgmt.UI.Desktop.Services.Asset.IAssetService {
        
        public AssetServiceClient() {
        }
        
        public AssetServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AssetServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AssetServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AssetServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Arvato.TestProject.UsrMgmt.Entity.Model.Asset[] GetList(bool Enabled) {
            return base.Channel.GetList(Enabled);
        }
        
        public bool Save(Arvato.TestProject.UsrMgmt.Entity.Model.Asset asset) {
            return base.Channel.Save(asset);
        }
        
        public bool Delete(Arvato.TestProject.UsrMgmt.Entity.Model.Asset entity) {
            return base.Channel.Delete(entity);
        }
    }
}