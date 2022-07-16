// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NestService.cs" company="HomeRun Software Systems">
//   Copyright (c) 2017 Jay McLain
//
//   Permission is hereby granted, free of charge, to any person 
//   obtaining a copy of this software and associated documentation
//   files (the "Software"), to deal in the Software without
//   restriction, including without limitation the rights to use,
//   copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the
//   Software is furnished to do so, subject to the following
//   conditions:
//
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//   OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//   HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//   WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//   FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//   OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary>
//   Defines the NestService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Nest.NET.Service.Model;

namespace Nest.NET.Service;

public class NestService
{
    private const string StructuresEntity = "structures";

    private readonly INestServiceProvider _serviceProvider;

    public NestService(ServiceOptions serviceOptions)
    {
        _serviceProvider = new NestServiceProvider(serviceOptions);
    }

    /// <summary>
    /// Creates the Nest API communications service
    /// </summary>
    /// <param name="options">Options for the NestService</param>
    /// <returns>The NestService</returns>
    /// <code>
    ///     var service = NestService.Create(new ServiceOptions { AccessToken = "{Your Access Token}" });
    /// </code>
    public static NestService Create(ServiceOptions options)
    {
        return new NestService(options);
    }

    public IEnumerable<Structure> Structures => _serviceProvider.GetAsync<IEnumerable<Structure>>(StructuresEntity).Result;
}