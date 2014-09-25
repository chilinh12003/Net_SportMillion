﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyWap.WS_SportMillion {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://hbcom.vn/", ConfigurationName="WS_SportMillion.SportMillionSoap")]
    public interface SportMillionSoap {
        
        // CODEGEN: Generating message contract since element name Signature from namespace http://hbcom.vn/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://hbcom.vn/Check", ReplyAction="*")]
        MyWap.WS_SportMillion.CheckResponse Check(MyWap.WS_SportMillion.CheckRequest request);
        
        // CODEGEN: Generating message contract since element name Signature from namespace http://hbcom.vn/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://hbcom.vn/Reg", ReplyAction="*")]
        MyWap.WS_SportMillion.RegResponse Reg(MyWap.WS_SportMillion.RegRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CheckRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Check", Namespace="http://hbcom.vn/", Order=0)]
        public MyWap.WS_SportMillion.CheckRequestBody Body;
        
        public CheckRequest() {
        }
        
        public CheckRequest(MyWap.WS_SportMillion.CheckRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://hbcom.vn/")]
    public partial class CheckRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Signature;
        
        public CheckRequestBody() {
        }
        
        public CheckRequestBody(string Signature) {
            this.Signature = Signature;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CheckResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CheckResponse", Namespace="http://hbcom.vn/", Order=0)]
        public MyWap.WS_SportMillion.CheckResponseBody Body;
        
        public CheckResponse() {
        }
        
        public CheckResponse(MyWap.WS_SportMillion.CheckResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://hbcom.vn/")]
    public partial class CheckResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string CheckResult;
        
        public CheckResponseBody() {
        }
        
        public CheckResponseBody(string CheckResult) {
            this.CheckResult = CheckResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RegRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Reg", Namespace="http://hbcom.vn/", Order=0)]
        public MyWap.WS_SportMillion.RegRequestBody Body;
        
        public RegRequest() {
        }
        
        public RegRequest(MyWap.WS_SportMillion.RegRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://hbcom.vn/")]
    public partial class RegRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int ChannelType;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Signature;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string CommandCode;
        
        public RegRequestBody() {
        }
        
        public RegRequestBody(int ChannelType, string Signature, string CommandCode) {
            this.ChannelType = ChannelType;
            this.Signature = Signature;
            this.CommandCode = CommandCode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RegResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RegResponse", Namespace="http://hbcom.vn/", Order=0)]
        public MyWap.WS_SportMillion.RegResponseBody Body;
        
        public RegResponse() {
        }
        
        public RegResponse(MyWap.WS_SportMillion.RegResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://hbcom.vn/")]
    public partial class RegResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string RegResult;
        
        public RegResponseBody() {
        }
        
        public RegResponseBody(string RegResult) {
            this.RegResult = RegResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SportMillionSoapChannel : MyWap.WS_SportMillion.SportMillionSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SportMillionSoapClient : System.ServiceModel.ClientBase<MyWap.WS_SportMillion.SportMillionSoap>, MyWap.WS_SportMillion.SportMillionSoap {
        
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
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MyWap.WS_SportMillion.CheckResponse MyWap.WS_SportMillion.SportMillionSoap.Check(MyWap.WS_SportMillion.CheckRequest request) {
            return base.Channel.Check(request);
        }
        
        public string Check(string Signature) {
            MyWap.WS_SportMillion.CheckRequest inValue = new MyWap.WS_SportMillion.CheckRequest();
            inValue.Body = new MyWap.WS_SportMillion.CheckRequestBody();
            inValue.Body.Signature = Signature;
            MyWap.WS_SportMillion.CheckResponse retVal = ((MyWap.WS_SportMillion.SportMillionSoap)(this)).Check(inValue);
            return retVal.Body.CheckResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MyWap.WS_SportMillion.RegResponse MyWap.WS_SportMillion.SportMillionSoap.Reg(MyWap.WS_SportMillion.RegRequest request) {
            return base.Channel.Reg(request);
        }
        
        public string Reg(int ChannelType, string Signature, string CommandCode) {
            MyWap.WS_SportMillion.RegRequest inValue = new MyWap.WS_SportMillion.RegRequest();
            inValue.Body = new MyWap.WS_SportMillion.RegRequestBody();
            inValue.Body.ChannelType = ChannelType;
            inValue.Body.Signature = Signature;
            inValue.Body.CommandCode = CommandCode;
            MyWap.WS_SportMillion.RegResponse retVal = ((MyWap.WS_SportMillion.SportMillionSoap)(this)).Reg(inValue);
            return retVal.Body.RegResult;
        }
    }
}
