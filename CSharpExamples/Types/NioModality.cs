// ----------------------------------------------------------------------------------
// <copyright file="NioModality.cs" company="Copyright 2012 General Electric Company">
//     Copyright statement. All right reserved
// </copyright>
//
// ------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "GEHealthcare.ZFP.DicomObjectInterface.NioModality.#CPACSEXAMNOTE", MessageId = "CPACSEXAMNOTE", Justification = "Must match CPACS database. Reviewed.")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "GEHealthcare.ZFP.DicomObjectInterface.NioModality.#CPACSREPORT", MessageId = "CPACSREPORT", Justification = "Must match CPACS database. Reviewed.")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "GEHealthcare.ZFP.DicomObjectInterface.NioModality.#CPACSRPPS", MessageId = "CPACSRPPS", Justification = "Must match CPACS database. Reviewed.")]

namespace GEHealthcare.ZFP.Model.Types
{
    /// <summary>
    /// Other Modality.
    /// </summary>
    public enum NioModality
    {
        SR,
        KO,
        PR,
        CPACSRPPS,
        CPACSREPORT,
        CPACSEXAMNOTE
    }
}
