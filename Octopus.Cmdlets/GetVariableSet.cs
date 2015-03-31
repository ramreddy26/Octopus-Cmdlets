﻿#region License
// Copyright 2014 Colin Svingen

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octopus.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "VariableSet", DefaultParameterSetName = "ByName")]
    public class GetVariableSet : PSCmdlet
    {
        [Parameter(
            ParameterSetName = "ByName",
            Position = 0,
            Mandatory = false,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The name of the action to retrieve.")]
        public string[] Name { get; set; }

        [Parameter(
            ParameterSetName = "ById",
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The id of the machine to retrieve.")]
        public string[] Id { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Cache { get; set; }

        private IOctopusRepository _octopus;
        private List<LibraryVariableSetResource> _variableSets;

        protected override void BeginProcessing()
        {
            _octopus = Session.RetrieveSession(this);

            WriteDebug("Connection established");

            if (!Cache || Extensions.Cache.LibraryVariableSets.IsExpired)
                _variableSets = _octopus.LibraryVariableSets.FindAll();

            if (Cache)
            {
                if (Extensions.Cache.LibraryVariableSets.IsExpired)
                    Extensions.Cache.LibraryVariableSets.Set(_variableSets);
                else
                    _variableSets = Extensions.Cache.LibraryVariableSets.Values;
            }

            WriteDebug("Loaded environments");
        }

        protected override void ProcessRecord()
        {
            switch (ParameterSetName)
            {
                case "ByName":
                    ProcessByName();
                    break;
                case "ById":
                    ProcessById();
                    break;
                default:
                    throw new ArgumentException("Unknown ParameterSetName: " + ParameterSetName);
            }
        }

        private void ProcessById()
        {
            var variableSets = from id in Id
                               from v in _variableSets
                               where v.Id == id
                               select v;

            foreach (var variableSet in variableSets)
                WriteObject(variableSet);
        }

        private void ProcessByName()
        {
            IEnumerable<LibraryVariableSetResource> variableSets;

            if (Name == null)
            {
                variableSets = _variableSets;
            }
            else
            {
                variableSets = from name in Name
                               from v in _variableSets 
                               where v.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
                               select v;
            }

            foreach (var variableSet in variableSets)
                WriteObject(variableSet);
        }
    }
}
