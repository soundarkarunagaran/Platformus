﻿// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Platformus.Configuration.Data.Abstractions;
using Platformus.Configuration.Data.Models;

namespace Platformus.Configuration
{
  public class StorageConfigurationProvider : IConfigurationProvider
  {
    private IStorage storage;

    public StorageConfigurationProvider(IStorage storage)
    {
      this.storage = storage;
    }

    public IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
    {
      return new string[] { };
    }

    public IChangeToken GetReloadToken()
    {
      return null;
    }

    public void Load()
    {
    }

    public void Set(string key, string value)
    {
    }

    public bool TryGet(string key, out string value)
    {
      try
      {
        string[] codes = key.Split(':');
        string configurationCode = codes[0];
        Section section = this.storage.GetRepository<ISectionRepository>().WithCode(configurationCode);
        string variableCode = codes[1];
        Variable variable = this.storage.GetRepository<IVariableRepository>().WithSectionIdAndCode(section.Id, variableCode);

        value = variable.Value;
        return true;
      }

      catch
      {
        value = null;
        return false;
      }
    }
  }
}