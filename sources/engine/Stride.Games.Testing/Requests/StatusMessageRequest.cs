// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Core;

namespace Stride.Games.Testing.Requests
{
    [DataContract]
    internal class StatusMessageRequest : TestRequestBase
    {
        public bool Error;
        public string Message;
    }
}
