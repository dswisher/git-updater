// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Spectre.Console.Cli;

namespace GitUpdater.Infrastructure
{
    public sealed class TypeResolver : ITypeResolver, IDisposable
    {
        private readonly IServiceProvider provider;

        public TypeResolver(IServiceProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }


        public object Resolve(Type type)
        {
            if (type == null)
            {
                return null;
            }

            return provider.GetService(type);
        }


        public void Dispose()
        {
            if (provider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
