﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyAdmin.WS_SportMillion {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://hbcom.vn/", ConfigurationName="WS_SportMillion.SportMillionSoap")]
    public interface SportMillionSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://hbcom.vn/Check", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Check(string Signature);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://hbcom.vn/Reg", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Reg(int ChannelType, string Signature, string CommandCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://hbcom.vn/Dereg", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Dereg(int ChannelType, string Signature, string CommandCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://hbcom.vn/GetInfo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable GetInfo(string Signature);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SportMillionSoapChannel : MyAdmin.WS_SportMillion.SportMillionSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SportMillionSoapClient : System.ServiceModel.ClientBase<MyAdmin.WS_SportMillion.SportMillionSoap>, MyAdmin.WS_SportMillion.SportMillionSoap {
        
        public SportMillionSoapClient() {
        }
        
        public SportMillionSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SportMillionSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SportMillionSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SportMillionSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Check(string Signature) {
            return base.Channel.Check(Signature);
        }
        
        public string Reg(int ChannelType, string Signature, string CommandCode) {
            return base.Channel.Reg(ChannelType, Signature, CommandCode);
        }
        
        public string Dereg(int ChannelType, string Signature, string CommandCode) {
            return base.Channel.Dereg(ChannelType, Signature, CommandCode);
        }
        
        public System.Data.DataTable GetInfo(string Signature) {
            return base.Channel.GetInfo(Signature);
        }
    }
}