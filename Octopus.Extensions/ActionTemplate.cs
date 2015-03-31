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

using Octopus.Client;
using Octopus.Client.Model;

namespace Octopus.Extensions
{
    public class ActionTemplate
    {
        public static ActionTemplateResource GetActionTemplate(OctopusRepository repo, string id)
        {
            return repo.Client.Get<ActionTemplateResource>("/api/actiontemplates/{id}", id);
        }

        public static ResourceCollection<ActionTemplateResource> GetActionTemplates(OctopusRepository repo)
        {
            return repo.Client.List<ActionTemplateResource>("/api/actiontemplates/");
        }
    }
}
