﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TradeMaster.MarketManager.QuotesServiceReference {
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TransactionType", Namespace="http://schemas.datacontract.org/2004/07/TradeMaster.Common")]
    public enum TransactionType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Buy = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Sell = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="QuotesServiceReference.IQuotesService")]
    public interface IQuotesService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuotesService/Echo", ReplyAction="http://tempuri.org/IQuotesService/EchoResponse")]
        bool Echo();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuotesService/Echo", ReplyAction="http://tempuri.org/IQuotesService/EchoResponse")]
        System.Threading.Tasks.Task<bool> EchoAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuotesService/GetQuotes", ReplyAction="http://tempuri.org/IQuotesService/GetQuotesResponse")]
        TradeMaster.Entities.OHLC[] GetQuotes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuotesService/GetQuotes", ReplyAction="http://tempuri.org/IQuotesService/GetQuotesResponse")]
        System.Threading.Tasks.Task<TradeMaster.Entities.OHLC[]> GetQuotesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuotesService/GetTradeSignals", ReplyAction="http://tempuri.org/IQuotesService/GetTradeSignalsResponse")]
        TradeMaster.Entities.TradeSignal[] GetTradeSignals();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuotesService/GetTradeSignals", ReplyAction="http://tempuri.org/IQuotesService/GetTradeSignalsResponse")]
        System.Threading.Tasks.Task<TradeMaster.Entities.TradeSignal[]> GetTradeSignalsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IQuotesServiceChannel : TradeMaster.MarketManager.QuotesServiceReference.IQuotesService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class QuotesServiceClient : System.ServiceModel.ClientBase<TradeMaster.MarketManager.QuotesServiceReference.IQuotesService>, TradeMaster.MarketManager.QuotesServiceReference.IQuotesService {
        
        public QuotesServiceClient() {
        }
        
        public QuotesServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public QuotesServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QuotesServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QuotesServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Echo() {
            return base.Channel.Echo();
        }
        
        public System.Threading.Tasks.Task<bool> EchoAsync() {
            return base.Channel.EchoAsync();
        }
        
        public TradeMaster.Entities.OHLC[] GetQuotes() {
            return base.Channel.GetQuotes();
        }
        
        public System.Threading.Tasks.Task<TradeMaster.Entities.OHLC[]> GetQuotesAsync() {
            return base.Channel.GetQuotesAsync();
        }
        
        public TradeMaster.Entities.TradeSignal[] GetTradeSignals() {
            return base.Channel.GetTradeSignals();
        }
        
        public System.Threading.Tasks.Task<TradeMaster.Entities.TradeSignal[]> GetTradeSignalsAsync() {
            return base.Channel.GetTradeSignalsAsync();
        }
    }
}
