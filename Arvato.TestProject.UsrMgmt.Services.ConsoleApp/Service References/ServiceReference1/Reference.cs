﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BookingsData", Namespace="http://schemas.datacontract.org/2004/07/Arvato.TestProject.UsrMgmt.Services.Contr" +
        "acts.DataContracts", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class BookingsData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Booking[] BookingsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime EndField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsCanceledField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RoomIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime StartField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Booking[] Bookings {
            get {
                return this.BookingsField;
            }
            set {
                if ((object.ReferenceEquals(this.BookingsField, value) != true)) {
                    this.BookingsField = value;
                    this.RaisePropertyChanged("Bookings");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime End {
            get {
                return this.EndField;
            }
            set {
                if ((this.EndField.Equals(value) != true)) {
                    this.EndField = value;
                    this.RaisePropertyChanged("End");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsCanceled {
            get {
                return this.IsCanceledField;
            }
            set {
                if ((this.IsCanceledField.Equals(value) != true)) {
                    this.IsCanceledField = value;
                    this.RaisePropertyChanged("IsCanceled");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RoomId {
            get {
                return this.RoomIdField;
            }
            set {
                if ((this.RoomIdField.Equals(value) != true)) {
                    this.RoomIdField = value;
                    this.RaisePropertyChanged("RoomId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Start {
            get {
                return this.StartField;
            }
            set {
                if ((this.StartField.Equals(value) != true)) {
                    this.StartField = value;
                    this.RaisePropertyChanged("Start");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((this.UserIdField.Equals(value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Booking", Namespace="http://schemas.datacontract.org/2004/07/Arvato.TestProject.UsrMgmt.Entity.Model")]
    [System.SerializableAttribute()]
    public partial class Booking : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.AssetBooking[] AssetBookingsk__BackingFieldField;
        
        private System.DateTime DateCreatedk__BackingFieldField;
        
        private System.DateTime EndDatek__BackingFieldField;
        
        private int IDk__BackingFieldField;
        
        private bool IsCanceledk__BackingFieldField;
        
        private string RefNumk__BackingFieldField;
        
        private Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Room Roomk__BackingFieldField;
        
        private System.DateTime StartDatek__BackingFieldField;
        
        private Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.User Userk__BackingFieldField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<AssetBookings>k__BackingField", IsRequired=true)]
        public Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.AssetBooking[] AssetBookingsk__BackingField {
            get {
                return this.AssetBookingsk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.AssetBookingsk__BackingFieldField, value) != true)) {
                    this.AssetBookingsk__BackingFieldField = value;
                    this.RaisePropertyChanged("AssetBookingsk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<DateCreated>k__BackingField", IsRequired=true)]
        public System.DateTime DateCreatedk__BackingField {
            get {
                return this.DateCreatedk__BackingFieldField;
            }
            set {
                if ((this.DateCreatedk__BackingFieldField.Equals(value) != true)) {
                    this.DateCreatedk__BackingFieldField = value;
                    this.RaisePropertyChanged("DateCreatedk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<EndDate>k__BackingField", IsRequired=true)]
        public System.DateTime EndDatek__BackingField {
            get {
                return this.EndDatek__BackingFieldField;
            }
            set {
                if ((this.EndDatek__BackingFieldField.Equals(value) != true)) {
                    this.EndDatek__BackingFieldField = value;
                    this.RaisePropertyChanged("EndDatek__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<ID>k__BackingField", IsRequired=true)]
        public int IDk__BackingField {
            get {
                return this.IDk__BackingFieldField;
            }
            set {
                if ((this.IDk__BackingFieldField.Equals(value) != true)) {
                    this.IDk__BackingFieldField = value;
                    this.RaisePropertyChanged("IDk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<IsCanceled>k__BackingField", IsRequired=true)]
        public bool IsCanceledk__BackingField {
            get {
                return this.IsCanceledk__BackingFieldField;
            }
            set {
                if ((this.IsCanceledk__BackingFieldField.Equals(value) != true)) {
                    this.IsCanceledk__BackingFieldField = value;
                    this.RaisePropertyChanged("IsCanceledk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<RefNum>k__BackingField", IsRequired=true)]
        public string RefNumk__BackingField {
            get {
                return this.RefNumk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.RefNumk__BackingFieldField, value) != true)) {
                    this.RefNumk__BackingFieldField = value;
                    this.RaisePropertyChanged("RefNumk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Room>k__BackingField", IsRequired=true)]
        public Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Room Roomk__BackingField {
            get {
                return this.Roomk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Roomk__BackingFieldField, value) != true)) {
                    this.Roomk__BackingFieldField = value;
                    this.RaisePropertyChanged("Roomk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<StartDate>k__BackingField", IsRequired=true)]
        public System.DateTime StartDatek__BackingField {
            get {
                return this.StartDatek__BackingFieldField;
            }
            set {
                if ((this.StartDatek__BackingFieldField.Equals(value) != true)) {
                    this.StartDatek__BackingFieldField = value;
                    this.RaisePropertyChanged("StartDatek__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<User>k__BackingField", IsRequired=true)]
        public Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.User Userk__BackingField {
            get {
                return this.Userk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Userk__BackingFieldField, value) != true)) {
                    this.Userk__BackingFieldField = value;
                    this.RaisePropertyChanged("Userk__BackingField");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Room", Namespace="http://schemas.datacontract.org/2004/07/Arvato.TestProject.UsrMgmt.Entity.Model")]
    [System.SerializableAttribute()]
    public partial class Room : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Asset[] Assetk__BackingFieldField;
        
        private Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Booking[] Bookingk__BackingFieldField;
        
        private int Capacityk__BackingFieldField;
        
        private int IDk__BackingFieldField;
        
        private bool IsEnabledk__BackingFieldField;
        
        private string Locationk__BackingFieldField;
        
        private string Namek__BackingFieldField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Asset>k__BackingField", IsRequired=true)]
        public Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Asset[] Assetk__BackingField {
            get {
                return this.Assetk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Assetk__BackingFieldField, value) != true)) {
                    this.Assetk__BackingFieldField = value;
                    this.RaisePropertyChanged("Assetk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Booking>k__BackingField", IsRequired=true)]
        public Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Booking[] Bookingk__BackingField {
            get {
                return this.Bookingk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Bookingk__BackingFieldField, value) != true)) {
                    this.Bookingk__BackingFieldField = value;
                    this.RaisePropertyChanged("Bookingk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Capacity>k__BackingField", IsRequired=true)]
        public int Capacityk__BackingField {
            get {
                return this.Capacityk__BackingFieldField;
            }
            set {
                if ((this.Capacityk__BackingFieldField.Equals(value) != true)) {
                    this.Capacityk__BackingFieldField = value;
                    this.RaisePropertyChanged("Capacityk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<ID>k__BackingField", IsRequired=true)]
        public int IDk__BackingField {
            get {
                return this.IDk__BackingFieldField;
            }
            set {
                if ((this.IDk__BackingFieldField.Equals(value) != true)) {
                    this.IDk__BackingFieldField = value;
                    this.RaisePropertyChanged("IDk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<IsEnabled>k__BackingField", IsRequired=true)]
        public bool IsEnabledk__BackingField {
            get {
                return this.IsEnabledk__BackingFieldField;
            }
            set {
                if ((this.IsEnabledk__BackingFieldField.Equals(value) != true)) {
                    this.IsEnabledk__BackingFieldField = value;
                    this.RaisePropertyChanged("IsEnabledk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Location>k__BackingField", IsRequired=true)]
        public string Locationk__BackingField {
            get {
                return this.Locationk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Locationk__BackingFieldField, value) != true)) {
                    this.Locationk__BackingFieldField = value;
                    this.RaisePropertyChanged("Locationk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Name>k__BackingField", IsRequired=true)]
        public string Namek__BackingField {
            get {
                return this.Namek__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Namek__BackingFieldField, value) != true)) {
                    this.Namek__BackingFieldField = value;
                    this.RaisePropertyChanged("Namek__BackingField");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/Arvato.TestProject.UsrMgmt.Entity.Model")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Booking[] Bookingk__BackingFieldField;
        
        private string Emailk__BackingFieldField;
        
        private string FirstNamek__BackingFieldField;
        
        private int IDk__BackingFieldField;
        
        private bool IsWindowAuthenticatek__BackingFieldField;
        
        private string LastNamek__BackingFieldField;
        
        private string LoginIDk__BackingFieldField;
        
        private string Passwordk__BackingFieldField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Booking>k__BackingField", IsRequired=true)]
        public Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Booking[] Bookingk__BackingField {
            get {
                return this.Bookingk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Bookingk__BackingFieldField, value) != true)) {
                    this.Bookingk__BackingFieldField = value;
                    this.RaisePropertyChanged("Bookingk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Email>k__BackingField", IsRequired=true)]
        public string Emailk__BackingField {
            get {
                return this.Emailk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Emailk__BackingFieldField, value) != true)) {
                    this.Emailk__BackingFieldField = value;
                    this.RaisePropertyChanged("Emailk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<FirstName>k__BackingField", IsRequired=true)]
        public string FirstNamek__BackingField {
            get {
                return this.FirstNamek__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNamek__BackingFieldField, value) != true)) {
                    this.FirstNamek__BackingFieldField = value;
                    this.RaisePropertyChanged("FirstNamek__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<ID>k__BackingField", IsRequired=true)]
        public int IDk__BackingField {
            get {
                return this.IDk__BackingFieldField;
            }
            set {
                if ((this.IDk__BackingFieldField.Equals(value) != true)) {
                    this.IDk__BackingFieldField = value;
                    this.RaisePropertyChanged("IDk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<IsWindowAuthenticate>k__BackingField", IsRequired=true)]
        public bool IsWindowAuthenticatek__BackingField {
            get {
                return this.IsWindowAuthenticatek__BackingFieldField;
            }
            set {
                if ((this.IsWindowAuthenticatek__BackingFieldField.Equals(value) != true)) {
                    this.IsWindowAuthenticatek__BackingFieldField = value;
                    this.RaisePropertyChanged("IsWindowAuthenticatek__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<LastName>k__BackingField", IsRequired=true)]
        public string LastNamek__BackingField {
            get {
                return this.LastNamek__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNamek__BackingFieldField, value) != true)) {
                    this.LastNamek__BackingFieldField = value;
                    this.RaisePropertyChanged("LastNamek__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<LoginID>k__BackingField", IsRequired=true)]
        public string LoginIDk__BackingField {
            get {
                return this.LoginIDk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.LoginIDk__BackingFieldField, value) != true)) {
                    this.LoginIDk__BackingFieldField = value;
                    this.RaisePropertyChanged("LoginIDk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Password>k__BackingField", IsRequired=true)]
        public string Passwordk__BackingField {
            get {
                return this.Passwordk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Passwordk__BackingFieldField, value) != true)) {
                    this.Passwordk__BackingFieldField = value;
                    this.RaisePropertyChanged("Passwordk__BackingField");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AssetBooking", Namespace="http://schemas.datacontract.org/2004/07/Arvato.TestProject.UsrMgmt.Entity.Model")]
    [System.SerializableAttribute()]
    public partial class AssetBooking : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Asset Assetk__BackingFieldField;
        
        private Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Booking Bookingk__BackingFieldField;
        
        private int IDk__BackingFieldField;
        
        private bool Statusk__BackingFieldField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Asset>k__BackingField", IsRequired=true)]
        public Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Asset Assetk__BackingField {
            get {
                return this.Assetk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Assetk__BackingFieldField, value) != true)) {
                    this.Assetk__BackingFieldField = value;
                    this.RaisePropertyChanged("Assetk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Booking>k__BackingField", IsRequired=true)]
        public Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Booking Bookingk__BackingField {
            get {
                return this.Bookingk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Bookingk__BackingFieldField, value) != true)) {
                    this.Bookingk__BackingFieldField = value;
                    this.RaisePropertyChanged("Bookingk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<ID>k__BackingField", IsRequired=true)]
        public int IDk__BackingField {
            get {
                return this.IDk__BackingFieldField;
            }
            set {
                if ((this.IDk__BackingFieldField.Equals(value) != true)) {
                    this.IDk__BackingFieldField = value;
                    this.RaisePropertyChanged("IDk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Status>k__BackingField", IsRequired=true)]
        public bool Statusk__BackingField {
            get {
                return this.Statusk__BackingFieldField;
            }
            set {
                if ((this.Statusk__BackingFieldField.Equals(value) != true)) {
                    this.Statusk__BackingFieldField = value;
                    this.RaisePropertyChanged("Statusk__BackingField");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Asset", Namespace="http://schemas.datacontract.org/2004/07/Arvato.TestProject.UsrMgmt.Entity.Model")]
    [System.SerializableAttribute()]
    public partial class Asset : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.AssetBooking[] AssetBookingk__BackingFieldField;
        
        private int IDk__BackingFieldField;
        
        private bool IsEnabledk__BackingFieldField;
        
        private string Namek__BackingFieldField;
        
        private Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Room Roomk__BackingFieldField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<AssetBooking>k__BackingField", IsRequired=true)]
        public Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.AssetBooking[] AssetBookingk__BackingField {
            get {
                return this.AssetBookingk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.AssetBookingk__BackingFieldField, value) != true)) {
                    this.AssetBookingk__BackingFieldField = value;
                    this.RaisePropertyChanged("AssetBookingk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<ID>k__BackingField", IsRequired=true)]
        public int IDk__BackingField {
            get {
                return this.IDk__BackingFieldField;
            }
            set {
                if ((this.IDk__BackingFieldField.Equals(value) != true)) {
                    this.IDk__BackingFieldField = value;
                    this.RaisePropertyChanged("IDk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<IsEnabled>k__BackingField", IsRequired=true)]
        public bool IsEnabledk__BackingField {
            get {
                return this.IsEnabledk__BackingFieldField;
            }
            set {
                if ((this.IsEnabledk__BackingFieldField.Equals(value) != true)) {
                    this.IsEnabledk__BackingFieldField = value;
                    this.RaisePropertyChanged("IsEnabledk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Name>k__BackingField", IsRequired=true)]
        public string Namek__BackingField {
            get {
                return this.Namek__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Namek__BackingFieldField, value) != true)) {
                    this.Namek__BackingFieldField = value;
                    this.RaisePropertyChanged("Namek__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Room>k__BackingField", IsRequired=true)]
        public Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.Room Roomk__BackingField {
            get {
                return this.Roomk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Roomk__BackingFieldField, value) != true)) {
                    this.Roomk__BackingFieldField = value;
                    this.RaisePropertyChanged("Roomk__BackingField");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IBookingService")]
    public interface IBookingService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookingService/GetBookings", ReplyAction="http://tempuri.org/IBookingService/GetBookingsResponse")]
        Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.BookingsData GetBookings(Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.BookingsData data);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBookingServiceChannel : Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.IBookingService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BookingServiceClient : System.ServiceModel.ClientBase<Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.IBookingService>, Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.IBookingService {
        
        public BookingServiceClient() {
        }
        
        public BookingServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BookingServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BookingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BookingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.BookingsData GetBookings(Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1.BookingsData data) {
            return base.Channel.GetBookings(data);
        }
    }
}