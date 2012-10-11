﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行库版本:2.0.50727.1433
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace UpLoadWebApplication.UpLoadService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UpLoadService.IUpLoadService")]
    public interface IUpLoadService {
        
        // CODEGEN: 消息 FileUploadMessage 的包装名称(FileUploadMessage)以后生成的消息协定与默认值(UploadFile)不匹配
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="UploadFile")]
        void UploadFile(UpLoadWebApplication.UpLoadService.FileUploadMessage request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="FileUploadMessage", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class FileUploadMessage {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string FileName;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string SavePath;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream FileData;
        
        public FileUploadMessage() {
        }
        
        public FileUploadMessage(string FileName, string SavePath, System.IO.Stream FileData) {
            this.FileName = FileName;
            this.SavePath = SavePath;
            this.FileData = FileData;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IUpLoadServiceChannel : UpLoadWebApplication.UpLoadService.IUpLoadService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class UpLoadServiceClient : System.ServiceModel.ClientBase<UpLoadWebApplication.UpLoadService.IUpLoadService>, UpLoadWebApplication.UpLoadService.IUpLoadService {
        
        public UpLoadServiceClient() {
        }
        
        public UpLoadServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UpLoadServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UpLoadServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UpLoadServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void UpLoadWebApplication.UpLoadService.IUpLoadService.UploadFile(UpLoadWebApplication.UpLoadService.FileUploadMessage request) {
            base.Channel.UploadFile(request);
        }
        
        public void UploadFile(string FileName, string SavePath, System.IO.Stream FileData) {
            UpLoadWebApplication.UpLoadService.FileUploadMessage inValue = new UpLoadWebApplication.UpLoadService.FileUploadMessage();
            inValue.FileName = FileName;
            inValue.SavePath = SavePath;
            inValue.FileData = FileData;
            ((UpLoadWebApplication.UpLoadService.IUpLoadService)(this)).UploadFile(inValue);
        }
    }
}