using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RecruitMe.Logic.Operations.Payments.Enums
{
    /// <summary>
    /// Status operacji. <br />
    /// Operacja jest uważana za zakończoną, jeżeli jest wykonana 
    /// (<see cref="OperationStatus.Completed" />) lub odrzucona 
    /// (<see cref="OperationStatus.Rejected" />)
    /// </summary>
    public enum OperationStatus
    {
        /// <summary>
        /// Nowa operacja
        /// </summary>
        [EnumMember(Value = "new")]
        New,

        /// <summary>
        /// Oczekuje na wpłatę
        /// </summary>
        [EnumMember(Value = "processing")]
        Processing,

        /// <summary>
        /// Wykonana
        /// </summary>
        [EnumMember(Value = "completed")]
        Completed,

        /// <summary>
        /// Odrzucona
        /// </summary>
        [EnumMember(Value = "rejected")]
        Rejected,

        /// <summary>
        /// Oczekuje na realizację
        /// </summary>
        [EnumMember(Value = "processing_realization_waiting")]
        RealizationWaiting,

        /// <summary>
        /// Realizowana
        /// </summary>
        [EnumMember(Value = "processing_realization")]
        InRealization
    }
}
