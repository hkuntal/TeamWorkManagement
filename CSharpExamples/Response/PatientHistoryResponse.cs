// --------------------------------------------------------------------------------------------------------------------
// <copyright company="GE Healthcare IT" file="PatientHistoryResponse.cs">
// Copyright © 2014 General Electric Company  
// </copyright> 
// --------------------------------------------------------------------------------------------------------------------

namespace GEHealthcare.ZFP.Model.Response
{
    using System.Collections.Generic;
    using System.Linq;

    using GEHealthcare.ZFP.Model.Types;

    using Resources;

    public class PatientHistoryResponse
    {
        public PatientHistoryResponse(StoredQueryResponse storedQueryResponse, bool sortByDescendingUid, bool lastXdsQuery, string displayDateAttribute)
        {
            this.IsLastXdsQuery = lastXdsQuery;

            if (storedQueryResponse == null || !storedQueryResponse.RegistryEntries.Any())
            {
                this.DocumentEntries = new List<DocumentEntry>();
                return;
            }

            var oneEntry = storedQueryResponse.RegistryEntries.FirstOrDefault();
            if (oneEntry is DocumentEntry)
            {
                this.DocumentEntries =
                    storedQueryResponse.RegistryEntries.Select(e => e as DocumentEntry)
                                       .OrderByDescending(e => e.DisplayDate)
                                       .ThenBy(e => e.DocumentUid)
                                       .ToList();
            }
            else if (oneEntry is DocumentMetadata)
            {
                var documentMetadata = oneEntry as DocumentMetadata;

                this.PatientId = documentMetadata.PatientId;
                this.DomainId = documentMetadata.DomainId;

                if (sortByDescendingUid)
                {
                    this.DocumentEntries = storedQueryResponse.RegistryEntries.Select(e => new DocumentEntry((DocumentMetadata)e, displayDateAttribute))
                                            .OrderByDescending(e => e.DisplayDate)
                                            .ThenByDescending(e => e.DocumentUid)
                                            .ToList();
                }
                else
                {
                    this.DocumentEntries = storedQueryResponse.RegistryEntries.Select(e => new DocumentEntry((DocumentMetadata)e, displayDateAttribute))
                                            .OrderByDescending(e => e.DisplayDate)
                                            .ThenBy(e => e.DocumentUid)
                                            .ToList();
                }
            }
           
            SetPatientDemographicData();
        }

        /// <summary>
        /// Gets Patient information.
        /// </summary>
        public Patient PatientInfo { get; private set; }

        /// <summary>
        /// Gets patient id.
        /// </summary>
        public string PatientId { get; private set; }

        public bool PatientMismatch { get; set; }

        public string PatientMismatchMessage { get; set; }

        /// <summary>
        /// Gets domain id.
        /// </summary>
        public string DomainId { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether Demographics retrieved from MPI.
        /// </summary>
        public bool DemographicsFromMpi { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is Last query.
        /// </summary>
        public bool IsLastXdsQuery { get; set; }

        /// <summary>
        /// Gets list of documents.
        /// </summary>
        public IList<DocumentEntry> DocumentEntries { get; private set; }

        private void SetPatientDemographicData()
        {
            this.PatientInfo = new Patient();
            
            if (this.DocumentEntries == null || !this.DocumentEntries.Any())
            {
                return;
            }

            var entry = this.DocumentEntries.FirstOrDefault(doc => doc.SourcePatientInfo != null 
                && doc.SourcePatientInfo.IsValid);

            if (entry != null)
            {
                this.PatientInfo.Name.GivenName = entry.SourcePatientInfo.FirstName;
                this.PatientInfo.Name.FamilyName = entry.SourcePatientInfo.LastName;
                this.PatientInfo.Name.MiddleName = entry.SourcePatientInfo.MiddleName;
                this.PatientInfo.DateOfBirth = entry.SourcePatientInfo.DateOfBirth;
                this.PatientInfo.AdministrativeSex = GenderExtension.Parse(entry.SourcePatientInfo.Gender.ToCharArray()[0]);
            }
            else
            {
                this.PatientInfo.Name.GivenName = string.Empty;
                this.PatientInfo.Name.FamilyName = PivStrings.PatientNameUnknown;
                this.PatientInfo.Name.MiddleName = string.Empty;
                this.PatientInfo.DateOfBirth = null;
                this.PatientInfo.AdministrativeSex = Gender.Unknown;
            }

            this.DemographicsFromMpi = false;
        }
    }
}