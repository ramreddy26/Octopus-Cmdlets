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
using System.Management.Automation;
using Octopus.Client;

namespace Octopus.Cmdlets
{
    static class Session
    {
        internal static IOctopusRepository RetrieveSession(PSCmdlet cmdlet)
        {
            var octopus = (IOctopusRepository) cmdlet.SessionState.PSVariable.GetValue("OctopusRepository");
            if (octopus == null)
                throw new Exception(
                    "Connection not established. Please connect to your Octopus Deploy instance with Connect-OctoServer");

            return octopus;
        }
    }
}
